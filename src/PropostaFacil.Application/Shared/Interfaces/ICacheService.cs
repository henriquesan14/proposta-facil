namespace PropostaFacil.Application.Shared.Interfaces
{
    public interface ICacheService
    {
        Task Set<T>(string key, T value, TimeSpan expiration);
        Task<T?> Get<T>(string key);
        Task Remove(string key);
        Task RemoveByPrefix(string prefix);
    }
}
