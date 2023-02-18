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
    public class PersonasController : ApiController
    {
        private GestorVentasEntities1 db = new GestorVentasEntities1();

        // GET: api/Personas
        public IQueryable<Persona> GetPersona()
        {
            return db.Persona;
        }

        // GET: api/Personas/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult GetPersona(int id)
        {
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        //// PUT: api/Personas/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutPersona(int id, Persona persona)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != persona.idPersona)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(persona).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PersonaExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Personas
        [HttpPost]
        [Route("api/persona/guardar")]
        [ResponseType(typeof(Persona))]
        public IHttpActionResult PostPersona(PersonaDto personaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Persona per =new Persona();
            per.tipo_persona=personaDto.tipo_persona;
            per.nombre=personaDto.nombre;
            per.tipo_documento=personaDto.tipo_documento;
            per.num_documento=personaDto.num_documento;
            per.direccion=personaDto.direccion;
            per.telefono=personaDto.telefono;
            per.email=personaDto.email;

            db.Persona.Add(per);
            db.SaveChanges();

            return Ok("Se guardo correctamente");
        }

        // DELETE: api/Personas/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult DeletePersona(int id)
        {
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            db.Persona.Remove(persona);
            db.SaveChanges();

            return Ok(persona);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonaExists(int id)
        {
            return db.Persona.Count(e => e.idPersona == id) > 0;
        }
    }
}