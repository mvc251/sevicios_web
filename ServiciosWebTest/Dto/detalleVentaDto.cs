using ServiciosWebTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWebTest.Dto
{
    public class detalleVentaDto
    {
        public int detalle_venta1 { get; set; }
        public int idVenta { get; set; }
        public int idArticulo { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal descuento { get; set; }

        public ArticuloDto ArticuloDto { get; set; }
        public VentaDto VentaDto { get; set; }
    }
}