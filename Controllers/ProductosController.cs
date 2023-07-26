using IDGS904_API.Context;
using IDGS904_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//TABLAS
//insumo-producto
//insumo-proveedor
//insumos
//productos
//proveedores
//usuarios
//venta-Productos
//venta

namespace IDGS904_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public productosController(AppDbContext context){_context = context;}

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                //var productos = (from p in _context.tbl_productos select p).Skip(15).Take(16).ToList();

                //from t1 in _context.tbl_ventas
//             join t2 in _context.tbl_usuarios on t1.fk_id_usuario equals t2.id_usuario
//             where t2.id_usuario == id
//             select new
//             {
//                 venta = t1.id_venta,
//                 nombre = t2.nombre,
//                 fecha = t1.fecha_compra
//             };
                return Ok((from p in _context.tbl_productos select p).Take(4).ToList());
                //return Ok(_context.tbl_productos.ToList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{rango}", Name = "productos")]
        public ActionResult Get(int rango)
        {
            try
            {
                if (rango == 1 || rango <= 0) {
                    return Get();
                }
                int inicio = (rango-1) * 4;
                var productos = (from p in _context.tbl_productos select p).Skip(inicio).Take(4).ToList();
                return Ok(productos);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("insumoProducto/{ano}/{mes}", Name = "insumoProducto")]
        public ActionResult insumoProducto(int mes, int ano)
        {
            if (mes >= 13 || mes <= 0 || ano >= 2024) { return Ok(new { status = "no", msg = "Error al consultar, la fecha no es correcta." }); }
            try
            {
                var insumos_usados = from ip in _context.tbl_insumo_producto
                                     join p in _context.tbl_productos on ip.fk_id_insumo equals p.id_producto
                                     where ip.fecha.Year == ano && ip.fecha.Month == mes
                                     select ip;
                return Ok(insumos_usados);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        
        //....................................................................................

        [HttpPost("crearProducto")]
        public ActionResult<Productos> crearProducto([FromBody] Productos P)
        {
            try
            {
                _context.tbl_productos.Add(P);
                _context.SaveChanges();
                return Ok( new { status = "ok", msg = "Todo bien :)" });
            }
            catch (Exception ex){return BadRequest(ex.Message);}
        }

        [HttpPost("fabricarProducto")]
        public ActionResult fabricarProducto([FromBody] insumo_productos IP)
        {
            try
            {
                // restar insumo/sumar productos/añadir insumo_producto
                if (IP.lista.Count() == 0) { return Ok(new { status = "ok", msg = "No se seleccionaron insumos :[" }); }
                string fabricar = "ok";
                foreach (insumo_producto item in IP.lista)
                {
                    var I = _context.tbl_insumos.FirstOrDefault(x => x.id_insumo == item.fk_id_insumo);
                    if (I != null)
                    {
                        if (item.cantidad >= I.cantidad - I.cantidad_min)
                        {
                            fabricar = "no";
                            return Ok(new { status = "ok", msg = "Insumos insuficientes :[" });
                        }
                    }   
                }
                
                if (fabricar == "ok" && fabricar != "no")
                {
                    foreach (insumo_producto item in IP.lista)
                    {
                        //AÑADIR A insumo_producto
                        item.fk_id_producto = IP.id;
                        _context.tbl_insumo_producto.Add(item);
                        //RESTAR INSUMOS
                        var I = _context.tbl_insumos.FirstOrDefault(x => x.id_insumo == item.fk_id_insumo);
                        I.cantidad -= item.cantidad;
                        _context.Entry(I).State = EntityState.Modified;
                    }
                    //SUMAR PRODUCTOS
                    var P = _context.tbl_productos.FirstOrDefault(x => x.id_producto == IP.id);
                    P.cantidad += IP.cantidad;
                    _context.Entry(P).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                return Ok(new { status = "ok", msg = "Todo bien :)" });
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
                    _context.Entry(P).State = EntityState.Modified;
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
                return BadRequest(new { status = "no", msg = "Operacion rechazada :(" });
                //return BadRequest(ex.Message);
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
