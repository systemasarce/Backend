using Arce.Web.Data;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Arce.Web.Data.Inspecciones.GrupoDetalle;

public class GrupoDetalleRepository : IGrupoDetalleRepository
{
    private readonly string _connectionString;

    public GrupoDetalleRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<GrupoDetalleEntity>?> ListarGrupoDetalle(
        int? Detalle_Id,
        string? Detalle_Cod,
        string? Detalle_Nombre,
        int? Detalle_Valor,
        string? Grupo_Nombre,
        string? Estado)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Detalle_Id", Detalle_Id ?? 0);
        parametros.Add("@Detalle_Cod", Detalle_Cod ?? string.Empty);
        parametros.Add("@Detalle_Nombre", Detalle_Nombre ?? string.Empty);
        parametros.Add("@Detalle_Valor", Detalle_Valor ?? 0);
        parametros.Add("@Grupo_Nombre", Grupo_Nombre ?? string.Empty);
        parametros.Add("@Estado", NormalizarEstado(Estado) ?? "A");

        return await connection.QueryAsync<GrupoDetalleEntity>(
            "[dbo].[SP_Filtrar_Grupo_Detalle]",
            parametros,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarGrupoDetalle(GrupoDetalleEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Detalle_Cod", valores.Detalle_Cod);
        parametros.Add("@Detalle_Nombre", valores.Detalle_Nombre);
        parametros.Add("@Detalle_Valor", valores.Detalle_Valor);
        parametros.Add("@Grupo_Id", valores.Grupo_Id);
        parametros.Add("@Usr_Reg", valores.Usr_Reg);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Insertar_Grupo_Detalle]",
                parametros,
                commandType: CommandType.StoredProcedure);

            return (0, "Completado con éxito");
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
        catch (Exception ex)
        {
            return (1, ex.Message);
        }
    }

    public async Task<(int Codigo, string Mensaje)> ActualizarGrupoDetalle(GrupoDetalleEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Detalle_Id", valores.Detalle_Id);
        parametros.Add("@Detalle_Cod", valores.Detalle_Cod);
        parametros.Add("@Detalle_Nombre", valores.Detalle_Nombre);
        parametros.Add("@Detalle_Valor", valores.Detalle_Valor);
        parametros.Add("@Grupo_Id", valores.Grupo_Id);
        parametros.Add("@Usr_Mod", valores.Usr_Mod);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Actualizar_Grupo_Detalle]",
                parametros,
                commandType: CommandType.StoredProcedure);

            return (0, "Completado con éxito");
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
        catch (Exception ex)
        {
            return (1, ex.Message);
        }
    }

    public async Task<(int Codigo, string Mensaje)> EliminarGrupoDetalle(int? Detalle_Id, string? Usr_Mod)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Detalle_Id", Detalle_Id ?? 0);
        parametros.Add("@Usr_Mod", Usr_Mod);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Eliminar_Grupo_Detalle]",
                parametros,
                commandType: CommandType.StoredProcedure);

            return (0, "Completado con éxito");
        }
        catch (SqlException ex)
        {
            return (1, ex.Message);
        }
        catch (Exception ex)
        {
            return (1, ex.Message);
        }
    }

    private static string? NormalizarEstado(string? estado)
    {
        if (string.IsNullOrWhiteSpace(estado))
        {
            return null;
        }

        return estado.Trim().ToUpperInvariant() switch
        {
            "A" => "A",
            "ACTIVO" => "A",
            "I" => "I",
            "INACTIVO" => "I",
            _ => estado.Trim().ToUpperInvariant()[..1]
        };
    }
}
