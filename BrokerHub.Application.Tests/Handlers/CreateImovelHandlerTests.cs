using Moq;
using BrokerHub.Domain.Interfaces;
using BrokerHub.Application.Command.Imovel.CreateImovel;
using BrokerHub.Domain.Entities;
using BrokerHub.Application.DTOs;
using BrokerHub.Domain.Enums;

namespace BrokerHub.Application.Tests.Handlers;

public class CreateImovelHandlerTests
{
    private readonly Mock<IImovelRepository> _imovelRepositoryMock;
    private readonly CreateImovelHandler _handler;

    public CreateImovelHandlerTests()
    {
        _imovelRepositoryMock = new Mock<IImovelRepository>();
        _handler = new CreateImovelHandler(_imovelRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenImovelIsCreated()
    {
        // Arrange
        var request = GetCreateImovelRequestMock();
        _imovelRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Imovel>())).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Imóvel criado com sucesso.", result.Message);
        Assert.NotNull(result.Data);
        Assert.Equal(request.Imovel.Titulo, result.Data.Titulo);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
    {
        // Arrange
        var request = GetCreateImovelRequestMock();
        _imovelRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Imovel>())).ThrowsAsync(new Exception("Erro no banco de dados"));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Erro ao criar imóvel: Erro no banco de dados", result.Message);
        Assert.Null(result.Data);
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

    private static CreateImovelRequest GetCreateImovelRequestMock()
    {
        return new CreateImovelRequest
        {
            Imovel = new ImovelDTO
            {
                Titulo = "Casa com piscina",
                Tipo = TipoImovel.Casa,
                Endereco = GetEnderecoDTOMock(),
                Area = 800000,
                Preco = 120m,
                Status = StatusImovel.Disponivel,
                Quartos = 5,
                Banheiros = 4,
                VagasGaragem = 2
            }
        };
    }
    #endregion
}