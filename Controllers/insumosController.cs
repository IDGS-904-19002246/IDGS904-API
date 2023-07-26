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
//ventas
//var insumos = _context.tbl_insumos.FromSqlRaw("SELECT * FROM tbl_insumos").ToList();
//return Ok(insumos);

namespace IDGS904_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class insumosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public insumosController(AppDbContext context){_context = context;}
        //.......................................................................................

        [HttpGet]//api/<Grupos>
        public ActionResult Get()
        {
            try{return Ok(_context.tbl_insumos.ToList());}
            catch (Exception ex){return BadRequest(ex.Message);}
        }
        //[HttpGet("{id}", Name = "tbl_insumos")]
        //public ActionResult Get(int id)
        //{
        //    try{return Ok(_context.tbl_insumos.FirstOrDefault(x => x.id_insumo == id));}
        //    catch (Exception ex){return BadRequest(ex.Message);}
        //}


        [HttpPost("insertarInsumo")]
        public ActionResult insertarInsumo([FromBody] Insumos I)
        {
            try
            {
                _context.tbl_insumos.Add(I);
                _context.SaveChanges();
                return Ok(new { status = "ok", msg = "Todo bien :)" });
            }
            catch (Exception ex){return Ok(new { status = "no", msg = "No se pudo añadir el insumo al inventario :[" });}
        }
        [HttpPost("editarInsumo")]
        public ActionResult editarInsumo([FromBody] Insumos I)
        {
            try
            {
                var P = _context.tbl_insumos.FirstOrDefault(x => x.id_insumo == I.id_insumo);
                if (P != null)
                {
                    P = I;
                    _context.SaveChanges();
                    return Ok(new { status = "ok", msg = "Todo bien :]" });
                }
                else
                {
                    return Ok(new { status = "no", msg = "Insumo no encontrado :[" });
                }
            }
            catch (Exception ex) { return Ok(new { status = "no", msg = "No se pudo editar el insumo del inventario :[" }); }
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



        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var I = _context.tbl_insumos.FirstOrDefault(x => x.id_insumo == id);
                if (I != null)
                {
                    I.estado = "Eliminado";
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
