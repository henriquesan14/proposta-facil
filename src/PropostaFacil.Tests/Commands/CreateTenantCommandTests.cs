using Moq;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Tenants.Commands.CreateTenant;
using PropostaFacil.Domain.Tenants;
using PropostaFacil.Domain.Tenants.Contracts;
using PropostaFacil.Tests.Builders.Commands;
using PropostaFacil.Tests.Builders.Entities;
using System.Linq.Expressions;

namespace PropostaFacil.Tests.Commands
{
    public class CreateTenantCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IAsaasService> _paymentService = new();
        private readonly Mock<ITenantRuleCheck> _tenantRuleCheckMock = new();

        private CreateTenantCommandHandler CreateHandler()
        => new CreateTenantCommandHandler(_unitOfWorkMock.Object, _paymentService.Object, _tenantRuleCheckMock.Object);

        //[Fact]
        //public async Task Handle_Should_Return_Conflict_When_Document_Already_Exist()
        //{
        //    // Arrange
        //    var command = new CreateTenantCommandBuilder()
        //        .Build();

        //    var tenant = new TenantBuilder()
        //        .Build();

        //    _unitOfWorkMock
        //        .Setup(x => x.Tenants.GetSingleAsync(It.IsAny<Expression<Func<Tenant, bool>>>(), false, null!))
        //        .ReturnsAsync(tenant);

        //    var handler = CreateHandler();

        //    // Act
        //    var result = await handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    Assert.False(result.IsSuccess);
        //    Assert.Equal("Tenants.Conflict", result.Error!.Code);
        //}

        //[Fact]
        //public async Task Handle_Should_Create_Tenant_When_Data_Is_Valid()
        //{
        //    // Arrange
        //    var command = new CreateTenantCommandBuilder()
        //        .Build();

        //    _unitOfWorkMock
        //        .Setup(x => x.Tenants.GetSingleAsync(It.IsAny<Expression<Func<Tenant, bool>>>(), It.IsAny<bool>(), null!))
        //        .ReturnsAsync((Tenant?)null);

        //    var handler = CreateHandler();

        //    // Act
        //    var result = await handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    Assert.True(result.IsSuccess);
        //    _unitOfWorkMock.Verify(x => x.Tenants.AddAsync(It.IsAny<Tenant>()), Times.Once);
        //    _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
        //}
    }
}
