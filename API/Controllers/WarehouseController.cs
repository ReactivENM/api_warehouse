using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    public class WarehouseController : ApiController
    {
        [Route("api/warehouse")]
        public IHttpActionResult GetAllWarehouses()
        {
            return Ok(Logica.WarehouseController.GetAll());
        }

        [Route("api/warehouse/{id}")]
        public IHttpActionResult GetWarehouseById([FromUri] string id)
        {
            var wh = Logica.WarehouseController.GetWarehouse(id);
            if (wh == null)
            {
                return NotFound();
            }

            return Ok(wh);
        }

    }
}
