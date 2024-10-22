using BrokerHub.Application.Common;
using BrokerHub.Domain.Interfaces;
using BrokerHub.Domain.ValueObjects;
using MediatR;

namespace BrokerHub.Application.Command.Imovel.UpdateImovel;

public class UpdateImovelHandler(IImovelRepository imovelRepository) : IRequestHandler<UpdateImovelRequest, ApiResponse<ImovelGenericResponse>>
{
    private readonly IImovelRepository _imovelRepository = imovelRepository;

    public async Task<ApiResponse<ImovelGenericResponse>> Handle(UpdateImovelRequest request, CancellationToken cancellationToken)
    {
        var imovel = await _imovelRepository.GetByIdAsync(request.ImovelId);
        if (imovel == null)
        {
            return ApiResponse<ImovelGenericResponse>.FailureResult("Imóvel não encontrado.");
        }

        try
        {
            imovel.Update(
                request.Titulo!,
                request.Tipo,
                new Endereco(
                    request.Endereco!.Rua,
                    request.Endereco.Numero,
                    request.Endereco.Cidade,
                    request.Endereco.Estado.ToUpperInvariant(),
                    request.Endereco.CEP,
                    string.IsNullOrEmpty(request.Endereco.Complemento) ? "N/A" : request.Endereco.Complemento
                ),
                request.Area,
                request.Quartos,
                request.Banheiros,
                request.VagasGaragem,
                request.Preco,
                request.Status
            );

            await _imovelRepository.UpdateAsync(imovel);

            var response = new ImovelGenericResponse(imovel.Id, imovel.Titulo, imovel.Tipo.ToString());

            return ApiResponse<ImovelGenericResponse>.SuccessResult(response, "Imóvel atualizado com sucesso.");
        }
        catch (Exception ex)
        {
            return ApiResponse<ImovelGenericResponse>.FailureResult($"Erro ao atualizar imóvel: {ex.Message}");
        }
    }

}
