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
    public class detalle_ventaController : ApiController
    {
        private GestorVentasEntities1 db = new GestorVentasEntities1();

        // GET: api/detalle_venta
        public IQueryable<detalle_venta> Getdetalle_venta()
        {
            return db.detalle_venta;
        }

        [HttpGet]
        [Route("api/DetalleVenta/all")]
        public List<detalleVentaDto> GetAll()
        {
            var query = from dv in db.detalle_venta
                        join v in db.Venta
                        on dv.idVenta equals v.idVenta
                        select v;
            //1.- Crear una lista Categoria
            List<detalleVentaDto> detalles = new List<detalleVentaDto>();
            foreach (var q in query)
            {
                detalleVentaDto detalle = new detalleVentaDto();
                foreach(var d in q.detalle_venta)
                {

                }
            }
            return detalles;
        }

        // GET: api/detalle_venta/5
        [ResponseType(typeof(detalle_venta))]
        public IHttpActionResult Getdetalle_venta(int id)
        {
            detalle_venta detalle_venta = db.detalle_venta.Find(id);
            if (detalle_venta == null)
            {
                return NotFound();
            }

            return Ok(detalle_venta);
        }

        // PUT: api/detalle_venta/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdetalle_venta(int id, detalle_venta detalle_venta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detalle_venta.detalle_venta1)
            {
                return BadRequest();
            }

            db.Entry(detalle_venta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!detalle_ventaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/detalle_venta
        //[ResponseType(typeof(detalle_venta))]
        //public IHttpActionResult Postdetalle_venta(detalleVentaDto detalleVentaDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var query = from a in db.Articulo
        //                where a.idArticulo == detalleVentaDto.idArticulo
        //                select a;

        //    detalle_venta dtv = new detalle_venta();
            
        //    if (query!=null)
        //    {
        //        foreach (var q in query)
        //        {
        //            dtv.precio = q.Precio_venta;
        //        }
        //        dtv.detalle_venta1 = detalleVentaDto.idVenta;
        //        dtv.idVenta = detalleVentaDto.idVenta;
        //        dtv.idArticulo = detalleVentaDto.idArticulo;
        //        dtv.cantidad = detalleVentaDto.cantidad;
        //        dtv.descuento = detalleVentaDto.descuento;

        //    }
        //    db.detalle_venta.Add(dtv);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = dtv.detalle_venta1 }, dtv);
        //}

        // DELETE: api/detalle_venta/5
        [ResponseType(typeof(detalle_venta))]
        public IHttpActionResult Deletedetalle_venta(int id)
        {
            detalle_venta detalle_venta = db.detalle_venta.Find(id);
            if (detalle_venta == null)
            {
                return NotFound();
            }

            db.detalle_venta.Remove(detalle_venta);
            db.SaveChanges();

            return Ok(detalle_venta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool detalle_ventaExists(int id)
        {
            return db.detalle_venta.Count(e => e.detalle_venta1 == id) > 0;
        }
    }
}