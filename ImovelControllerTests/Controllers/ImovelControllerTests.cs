using Azure;
using BrokerHub.API.Controllers.Imovel;
using BrokerHub.Application.Command.Imovel;
using BrokerHub.Application.Command.Imovel.CreateImovel;
using BrokerHub.Application.Command.Imovel.DeleteImovel;
using BrokerHub.Application.Command.Imovel.UpdateImovel;
using BrokerHub.Application.Common;
using BrokerHub.Application.DTOs;
using BrokerHub.Application.Queries.Imovel.GetImovel;
using BrokerHub.Application.Queries.Imovel.GetImovelList;
using BrokerHub.Domain.Enums;
using BrokerHub.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BrokerHub.API.Tests.Controllers
{
    public class ImovelControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ImovelController _controller;

        public ImovelControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ImovelController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Create_ValidRequest_ReturnsCreatedAtAction()
        {
            // Arrange
            var createRequest = GetCreateImovelRequestMock();
            var mediatorResponse = ApiResponse<ImovelGenericResponse>.SuccessResult(GetImovelGenericResponseMock(), "Imóvel criado com sucesso.");
            _mediatorMock.Setup(m => m.Send(createRequest, default)).ReturnsAsync(mediatorResponse);

            // Act
            var result = await _controller.Create(createRequest);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, actionResult.StatusCode);
        }

        [Fact]
        public async Task Create_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var createRequest = new CreateImovelRequest { Imovel = null! };
            var mediatorResponse = ApiResponse<ImovelGenericResponse>.FailureResult($"Erro ao criar imóvel");
            _mediatorMock.Setup(m => m.Send(createRequest, default)).ReturnsAsync(mediatorResponse);

            // Act
            var result = await _controller.Create(createRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Erro ao criar imóvel", badRequestResult.Value);
        }

        [Fact]
        public async Task Update_ValidRequest_ReturnsOk()
        {
            // Arrange
            var updateRequest = GetUpdateImovelRequestMock();
            var mediatorResponse = ApiResponse<ImovelGenericResponse>.SuccessResult(GetImovelGenericResponseMock(), "Imóvel atualizado com sucesso.");
            _mediatorMock.Setup(m => m.Send(updateRequest, default)).ReturnsAsync(mediatorResponse);

            // Act
            var result = await _controller.Update(updateRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Update_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var updateRequest = new UpdateImovelRequest { };
            var mediatorResponse = ApiResponse<ImovelGenericResponse>.FailureResult($"Erro ao atualizar imóvel");
            _mediatorMock.Setup(m => m.Send(updateRequest, default)).ReturnsAsync(mediatorResponse);

            // Act
            var result = await _controller.Update(updateRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Erro ao atualizar imóvel", badRequestResult.Value);
        }

        [Fact]
        public async Task Delete_ValidId_ReturnsNoContent()
        {
            // Arrange
            var deleteRequest = new DeleteImovelRequest { ImovelId = 13 };
            var mediatorResponse = ApiResponse<ImovelGenericResponse>.SuccessResult(GetImovelGenericResponseMock(), "Imóvel deletado com sucesso."); ;
            _mediatorMock.Setup(m => m.Send(deleteRequest, default)).ReturnsAsync(mediatorResponse);

            // Act
            var result = await _controller.Delete(deleteRequest);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async Task Delete_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var deleteRequest = new DeleteImovelRequest { };
            var mediatorResponse = ApiResponse<ImovelGenericResponse>.FailureResult($"Erro ao deletar imóvel");
            _mediatorMock.Setup(m => m.Send(deleteRequest, default)).ReturnsAsync(mediatorResponse);

            // Act
            var result = await _controller.Delete(deleteRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Erro ao deletar imóvel", badRequestResult.Value);
        }

        [Fact]
        public async Task GetById_ValidId_ReturnsOk()
        {
            // Arrange
            var getRequest = new GetImovelRequest { ImovelId = 13 };
            var mediatorResponse = ApiResponse<GetImovelResponse>.SuccessResult(GetImovelResponseMock());
            _mediatorMock.Setup(m => m.Send(getRequest, default)).ReturnsAsync(mediatorResponse);

            // Act
            var result = await _controller.GetById(getRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetImovel_ReturnsNotFound_WithInvalidRequest()
        {
            // Arrange
            var getRequest = new GetImovelRequest { ImovelId = 13 };
            var mediatorResponse = ApiResponse<GetImovelResponse>.FailureResult("Imóvel não encontrado");
            _mediatorMock.Setup(m => m.Send(getRequest, default)).ReturnsAsync(mediatorResponse);

            // Act
            var result = await _controller.GetById(getRequest);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Imóvel não encontrado", notFoundResult.Value);
        }

        [Fact]
        public async Task GetImoveis_ReturnsOk_WithValidRequest()
        {
            // Arrange
            var getRequest = GetImovelListRequestMock();
            var mediatorResponse = ApiResponse<GetImovelListResponse>.SuccessResult(new GetImovelListResponse
            {
                Imoveis =
                [
                    GetImovelResponseMock(),
                    GetImovelResponseMock()
                ]
            });
            _mediatorMock.Setup(m => m.Send(getRequest, default)).ReturnsAsync(mediatorResponse);

            // Act
            var result = await _controller.GetImoveis(getRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(mediatorResponse, okResult.Value);
        }

        [Fact]
        public async Task GetImoveis_ReturnsBadRequest_WhenNoImoveisFound()
        {
            // Arrange
            var getRequest = GetImovelListRequestMock();
            var mediatorResponse = ApiResponse<GetImovelListResponse>.SuccessResult(new GetImovelListResponse
            {
                Imoveis = []
            });
            _mediatorMock.Setup(m => m.Send(getRequest, default)).ReturnsAsync(mediatorResponse);

            // Act
            var result = await _controller.GetImoveis(getRequest);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Nenhum imóvel foi encontrado com os critérios especificados.", notFoundResult.Value);
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

        private static GetImovelResponse GetImovelResponseMock()
        {
            return new GetImovelResponse(
                id: 1,
                titulo: "Apartamento Central",
                tipo: TipoImovel.Apartamento,
                endereco: new Endereco("Rua das Flores", "123", "São Paulo", "SP", "12345-678"),
                area: 75.0,
                quartos: 2,
                banheiros: 1,
                vagasGaragem: 1,
                preco: 350000.00m,
                dataCriacao: DateTime.Now,
                status: StatusImovel.Disponivel
            );
        }

        private static GetImovelListRequest GetImovelListRequestMock()
        {
            return new GetImovelListRequest
            {

                Page = 1,
                PageSize = 5,
                FiltroTitulo = "Casa com piscina",
                FiltroTipo = TipoImovel.Casa,
                FiltroStatus = StatusImovel.Disponivel,
                FiltroQuartos = 3,
                FiltroBanheiros = 2,
                FiltroVagasGaragem = 2,
                FiltroAreaMinima = 100,
                FiltroAreaMaxima = 200,
                FiltroPrecoMinimo = 200000m,
                FiltroPrecoMaximo = 500000m,
                FiltroCidade = "Fortaleza",
                FiltroEstado = "CE"

            };
        }

        private static ImovelGenericResponse GetImovelGenericResponseMock()
        {
            return new(
                id: 1,
                titulo: "Casa de campo",
                tipo: TipoImovel.Casa.ToString()
            );
        }
        #endregion
    }
}