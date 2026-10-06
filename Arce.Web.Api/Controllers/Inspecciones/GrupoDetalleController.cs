using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Inspecciones;

[Route("api/[controller]")]
[ApiController]
public class GrupoDetalleController : ControllerBase
{
    private readonly IGrupoDetalleService _service;

    public GrupoDetalleController(IGrupoDetalleService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("getListarGrupoDetalle")]
    public async Task<IActionResult> ListarGrupoDetalle(
        int? Detalle_Id,
        string? Detalle_Cod,
        string? Detalle_Nombre,
        int? Detalle_Valor,
        string? Grupo_Nombre,
        string? Estado)
    {
        var result = await _service.ListarGrupoDetalle(
            Detalle_Id, Detalle_Cod, Detalle_Nombre, Detalle_Valor, Grupo_Nombre, Estado);

        if (result!.Success)
        {
            result.CodeResult = StatusCodes.Status200OK;
            return Ok(result);
        }

        result.CodeResult = StatusCodes.Status400BadRequest;
        return BadRequest(result);
    }

    [HttpPost]
    [Route("postRegistrarGrupoDetalle")]
    public async Task<IActionResult> RegistrarGrupoDetalle([FromBody] GrupoDetalleEntity valores)
    {
        var parametros = new GrupoDetalleEntity
        {
            Detalle_Cod = valores.Detalle_Cod,
            Detalle_Nombre = valores.Detalle_Nombre,
            Detalle_Valor = valores.Detalle_Valor,
            Grupo_Id = valores.Grupo_Id,
            Usr_Reg = valores.Usr_Reg
        };

        var result = await _service.RegistrarGrupoDetalle(parametros);
        if (result!.Success)
        {
            result.CodeResult = StatusCodes.Status200OK;
            return Ok(result);
        }

        result.CodeResult = StatusCodes.Status400BadRequest;
        return BadRequest(result);
    }

    [HttpPatch]
    [Route("patchActualizarGrupoDetalle")]
    public async Task<IActionResult> ActualizarGrupoDetalle([FromBody] GrupoDetalleEntity valores)
    {
        var parametros = new GrupoDetalleEntity
        {
            Detalle_Id = valores.Detalle_Id,
            Detalle_Cod = valores.Detalle_Cod,
            Detalle_Nombre = valores.Detalle_Nombre,
            Detalle_Valor = valores.Detalle_Valor,
            Grupo_Id = valores.Grupo_Id,
            Usr_Mod = valores.Usr_Mod
        };

        var result = await _service.ActualizarGrupoDetalle(parametros);
        if (result!.Success)
        {
            result.CodeResult = StatusCodes.Status200OK;
            return Ok(result);
        }

        result.CodeResult = StatusCodes.Status400BadRequest;
        return BadRequest(result);
    }

    [HttpDelete]
    [Route("deleteEliminarGrupoDetalle/{Detalle_Id}")]
    public async Task<IActionResult> EliminarGrupoDetalle(int? Detalle_Id, string? Usr_Mod)
    {
        var result = await _service.EliminarGrupoDetalle(Detalle_Id, Usr_Mod);
        if (result!.Success)
        {
            result.CodeResult = StatusCodes.Status200OK;
            return Ok(result);
        }

        result.CodeResult = StatusCodes.Status400BadRequest;
        return BadRequest(result);
    }
}
