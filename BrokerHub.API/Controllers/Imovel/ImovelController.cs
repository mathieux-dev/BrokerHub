using BrokerHub.Application.Command.Imovel;
using BrokerHub.Application.Command.Imovel.CreateImovel;
using BrokerHub.Application.Command.Imovel.DeleteImovel;
using BrokerHub.Application.Command.Imovel.UpdateImovel;
using BrokerHub.Application.Queries.Imovel.GetImovel;
using BrokerHub.Application.Queries.Imovel.GetImovelList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrokerHub.API.Controllers.Imovel;

[Route("api/brokerhub/imovel")]
[ApiController]
public class ImovelController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Cria um novo imóvel.
    /// </summary>
    [Authorize]
    [HttpPost("create")]
    [ProducesResponseType(typeof(ImovelGenericResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateImovelRequest request)
    {
        var result = await _mediator.Send(request);

        if (result.Success)
        {
            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
        }

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Atualiza um imóvel existente.
    /// </summary>
    [Authorize]
    [HttpPut("update")]
    [ProducesResponseType(typeof(ImovelGenericResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromBody] UpdateImovelRequest request)
    {
        var result = await _mediator.Send(request);

        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Deleta um imóvel pelo ID.
    /// </summary>
    [Authorize]
    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete([FromQuery] DeleteImovelRequest request)
    {
        var result = await _mediator.Send(request);

        if (result.Success)
        {
            return NoContent();
        }

        return BadRequest(result.Message);
    }

    /// <summary>
    /// Obtém um imóvel pelo ID.
    /// </summary>
    [HttpGet("details")]
    [ProducesResponseType(typeof(GetImovelResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromQuery] GetImovelRequest request)
    {
        var result = await _mediator.Send(request);

        if (result.Success)
        {
            return Ok(result.Data);
        }

        return NotFound(result.Message);
    }

    /// <summary>
    /// Obtém a lista de imóveis com base nos parâmetros de consulta.
    /// </summary>
    [HttpGet("list")]
    [ProducesResponseType(typeof(GetImovelListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetImoveis([FromQuery] GetImovelListRequest query)
    {
        var result = await _mediator.Send(query);

        if (result is null || !result.Data!.Imoveis.Any())
        {
            return NotFound("Nenhum imóvel foi encontrado com os critérios especificados.");
        }

        return Ok(result);
    }
}
