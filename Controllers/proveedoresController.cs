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
        
        [HttpGet("{ano}/{mes}", Name = "comprasProveedor")]
        public ActionResult comprasProveedor(int mes, int ano)
        {
            if (mes >= 13 || mes <= 0 || ano >= 2024){return Ok(new { status = "no", msg = "Error al consultar, la fecha no es correcta." });}
            try
            {
                var compras = from ip in _context.tbl_insumo_proveedor
                              join p in _context.tbl_proveedores on ip.fk_id_insumo equals p.id_proveedor
                              join i in _context.tbl_insumos on ip.fk_id_insumo equals i.id_insumo
                              where ip.fecha.Year == ano && ip.fecha.Month == mes
                              select new {
                                  ip.id_insumo_proveedor,
                                  proveedor= p.nombre,
                                  fecha = ip.fecha,
                                  insumo = i.nombre,
                                  cantidad = ip.cantidad,
                                  precio = ip.precio

                              };
                return Ok(compras);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
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
        [HttpPost("compraProveedor")]
        public ActionResult compraProveedor([FromBody] compra c)
        {
            try
            {
                List<insumo_proveedor> lis = c.lista;
                foreach (insumo_proveedor item in lis)
                {
                    var I = _context.tbl_insumos.FirstOrDefault(x => x.id_insumo == item.fk_id_insumo);
                    if (I != null){
                        item.fk_id_proveedor = c.id;
                        _context.tbl_insumo_proveedor.Add(item);

                        I.cantidad += item.cantidad;
                        _context.Entry(I).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                }
                return Ok(new { status = "ok", msg = "Todo bien :)" });
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


    }
}
