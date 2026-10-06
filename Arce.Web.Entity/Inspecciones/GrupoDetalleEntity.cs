namespace Arce.Web.Entity;

public class GrupoDetalleEntity
{
    public int? Detalle_Id { get; set; }
    public string? Detalle_Cod { get; set; }
    public string? Detalle_Nombre { get; set; }
    public int? Detalle_Valor { get; set; }
    public int? Grupo_Id { get; set; }
    public string? Grupo_Nombre { get; set; }
    public string? Estado { get; set; }
    public string? Usr_Reg { get; set; }
    public DateTime? Fec_Reg { get; set; }
    public string? Usr_Mod { get; set; }
    public DateTime? Fec_Mod { get; set; }
}
