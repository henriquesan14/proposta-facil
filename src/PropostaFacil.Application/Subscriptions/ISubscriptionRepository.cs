using Ardalis.Specification;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Subscriptions;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Subscriptions;

public interface ISubscriptionRepository : IReadRepositoryBase<Subscription>, INoSaveSoftDeleteEfRepository<Subscription, SubscriptionId>;
