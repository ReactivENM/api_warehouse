using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    public class TruckController : ApiController
    {
        [Route("api/truck")]
        public IHttpActionResult GetAllTrucks()
        {
            return Ok(Logica.TruckController.GetAll());
        }

        [Route("api/truck/{id}")]
        public IHttpActionResult GetTruckById([FromUri] string id)
        {
            var t = Logica.TruckController.GetTruck(id);
            if (t == null)
            {
                return NotFound();
            }

            return Ok(t);
        }

        [Route("api/truck/by_user/{IdUsuario}")]
        public IHttpActionResult GetTruckById([FromUri] int IdUsuario)
        {
            var t = Logica.TruckController.GetTruckByUser(IdUsuario);
            if (t == null)
            {
                return NotFound();
            }

            return Ok(t);
        }

    }
}
