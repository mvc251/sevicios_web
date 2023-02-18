using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ServiciosWebTest.Data;
using ServiciosWebTest.Dto;

namespace ServiciosWebTest.Controllers
{
    public class CategoriasController : ApiController
    {
        private GestorVentasEntities1 db = new GestorVentasEntities1();
        //crea un objeto que apunta  a la base de datos

        [HttpGet]
        public List<CategoriaDto> GetId(int id)
        {
            var query = from c in db.Categoria
                        join a in db.Articulo
                        on c.Id equals a.idCategoria
                        where c.Id == id
                        select c;
            //1.- Crear una lista Categoria
            List<CategoriaDto> categorias = new List<CategoriaDto>();
            foreach (var q in query)
            {
                CategoriaDto categoria = new CategoriaDto();
                categoria.Id = q.Id;
                categoria.Nombre = q.Nombre;
                categoria.Descripcion = q.Descripcion;
                categoria.Condicion = q.Condicion.Value;
                categoria.ArticulosDto = q.Articulo
                    .Where(p => p.idCategoria == q.Id)
                    .Select(p => new ArticuloDto
                    {
                        Nombre = p.Nombre,
                        Codigo = p.Codigo,
                        Condicion = p.Condicion.Value,
                        Descripcion = p.Descripcion,
                        Precio_venta = p.Precio_venta,
                        idArticulo = p.idArticulo,
                        idCategoria= p.idCategoria,
                        Stock = p.Stock,
                    }).ToList();
                categorias.Add(categoria);
            }
            return categorias;
        }

        [HttpGet]
        [Route("api/Categorias/all")]
        public List<CategoriaDto> GetAll()
        {
            var query = from c in db.Categoria
                        join a in db.Articulo
                        on c.Id equals a.idCategoria
                        select c;
            //1.- Crear una lista Categoria
            List<CategoriaDto> categorias = new List<CategoriaDto>();
            foreach (var q in query)
            {
                CategoriaDto categoria = new CategoriaDto();
                categoria.Id = q.Id;
                categoria.Nombre = q.Nombre;
                categoria.Descripcion = q.Descripcion;
                categoria.Condicion = q.Condicion.Value;
                categoria.ArticulosDto = q.Articulo
                    .Where(p => p.idCategoria == q.Id)
                    .Select(p => new ArticuloDto
                    {
                        Nombre = p.Nombre,
                        Codigo = p.Codigo,
                        Condicion = p.Condicion.Value,
                        Descripcion = p.Descripcion,
                        Precio_venta = p.Precio_venta,
                        idArticulo = p.idArticulo,
                        Stock = p.Stock,
                    }).ToList();
                categorias.Add(categoria);
            }
            return categorias;
        }

        // GET: api/Categorias/5
        //[ResponseType(typeof(Categoria))]
        //public IHttpActionResult GetCategoria(int id)
        //{
        //  Categoria categoria = db.Categoria.Find(id);
        //  if (categoria == null)
        //{
        //  return NotFound();
        //}

        //return Ok(categoria);
        //}


        // PUT: api/Categorias/5
        [HttpPut]
        public IHttpActionResult PutCategoria(int id, CategoriaDto categoriaDto)
        {
             
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoriaDto.Id)
            {
                return BadRequest();
            }
            Categoria cta = new Categoria();
            cta.Id = categoriaDto.Id;
            cta.Condicion = categoriaDto.Condicion;
            cta.Nombre = categoriaDto.Nombre;
            cta.Descripcion = categoriaDto.Descripcion;

            db.Categoria.Add(cta);  
            db.Entry(cta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Se guardó correctamente" + HttpStatusCode.OK);
           // return StatusCode(HttpStatusCode.NoContent);
        }

        //// POST: api/Categorias
        //[ResponseType(typeof(Categoria))]
        [HttpPost]
        [Route("api/Categoria/add")]
        public IHttpActionResult PostCategoria(CategoriaDto categoriaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Categoria categoria= new Categoria();
            categoria.Nombre= categoriaDto.Nombre;
            categoria.Descripcion= categoriaDto.Descripcion.ToString();
            categoria.Condicion = categoriaDto.Condicion;
            db.Categoria.Add(categoria);
            db.SaveChanges();

            return Ok("los datos se guardaron correctamente");
            //return CreatedAtRoute("DefaultApi", new { id = categoria.Id }, categoria);
        }


        
        // DELETE: api/Categorias/5
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult DeleteCategoria(int id)
        {
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return NotFound();
            }

            db.Categoria.Remove(categoria);
            db.SaveChanges();

            return Ok(categoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriaExists(int id)
        {
            return db.Categoria.Count(e => e.Id == id) > 0;
        }
    }

    
}