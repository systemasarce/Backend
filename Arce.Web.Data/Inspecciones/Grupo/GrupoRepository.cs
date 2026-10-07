using Arce.Web.Data;
using Arce.Web.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Arce.Web.Data.Inspecciones.Grupo;

public class GrupoRepository : IGrupoRepository
{
    private readonly string _connectionString;

    public GrupoRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Connection")!;
    }

    public async Task<IEnumerable<GrupoEntity>?> ListarGrupo(int? Grupo_Id, int? Grupo_Cod, string? Grupo_Nombre, string? Grupo_Descripcion, string? Estado)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Grupo_Id", Grupo_Id ?? 0);
        parametros.Add("@Grupo_Cod", Grupo_Cod ?? 0);
        parametros.Add("@Grupo_Nombre", Grupo_Nombre ?? string.Empty);
        parametros.Add("@Grupo_Descripcion", Grupo_Descripcion ?? string.Empty);
        parametros.Add("@Estado", NormalizarEstado(Estado) ?? "A");

        return await connection.QueryAsync<GrupoEntity>(
            "[dbo].[SP_Filtrar_Ins_Grupo]",
            parametros,
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<(int Codigo, string Mensaje)> RegistrarGrupo(GrupoEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Grupo_Cod", valores.Grupo_Cod);
        parametros.Add("@Grupo_Nombre", valores.Grupo_Nombre);
        parametros.Add("@Usr_Reg", valores.Usr_Reg);
        parametros.Add("@Grupo_Descripcion", valores.Grupo_Descripcion);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Insertar_Ins_Grupo]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

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

    public async Task<(int Codigo, string Mensaje)> ActualizarGrupo(GrupoEntity valores)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Grupo_Id", valores.Grupo_Id);
        parametros.Add("@Usr_Mod", valores.Usr_Mod);
        parametros.Add("@Grupo_Cod", valores.Grupo_Cod);
        parametros.Add("@Grupo_Nombre", valores.Grupo_Nombre);
        parametros.Add("@Grupo_Descripcion", valores.Grupo_Descripcion);
        parametros.Add("@Estado", NormalizarEstado(valores.Estado) ?? "A");

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Actualizar_Ins_Grupo]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

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

    public async Task<(int Codigo, string Mensaje)> EliminarGrupo(int? Grupo_Id, string? Usr_Mod)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var parametros = new DynamicParameters();
        parametros.Add("@Grupo_Id", Grupo_Id ?? 0);
        parametros.Add("@Usr_Mod", Usr_Mod);

        try
        {
            await connection.ExecuteAsync(
                "[dbo].[SP_Eliminar_Ins_Cargo]",
                parametros,
                commandType: CommandType.StoredProcedure
            );

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

        var limpio = estado.Trim().ToUpperInvariant();

        return limpio switch
        {
            "A" => "A",
            "ACTIVO" => "A",
            "I" => "I",
            "INACTIVO" => "I",
            _ => limpio.Length > 0 ? limpio[..1] : null
        };
    }
}
