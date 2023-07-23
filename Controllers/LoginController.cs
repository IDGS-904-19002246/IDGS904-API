//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
using IDGS904_API.Context;
using IDGS904_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IDGS904_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        public LoginController(AppDbContext context) { _context = context; }

        //public class LoginData
        //{
        //    public int user { get; set; }
        //    public string pass { get; set; }
        //}
        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginData L){
        //    try{
        //        var alum = _context.tbl_productos.FirstOrDefault(x => x.id_producto == L.user);
        //        return Ok(alum);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new { status = "no", msg = "El correo no esta registrado o la contraseña es incorrecta :[" });
        //        //return BadRequest(ex.Message);
        //    }
        //}
        //[HttpGet]
        //public ActionResult Get()
        //{
        //    return Ok(new { status = "ok", msg = "Login get solito :)" });
        //    //return View();
        //}
        //[HttpGet]
        //public ActionResult Login2(int id)
        //{
        //    return Ok(new { status = "ok", msg = "Login get con parametro :] ... "+id });
        //    //return View();
        //}

        //[HttpPost('post')]
        //public IActionResult<Productos> Post2([FromBody] Productos P)
        //{
        //    try
        //    {
        //        //_context.tbl_productos.Add(P);
        //        //_context.SaveChanges();
        //        //return CreatedAtRoute("tbl_productos", new { id = P.id_productos }, P);
        //        return Ok(new { status = "ok", msg = "Post 2 :]" });
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpPost("metodoUno")]
        public IActionResult MetodoUno([FromBody] Productos P)
        {
            // Lógica del método 1
            return Ok(new { status = "ok", msg = "Post 2 :]" });
        }
        [HttpPost("Post3")]
        public IActionResult Post3([FromBody] Productos P)
        {
            try
            {
                //_context.tbl_productos.Add(P);
                //_context.SaveChanges();
                //return CreatedAtRoute("tbl_productos", new { id = P.id_productos }, P);
                return Ok(new { status = "ok", msg = "Post 3 :]" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}