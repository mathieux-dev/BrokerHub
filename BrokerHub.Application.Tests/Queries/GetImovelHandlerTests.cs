using BrokerHub.Application.Queries.Imovel.GetImovel;
using BrokerHub.Domain.Entities;
using BrokerHub.Domain.Enums;
using BrokerHub.Domain.Interfaces;
using BrokerHub.Domain.ValueObjects;
using Moq;

namespace BrokerHub.Application.Tests.Queries;

public class GetImovelHandlerTests
{
    private readonly Mock<IImovelRepository> _imovelRepositoryMock;

    public GetImovelHandlerTests()
    {
        _imovelRepositoryMock = new Mock<IImovelRepository>();
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenImovelIsFound()
    {
        // Arrange
        var request = new GetImovelRequest { ImovelId = 13 };

        var imovelMock = new Imovel(
            "Imóvel Teste",
            TipoImovel.Casa,
            new Endereco("Rua 1", "100", "Cidade A", "SP", "12345-678", "Apto 101"),
            100,
            2,
            1,
            1,
            500000m,
            StatusImovel.Disponivel
        );

        _imovelRepositoryMock.Setup(x => x.GetByIdAsync(request.ImovelId)).ReturnsAsync(imovelMock);

        var handler = new GetImovelHandler(_imovelRepositoryMock.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(imovelMock.Id, result.Data.Id);
        Assert.Equal(imovelMock.Titulo, result.Data.Titulo);
        _imovelRepositoryMock.Verify(x => x.GetByIdAsync(request.ImovelId), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenImovelNotFound()
    {
        // Arrange
        var request = new GetImovelRequest { ImovelId = 13 };

        _imovelRepositoryMock.Setup(x => x.GetByIdAsync(request.ImovelId)).ReturnsAsync(null as Imovel);

        var handler = new GetImovelHandler(_imovelRepositoryMock.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Imóvel não encontrado.", result.Message);
        _imovelRepositoryMock.Verify(x => x.GetByIdAsync(request.ImovelId), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var request = new GetImovelRequest { ImovelId = 13 };

        _imovelRepositoryMock.Setup(x => x.GetByIdAsync(request.ImovelId)).ThrowsAsync(new Exception("Erro ao acessar o banco de dados"));

        var handler = new GetImovelHandler(_imovelRepositoryMock.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("Erro ao acessar o banco de dados", result.Message);
        _imovelRepositoryMock.Verify(x => x.GetByIdAsync(request.ImovelId), Times.Once);
    }
}