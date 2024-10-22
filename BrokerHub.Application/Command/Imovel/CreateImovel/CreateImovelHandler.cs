using MediatR;
using Entities = BrokerHub.Domain.Entities;
using BrokerHub.Domain.Interfaces;
using BrokerHub.Application.Common;
using BrokerHub.Domain.ValueObjects;

namespace BrokerHub.Application.Command.Imovel.CreateImovel;

public class CreateImovelHandler(IImovelRepository imovelRepository) : IRequestHandler<CreateImovelRequest, ApiResponse<ImovelGenericResponse>>
{
    const string COMPLEMENTO_VAZIO = "N/A";
    private readonly IImovelRepository _imovelRepository = imovelRepository;

    public async Task<ApiResponse<ImovelGenericResponse>> Handle(CreateImovelRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var imovel = BuildImovelEntity(request);

            await _imovelRepository.AddAsync(imovel);

            var response = new ImovelGenericResponse(imovel.Id, imovel.Titulo, imovel.Tipo.ToString());

            return ApiResponse<ImovelGenericResponse>.SuccessResult(response, "Imóvel criado com sucesso.");
        }
        catch (Exception ex)
        {
            return ApiResponse<ImovelGenericResponse>.FailureResult($"Erro ao criar imóvel: {ex.Message}");
        }
    }

    private static Entities.Imovel BuildImovelEntity(CreateImovelRequest request)
    {
        var endereco = new Endereco(
            request.Imovel.Endereco!.Rua,
            request.Imovel.Endereco.Numero,
            request.Imovel.Endereco.Cidade,
            request.Imovel.Endereco.Estado.ToUpperInvariant(),
            request.Imovel.Endereco.CEP,
            
            string.IsNullOrEmpty(request.Imovel.Endereco.Complemento)
            ? COMPLEMENTO_VAZIO
            : request.Imovel.Endereco.Complemento
        );

        var imovel = new Entities.Imovel(
            request.Imovel.Titulo,
            request.Imovel.Tipo,
            endereco,
            request.Imovel.Area,
            request.Imovel.Quartos,
            request.Imovel.Banheiros,
            request.Imovel.VagasGaragem,
            request.Imovel.Preco,
            request.Imovel.Status

        );

        return imovel;
    }
}
