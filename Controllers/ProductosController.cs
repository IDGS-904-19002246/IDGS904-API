using IDGS904_API.Context;
using IDGS904_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IDGS904_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;
        

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]//api/<Grupos>
        public ActionResult Get()
        {
            try
            {
                return Ok(_context.tbl_productos.ToList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}", Name = "tbl_productos")]
        public ActionResult Get(int id)
        {
            try
            {
                var alum = _context.tbl_productos.FirstOrDefault(x => x.id_producto == id);
                return Ok(alum);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<Productos> Post([FromBody] Productos P)
        {
            try
            {
                _context.tbl_productos.Add(P);
                _context.SaveChanges();
                //return CreatedAtRoute("tbl_productos", new { id = P.id_productos }, P);
                return Ok( new { status = "ok", msg = "Todo bien :)" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


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
                    _context.Remove(P);
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
