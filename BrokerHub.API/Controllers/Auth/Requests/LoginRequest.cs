using System.ComponentModel.DataAnnotations;

namespace BrokerHub.API.Controllers.Auth.Requests;

public class LoginRequest
{
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail deve ser um endereço de e-mail válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(17, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 17 caracteres.")]
    public string Password { get; set; }
}
