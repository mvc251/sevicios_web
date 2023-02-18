using ServiciosWebTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWebTest.Dto
{
    public class ArticuloDto
    {
        public int idArticulo { get; set; }
        public int idCategoria { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Precio_venta { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }

        public  CategoriaDto CategoriaDto { get; set; }
    }
}