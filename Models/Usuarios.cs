using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;

namespace IDGS904_API.Models
{
    public class LoginData
    {
        public string user { get; set; }
        public string pass { get; set; }
    }
    public class Usuarios
    {
        [Key]
        public int id_usuario { get; set; }
        public string nombre { get; set; }
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }

        public string correo { get; set; }
        public string contrasena { get; set; }
        public string rol { get; set; }


//    [id_usuario] INT IDENTITY(1, 1) NOT NULL,

//        [nombre]     VARCHAR(64) NOT NULL,
//        [apellidoP]  VARCHAR(64) NOT NULL,
//        [apellidoM]  VARCHAR(64) NOT NULL,
//        [correo]     VARCHAR(64) NOT NULL,
//        [contrasena] VARCHAR(64) NOT NULL,
//        [rol]        INT NOT NULL,

//PRIMARY KEY CLUSTERED([id_usuario] ASC)


    }
}
