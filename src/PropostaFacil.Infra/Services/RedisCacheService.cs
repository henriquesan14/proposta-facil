using Microsoft.Extensions.Caching.Distributed;
using PropostaFacil.Application.Shared.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace PropostaFacil.Infra.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _connection;
        private readonly JsonSerializerOptions _jsonOptions;

        public RedisCacheService(IDistributedCache cache, IConnectionMultiplexer connection)
        {
            _cache = cache;
            _connection = connection;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task<T?> Get<T>(string key)
        {
            var cached = await _cache.GetStringAsync(key);
            if (cached == null) return default;
            return JsonSerializer.Deserialize<T>(cached, _jsonOptions);
        }

        public async Task Set<T>(string key, T value, TimeSpan expiration)
        {
            var serialized = JsonSerializer.Serialize(value, _jsonOptions);
            await _cache.SetStringAsync(key, serialized, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            });
        }

        public async Task Remove(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task RemoveByPrefix(string prefix)
        {
            var db = _connection.GetDatabase();
            var server = _connection.GetServer(_connection.GetEndPoints().First());

            var prefixWithInstanceName = $"PropostaFacil:{prefix}";

            var batch = new List<RedisKey>();

            await foreach (var key in server.KeysAsync(pattern: prefixWithInstanceName + "*"))
            {
                batch.Add(key);
                if (batch.Count >= 100)
                {
                    await db.KeyDeleteAsync(batch.ToArray());
                    batch.Clear();
                }
            }

            if (batch.Count > 0)
                await db.KeyDeleteAsync(batch.ToArray());
        }
    }

}
