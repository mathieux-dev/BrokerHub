using System.ComponentModel.DataAnnotations;
using BrokerHub.Application.Common;
using MediatR;

namespace BrokerHub.Application.Queries.Imovel.GetImovel;

public class GetImovelRequest : IRequest<ApiResponse<GetImovelResponse>>
{
    [Required(ErrorMessage = "O ID do imóvel é obrigatório.")]
    public int ImovelId { get; set; }
}
