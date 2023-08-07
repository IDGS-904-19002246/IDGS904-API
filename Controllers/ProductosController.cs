using IDGS904_API.Context;
using IDGS904_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Data.SqlClient;

namespace IDGS904_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        private readonly AppDb2Context _context2;
        private readonly IConfiguration _configuration;

        public productosController(AppDbContext context,IWebHostEnvironment environment
            , AppDb2Context context2, IConfiguration configuration
            )
        {
            _context = context;
            _environment = environment;

            _context2 = context2;
            _configuration = configuration;
        }
        public AppDb2Context sql(string user, string pass)
        {
            string connectionString = string.Format(
                _configuration.GetConnectionString("conexionComun"),
            user, pass);
            var optionsBuilder = new DbContextOptionsBuilder<AppDb2Context>();
            optionsBuilder.UseSqlServer(connectionString);
            //return new AppDb2Context(optionsBuilder.Options);
            try
            {
                var dbContext = new AppDb2Context(optionsBuilder.Options);
                dbContext.Database.ExecuteSqlRaw("SELECT 1");
                return dbContext;
            }
            catch (Exception ex){return null;}
        }
        //....................................................................................

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                //var producto = from p in _context.tbl_productos
                //               join ip in _context.tbl_insumo_producto on p.id_producto equals ip.fk_id_producto into receta
                //               from i in receta.DefaultIfEmpty()
                //               join ip in _context.tbl_insumos on i.fk_id_insumo equals ip.id_insumo into insumo
                //               from ip in insumo.DefaultIfEmpty()
                //               select new
                //               {
                //                   p.id_producto,
                //                   p.nombre,
                //                   p.precio,
                //                   p.cantidad,
                //                   p.cantidad_min,
                //                   p.descripcion,
                //                   p.estado,
                //                   p.pendientes,
                //                   p.img,

                //                   i_nombre = ip.nombre,
                //                   i_cantidad = i != null ? ip.cantidad : 0
                //               };

                //var listaNombres = producto
                //    .GroupBy(v => new {
                //        v.id_producto,
                //        v.nombre,
                //        v.precio,
                //        v.cantidad,
                //        v.cantidad_min,
                //        v.descripcion,
                //        v.estado,
                //        v.pendientes,
                //        v.img,
                //    })
                //    .Select(
                //    g => new
                //    {
                //        g.Key.id_producto,
                //        g.Key.nombre,
                //        g.Key.precio,
                //        g.Key.cantidad,
                //        g.Key.cantidad_min,
                //        g.Key.descripcion,
                //        g.Key.estado,
                //        g.Key.pendientes,
                //        g.Key.img,
                //        receta = g.Select(r => new { r.i_nombre, r.i_cantidad }).ToList()

                //    }).Take(4).ToList();
                

                return Ok((from p in _context.tbl_productos select p).Take(4).ToList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{rango}", Name = "productos")]
        public ActionResult Get(int rango, [FromHeader] string[] user)
        {
            try
            {
                if (rango == 1 || rango <= 0) {return Get();}
                var context = sql(user[0], user[1]);
                if (context == null){return BadRequest("xd");}

                int inicio = (rango - 1) * 4;
                var productos = (from p in context.tbl_productos select p).Skip(inicio).Take(4).ToList();
                return Ok(productos);
            }
            catch (Exception ex){return BadRequest(ex.Message);}
        }
        

        [HttpGet("reseta/{id}", Name = "getReseta")]
        public ActionResult getReseta(int id)
        {
            try
            {
                var producto = from p in _context.tbl_productos

                               join ip in _context.tbl_insumo_producto on p.id_producto equals ip.fk_id_producto into receta
                               from ip in receta.DefaultIfEmpty()

                               join i in _context.tbl_insumos on ip.fk_id_insumo equals i.id_insumo into insumo
                               from i in insumo.DefaultIfEmpty()

                               where p.id_producto == id
                               select new
                               {
                                   p.id_producto,
                                   p.nombre,

                                   i_nombre = i.nombre,
                                   i_cantidad = ip != null ? ip.cantidad : 0
                               };

                var reseta = producto
                    .GroupBy(v => new {
                        v.id_producto,
                        v.nombre,
                    })
                    .Select(
                    g => new {
                        g.Key.id_producto,
                        g.Key.nombre,
                        receta = g.Select(r => new { r.i_nombre, r.i_cantidad }).ToList()
                    });
                return Ok(reseta);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        //....................................................................................
        [HttpPost("crearProducto")]
        public ActionResult crearProducto([FromBody] Productos P)
        {
            try
            {
                if (P.fotos == null || P.fotos.Count() == 0)
                {
                    return Ok(new { status = "ok", msg = "El producto necesita mínimo una imagen :3" });
                }
                else
                {
                    string img = "[";
                    int index = P.fotos.Count();
                    foreach (Foto foto in P.fotos)
                    {
                        string nombre = P.nombre+" - "+ Guid.NewGuid().ToString().Substring(1, 4)+ ".jpg";
                        img += "'"+nombre+"'"+(index != 1 ? ",":"");
                        var rutaCompleta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Img", nombre);
                        System.IO.File.WriteAllBytes(rutaCompleta, bytes: foto.file64Foto);
                        index--;
                    }
                    img += "]";
                    P.imgJson = img;
                    _context.tbl_productos.Add(P);
                    _context.SaveChanges();
                    return Ok(new { status = "ok", msg = "Todo bien :]" });
                }
            }
            catch (Exception ex){
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
                return BadRequest(ex.Message);
            }
        }
        //{"id":1,"cantidad":2}
        [HttpPost("validarProductoCocinar")]
        public ActionResult validarProductoCocinar([FromBody] cocinar P)
        {
            try
            {
                var receta =
                    from ip in _context.tbl_insumo_producto
                    join i in _context.tbl_insumos on ip.fk_id_insumo equals i.id_insumo
                    where ip.fk_id_producto == P.id
                    select new {

                        ip.fk_id_insumo,
                        ip.fk_id_producto,
                        ip.cantidad,

                        i.nombre,
                        i.estado,
                        nececito = (ip.cantidad * P.cantidad),
                        tengo = i.cantidad,
                        minimo = i.cantidad_min
                    };
                return Ok(receta);
            }
            catch (Exception ex){return BadRequest(ex.Message);}
        }

        [HttpPost("cocinarProducto")]
        public ActionResult cocinarProducto([FromBody] cocinar IP)
        {
            try
            {
                // restar insumo/sumar productos/añadir insumo_producto
                var reseta = (from i in _context.tbl_insumos
                             join ip in _context.tbl_insumo_producto on i.id_insumo equals ip.fk_id_insumo
                             where ip.fk_id_producto == IP.id
                             select new {
                                 i.id_insumo,
                                 i.nombre,

                                 i.estado,
                                 ip.cantidad,

                                 tengo = i.cantidad,
                                 minimo = i.cantidad_min
                             }).ToList();
                // VALIDA
                foreach (var item in reseta)
                {
                    if ((item.cantidad * IP.cantidad >= item.tengo - item.minimo) || item.estado == "delet")
                    {
                        return Ok(new { status = "ok", msg = "Insumos insuficientes :[" });
                    }
                }
                // COCINA
                // RESTAR INSUMOS
                foreach (var item in reseta)
                {
                    var I = _context.tbl_insumos.FirstOrDefault(x => x.id_insumo == item.id_insumo);
                    if (I.perecedero == true)
                    {
                        var lotes = (from i in _context.tbl_insumos
                                      join p in _context.tbl_perecedero on i.id_insumo equals p.fk_id_insumo
                                      where i.id_insumo == I.id_insumo && p.cantidad != 0
                                      orderby p.fecha ascending
                                      select p).ToList();
                        var seOcupan =  item.cantidad* IP.cantidad;
                        foreach (var lote in lotes)
                        {
                            seOcupan -= lote.cantidad;
                            if (seOcupan == 0) { lote.cantidad = 0; break; }
                            if (seOcupan > 0) { lote.cantidad = 0; }
                            if (seOcupan < 0) { lote.cantidad = seOcupan * -1; break; }
                            _context.SaveChanges();
                        }
                        I.cantidad = (from p in _context.tbl_perecedero where p.fk_id_insumo == item.id_insumo select p.cantidad).Sum();
                        _context.SaveChanges();
                    }
                    else
                    {
                        I.cantidad -= item.cantidad * IP.cantidad;
                        _context.SaveChanges();
                    }
                }
                //SUMAR PRODUCTOS
                var P = _context.tbl_productos.FirstOrDefault(x => x.id_producto == IP.id);
                P.cantidad += IP.cantidad;
                _context.SaveChanges();
                return Ok(new { status = "ok", msg = "Producto cocinado :]" });
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        //....................................................................................
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Productos P)
        {
            try
            {
                if (P.id_producto == id)
                {
                    //var old_P = (from p in _context.tbl_productos where p.id_producto == id select p).Take(1);
                    var old_data = _context.tbl_productos.Find(id);
                    old_data.nombre = P.nombre;
                    old_data.precio = P.precio;
                    old_data.cantidad = P.cantidad;
                    old_data.cantidad_min = P.cantidad_min;
                    old_data.descripcion = P.descripcion;
                    old_data.estado = P.estado;
                    old_data.pendientes = P.pendientes;

                    if (P.fotos.Count() != 0)
                    {
                        string img = "[";
                        int index = P.fotos.Count();
                        foreach (Foto foto in P.fotos)
                        {
                            string nombre = P.nombre + " - " + Guid.NewGuid().ToString().Substring(1, 4) + ".jpg";
                            img += "'" + nombre + "'" + (index != 1 ? "," : "");
                            var rutaCompleta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Img", nombre);
                            System.IO.File.WriteAllBytes(rutaCompleta, bytes: foto.file64Foto);
                            index--;
                        }
                        img += "]";
                        old_data.imgJson = img;
                    }
                    _context.SaveChanges();
                    return Ok(new { status = "ok", msg = "Todo bien :)" });
                }
                else
                {
                    return BadRequest(new { status = "no", msg = "Las id de la url y la del objeto JSON no son iguales :(" });
                }
            }
            catch (Exception ex)
            {
                //return BadRequest(new { status = "no", msg = "Operacion rechazada :(" });
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var P = _context.tbl_productos.FirstOrDefault(x => x.id_producto == id);
                if (P != null)
                {
                    P.estado = "descontinuado";
                    //_context.Remove(P);
                    _context.SaveChanges();
                    return Ok(new { status = "ok", msg = "Todo bien :)" });
                }
                else
                {
                    return BadRequest(new { status = "no", msg = "No se pudo eliminar :(" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "no", msg = "Operación rechazada :(" });
                //return BadRequest(ex.Message);
            }
        }


    }
}
