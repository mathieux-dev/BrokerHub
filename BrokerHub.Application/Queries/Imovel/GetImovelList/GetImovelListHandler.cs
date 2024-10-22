using MediatR;
using Entities = BrokerHub.Domain.Entities;
using BrokerHub.Domain.Interfaces;
using BrokerHub.Application.Queries.Imovel.GetImovel;
using BrokerHub.Domain.ValueObjects;
using BrokerHub.Application.Common;

namespace BrokerHub.Application.Queries.Imovel.GetImovelList;

public class GetImovelListHandler(IImovelRepository imovelRepository) : IRequestHandler<GetImovelListRequest, ApiResponse<GetImovelListResponse>>
{
    private readonly IImovelRepository _imovelRepository = imovelRepository;

    public async Task<ApiResponse<GetImovelListResponse>> Handle(GetImovelListRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var imoveis = await _imovelRepository.GetAllAsync(
                request.Page <= 0 ? 1 : request.Page, 
                request.PageSize <= 0 ? 5 : request.PageSize, 
                request.FiltroTitulo, 
                request.FiltroTipo, 
                request.FiltroStatus,
                request.FiltroQuartos, 
                request.FiltroBanheiros, 
                request.FiltroVagasGaragem, 
                request.FiltroAreaMinima, 
                request.FiltroAreaMaxima, 
                request.FiltroPrecoMinimo, 
                request.FiltroPrecoMaximo,
                request.FiltroCidade,
                request.FiltroEstado
            );

            var imoveisResponse = new GetImovelListResponse
            {
                Imoveis = imoveis.Select(imovel => BuildImovelResponse(imovel)).ToList()
            };

            return ApiResponse<GetImovelListResponse>.SuccessResult(imoveisResponse);
        }
        catch (Exception ex)
        {
            return ApiResponse<GetImovelListResponse>.FailureResult($"Erro ao acessar o banco de dados: {ex.Message}");
        }
    }

    private static GetImovelResponse BuildImovelResponse(Entities.Imovel imovel)
    {
        var endereco = new Endereco(
            imovel.Endereco.Rua,
            imovel.Endereco.Numero,
            imovel.Endereco.Cidade,
            imovel.Endereco.Estado,
            imovel.Endereco.CEP,
            imovel.Endereco.Complemento
        );

        var imovelResponse = new GetImovelResponse(
            id: imovel.Id,
            titulo: imovel.Titulo,
            tipo: imovel.Tipo,
            endereco: endereco,
            area: imovel.Area,
            quartos: imovel.Quartos,
            banheiros: imovel.Banheiros,
            vagasGaragem: imovel.VagasGaragem,
            preco: imovel.Preco,
            dataCriacao: imovel.DataCriacao,
            status: imovel.Status
        );

        return imovelResponse;
    }
}
