using BrokerHub.Application.Command.Imovel.DeleteImovel;
using BrokerHub.Domain.Entities;
using BrokerHub.Domain.Enums;
using BrokerHub.Domain.Interfaces;
using BrokerHub.Domain.ValueObjects;
using Moq;

namespace BrokerHub.Application.Tests.Handlers;

public class DeleteImovelHandlerTests
{
    private readonly Mock<IImovelRepository> _imovelRepositoryMock;

    public DeleteImovelHandlerTests()
    {
        _imovelRepositoryMock = new Mock<IImovelRepository>();
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenImovelIsDeletedSuccessfully()
    {
        // Arrange
        var request = new DeleteImovelRequest
        {
            ImovelId = 13
        };

        var imovelMock = new Imovel(
            "Imóvel Teste",
            TipoImovel.Casa,
            new Endereco("Rua 1", "100", "Cidade A", "SP", "12345-678", "N/A"),
            100,
            2,
            1,
            1,
            500000m,
            StatusImovel.Disponivel
        );

        _imovelRepositoryMock.Setup(x => x.GetByIdAsync(request.ImovelId)).ReturnsAsync(imovelMock);
        _imovelRepositoryMock.Setup(x => x.DeleteAsync(imovelMock.Id)).Returns(Task.CompletedTask);

        var handler = new DeleteImovelHandler(_imovelRepositoryMock.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Imóvel deletado com sucesso.", result.Message);
        Assert.Equal(imovelMock.Id, result.Data!.ImovelId);
        _imovelRepositoryMock.Verify(x => x.DeleteAsync(imovelMock.Id), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenImovelNotFound()
    {
        // Arrange
        var request = new DeleteImovelRequest
        {
            ImovelId = 13
        };

        _imovelRepositoryMock.Setup(x => x.GetByIdAsync(request.ImovelId)).ReturnsAsync(null as Imovel);

        var handler = new DeleteImovelHandler(_imovelRepositoryMock.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Imóvel não encontrado.", result.Message);
        _imovelRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var request = new DeleteImovelRequest
        {
            ImovelId = 13
        };

        var imovelMock = new Imovel(
            "Imóvel Teste",
            TipoImovel.Casa,
            new Endereco("Rua 1", "100", "Cidade A", "SP", "12345-678", "N/A"),
            100,
            2,
            1,
            1,
            500000m,
            StatusImovel.Disponivel
        );

        _imovelRepositoryMock.Setup(x => x.GetByIdAsync(request.ImovelId)).ReturnsAsync(imovelMock);
        _imovelRepositoryMock.Setup(x => x.DeleteAsync(imovelMock.Id)).ThrowsAsync(new Exception("Erro de banco de dados"));

        var handler = new DeleteImovelHandler(_imovelRepositoryMock.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("Erro ao deletar o imóvel: Erro de banco de dados", result.Message);
        _imovelRepositoryMock.Verify(x => x.DeleteAsync(imovelMock.Id), Times.Once);
    }
}