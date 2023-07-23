using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;

namespace IDGS904_API.Models
{
    public class Insumos
    {
        [Key]
        public int id_insumo { get; set; }
        public string nombre { get; set; }
        public int cantidad { get; set; }
        public int cantidad_min { get; set; }
        public string status{ get; set; }


        //CREATE TABLE[dbo].[tbl_insumos](
        //    [id_insumo] INT          IDENTITY(1, 1) NOT NULL,
        //    [nombre]       VARCHAR(32) NOT NULL,
        //    [cantidad]     INT NOT NULL,
        //    [cantidad_min] INT NOT NULL,
        //    PRIMARY KEY CLUSTERED([id_insumo] ASC)
        //);


    }
}
