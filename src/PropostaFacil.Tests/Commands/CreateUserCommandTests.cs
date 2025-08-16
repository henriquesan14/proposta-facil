using Moq;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Users.Commands.CreateUser;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Tests.Builders.Commands;
using PropostaFacil.Tests.Builders.Entities;
using System.Linq.Expressions;

namespace PropostaFacil.Tests.Commands
{
    public class CreateUserCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();

        private CreateUserCommandHandler CreateHandler()
        => new CreateUserCommandHandler(_unitOfWorkMock.Object, _currentUserServiceMock.Object);

        [Fact]
        public async Task Handle_Should_Return_TenantRequired_When_AdminSystem_Without_TenantId()
        {
            // Arrange
            var command = new CreateUserCommandBuilder()
                .WithRole(UserRoleEnum.AdminTenant)
                .WithTenantId(null)
                .Build();

            _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminSystem);

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Tenants.Validation", result.Error!.Code);
        }

        [Fact]
        public async Task Handle_Should_Return_NotFound_When_AdminSystem_Passes_Invalid_Tenant()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var command = new CreateUserCommandBuilder()
                .WithTenantId(tenantId)
                .Build();

            _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminSystem);
            _unitOfWorkMock.Setup(x => x.Tenants.GetByIdAsync(It.IsAny<TenantId>(), false, null!))
                .ReturnsAsync((Tenant?)null);

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Tenants.NotFound", result.Error!.Code);
        }

        [Fact]
        public async Task Handle_Should_Return_Forbidden_When_AdminTenant_Tries_To_Create_AdminSystem()
        {
            // Arrange
            var command = new CreateUserCommandBuilder()
                .WithRole(UserRoleEnum.AdminSystem)
                .Build();

            _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminTenant);
            _currentUserServiceMock.Setup(x => x.TenantId).Returns(Guid.NewGuid());

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Users.Forbidden", result.Error!.Code);
        }

        [Fact]
        public async Task Handle_Should_Return_Conflict_When_Email_Already_Exist()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var command = new CreateUserCommandBuilder()
                .WithTenantId(tenantId)
                .Build();

            var tenant = new TenantBuilder()
                .Build();

            var user = new UserBuilder()
                .Build();

            _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminSystem);

            _unitOfWorkMock.Setup(x => x.Tenants.GetByIdAsync(It.IsAny<TenantId>(), false, null!))
                .ReturnsAsync(tenant);

            _unitOfWorkMock
                .Setup(x => x.Users.GetSingleAsync(It.IsAny<Expression<Func<User, bool>>>(), false, null!))
                .ReturnsAsync(user);

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Users.Conflict", result.Error!.Code);
        }

        [Fact]
        public async Task Handle_Should_Create_User_When_Data_Is_Valid()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var command = new CreateUserCommandBuilder()
                .WithTenantId(tenantId)
                .Build();

            var tenant = new TenantBuilder()
                .Build();

            _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminSystem);

            _unitOfWorkMock.Setup(x => x.Tenants.GetByIdAsync(It.IsAny<TenantId>(), false, null!))
                .ReturnsAsync(tenant);

            _unitOfWorkMock
                .Setup(x => x.Users.GetSingleAsync(It.IsAny<Expression<Func<User, bool>>>(), false, null!))
                .ReturnsAsync((User?)null);

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _unitOfWorkMock.Verify(x => x.Users.AddAsync(It.IsAny<User>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
        }
    }
}
