using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
namespace IDGS904_API.Models
{
    public class Proveedores
    {
        [Key]
        public int? id_proveedor { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        
        //[NotMapped] // Esta propiedad no se mapeará directamente a una columna de la base de datos.
        //public List<string>? direccion
        //{
        //    get => Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(direccionJson);
        //    set => direccionJson = Newtonsoft.Json.JsonConvert.SerializeObject(value);
        //}

        //[Column("direccion")]
        //public string direccionJson { get; set; }
        [NotMapped] // Esta propiedad no se mapeará directamente a una columna de la base de datos.
        public List<string>? direccion
        {
            get => Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(direccionJson);
            set => direccionJson = Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }

        [Column("direccion")] // Esta propiedad se mapeará a la columna "img" en la base de datos.
        public string direccionJson { get; set; }
        //        CREATE TABLE[dbo].[tbl_proveedores]
        //        (
        //    [id_proveedor] INT            IDENTITY(1, 1) NOT NULL,
        //    [nombre]       VARCHAR(32)   NOT NULL,
        //    [correo]       VARCHAR(32)   NOT NULL,
        //    [telefono]     VARCHAR(15)   NOT NULL,
        //    [direccion]    NVARCHAR(MAX) NULL,
        //    PRIMARY KEY CLUSTERED([id_proveedor] ASC)
        //);

    }
}
