using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service.Inspecciones.Grupo;

public class GrupoService : IGrupoService
{
    private readonly IGrupoRepository _repository;

    public GrupoService(IGrupoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<GrupoEntity>?> ListarGrupo(int? Grupo_Id, int? Grupo_Cod, string? Grupo_Nombre, string? Estado)
    {
        var result = new ServiceResponseList<GrupoEntity>();
        try
        {
            var resultData = await _repository.ListarGrupo(Grupo_Id, Grupo_Cod, Grupo_Nombre, Estado);
            var elements = (resultData ?? Enumerable.Empty<GrupoEntity>()).ToList();

            result.Success = true;
            result.Message = elements.Any() ? "Completado con éxito" : "No existe información";
            result.Elements = elements;
            result.TotalElements = elements.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> RegistrarGrupo(GrupoEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.RegistrarGrupo(valores);
            result.Success = resultData.Codigo == 0;
            result.Message = resultData.Mensaje;
            result.CodeTransacc = resultData.Codigo;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Excepcion no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> ActualizarGrupo(GrupoEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarGrupo(valores);
            result.Success = resultData.Codigo == 0;
            result.Message = resultData.Mensaje;
            result.CodeTransacc = resultData.Codigo;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Excepcion no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> EliminarGrupo(int? Grupo_Id, string? Usr_Mod)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.EliminarGrupo(Grupo_Id, Usr_Mod);
            result.Success = resultData.Codigo == 0;
            result.Message = resultData.Mensaje;
            result.CodeTransacc = resultData.Codigo;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Excepcion no controlada " + ex.Message;
            return result;
        }
    }
}
