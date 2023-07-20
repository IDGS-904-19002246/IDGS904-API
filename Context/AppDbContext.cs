using IDGS904_API.Models;
using Microsoft.EntityFrameworkCore;

namespace IDGS904_API.Context
{
    public class AppDbContext:DbContext
    {
        private const string conectionstring= "conexion";

        public AppDbContext(DbContextOptions<AppDbContext> options) : 
            base(options) { } 
        //public DbSet<Alumnos>Alumnos { get; set; }
        public DbSet<Productos>tbl_productos { get; set; }
    }
}
