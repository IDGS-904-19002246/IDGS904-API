using IDGS904_API.Context;
using IDGS904_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi.Any;
using System;
using System.IO.Pipes;

//var result = dbContext.tabla1
//    .Join(
//        dbContext.tabla2,
//        t1 => t1.columna_comun,
//        t2 => t2.columna_comun,
//        (t1, t2) => new { Tabla1 = t1, Tabla2 = t2 }
//    ).ToList();


//var envios = from t1 in _context.tbl_ventas
//             join t2 in _context.tbl_usuarios on t1.fk_id_usuario equals t2.id_usuario
//             where t2.id_usuario == id
//             select new
//             {
//                 venta = t1.id_venta,
//                 nombre = t2.nombre,
//                 fecha = t1.fecha_compra
//             };

namespace IDGS904_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ventasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public class ventaXproducto
        {
            public Ventas venta { get; set; }
            public List<venta_producto> venta_producto { get; set; }
        }


        public ventasController(AppDbContext context){_context = context;}

        [HttpGet]//api/<Grupos>
        public ActionResult index()
        {
            try { return Ok(_context.tbl_ventas.ToList()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpGet("u/{id}", Name = "ventasXusuario")]
        public ActionResult ventasXusuario(int id)
        {
            try
            {
                var ventasXusuario = from v in _context.tbl_ventas
                                     join vp in _context.tbl_venta_producto on v.id_venta equals vp.fk_id_venta
                                     join p in _context.tbl_productos on vp.fk_id_producto equals p.id_producto
                                     join u in _context.tbl_usuarios on v.fk_id_usuario equals u.id_usuario
                                     where u.id_usuario == id
                                     select new
                                     {

                                         u_nombre = u.nombre,
                                         v.fecha_compra,
                                         v.status,

                                         p.nombre,
                                         vp.cantidad,
                                         vp.precio,
                                         p.descripcion
                                     };
                var listaNombres = ventasXusuario
                    .GroupBy(v => new { v.fecha_compra, v.u_nombre, v.status })
                    .Select(
                    g => new venta_productos
                    {
                        fecha = g.Key.fecha_compra,
                        u_nombre = g.Key.u_nombre,
                        status = g.Key.status,

                        lista = g.Select(r => new producto
                        {
                            p_nombre = r.nombre,
                            cantidad = r.cantidad,
                            precio = r.precio,
                            descripcion = r.descripcion
                        }).ToList()
                    }).ToList();


                return Ok(listaNombres);
            }
            catch (Exception ex){return BadRequest(ex.Message);}
        }
        [HttpGet("v/{id_venta}", Name = "venta")]
        public ActionResult venta(int id_venta)
        {
            try
            {
                var venta = from v in _context.tbl_ventas
                            join vp in _context.tbl_venta_producto on v.id_venta equals vp.fk_id_venta
                            join p in _context.tbl_productos on vp.fk_id_producto equals p.id_producto
                            join u in _context.tbl_usuarios on v.fk_id_usuario equals u.id_usuario
                            where v.id_venta == id_venta
                            select new
                            {
                                usuario=u.nombre,
                                v.fecha_compra,
                                v.status,
                                p.nombre,
                                vp.cantidad,
                                vp.precio,
                                p.descripcion
                            };
                                 
                return Ok(venta);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("{ano}/{mes}", Name = "ventasXmes")]
        public ActionResult ventasXmes(int mes, int ano)
        {
            if (mes >= 13 || mes <= 0 || ano >= 2024)
            {
                return Ok(new { status = "no", msg = "Error al consultar, la fecha no es correcta." });
            }
            try
            {
                var ventasXmes = from vp in _context.tbl_venta_producto
                    join v in _context.tbl_ventas on vp.fk_id_venta equals v.id_venta
                    join u in _context.tbl_usuarios on v.fk_id_usuario equals u.id_usuario
                    join p in _context.tbl_productos on vp.fk_id_producto equals p.id_producto
                    where
                        v.fecha_compra.Year == ano &&
                        v.fecha_compra.Month == mes
                    select new {
                        v.id_venta,
                        usuario=u.nombre,
                        v.fecha_compra,
                        p.nombre,
                        vp.cantidad,
                        vp.precio
                    };
                return Ok(ventasXmes);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("status/{status}", Name = "ventasEstatus")]
        public ActionResult ventasEstatus(string status)
        {
            try
            {
                return Ok(from v in _context.tbl_ventas where v.status == status select v);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        //.......................................................................................
        [HttpPost("insertarVenta")]
        public IActionResult insertarVenta(ventaXproducto datos)
        {
            try
            {
                Ventas venta = datos.venta;
                List<venta_producto> lista = datos.venta_producto;

                _context.tbl_ventas.Add(venta);
                _context.SaveChanges();

                foreach (venta_producto item in lista)
                {
                    item.fk_id_venta = venta.id_venta;
                    _context.tbl_venta_producto.Add(item);
                    _context.SaveChanges();
                }

                return Ok(new { status = "ok", msg = "La venta se registro correctamente :]" });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "no", msg = "La venta no se pudo concretar :[" });
            }
        }

        [HttpPost("actualizarStatus")]
        public ActionResult actualizarStatus([FromBody] Ventas new_status)
        {
            try
            {
                var P = _context.tbl_ventas.FirstOrDefault(x => x.id_venta == new_status.id_venta);
                if (P != null)
                {
                    P.status = new_status.status;
                    //_context.Remove(P);
                    _context.SaveChanges();
                    return Ok(new { status = "ok", msg = "Todo bien :]" });
                }
                else {
                    return Ok(new { status = "no", msg = "Id de la venta no fue encontrada :[" });
                }
                //List<Ventas> ven = (List<Ventas>)(
                //    from v in _context.tbl_ventas
                //    where v.id_venta == new_status.id_venta
                //    select v
                //    );
                //ven[0].status = new_status.status;
                //return Ok(new { status = "ok", msg = "Todo bien :)" });
            }
            catch (Exception ex){return BadRequest(ex.Message);}
        }


        //[HttpPut("{id}")]
        //public ActionResult Put(int id, [FromBody] Productos P)
        //{
        //    try
        //    {
        //        if (P.id_producto == id)
        //        {
        //            _context.Entry(P).State = EntityState.Modified;
        //            _context.SaveChanges();
        //            return Ok(new { status = "ok", msg = "Todo bien :)" });
        //        }
        //        else
        //        {
        //            return BadRequest(new { status = "no", msg = "Las id de la url y la del objeto JSON no son iguales :(" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { status = "no", msg = "Operacion rechazada :(" });
        //        //return BadRequest(ex.Message);
        //    }
        //}
        //[HttpDelete("{id}")]
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        var P = _context.tbl_productos.FirstOrDefault(x => x.id_producto == id);
        //        if (P != null)
        //        {
        //            _context.Remove(P);
        //            _context.SaveChanges();
        //            return Ok(new { status = "ok", msg = "Todo bien :)" });
        //        }
        //        else
        //        {
        //            return BadRequest(new { status = "no", msg = "No se pudo eliminar :(" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { status = "no", msg = "Operación rechazada :(" });
        //        //return BadRequest(ex.Message);
        //    }
        //}



    }
}
