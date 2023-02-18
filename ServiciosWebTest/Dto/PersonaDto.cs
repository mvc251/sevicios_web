using ServiciosWebTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosWebTest.Dto
{
    public class PersonaDto
    {
        public int idPersona { get; set; }
        public string tipo_persona { get; set; }
        public string nombre { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }

        public List<VentaDto> VentaDto { get; set; }
    }
}