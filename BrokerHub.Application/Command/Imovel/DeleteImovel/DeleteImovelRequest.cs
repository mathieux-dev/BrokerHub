using MediatR;
using BrokerHub.Application.Common;
using System.ComponentModel.DataAnnotations;

namespace BrokerHub.Application.Command.Imovel.DeleteImovel;

public class DeleteImovelRequest : IRequest<ApiResponse<ImovelGenericResponse>>
{
    [Required(ErrorMessage = "O ID do imóvel é obrigatório.")]
    public int ImovelId { get; set; }
}
