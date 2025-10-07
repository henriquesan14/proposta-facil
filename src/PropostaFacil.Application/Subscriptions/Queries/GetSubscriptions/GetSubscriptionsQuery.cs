using Common.ResultPattern;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Subscriptions.Queries.GetSubscriptions;

public record GetSubscriptionsQuery(string? TenantName, Guid? SubscriptionPlanId, DateTime? StartDate, DateTime? EndDate, SubscriptionStatusEnum? Status,
    int PageIndex = 1, int PageSize = 20, bool OnlyActive = true) : IQuery<ResultT<PaginatedResult<SubscriptionResponse>>>;
