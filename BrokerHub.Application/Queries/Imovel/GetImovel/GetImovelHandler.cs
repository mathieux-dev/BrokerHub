using MediatR;
using BrokerHub.Application.Common;
using Entities = BrokerHub.Domain.Entities;
using BrokerHub.Domain.Interfaces;
using BrokerHub.Domain.ValueObjects;

namespace BrokerHub.Application.Queries.Imovel.GetImovel;

public class GetImovelHandler(IImovelRepository imovelRepository) : IRequestHandler<GetImovelRequest, ApiResponse<GetImovelResponse>>
{
    private readonly IImovelRepository _imovelRepository = imovelRepository;

    public async Task<ApiResponse<GetImovelResponse>> Handle(GetImovelRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var imovel = await _imovelRepository.GetByIdAsync(request.ImovelId);

            if (imovel == null)
            {
                return ApiResponse<GetImovelResponse>.FailureResult("Imóvel não encontrado.");
            }

            var imovelResponse = BuildImovelResponse(imovel);
            return ApiResponse<GetImovelResponse>.SuccessResult(imovelResponse);
        }
        catch (Exception ex)
        {
            return ApiResponse<GetImovelResponse>.FailureResult($"Erro ao acessar o banco de dados: {ex.Message}");
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
