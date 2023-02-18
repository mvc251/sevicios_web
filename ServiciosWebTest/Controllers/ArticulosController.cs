using ServiciosWebTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServiciosWebTest.Dto;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Web.Http.Description;
using System.Collections;

namespace ServiciosWebTest.Controllers
{
    public class ArticulosController : ApiController
    {
        private GestorVentasEntities1 db = new GestorVentasEntities1();

        ////GET:api/Articulos
        //public List<ArticuloDto> GetArticulos()
        //{
        //    var query = from c in db.Articulo
        //                select c;
        //    //1.- Crear lista de articulos vacia
        //    var articulos = new List<ArticuloDto>();
        //    foreach(var q in query)
        //    {
        //        //2.- Objeto de tipo articuloDto
        //        ArticuloDto dto = new ArticuloDto();
        //        dto.idArticulo = q.idArticulo;
        //        dto.Codigo = q.Codigo;
        //        dto.Condicion = q.Condicion.Value;
        //        dto.Descripcion = q.Descripcion;
        //        dto.Stock = q.Stock;
        //        dto.Nombre = q.Nombre;
        //        dto.Precio_venta = q.Precio_venta;
        //        //3.- Paso a la lista de articulos
        //        articulos.Add(dto);
        //    }
        //    return articulos;
        //}
        [HttpGet]
        public List<ArticuloDto> GetAll(int id)
        {
            var query = from a in db.Articulo
                        join c in db.Categoria
                        on a.idCategoria equals c.Id
                        where a.idArticulo == id
                        select a;
            //1.- Crear la lista de articulos vacia
            var articulos = new List<ArticuloDto>();
            foreach (var q in query)
            {
                //2.- Esto es un objeto de tipo articuloDto
                ArticuloDto dto = new ArticuloDto();
                dto.idArticulo = q.idArticulo;
                dto.Codigo = q.Codigo;
                dto.Condicion = q.Condicion.Value;
                dto.Descripcion = q.Descripcion;
                dto.Stock = q.Stock;
                dto.Nombre = q.Nombre;
                dto.Precio_venta = q.Precio_venta;
                //3.- Agregar la entidad categoria
                dto.idCategoria = q.idCategoria;
                //4.- Se extraen los valores desde la entidad categoria
                dto.CategoriaDto = new CategoriaDto
                {
                    Id = q.Categoria.Id,
                    Nombre = q.Categoria.Nombre,
                    Condicion = q.Condicion.Value,
                    Descripcion = q.Categoria.Descripcion
                };
                //5.- Paso a la lista de articulos
                articulos.Add(dto);
            }
            return articulos;
        }
        [HttpGet]
        [Route("api/Articulo/all")]
        public List<ArticuloDto> GetAll()
        {
            var query = from a in db.Articulo
                        join c in db.Categoria
                        on a.idCategoria equals c.Id
                        select a;
            //1.- Crear la lista de articulos vacia
            var articulos = new List<ArticuloDto>();
            foreach (var q in query)
            {
                //2.- Esto es un objeto de tipo articuloDto
                ArticuloDto dto = new ArticuloDto();
                dto.idArticulo = q.idArticulo;
                dto.Codigo = q.Codigo;
                dto.Condicion = q.Condicion.Value;
                dto.Descripcion = q.Descripcion;
                dto.Stock = q.Stock;
                dto.Nombre = q.Nombre;
                dto.Precio_venta = q.Precio_venta;
                //3.- Agregar la entidad categoria
                dto.idCategoria = q.idCategoria;
                //4.- Se extraen los valores desde la entidad categoria
                dto.CategoriaDto = new CategoriaDto
                {
                    Id = q.Categoria.Id,
                    Nombre = q.Categoria.Nombre,
                    Condicion = q.Condicion.Value,
                    Descripcion = q.Categoria.Descripcion
                };
                //5.- Paso a la lista de articulos
                articulos.Add(dto);
            }
            return articulos;
        }

        //Metodo Get
        [HttpGet]
        [Route("api/Articulos/max")]
        public int GetStockMaximo()
        {
            var query = (from m in db.Articulo
                         select m).Max(p => p.Stock);
            return query;
        }

        //metodo post 
        [HttpPost]
        [Route("api/articulo/guardar")]
        public IHttpActionResult PostArticulo(ArticuloDto articuloDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Articulo art = new Articulo();
            art.idCategoria=articuloDto.idCategoria;
            art.Codigo= articuloDto.Codigo;
            art.Nombre= articuloDto.Nombre;
            art.Precio_venta = articuloDto.Precio_venta;
            art.Stock=articuloDto.Stock;
            art.Descripcion= articuloDto.Descripcion;
            art.Condicion = articuloDto.Condicion;

            db.Articulo.Add(art);
            db.SaveChanges();

            return Ok("Se guardo Correctamente");
        }

        //metod delete
        // DELETE: api/Ventas/5
        [HttpDelete]
        [ResponseType(typeof(Articulo))]
        public IHttpActionResult DeleteArticulo(int id)
        {
            Articulo articulo = db.Articulo.Find(id);
            if (articulo == null)
            {
                return NotFound();
            }

            db.Articulo.Remove(articulo);
            db.SaveChanges();

            return Ok(articulo);
        }

        [HttpGet]
        [Route("api/Articulos/min")]
        public int GetStockMinimo()
        {
            var query = (from m in db.Articulo
                         select m).Min(p => p.Stock);
            return query;
        }

        [HttpGet]
        [Route("api/Articulos/sumar")]
        public int GetStockTotal()
        {
            var query = (from m in db.Articulo
                         select m).Sum(p => p.Stock);
            return query;
        }

        [HttpGet]
        [Route("api/Articulos/totalporcategoria")]
        public int GetTotalArticulosPorCategoria(int id)
        {
            var query = (from m in db.Articulo
                         select m).Count(p => p.idCategoria == id);
            return query;
        }

        [HttpGet]
        [Route("api/Articulos/agrupacioncategoria")]
        public IQueryable GetAgruparArticulosCategoria()
        {
            var query = (from a in db.Articulo
                         group a by a.idCategoria into articuloGroup
                         select new
                         {
                             Categoria = articuloGroup.Key,
                             Nombre = articuloGroup.Select(p => p.Nombre),
                             Total = articuloGroup.Count(),
                             Suma = articuloGroup.Sum(p => p.Precio_venta)
                         }).AsQueryable();
            return query;
        }

        [HttpPut]
        public IHttpActionResult Put(int id, ArticuloDto articuloDto)
        {
             
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != articuloDto.idArticulo)
            {
                return BadRequest();
            }
            Articulo art = new Articulo();
            art.idArticulo = articuloDto.idArticulo;
            art.idCategoria = articuloDto.idCategoria;
            art.Codigo= articuloDto.Codigo;
            art.Nombre= articuloDto.Nombre;
            art.Precio_venta=articuloDto.Precio_venta;
            art.Stock= articuloDto.Stock;
            art.Descripcion = articuloDto.Descripcion;
            art.Condicion = articuloDto.Condicion;

            db.Articulo.Add(art);  
            db.Entry(art).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!articuloExist(id))
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

        private bool articuloExist(int id)
        {
            return db.Articulo.Count(e => e.idArticulo == id) > 0;
        }
    }
}
