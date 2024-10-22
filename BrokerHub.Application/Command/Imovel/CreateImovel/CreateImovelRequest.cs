using BrokerHub.Application.Common;
using BrokerHub.Application.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BrokerHub.Application.Command.Imovel.CreateImovel;

public class CreateImovelRequest : IRequest<ApiResponse<ImovelGenericResponse>>
{
    [Required(ErrorMessage = "As informações do imóvel são obrigatórias.")]
    public required ImovelDTO Imovel { get; set; }
}
