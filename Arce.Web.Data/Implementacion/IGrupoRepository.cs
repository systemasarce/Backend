using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IGrupoRepository
{
    Task<IEnumerable<GrupoEntity>?> ListarGrupo(int? Grupo_Id, int? Grupo_Cod, string? Grupo_Nombre, string? Estado);
    Task<(int Codigo, string Mensaje)> RegistrarGrupo(GrupoEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarGrupo(GrupoEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarGrupo(int? Grupo_Id, string? Usr_Mod);
}
