using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IGrupoDetalleRepository
{
    Task<IEnumerable<GrupoDetalleEntity>?> ListarGrupoDetalle(
        int? Detalle_Id,
        string? Detalle_Cod,
        string? Detalle_Nombre,
        int? Detalle_Valor,
        string? Grupo_Nombre,
        string? Estado);

    Task<(int Codigo, string Mensaje)> RegistrarGrupoDetalle(GrupoDetalleEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarGrupoDetalle(GrupoDetalleEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarGrupoDetalle(int? Detalle_Id, string? Usr_Mod);
}
