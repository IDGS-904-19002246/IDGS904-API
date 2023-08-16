using IDGS904_API.Context;
using IDGS904_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi.Any;
using System;
using System.IO.Pipes;  


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
    public class usuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public usuariosController(AppDbContext context) { _context = context; }
        //....................................................................................
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginData L)
        {
            try
            {
                var U = (from u in _context.tbl_usuarios where
                        u.correo == L.user &&
                        u.contrasena == L.pass
                        //&& u.rol != "baned"
                        //&& u.rol != "delet"
                        select u).FirstOrDefault();
                if (U == null)
                { return Ok(); }
                else
                { return Ok(U); }
            }
            catch (Exception ex)
            {
                //return Ok(new { status = "no", msg = "El correo no esta registrado o la contraseña es incorrecta :[" });
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("crearUsuario")]
        public ActionResult crearUsuario([FromBody] Usuarios U)
        {
            try
            {
                var P = _context.tbl_usuarios.FirstOrDefault(x => x.id_usuario == U.id_usuario);
                if ( P == null)
                {
                    _context.tbl_usuarios.Add(U);
                    _context.SaveChanges();
                    return Ok(U);
                }
                else{
                    return Ok();
                }

                
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPost("editarUsuario")]
        public ActionResult editarUsuario([FromBody] Usuarios U)
        {
            try
            {
                var user = from u in _context.tbl_usuarios where u.id_usuario == U.id_usuario select u;
                if (user == null)
                { return Ok(new { status = "no", msg = "Ocurrió un error al editar sus datos (resise su id) :[" }); }
                else
                {
                    _context.Entry(U).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok(U); 
                }
            }
            catch (Exception ex){return BadRequest(ex.Message);}
        }
        //....................................................................................

        [HttpDelete("{id}/{status}")]
        public ActionResult Delete(int id, string status)
        {
            try
            {
                var P = _context.tbl_usuarios.FirstOrDefault(x => x.id_usuario == id);
                if (P != null)
                {
                    P.rol = status;
                    _context.SaveChanges();
                    return Ok(new { status = "ok", msg = "Todo bien :)" });
                }
                else{return BadRequest(new { status = "no", msg = "No se pudo eliminar :(" });}
            }
            catch (Exception ex){return BadRequest(ex.Message);}
        }





        //[HttpGet]//api/<Grupos>
        //public ActionResult index()
        //{
        //    try { return Ok(_context.tbl_ventas.ToList()); }
        //    catch (Exception ex) { return BadRequest(ex.Message); }
        //}
        //[HttpGet("u/{id}", Name = "ventasXusuario")]
        //public ActionResult ventasXusuario(int id)
        //{
        //    try
        //    {
        //        var ventasXusuario = from v in _context.tbl_ventas where v.fk_id_usuario == id select v;
        //        return Ok(ventasXusuario);
        //    }
        //    catch (Exception ex){return BadRequest(ex.Message);}
        //}
        //[HttpGet("v/{id_venta}", Name = "venta")]
        //public ActionResult venta(int id_venta)
        //{
        //    try
        //    {
        //        var venta = from v in _context.tbl_ventas
        //                    join vp in _context.tbl_venta_producto on v.id_venta equals vp.fk_id_venta
        //                    join p in _context.tbl_productos on vp.fk_id_producto equals p.id_producto

        //                    where v.id_venta == id_venta
        //                    select new
        //                    {
        //                        v.fecha_compra,
        //                        v.status,
        //                        vp.cantidad,
        //                        p.nombre,
        //                        vp.precio,
        //                        p.descripcion
        //                    };

        //        return Ok(venta);
        //    }
        //    catch (Exception ex) { return BadRequest(ex.Message); }
        //}

        //[HttpGet("{ano}/{mes}", Name = "ventasXmes")]
        //public ActionResult ventasXmes(int mes, int ano)
        //{
        //    if (mes >= 13 || mes <= 0 || ano >= 2024)
        //    {
        //        return Ok(new { status = "no", msg = "Error al consultar, la fecha no es correcta." });
        //    }
        //    try
        //    {
        //        DateTime fecha = new DateTime(ano,mes,1);
        //        var ventasXmes = from v in _context.tbl_ventas
        //                         where
        //                            v.fecha_compra.Year == ano &&
        //                            v.fecha_compra.Month == mes
        //                         select v;
        //        return Ok(ventasXmes);
        //    }
        //    catch (Exception ex) { return BadRequest(ex.Message); }
        //}

        //[HttpGet("status/{status}", Name = "ventasEstatus")]
        //public ActionResult ventasEstatus(string status)
        //{
        //    try
        //    {
        //        return Ok(from v in _context.tbl_ventas where v.status == status select v);
        //    }
        //    catch (Exception ex) { return BadRequest(ex.Message); }
        //}
        ////.......................................................................................
        //[HttpPost("insertarVenta")]
        //public IActionResult insertarVenta(ventaXproducto datos)
        //{
        //    try
        //    {
        //        Ventas venta = datos.venta;
        //        List<venta_producto> lista = datos.venta_producto;

        //        _context.tbl_ventas.Add(venta);
        //        _context.SaveChanges();

        //        foreach (venta_producto item in lista)
        //        {
        //            item.fk_id_venta = venta.id_venta;
        //            _context.tbl_venta_producto.Add(item);
        //            _context.SaveChanges();
        //        }

        //        return Ok(new { status = "ok", msg = "La venta se registro correctamente :]" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new { status = "no", msg = "La venta no se pudo concretar :[" });
        //    }
        //}

        //[HttpPost("actualizarStatus")]
        //public ActionResult actualizarStatus([FromBody] Ventas new_status)
        //{
        //    try
        //    {
        //        var P = _context.tbl_ventas.FirstOrDefault(x => x.id_venta == new_status.id_venta);
        //        if (P != null)
        //        {
        //            P.status = new_status.status;
        //            //_context.Remove(P);
        //            _context.SaveChanges();
        //            return Ok(new { status = "ok", msg = "Todo bien :]" });
        //        }
        //        else {
        //            return Ok(new { status = "no", msg = "Id de la venta no fue encontrada :[" });
        //        }
        //        //List<Ventas> ven = (List<Ventas>)(
        //        //    from v in _context.tbl_ventas
        //        //    where v.id_venta == new_status.id_venta
        //        //    select v
        //        //    );
        //        //ven[0].status = new_status.status;
        //        //return Ok(new { status = "ok", msg = "Todo bien :)" });
        //    }
        //    catch (Exception ex){return BadRequest(ex.Message);}
        //}


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




    }
}
