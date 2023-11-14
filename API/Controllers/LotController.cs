using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    public class LotController : ApiController
    {
        [Route("api/lot")]
        public IHttpActionResult GetAllLots()
        {
            return Ok(Logica.LotController.GetAll());
        }

        [Route("api/lot/{id}")]
        public IHttpActionResult GetLotById([FromUri] string id)
        {
            var package = Logica.LotController.GetLot(id);
            if (package == null)
            {
                return NotFound();
            }

            return Ok(package);
        }

        [Route("api/lot")]
        public IHttpActionResult Post([FromBody]LotModel l)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Faltan uno o más campos!");
            }
            
            return Ok(Logica.LotController.Create(l.IdAlmacen, l.Estado));
        }

        [Route("api/lot/{IdLote}")]
        public IHttpActionResult Put([FromUri]string IdLote, [FromBody]LotModel lot)
        {
            Datos.LotModel _lot = new Datos.LotModel
            {
                IdAlmacen = lot.IdAlmacen,
                Estado = lot.Estado
            };
            return Ok(Logica.LotController.Update(IdLote, _lot));
        }

        [Route("api/lot/{IdLote}")]
        public IHttpActionResult Delete([FromBody]string IdLote)
        {
            return Ok(Logica.LotController.Delete(IdLote));
        }

        [Route("api/lot/packages/{IdLote}")]
        public IHttpActionResult GetPackagesFromLot([FromUri]string IdLote)
        {
            return Ok(Logica.PackageController.GetPackagesFromLot(IdLote));
        }

        [Route("api/lot/assign")]
        public IHttpActionResult Post([FromBody]AssignLotToTruck assignData)
        {
            return Ok(Logica.LotController.AssignToTruck(assignData.IdLote, assignData.IdCamion));
        }

        [Route("api/lot/unassign/{IdLote}")]
        public IHttpActionResult Post([FromUri]string IdLote)
        {
            return Ok(Logica.LotController.UnassignFromTruck(IdLote));
        }

    }
}
