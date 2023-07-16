using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;

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
        public string? img { get; set; }
        public string? descripcion { get; set; }
        public string? estado { get; set; }
        public int pendientes { get; set; }



        //       [id_productos] INT IDENTITY(1,1),
        //   [nombre] VARCHAR(32)  NOT NULL,
        //   [precio]       INT NOT NULL,
        //cantidad INT NOT NULL DEFAULT 0,
        //cantidad_min INT NOT NULL DEFAULT 0,
        //[img] NVARCHAR(MAX),
        //descripcion VARCHAR(64) NULL,
        //estado VARCHAR(4) NULL,
        //pendientes INT NULL
    }
}
