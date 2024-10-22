using MediatR;
using BrokerHub.Application.Common;
using BrokerHub.Domain.Interfaces;

namespace BrokerHub.Application.Command.Imovel.DeleteImovel;

public class DeleteImovelHandler(IImovelRepository imovelRepository) : IRequestHandler<DeleteImovelRequest, ApiResponse<ImovelGenericResponse>>
{
    private readonly IImovelRepository _imovelRepository = imovelRepository;

    public async Task<ApiResponse<ImovelGenericResponse>> Handle(DeleteImovelRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var imovel = await _imovelRepository.GetByIdAsync(request.ImovelId);

            if (imovel == null)
            {
                return ApiResponse<ImovelGenericResponse>.FailureResult("Imóvel não encontrado.");
            }

            await _imovelRepository.DeleteAsync(id: imovel.Id);

            var response = new ImovelGenericResponse(imovel.Id, imovel.Titulo, imovel.Tipo.ToString());

            return ApiResponse<ImovelGenericResponse>.SuccessResult(response, "Imóvel deletado com sucesso.");
        }
        catch (Exception ex)
        {
            return ApiResponse<ImovelGenericResponse>.FailureResult($"Erro ao deletar o imóvel: {ex.Message}");
        }
    }
}
