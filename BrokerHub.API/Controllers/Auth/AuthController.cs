using BrokerHub.API.Controllers.Auth.Requests;
using BrokerHub.API.Controllers.Auth.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BrokerHub.API.Controllers.Auth;

[Route("api/brokerhub/auth")]
[ApiController]
public class AuthController(IConfiguration configuration) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration;

    /// <summary>
    /// Endpoint para realizar o login do usuário.
    /// </summary>
    /// <param name="request">Objeto que contém as credenciais do usuário (email e senha).</param>
    /// <returns>Retorna um token JWT se as credenciais forem válidas.</returns>
    /// <response code="200">Se o login for bem-sucedido e o token for gerado.</response>
    /// <response code="401">Se as credenciais do usuário forem inválidas.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // O ideal aqui seria que houvesse uma tabela de usuários e que as credenciais fossem validadas
        // com os dados de lá, mas por motivos de praticidade e demonstração, optei pela validação simplificada.
        if (request.Email == "mateus.mourao@brokerhub.com" && request.Password == "garçagaiata")
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new TokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        return Unauthorized("Credenciais inválidas.");
    }
}
