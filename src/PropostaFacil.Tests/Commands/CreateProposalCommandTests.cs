using Moq;
using PropostaFacil.Application.Proposals.Commands.CreateProposal;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Tenants;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Tests.Builders.Commands;
using System.Linq.Expressions;

namespace PropostaFacil.Tests.Commands
{
    public class CreateProposalCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();

        private CreateProposalCommandHandler CreateHandler()
        => new CreateProposalCommandHandler(_unitOfWorkMock.Object, _currentUserServiceMock.Object);

        [Fact]
        public async Task Handle_Should_Return_TenantRequired_When_AdminSystem_Without_TenantId()
        {
            // Arrange
            var command = new CreateProposalCommandBuilder()
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
        //    var command = new CreateProposalCommandBuilder()
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
        //public async Task Handle_Should_Return_Forbidden_When_ClientId_Not_Belong_To_Tenant()
        //{
        //    // Arrange
        //    var command = new CreateProposalCommandBuilder()
        //        .Build();

        //    _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminTenant);
        //    _currentUserServiceMock.Setup(x => x.TenantId).Returns(Guid.NewGuid());

        //    _unitOfWorkMock
        //        .Setup(x => x.Clients.GetSingleAsync(It.IsAny<Expression<Func<Client, bool>>>(), false, null!))
        //        .ReturnsAsync((Client?)null);

        //    var handler = CreateHandler();

        //    // Act
        //    var result = await handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    Assert.False(result.IsSuccess);
        //    Assert.Equal("Proposals.Forbidden", result.Error!.Code);
        //}

        //[Fact]
        //public async Task Handle_Should_Create_Proposal_When_Data_Is_Valid()
        //{
        //    // Arrange
        //    var tenant = new TenantBuilder()
        //        .Build();

        //    var command = new CreateProposalCommandBuilder()
        //        .WithTenantId(tenant.Id.Value)
        //        .Build();

        //    var client = new ClientBuilder()
        //        .WithTenantId(tenant.Id)
        //        .Build();

        //    var proposalRepositoryMock = new Mock<IProposalRepository>();

        //    _currentUserServiceMock.Setup(x => x.Role).Returns(UserRoleEnum.AdminSystem);

        //    _unitOfWorkMock.Setup(x => x.Tenants.GetByIdAsync(It.IsAny<TenantId>(), false, null!))
        //        .ReturnsAsync(tenant);

        //    _unitOfWorkMock
        //        .Setup(x => x.Clients.GetSingleAsync(It.IsAny<Expression<Func<Client, bool>>>(), false, null!))
        //        .ReturnsAsync(client);

        //    _unitOfWorkMock
        //        .SetupGet(x => x.Proposals)
        //        .Returns(proposalRepositoryMock.Object);

        //    var handler = CreateHandler();

        //    // Act
        //    var result = await handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    Assert.True(result.IsSuccess);
        //    _unitOfWorkMock.Verify(x => x.Proposals.AddAsync(It.IsAny<Proposal>()), Times.Once);
        //    _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);
        //}
    }
}
