using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    public class PackageController : ApiController
    {
        [Route("api/package")]
        public IHttpActionResult GetAllPackages()
        {
            return Ok(Logica.PackageController.GetAll());
        }

        [Route("api/package/unassigned")]
        public IHttpActionResult GetAllUnassignedPackages()
        {
            return Ok(Logica.PackageController.GetAllUnassignedPackages());
        }

        [Route("api/package/{id}")]
        public IHttpActionResult GetPackageById([FromUri] string id)
        {
            var package = Logica.PackageController.GetPackage(id);
            if (package == null)
            {
                return NotFound();
            }

            return Ok(package);
        }

        [Route("api/package")]
        public IHttpActionResult Post([FromBody]PackageModel p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Faltan uno o más campos!");
            }

            return Ok(Logica.PackageController.Create(p.IdExterno, p.IdCliente, p.Peso, p.DirEnvio, p.Estado));
        }

        [Route("api/package/{IdPaquete}")]
        public IHttpActionResult Delete([FromUri]string IdPaquete)
        {
            return Ok(Logica.PackageController.Delete(IdPaquete));
        }

        [Route("api/package/{IdPaquete}")]
        public IHttpActionResult Put([FromUri]string IdPaquete, [FromBody]PackageModel pkg)
        {
            Datos.PackageModel _pkg = new Datos.PackageModel
            {
                IdExterno = pkg.IdExterno,
                IdCliente = pkg.IdCliente,
                Peso = pkg.Peso,
                DirEnvio = pkg.DirEnvio,
                Estado = pkg.Estado
            };
            return Ok(Logica.PackageController.Update(IdPaquete, _pkg));
        }

        [Route("api/package/assign")]
        public IHttpActionResult Post([FromBody]AssignPackageToLot assignData)
        {
            return Ok(Logica.PackageController.AssignToLot(assignData.IdPaquete, assignData.IdLote, assignData.IdUsuario));
        }

        [Route("api/package/unassign/{IdPaquete}")]
        public IHttpActionResult Post([FromUri]string IdPaquete)
        {
            return Ok(Logica.PackageController.UnassignFromLot(IdPaquete));
        }

    }
}
