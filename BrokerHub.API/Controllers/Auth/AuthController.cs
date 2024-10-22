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
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] LoginRequest request)
    {
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