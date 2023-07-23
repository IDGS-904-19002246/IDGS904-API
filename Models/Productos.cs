using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace IDGS904_API.Models
{
    public class Productos
    {
        [Key]
        public int? id_producto { get; set; }
        public string? nombre { get; set; }
        public int precio { get; set; }
        public int cantidad { get; set; }
        public int cantidad_min { get; set; }
        //[NotMapped]
        //public List<string> img {

        //    get; set => imgJson = Newtonsoft.Json.JsonConvert.SerializeObject(value);
        //}
        public string? descripcion { get; set; }
        public string? estado { get; set; }
        public int pendientes { get; set; }


        [NotMapped] // Esta propiedad no se mapeará directamente a una columna de la base de datos.
        public List<string>? img
        {
            get => Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(imgJson);
            set => imgJson = Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }

        [Column("img")] // Esta propiedad se mapeará a la columna "img" en la base de datos.
        public string imgJson { get; set; }

    }
}
