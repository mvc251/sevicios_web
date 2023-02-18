using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServiciosWebTest.Data;

namespace ServiciosWebTest.Controllers
{
    //[Route("api/[controller]")]
   
    public class TestController : ApiController
    {

        private readonly GestorVentasEntities1 database;
        [HttpGet]
        public IHttpActionResult Get()
        {
            var database = new Data.GestorVentasEntities1();
            var categorias = database.Categoria.ToList();
            return Ok(categorias);
        }

    }
}
