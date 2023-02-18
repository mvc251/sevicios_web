using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWebTest.Dto
{
    public class ventas_articuloDto
    {
        //Detalle venta
        public int detalle_venta1 { get; set; }
        public int idArticulo { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal descuento { get; set; }

        public ArticuloDto ArticuloDto { get; set; }
        public VentaDto VentaDto { get; set; }

        //venta
        public int idVenta { get; set; }
        public int idCliente { get; set; }
        public int idUsuario { get; set; }
        public System.DateTime fecha_hora { get; set; }
        public decimal impuesto { get; set; }
        public decimal total { get; set; }
        public string estado { get; set; }

        public List<detalleVentaDto> detalleVentaDto { get; set; }
        public PersonaDto PersonaDto { get; set; }
        //articulo
        public string Nombre { get; set; }
    }
}