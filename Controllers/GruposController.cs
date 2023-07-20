//using IDGS904_API.Context;
//using IDGS904_API.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace IDGS904_API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class GruposController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public GruposController(AppDbContext context)
//        {
//            _context = context;
//        }
//        [HttpGet]//api/<Grupos>
//        public ActionResult Get()
//        {
//            try
//            {
//                return Ok(_context.Alumnos.ToList());
//            }
//            catch (Exception ex)
//            {

//                return BadRequest(ex.Message);
//            }
//        }
//        [HttpGet("{id}", Name = "Alumnos")]
//        public ActionResult Get(int id)
//        {
//            try
//            {
//                var alum = _context.Alumnos.FirstOrDefault(x => x.Id == id);
//                return Ok(alum);
//            }
//            catch (Exception ex)
//            {

//                return BadRequest(ex.Message);
//            }
//        }
//        [HttpPost]
//        public ActionResult<Alumnos> Post([FromBody] Alumnos alum)
//        {
//            try
//            {
//                _context.Alumnos.Add(alum);

//                _context.SaveChanges();
//                return CreatedAtRoute("Alumnos", new { id = alum.Id }, alum);
//            }
//            catch (Exception ex)
//            {

//                return BadRequest(ex.Message);
//            }
//        }
//        [HttpPut("{id}")]
//        public ActionResult Put(int id, [FromBody] Alumnos alum)
//        {
//            try
//            {
//                if (alum.Id == id)
//                {
//                    _context.Entry(alum).State=EntityState.Modified;
//                    _context.SaveChanges();
//                    return CreatedAtRoute("Alumnos",new {id=alum.Id}, alum);
//                }
//                else
//                {
//                    return BadRequest();
//                }
//            }
//            catch (Exception ex)
//            {

//                return BadRequest(ex.Message);
//            }
//        }
//        [HttpDelete("{id}")]
//        public ActionResult Delete(int id)
//        {
//            try
//            {
//                var alum=_context.Alumnos.FirstOrDefault(x=>x.Id==id);    
//                if (alum!= null)
//                {
//                    _context.Remove(alum);
//                    _context.SaveChanges();
//                    return Ok(id);
//                }
//                else
//                {
//                    return BadRequest();
//                }
//            }
//            catch (Exception ex)
//            {

//                return BadRequest(ex.Message);
//            }
//        }
//    }
//}
