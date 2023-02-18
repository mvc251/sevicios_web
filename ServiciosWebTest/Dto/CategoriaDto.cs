using ServiciosWebTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWebTest.Dto
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }

        public List<ArticuloDto> ArticulosDto { get; set; }
    }
}