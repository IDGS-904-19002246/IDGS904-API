using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;

namespace IDGS904_API.Models
{
    //[Keyless]
    public class venta_producto
    {
        [Key]
        public int id_venta_producto { get; set; }
        public int fk_id_venta { get; set; }
        public int fk_id_producto { get; set; }
        public int cantidad { get; set; }
        public int precio { get; set; }
    }
    public class Ventas
    {
        [Key]
        public int id_venta { get; set; }
        public int fk_id_usuario { get; set; }
        public DateTime fecha_compra { get; set; }
        public string status { get; set; }
        [NotMapped]
        public List<string>? direccionJson
        {
            get => Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(direccion);
            set => direccion = Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }
        [Column("direccion")]
        public string? direccion { get; set; }
    }

    //.......................................................................................

    public class producto
    {
        public string? p_nombre { get; set; }
        public int cantidad { get; set; }
        public int precio { get; set; }
        public string descripcion { get; set; }
    }
    public class venta_productos
    {
        public string u_nombre { get; set; }
        public DateTime fecha { get; set; }
        public string status { get; set; }

        public List<producto> lista { get; set; }
    }
}
