using IDGS904_API.Context;
using IDGS904_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi.Any;
using System;
using System.IO.Pipes;
using static IDGS904_API.Controllers.LoginController;



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
    public class proveedoresController : ControllerBase
    {
        private readonly AppDbContext _context;
        public proveedoresController(AppDbContext context) { _context = context; }
        //....................................................................................
        [HttpGet]
        public ActionResult Get()
        {
            try{
                return Ok((from p in _context.tbl_proveedores select p).ToList());
                //return Ok((from p in _context.tbl_productos select p).Take(4).ToList());
            }
            catch (Exception ex){return BadRequest(ex.Message);}
        }
        //....................................................................................
        [HttpPost("crearProveedor")]
        public ActionResult crearProveedor([FromBody] Proveedores P)
        {
            try
            {
                _context.tbl_proveedores.Add(P);
                _context.SaveChanges();
                return Ok(new { status = "ok", msg = "Todo bien :)" });
            }
            catch (Exception ex){return BadRequest(ex.Message);}
        }
        //....................................................................................
        [HttpPost("editarProveedor")]
        public ActionResult editarProveedor([FromBody] Proveedores P)
        {
            try
            {
               var prov = from p in _context.tbl_proveedores where p.id_proveedor == P.id_proveedor select p;
                if (prov == null)
                { return Ok(new { status = "no", msg = "Ocurrió un error al editar sus datos (resise su id) :[" }); }
                else
                {
                    _context.Entry(P).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok(P);
                }
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }



    }
}
