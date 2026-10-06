using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public interface IGrupoService
{
    Task<ServiceResponseList<GrupoEntity>?> ListarGrupo(int? Grupo_Id, int? Grupo_Cod, string? Grupo_Nombre, string? Estado);
    Task<ServiceResponse<int>> RegistrarGrupo(GrupoEntity valores);
    Task<ServiceResponse<int>> ActualizarGrupo(GrupoEntity valores);
    Task<ServiceResponse<int>> EliminarGrupo(int? Grupo_Id, string? Usr_Mod);
}
