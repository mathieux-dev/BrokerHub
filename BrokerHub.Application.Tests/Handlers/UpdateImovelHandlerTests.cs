using BrokerHub.Application.Command.Imovel.UpdateImovel;
using BrokerHub.Application.DTOs;
using BrokerHub.Application.Queries.Imovel.GetImovel;
using BrokerHub.Domain.Entities;
using BrokerHub.Domain.Enums;
using BrokerHub.Domain.Interfaces;
using BrokerHub.Domain.ValueObjects;
using Moq;

namespace BrokerHub.Application.Tests.Handlers;

public class UpdateImovelHandlerTests
{
    private readonly Mock<IImovelRepository> _imovelRepositoryMock;

    public UpdateImovelHandlerTests()
    {
        _imovelRepositoryMock = new Mock<IImovelRepository>();
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenImovelIsUpdatedSuccessfully()
    {
        // Arrange
        var request = GetUpdateImovelRequestMock();

        var imovelMock = GetImovelMock();

        _imovelRepositoryMock.Setup(x => x.GetByIdAsync(request.ImovelId)).ReturnsAsync(imovelMock);
        _imovelRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Imovel>())).Returns(Task.CompletedTask);

        var handler = new UpdateImovelHandler(_imovelRepositoryMock.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Imóvel atualizado com sucesso.", result.Message);
        Assert.Equal(request.Titulo, result.Data!.Titulo);
        _imovelRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Imovel>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenImovelNotFound()
    {
        // Arrange
        var request = GetUpdateImovelRequestMock();

        _imovelRepositoryMock.Setup(x => x.GetByIdAsync(request.ImovelId)).ReturnsAsync(null as Imovel);

        var handler = new UpdateImovelHandler(_imovelRepositoryMock.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Imóvel não encontrado.", result.Message);
        _imovelRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _imovelRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Imovel>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var request = GetUpdateImovelRequestMock();

        var imovelMock = new Imovel(
            request.Titulo!,
            request.Tipo,
            new Endereco("Rua 1", "100", "Cidade A", "SP", "12345-678", "N/A"),
            request.Area,
            request.Quartos,
            request.Banheiros,
            request.VagasGaragem,
            request.Preco,
            request.Status
        );

        _imovelRepositoryMock.Setup(x => x.GetByIdAsync(request.ImovelId)).ReturnsAsync(imovelMock);
        _imovelRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Imovel>())).ThrowsAsync(new Exception("Erro de banco de dados"));

        var handler = new UpdateImovelHandler(_imovelRepositoryMock.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("Erro ao atualizar imóvel: Erro de banco de dados", result.Message);
        _imovelRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Imovel>()), Times.Once);
    }

    #region Mocks
    private static EnderecoDTO GetEnderecoDTOMock()
    {
        return new EnderecoDTO
        {
            Rua = "Rua das Palmeiras",
            Numero = "123",
            Cidade = "Fortaleza",
            Estado = "CE",
            CEP = "60150-360",
            Complemento = "Apt 501"
        };
    }

    private static UpdateImovelRequest GetUpdateImovelRequestMock()
    {
        return new UpdateImovelRequest
        {
            ImovelId = 13,
            Titulo = "Casa com piscina",
            Tipo = TipoImovel.Casa,
            Endereco = GetEnderecoDTOMock(),
            Area = 800000,
            Preco = 120m,
            Status = StatusImovel.Disponivel,
            Quartos = 5,
            Banheiros = 4,
            VagasGaragem = 2
        };
    }

    private static Imovel GetImovelMock()
    {
        return new Imovel(
            "Casa com piscina",
            TipoImovel.Casa,
            new Endereco(
                rua: "Rua das Palmeiras",
                numero: "123",
                cidade: "Fortaleza",
                estado: "CE",
                cep: "60150-360",
                complemento: "Apt 501"
            ),
            80,
            3,
            5,
            4,
            125000000,
            StatusImovel.Disponivel
        );
    }
    #endregion
}