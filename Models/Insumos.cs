using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;

namespace IDGS904_API.Models
{
    public class Insumos
    {
        [Key]
        public int? id_insumo { get; set; }
        public string? nombre { get; set; }
        public int cantidad { get; set; }
        public int cantidad_min { get; set; }
        public string? estado { get; set; }
        public bool perecedero { get; set; }
    }
    //public class surtitInsumo
    //{
    //    public int id_insumo { get; set; }
    //    public int cantidad { get; set; }
    //    public DateTime? fecha{ get; set; }
    //    public int id_proveedor { get; set; }
    //}
}