using Common.ResultPattern;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Users.Queries.AdminGetUsers;

public record AdminGetUsersQuery(Guid? TenantId, string? Name, UserRoleEnum? Role, bool OnlyActive = true, int PageIndex = 1, int PageSize = 10) : IQuery<ResultT<PaginatedResult<UserResponse>>>;