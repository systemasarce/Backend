using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arce.Web.Api.Controllers.Inspecciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoController : ControllerBase
    {
        private readonly IGrupoService _service;

        public GrupoController(IGrupoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getListarGrupo")]
        public async Task<IActionResult> ListarGrupo(int? Grupo_Id, int? Grupo_Cod, string? Grupo_Nombre, string? Estado)
        {
            var result = await _service.ListarGrupo(Grupo_Id, Grupo_Cod, Grupo_Nombre, Estado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarGrupo")]
        public async Task<IActionResult> RegistrarGrupo([FromBody] GrupoEntity valores)
        {
            var parametros = new GrupoEntity
            {
                Grupo_Cod = valores.Grupo_Cod,
                Grupo_Nombre = valores.Grupo_Nombre,
                Usr_Reg = valores.Usr_Reg
            };

            var result = await _service.RegistrarGrupo(parametros);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarGrupo")]
        public async Task<IActionResult> ActualizarGrupo([FromBody] GrupoEntity valores)
        {
            var parametros = new GrupoEntity
            {
                Grupo_Id = valores.Grupo_Id,
                Grupo_Cod = valores.Grupo_Cod,
                Grupo_Nombre = valores.Grupo_Nombre,
                Estado = valores.Estado,
                Usr_Mod = valores.Usr_Mod
            };

            var result = await _service.ActualizarGrupo(parametros);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarGrupo/{Grupo_Id}")]
        public async Task<IActionResult> EliminarGrupo(int? Grupo_Id, string? Usr_Mod)
        {
            var result = await _service.EliminarGrupo(Grupo_Id, Usr_Mod);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }
    }
}
