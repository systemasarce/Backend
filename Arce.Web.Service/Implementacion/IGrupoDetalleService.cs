using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IGrupoDetalleService
{
    Task<ServiceResponseList<GrupoDetalleEntity>?> ListarGrupoDetalle(
        int? Detalle_Id,
        string? Detalle_Cod,
        string? Detalle_Nombre,
        int? Detalle_Valor,
        string? Grupo_Nombre,
        string? Estado);

    Task<ServiceResponse<int>> RegistrarGrupoDetalle(GrupoDetalleEntity valores);
    Task<ServiceResponse<int>> ActualizarGrupoDetalle(GrupoDetalleEntity valores);
    Task<ServiceResponse<int>> EliminarGrupoDetalle(int? Detalle_Id, string? Usr_Mod);
}
