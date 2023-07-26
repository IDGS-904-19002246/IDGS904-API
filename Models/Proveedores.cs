using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace IDGS904_API.Models
{
    public class Proveedores
    {
        [Key]
        public int? id_proveedor { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        [NotMapped]
        public List<string>? direccion
        {
            get => Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(direccionJson);
            set => direccionJson = Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }
        [Column("direccion")]
        public string direccionJson { get; set; }
    }

    public class insumo_proveedor {
        [Key]
        public int? id_insumo_proveedor { get; set; }
        public int fk_id_insumo { get; set; }
        public int fk_id_proveedor { get; set; }
        //.........................................
        public int cantidad { get; set; }
        public int precio { get; set; }
        public DateTime fecha{ get; set; }
    }

    public class compra {
        public int id { get; set; }
        public List<insumo_proveedor> lista { get; set;}
    }
}
