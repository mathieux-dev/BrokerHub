using BrokerHub.Application.Common;
using BrokerHub.Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BrokerHub.Application.Queries.Imovel.GetImovelList;

public class GetImovelListRequest : IRequest<ApiResponse<GetImovelListResponse>>
{
    [Range(0, int.MaxValue, ErrorMessage = "A página deve ser maior ou igual a 0.")]
    public int Page { get; set; }

    [Range(0, 100, ErrorMessage = "O tamanho da página deve ser entre 0 e 100.")]
    public int PageSize { get; set; }
    
    // Filtros básicos
    [StringLength(150, ErrorMessage = "O título do imóvel não pode ter mais de 150 caracteres.")]
    public string? FiltroTitulo { get; set; }

    public TipoImovel? FiltroTipo { get; set; }

    public StatusImovel? FiltroStatus { get; set; }
    
    // Filtros por características do imóvel
    [Range(0, int.MaxValue, ErrorMessage = "A quantidade de quartos deve ser um número positivo.")]
    public int? FiltroQuartos { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A quantidade de banheiros deve ser um número positivo.")]
    public int? FiltroBanheiros { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A quantidade de vagas na garagem deve ser um número positivo.")]
    public int? FiltroVagasGaragem { get; set; }
    
    // Filtros por área
    [Range(0.1, double.MaxValue, ErrorMessage = "A área mínima deve ser maior que 0.")]
    public double? FiltroAreaMinima { get; set; }

    [Range(0.1, double.MaxValue, ErrorMessage = "A área máxima deve ser maior que 0.")]
    public double? FiltroAreaMaxima { get; set; }
    
    // Filtros por preço
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço mínimo deve ser maior que 0.")]
    public decimal? FiltroPrecoMinimo { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço máximo deve ser maior que 0.")]
    public decimal? FiltroPrecoMaximo { get; set; }

    // Filtros por endereço
    [StringLength(100, ErrorMessage = "A cidade não pode ter mais de 100 caracteres.")]
    public string? FiltroCidade { get; set; }

    [StringLength(2, ErrorMessage = "O estado deve ter 2 caracteres.")]
    public string? FiltroEstado { get; set; }
}
