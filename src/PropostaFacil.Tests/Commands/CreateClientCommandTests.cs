using Moq;
using PropostaFacil.Application.Clients.Commands.CreateClient;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Clients;
using PropostaFacil.Domain.Clients.Contracts;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Tenants;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Tests.Builders.Commands;
using PropostaFacil.Tests.Builders.Entities;
using System.Linq.Expressions;

namespace PropostaFacil.Tests.Commands;

public class CreateClientCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
    private readonly Mock<IClientRuleCheck> _ClientRuleCheckMock = new();

    private CreateClientCommandHandler CreateHandler()
    => new CreateClientCommandHandler(_unitOfWorkMock.Object, _currentUserServiceMock.Object, _ClientRuleCheckMock.Object);

    [Fact]
    public async Task Handle_Should_Return_TenantRequired_When_AdminSystem_Without_TenantId()
    {
        // Arrange
        var command = new CreateClientCommandBuilder()
            .Build();

        _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminSystem);

        var handler = CreateHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Tenants.Validation", result.Error!.Code);
    }

    //[Fact]
    //public async Task Handle_Should_Return_NotFound_When_AdminSystem_Passes_Invalid_Tenant()
    //{
    //    // Arrange
    //    var command = new CreateClientCommandBuilder()
    //        .Build();

    //    _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminSystem);
    //    _unitOfWorkMock.Setup(x => x.Tenants.GetByIdAsync(
    //        It.IsAny<TenantId>(),
    //        It.IsAny<bool>(),
    //        It.IsAny<List<Expression<Func<Tenant, object>>>>()
    //        )
    //    )
    //        .ReturnsAsync((Tenant?)null);

    //    var handler = CreateHandler();

    //    // Act
    //    var result = await handler.Handle(command, CancellationToken.None);

    //    // Assert
    //    Assert.False(result.IsSuccess);
    //    Assert.Equal("Tenants.NotFound", result.Error!.Code);
    //}

    //[Fact]
    //public async Task Handle_Should_Return_Conflict_When_Document_Already_Exist()
    //{
    //    // Arrange
    //    var command = new CreateClientCommandBuilder()
    //        .Build();

    //    var tenant = new TenantBuilder()
    //        .Build();

    //    var client = new ClientBuilder()
    //        .Build();

    //    _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminSystem);

    //    _unitOfWorkMock.Setup(x => x.Tenants.GetByIdAsync(It.IsAny<TenantId>(), false, null!))
    //        .ReturnsAsync(tenant);

    //    _unitOfWorkMock
    //        .Setup(x => x.Clients.GetSingleAsync(It.IsAny<Expression<Func<Client, bool>>>(), false, null!))
    //        .ReturnsAsync(client);

    //    var handler = CreateHandler();

    //    // Act
    //    var result = await handler.Handle(command, CancellationToken.None);

    //    // Assert
    //    Assert.False(result.IsSuccess);
    //    Assert.Equal("Clients.Conflict", result.Error!.Code);
    //}

    //[Fact]
    //public async Task Handle_Should_Create_Client_When_Data_Is_Valid()
    //{
    //    // Arrange
    //    var command = new CreateClientCommandBuilder()
    //        .Build();

    //    var tenant = new TenantBuilder()
    //        .Build();

    //    _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminSystem);

    //    _unitOfWorkMock.Setup(x => x.Tenants.GetByIdAsync(It.IsAny<TenantId>(), false, null!))
    //        .ReturnsAsync(tenant);

    //    _unitOfWorkMock
    //        .Setup(x => x.Clients.GetSingleAsync(It.IsAny<Expression<Func<Client, bool>>>(), It.IsAny<bool>(), null!))
    //        .ReturnsAsync((Client?)null);

    //    var handler = CreateHandler();

    //    // Act
    //    var result = await handler.Handle(command, CancellationToken.None);

    //    // Assert
    //    Assert.True(result.IsSuccess);
    //    _unitOfWorkMock.Verify(x => x.Clients.AddAsync(It.IsAny<Client>()), Times.Once);
    //    _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
    //}
}
