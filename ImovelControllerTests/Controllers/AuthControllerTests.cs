using BrokerHub.API.Controllers.Auth;
using BrokerHub.API.Controllers.Auth.Requests;
using BrokerHub.API.Controllers.Auth.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;

namespace BrokerHub.API.Tests.Controllers;

public class AuthControllerTests
{
    private readonly Mock<IConfiguration> _mockConfig;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _mockConfig = new Mock<IConfiguration>();

        _mockConfig.SetupGet(c => c["Jwt:Key"]).Returns("lJzdCpEqLULQmKiY16ku8SosG2Q1KW3MRCOdco9W1mU05DGrKYiFDrNPW87is2YsHa3uAcVhEKE3Z3yp7nuUUzQK1HgmOVl9Cv1rMvlCWZdfW0tjOYydsQ99kIiFAjmZJnILTq1cTxl+4eVPLds+st0u6US+gIpvWvKojdPJCeQ=");
        _mockConfig.SetupGet(c => c["Jwt:Issuer"]).Returns("BrokerHub");
        _mockConfig.SetupGet(c => c["Jwt:Audience"]).Returns("BrokerHubAPI");

        _controller = new AuthController(_mockConfig.Object);
    }

    [Fact]
    public void Login_ValidCredentials_ReturnsToken()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Email = "mateus.mourao@brokerhub.com",
            Password = "garçagaiata"
        };

        // Act
        var result = _controller.Login(loginRequest) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);

        var tokenResponse = result.Value as TokenResponse;
        Assert.NotNull(tokenResponse);
        Assert.IsType<string>(tokenResponse.Token);

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(tokenResponse.Token);
        Assert.Equal(loginRequest.Email, jwtToken.Subject);
    }


    [Fact]
    public void Login_InvalidCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Email = "wrong.email@domain.com",
            Password = "wrongpassword"
        };

        // Act
        var result = _controller.Login(loginRequest) as UnauthorizedObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(401, result.StatusCode);
        Assert.Equal("Credenciais inválidas.", result.Value);
    }

    [Fact]
    public void Login_EmptyCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Email = "",
            Password = ""
        };

        // Act
        var result = _controller.Login(loginRequest) as UnauthorizedObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(401, result.StatusCode);
        Assert.Equal("Credenciais inválidas.", result.Value);
    }

    [Fact]
    public void Login_NullCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Email = null!,
            Password = null!
        };

        // Act
        var result = _controller.Login(loginRequest) as UnauthorizedObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(401, result.StatusCode);
        Assert.Equal("Credenciais inválidas.", result.Value);
    }
}
