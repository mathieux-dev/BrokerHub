using System.ComponentModel.DataAnnotations;
using BrokerHub.Application.Common;
using BrokerHub.Application.DTOs;
using BrokerHub.Domain.Enums;
using MediatR;

namespace BrokerHub.Application.Command.Imovel.UpdateImovel;

public class UpdateImovelRequest : IRequest<ApiResponse<ImovelGenericResponse>>
{
    [Required(ErrorMessage = "O ID do imóvel é obrigatório.")]
    public int ImovelId { get; set; }

    [StringLength(150, ErrorMessage = "O título do imóvel não pode ter mais de 150 caracteres.")]
    public string? Titulo { get; set; }

    [Required(ErrorMessage = "O tipo de imóvel é obrigatório.")]
    public TipoImovel Tipo { get; set; }

    [Required(ErrorMessage = "O endereço do imóvel é obrigatório.")]
    public EnderecoDTO? Endereco { get; set; }

    [Range(0.1, double.MaxValue, ErrorMessage = "A área do imóvel deve ser maior que 0.")]
    public double Area { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço do imóvel deve ser maior que 0.")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "O status do imóvel é obrigatório.")]
    public StatusImovel Status { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A quantidade de quartos deve ser um número positivo.")]
    public int Quartos { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A quantidade de banheiros deve ser um número positivo.")]
    public int Banheiros { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A quantidade de vagas na garagem deve ser um número positivo.")]
    public int VagasGaragem { get; set; }
}
