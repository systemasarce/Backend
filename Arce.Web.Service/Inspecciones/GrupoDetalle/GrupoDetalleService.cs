using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service.Inspecciones.GrupoDetalle;

public class GrupoDetalleService : IGrupoDetalleService
{
    private readonly IGrupoDetalleRepository _repository;

    public GrupoDetalleService(IGrupoDetalleRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponseList<GrupoDetalleEntity>?> ListarGrupoDetalle(
        int? Detalle_Id,
        string? Detalle_Cod,
        string? Detalle_Nombre,
        int? Detalle_Valor,
        string? Grupo_Nombre,
        string? Estado,
        string? Grupo_Descripcion)
    {
        var result = new ServiceResponseList<GrupoDetalleEntity>();
        try
        {
            var resultData = await _repository.ListarGrupoDetalle(
                Detalle_Id, Detalle_Cod, Detalle_Nombre, Detalle_Valor, Grupo_Nombre, Estado, Grupo_Descripcion);
            var elements = (resultData ?? Enumerable.Empty<GrupoDetalleEntity>()).ToList();

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

    public async Task<ServiceResponse<int>> RegistrarGrupoDetalle(GrupoDetalleEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.RegistrarGrupoDetalle(valores);
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

    public async Task<ServiceResponse<int>> ActualizarGrupoDetalle(GrupoDetalleEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarGrupoDetalle(valores);
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

    public async Task<ServiceResponse<int>> EliminarGrupoDetalle(int? Detalle_Id, string? Usr_Mod)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.EliminarGrupoDetalle(Detalle_Id, Usr_Mod);
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
