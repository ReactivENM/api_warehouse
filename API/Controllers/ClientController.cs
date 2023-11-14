using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    public class ClientController : ApiController
    {
        [Route("api/client")]
        public IHttpActionResult GetAllClients()
        {
            return Ok(Logica.ClientController.GetAll());
        }

        [Route("api/client/{id}")]
        public IHttpActionResult GetClientById([FromUri] string id)
        {
            var client = Logica.ClientController.GetClient(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

    }
}
