using System.ComponentModel.DataAnnotations;

namespace BrokerHub.Application.DTOs;

public class EnderecoDTO
{
    [Required(ErrorMessage = "A rua é obrigatória.")]
    [StringLength(150, ErrorMessage = "A rua não pode ter mais de 150 caracteres.")]
    public required string Rua { get; set; }

    [Required(ErrorMessage = "O número é obrigatório.")]
    public required string Numero { get; set; }

    [Required(ErrorMessage = "A cidade é obrigatória.")]
    [StringLength(100, ErrorMessage = "A cidade não pode ter mais de 100 caracteres.")]
    public required string Cidade { get; set; }

    [Required(ErrorMessage = "O estado é obrigatório.")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "O estado deve ter 2 caracteres.")]
    public required string Estado { get; set; }

    [Required(ErrorMessage = "O CEP é obrigatório.")]
    [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "O CEP informado é inválido.")]
    public required string CEP { get; set; }

    public string? Complemento { get; set; }
}
