using BrokerHub.Application.Queries.Imovel.GetImovelList;
using BrokerHub.Domain.Entities;
using BrokerHub.Domain.Enums;
using BrokerHub.Domain.Interfaces;
using BrokerHub.Domain.ValueObjects;
using Moq;

namespace BrokerHub.Application.Tests.Queries;

public class GetImovelListHandlerTests
{
    private readonly Mock<IImovelRepository> _imovelRepositoryMock;

    public GetImovelListHandlerTests()
    {
        _imovelRepositoryMock = new Mock<IImovelRepository>();
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenImoveisAreFound()
    {
        // Arrange
        var request = new GetImovelListRequest
        {
            Page = 1,
            PageSize = 10
        };

        var imoveisMock = new List<Imovel>
    {
        new Imovel(
            "Imóvel 1",
            TipoImovel.Casa,
            new Endereco("Rua 1", "100", "Cidade A", "SP", "12345-678", "Apto 101"),
            100,
            2,
            1,
            1,
            500000m,
            StatusImovel.Disponivel
        ),
        new Imovel(
            "Imóvel 2",
            TipoImovel.Apartamento,
            new Endereco("Rua 2", "200", "Cidade B", "RJ", "98765-432", ""),
            150,
            3,
            2,
            2,
            800000m,
            StatusImovel.Vendido
        )
    };

        _imovelRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(),
                                                       It.IsAny<TipoImovel?>(), It.IsAny<StatusImovel?>(),
                                                       It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(),
                                                       It.IsAny<double?>(), It.IsAny<double?>(),
                                                       It.IsAny<decimal?>(), It.IsAny<decimal?>(),
                                                       It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(imoveisMock);

        var handler = new GetImovelListHandler(_imovelRepositoryMock.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.Imoveis.Count());
        _imovelRepositoryMock.Verify(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(),
                                                       It.IsAny<TipoImovel?>(), It.IsAny<StatusImovel?>(),
                                                       It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(),
                                                       It.IsAny<double?>(), It.IsAny<double?>(),
                                                       It.IsAny<decimal?>(), It.IsAny<decimal?>(),
                                                       It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoImoveisAreFound()
    {
        // Arrange
        var request = new GetImovelListRequest
        {
            Page = 1,
            PageSize = 10
        };

        _imovelRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(),
                                                       It.IsAny<TipoImovel?>(), It.IsAny<StatusImovel?>(),
                                                       It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(),
                                                       It.IsAny<double?>(), It.IsAny<double?>(),
                                                       It.IsAny<decimal?>(), It.IsAny<decimal?>(),
                                                       It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new List<Imovel>());

        var handler = new GetImovelListHandler(_imovelRepositoryMock.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data.Imoveis);
        _imovelRepositoryMock.Verify(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(),
                                                       It.IsAny<TipoImovel?>(), It.IsAny<StatusImovel?>(),
                                                       It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(),
                                                       It.IsAny<double?>(), It.IsAny<double?>(),
                                                       It.IsAny<decimal?>(), It.IsAny<decimal?>(),
                                                       It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var request = new GetImovelListRequest
        {
            Page = 1,
            PageSize = 10
        };

        _imovelRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(),
                                                       It.IsAny<TipoImovel?>(), It.IsAny<StatusImovel?>(),
                                                       It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(),
                                                       It.IsAny<double?>(), It.IsAny<double?>(),
                                                       It.IsAny<decimal?>(), It.IsAny<decimal?>(),
                                                       It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new Exception("Erro ao acessar o banco de dados"));

        var handler = new GetImovelListHandler(_imovelRepositoryMock.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("Erro ao acessar o banco de dados", result.Message);
        _imovelRepositoryMock.Verify(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(),
                                                       It.IsAny<TipoImovel?>(), It.IsAny<StatusImovel?>(),
                                                       It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(),
                                                       It.IsAny<double?>(), It.IsAny<double?>(),
                                                       It.IsAny<decimal?>(), It.IsAny<decimal?>(),
                                                       It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}