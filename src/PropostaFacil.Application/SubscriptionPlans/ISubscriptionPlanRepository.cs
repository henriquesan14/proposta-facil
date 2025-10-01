using Ardalis.Specification;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.SubscriptionPlans;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.SubscriptionPlans;

public interface ISubscriptionPlanRepository : IReadRepositoryBase<SubscriptionPlan>, INoSaveEfRepository<SubscriptionPlan, SubscriptionPlanId>;
