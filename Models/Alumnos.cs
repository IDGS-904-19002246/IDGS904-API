using System.ComponentModel.DataAnnotations;

namespace IDGS904_API.Models
{
    public class Alumnos
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int Edad { get; set; }
        public string? Correo { get; set; }
    }
}
