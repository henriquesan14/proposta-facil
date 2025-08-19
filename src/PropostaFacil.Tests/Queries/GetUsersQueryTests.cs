using Moq;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Users.Queries.GetUsers;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.Pagination;
using PropostaFacil.Tests.Builders.Entities;
using System.Linq.Expressions;

namespace PropostaFacil.Tests.Queries
{
    public class GetUsersQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();

        private GetUsersQueryHandler CreateHandler()
        => new GetUsersQueryHandler(_unitOfWorkMock.Object, _currentUserServiceMock.Object);


        [Fact]
        public async Task Handle_Should_Return_All_Users_When_AdminSystem()
        {
            // Arrange
            _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminSystem);
            var query = new GetUsersQuery(Guid.NewGuid(), "teste", UserRoleEnum.AdminSystem);

            var users = Enumerable.Range(1, 5)
                .Select(i => new UserBuilder().Build())
                .ToList();

            _unitOfWorkMock.Setup(x => x.Users.GetAsync(
                    It.IsAny<Expression<Func<User, bool>>>(),
                    It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                    It.IsAny<List<Expression<Func<User, object>>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>()
                ))
                .ReturnsAsync(users);
            _unitOfWorkMock.Setup(x => x.Users.GetCountAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(users.Count);

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(users.Count, result.Value!.Count);
            Assert.Equal(users.Count, result.Value.Data.Count());
        }

        [Fact]
        public async Task Handle_Should_Return_Tenant_Users_When_AdminTenant()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminTenant);
            _currentUserServiceMock.Setup(x => x.TenantId).Returns(tenantId);
            var query = new GetUsersQuery(Guid.NewGuid(), "teste", UserRoleEnum.AdminSystem);

            var tenantUsers = Enumerable.Range(1, 3)
                .Select(i => new UserBuilder().WithTenantId(TenantId.Of(tenantId)).Build())
                .ToList();

            _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminTenant);
            _currentUserServiceMock.Setup(x => x.TenantId).Returns(tenantId);

            _unitOfWorkMock.Setup(x => x.Users.GetAsync(
                    It.IsAny<Expression<Func<User, bool>>>(),
                    It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                    It.IsAny<List<Expression<Func<User, object>>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>()
                ))
                .ReturnsAsync(tenantUsers);
            _unitOfWorkMock.Setup(x => x.Users.GetCountAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(tenantUsers.Count);

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(tenantUsers.Count, result.Value!.Count);
            Assert.All(result.Value.Data, u => Assert.Equal(tenantId, u.TenantId));
        }
    }
}
