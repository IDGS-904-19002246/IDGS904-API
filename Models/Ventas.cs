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
        //public List<venta_producto>? productos { get; set; }


        //  [id_venta] INT IDENTITY(1, 1) NOT NULL,
        //  [fk_id_usuario] INT NOT NULL,

        //[fecha_compra] DATE NOT NULL,
        //[entrega] INT NOT NULL,

        //PRIMARY KEY CLUSTERED([id_venta] ASC),
        //FOREIGN KEY([fk_id_usuario]) REFERENCES[dbo].[tbl_usuarios] ([id_usuario])


    }
}
