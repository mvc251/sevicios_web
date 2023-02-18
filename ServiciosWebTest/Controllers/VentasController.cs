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
    public class VentasController : ApiController
    {
        private GestorVentasEntities1 db = new GestorVentasEntities1();

        // GET: api/Ventas
        public List<VentaDto> GetCategoria()
        {
            var query = from v in db.Venta
                        select v;
            //1.- Crear lista de articulos vacia
            var ventas = new List<VentaDto>();
            foreach (var q in query)
            {
                //2.- Objeto de tipo articuloDto
                VentaDto vent = new VentaDto();
                vent.idVenta=q.idVenta;
                vent.idCliente=q.idCliente;
                vent.idUsuario=q.idUsuario;
                vent.tipo_comprobante=q.tipo_comprobante;
                vent.serie_comprobante = q.serie_comprobante;
                vent.num_comprobante=q.num_comprobante;
                vent.fecha_hora=q.fecha_hora;
                vent.impuesto=q.impuesto;
                vent.total=q.total;
                vent.estado=q.estado;
                //3.- Paso a la lista de articulos
                ventas.Add(vent);
            }
            return ventas;
        }

        /////////
        [HttpGet]
        [Route("api/Venta/all")]
        public List<VentaDto> GetAll()
        {
            var query = from v in db.Venta
                        join dv in db.detalle_venta
                        on v.idVenta equals dv.idVenta
                        select v;
            List<VentaDto> ventas = new List<VentaDto>();
            foreach (var q in query)
            {
                VentaDto venta = new VentaDto();
                venta.idVenta = q.idVenta;
                venta.idCliente = q.idCliente;
                venta.idUsuario= q.idUsuario;
                venta.tipo_comprobante = q.tipo_comprobante;
                venta.serie_comprobante=q.serie_comprobante;
                venta.num_comprobante=q.num_comprobante;
                venta.fecha_hora= q.fecha_hora;
                venta.impuesto= q.impuesto;
                venta.total=q.total;
                venta.estado = q.estado;
                venta.detalleVentaDto=q.detalle_venta
                    .Where(p => p.idVenta == q.idVenta)
                    .Select(p => new detalleVentaDto
                    {
                        detalle_venta1=p.detalle_venta1,
                        idVenta=p.idVenta,
                        idArticulo=p.idArticulo,
                        cantidad=p.cantidad,
                        precio=p.precio,
                        descuento=p.descuento,
                    }).ToList();
                ventas.Add(venta);
            }
            return ventas;
        }

        // GET: api/Ventas/5
        [ResponseType(typeof(Venta))]
        public IHttpActionResult GetVenta(int id)
        {
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return NotFound();
            }

            return Ok(venta);
        }

        // PUT: api/Ventas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVenta(int id, Venta venta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != venta.idVenta)
            {
                return BadRequest();
            }

            db.Entry(venta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentaExists(id))
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

        //ventasSum
        [HttpGet]
        [Route("api/ventas/sumar")]
        public decimal GetStockTotal()
        {
            var query = (from m in db.Venta
                         select m).Sum(p => p.total);
            return query;
        }

        // POST: api/Ventas
        [HttpPost]
        [Route("api/ventas/guardar")]
        [ResponseType(typeof(Venta))]
        public IHttpActionResult PostVenta(VentasDetalleVentas ventaDetaDto)
        {
            decimal totolc = 1;
            var query = from a in db.Articulo
                        where a.idArticulo == ventaDetaDto.idArticulo
                        select a;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var i in query)
            {
                totolc = (i.Precio_venta * ventaDetaDto.cantidad);
            }
            detalle_venta dtv = new detalle_venta();

            Venta vt=new Venta();
            vt.idCliente=ventaDetaDto.idCliente;
            vt.idUsuario=ventaDetaDto.idUsuario;
            vt.tipo_comprobante = ventaDetaDto.tipo_comprobante;
            vt.serie_comprobante = ventaDetaDto.serie_comprobante.ToString();
            vt.num_comprobante = ventaDetaDto.num_comprobante;
            vt.fecha_hora=ventaDetaDto.fecha_hora;
            /////////////////////////////
            vt.impuesto=totolc*decimal.Parse("0.16");
            vt.total=(totolc-((totolc*ventaDetaDto.descuento)/100));
            ////////////////////////////
            vt.estado=ventaDetaDto.estado;

            db.Venta.Add(vt);
            db.SaveChanges();

            // detalle ventas 
            dtv.idVenta=vt.idVenta; 
            dtv.idArticulo = ventaDetaDto.idArticulo;
            dtv.cantidad=ventaDetaDto.cantidad;
            ///////////////////////
            dtv.precio=vt.total;
            ////////////////////////
            dtv.descuento = ventaDetaDto.descuento;
            db.detalle_venta.Add(dtv);
            db.SaveChanges();


            return Ok("Se guardo correctamente");
        }

        //consulta 
        [HttpGet]
        [Route("api/ventas/date")]
        public List<ventas_articuloDto> GetAll([FromUri]String fecha)
        {
            //var query = from v in db.Venta
            //            select v;
            //var query1 = from dt in db.detalle_venta
            //             select dt;
            //var query2 = from a in db.Articulo
            //             select a;
            //////////////
            var query = from v in db.Venta
                        join dt in db.detalle_venta
                        on v.idVenta equals dt.idVenta
                        join a in db.Articulo
                        on dt.idArticulo equals a.idArticulo
                        select new {v.idVenta,v.idCliente,v.idUsuario,v.fecha_hora,v.impuesto,v.total,v.estado,v.detalle_venta,a.Nombre};

            List< ventas_articuloDto > ventas = new List<ventas_articuloDto>();
            foreach (var q in query)
                {
                string fechaC =q.fecha_hora.ToShortDateString();
                if (fecha == fechaC)
                {
                    ventas_articuloDto venta = new ventas_articuloDto();
                    venta.idVenta = q.idVenta;
                    venta.idCliente = q.idCliente;
                    venta.idUsuario = q.idUsuario;
                    venta.fecha_hora = q.fecha_hora;
                    venta.impuesto = q.impuesto;
                    venta.total = q.total;
                    venta.estado = q.estado;
                    venta.detalleVentaDto = q.detalle_venta
                        .Where(e => e.idVenta == q.idVenta)
                        .Select(e => new detalleVentaDto
                        {
                            detalle_venta1= e.detalle_venta1,
                            idVenta=e.idVenta,
                            idArticulo= e.idArticulo,
                            cantidad= e.cantidad,
                            precio= e.precio,
                            descuento= e.descuento,

                        }).ToList();
                        venta.Nombre= q.Nombre;
                    ventas.Add(venta);
                }
                }
            return ventas;
        }

        // DELETE: api/Ventas/5
        [ResponseType(typeof(Venta))]
        public IHttpActionResult DeleteVenta(int id)
        {
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return NotFound();
            }

            db.Venta.Remove(venta);
            db.SaveChanges();

            return Ok(venta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VentaExists(int id)
        {
            return db.Venta.Count(e => e.idVenta == id) > 0;
        }
    }
}