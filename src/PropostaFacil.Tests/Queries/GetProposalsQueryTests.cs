using Moq;
using PropostaFacil.Application.Proposals.Queries.GetProposals;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.Pagination;
using PropostaFacil.Tests.Builders.Entities;
using PropostaFacil.Tests.Builders.Queries;
using System.Linq.Expressions;

namespace PropostaFacil.Tests.Queries
{
    public class GetProposalsQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();

        private GetProposalsQueryHandler CreateHandler()
        => new GetProposalsQueryHandler(_unitOfWorkMock.Object, _currentUserServiceMock.Object);

        [Fact]
        public async Task Handle_Should_Return_All_Proposals_When_AdminSystem()
        {
            // Arrange
            _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminSystem);
            var query = new GetProposalsQueryBuilder().Build();

            var proposals = Enumerable.Range(1, 5)
                .Select(i => new ProposalBuilder().Build())
                .ToList();

            _unitOfWorkMock.Setup(x => x.Proposals.GetAsync(
                    It.IsAny<Expression<Func<Proposal, bool>>>(),
                    It.IsAny<Func<IQueryable<Proposal>, IOrderedQueryable<Proposal>>>(),
                    It.IsAny<List<Expression<Func<Proposal, object>>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>()
                ))
                .ReturnsAsync(proposals);
            _unitOfWorkMock.Setup(x => x.Proposals.GetCountAsync(It.IsAny<Expression<Func<Proposal, bool>>>()))
                .ReturnsAsync(proposals.Count);

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(proposals.Count, result.Value!.Count);
            Assert.Equal(proposals.Count, result.Value.Data.Count());
        }

        [Fact]
        public async Task Handle_Should_Return_Tenant_Proposals_When_AdminTenant()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminTenant);
            _currentUserServiceMock.Setup(x => x.TenantId).Returns(tenantId);
            var query = new GetProposalsQueryBuilder().Build();

            var tenantProposals = Enumerable.Range(1, 3)
                .Select(i => new ProposalBuilder().WithTenantId(TenantId.Of(tenantId)).Build())
                .ToList();

            _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminTenant);
            _currentUserServiceMock.Setup(x => x.TenantId).Returns(tenantId);

            _unitOfWorkMock.Setup(x => x.Proposals.GetAsync(
                    It.IsAny<Expression<Func<Proposal, bool>>>(),
                    It.IsAny<Func<IQueryable<Proposal>, IOrderedQueryable<Proposal>>>(),
                    It.IsAny<List<Expression<Func<Proposal, object>>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>()
                ))
                .ReturnsAsync(tenantProposals);
            _unitOfWorkMock.Setup(x => x.Proposals.GetCountAsync(It.IsAny<Expression<Func<Proposal, bool>>>()))
                .ReturnsAsync(tenantProposals.Count);

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(tenantProposals.Count, result.Value!.Count);
            Assert.All(result.Value.Data, u => Assert.Equal(tenantId, u.TenantId));
        }

    }
}
