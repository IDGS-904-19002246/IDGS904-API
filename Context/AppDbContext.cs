using IDGS904_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using static IDGS904_API.Controllers.ventasController;

namespace IDGS904_API.Context
{
    public class AppDbContext:DbContext
    {
        private const string conectionstring = "conexion";
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Productos>tbl_productos { get; set; }
        public DbSet<Insumos>tbl_insumos { get; set; }
        public DbSet<Usuarios>tbl_usuarios { get; set; }
        public DbSet<Ventas>tbl_ventas { get; set; }
        public DbSet<Proveedores> tbl_proveedores { get; set; }
        //................................................................................
        public DbSet<venta_producto>tbl_venta_producto { get; set; }
        public DbSet<insumo_proveedor> tbl_insumo_proveedor { get; set; }
        public DbSet<insumo_producto> tbl_insumo_producto { get; set; }
        public DbSet<perecedero> tbl_perecedero { get; set; }
    }

    public class AppDb2Context : DbContext
    {
        private const string conectionstring = "conexionComun";
        public AppDb2Context(DbContextOptions<AppDb2Context> options) : base(options) { }

        public DbSet<Productos> tbl_productos { get; set; }
        //public DbSet<Insumos> tbl_insumos { get; set; }
        public DbSet<Usuarios> tbl_usuarios { get; set; }
        public DbSet<Ventas> tbl_ventas { get; set; }
        //public DbSet<Proveedores> tbl_proveedores { get; set; }
        //................................................................................
        public DbSet<venta_producto> tbl_venta_producto { get; set; }
        //public DbSet<insumo_proveedor> tbl_insumo_proveedor { get; set; }
        //public DbSet<insumo_producto> tbl_insumo_producto { get; set; }
        public DbSet<perecedero> tbl_perecedero { get; set; }
    }
}
