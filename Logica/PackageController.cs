using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class PackageController
    {
        public static PackageModel Create(string idexterno, string idcliente, string peso, string direnvio, string estado)
        {
            PackageModel p = new PackageModel
            {
                IdExterno = idexterno,
                IdCliente = idcliente,
                Peso = peso,
                DirEnvio = direnvio,
                Estado = estado,
            };
            return p.Save();
        }

        public static string Delete(string id)
        {
            PackageModel p = new PackageModel();
            return p.DeletePackage(id);
        }

        public static string Update(string IdPaquete, PackageModel pkg)
        {
            PackageModel p = new PackageModel();
            return p.UpdatePackage(IdPaquete, pkg);
        }

        public static Dictionary<string, string> GetPackage(string id)
        {
            PackageModel p = new PackageModel().GetPackage(id);
            Dictionary<string, string> elemento = new Dictionary<string, string>();
            elemento.Add("Id", p.Id.ToString());
            elemento.Add("Id_Externo", p.IdExterno.ToString());
            elemento.Add("Id_Cliente", p.IdCliente.ToString());
            elemento.Add("Peso", p.Peso);
            elemento.Add("Dir_Envio", p.DirEnvio);
            elemento.Add("Estado", p.Estado);
            return elemento;
        }

        public static List<Dictionary<string, string>> GetAll()
        {
            PackageModel package = new PackageModel();
            List<PackageModel> _p = package.GetAllPackages();

            List<Dictionary<string, string>> resultado = new List<Dictionary<string, string>>();

            foreach (PackageModel p in _p)
            {
                Dictionary<string, string> elemento = new Dictionary<string, string>();
                elemento.Add("Id", p.Id.ToString());
                elemento.Add("Id_Externo", p.IdExterno.ToString());
                elemento.Add("Id_Cliente", p.IdCliente.ToString());
                elemento.Add("Peso", p.Peso);
                elemento.Add("Dir_Envio", p.DirEnvio);
                elemento.Add("Estado", p.Estado);

                resultado.Add(elemento);
            }
            return resultado;


        }

        public static List<Dictionary<string, string>> GetAllUnassignedPackages()
        {
            PackageModel package = new PackageModel();
            List<PackageModel> _p = package.GetAllUnassignedPackages();

            List<Dictionary<string, string>> resultado = new List<Dictionary<string, string>>();

            foreach (PackageModel p in _p)
            {
                Dictionary<string, string> elemento = new Dictionary<string, string>();
                elemento.Add("Id", p.Id.ToString());
                elemento.Add("Id_Externo", p.IdExterno.ToString());
                elemento.Add("Id_Cliente", p.IdCliente.ToString());
                elemento.Add("Peso", p.Peso);
                elemento.Add("Dir_Envio", p.DirEnvio);
                elemento.Add("Estado", p.Estado);

                resultado.Add(elemento);
            }
            return resultado;


        }

        public static List<Dictionary<string, string>> GetPackagesFromLot(string IdLote)
        {
            PackageModel package = new PackageModel();
            List<PackageModel> _p = package.GetPackagesFromLot(IdLote);

            List<Dictionary<string, string>> resultado = new List<Dictionary<string, string>>();

            foreach (PackageModel p in _p)
            {
                Dictionary<string, string> elemento = new Dictionary<string, string>();
                elemento.Add("Id", p.Id.ToString());
                elemento.Add("Id_Externo", p.IdExterno.ToString());
                elemento.Add("Id_Cliente", p.IdCliente.ToString());
                elemento.Add("Peso", p.Peso);
                elemento.Add("Dir_Envio", p.DirEnvio);
                elemento.Add("Estado", p.Estado);

                resultado.Add(elemento);
            }
            return resultado;


        }

        public static string AssignToLot(string IdPaquete, string IdLote, string IdUsuario)
        {
            if (GetPackage(IdPaquete) == null) return "Paquete no existe!";
            if (LotController.GetLot(IdLote) == null) return "Lote no existe!";

            PackageModel package = new PackageModel();

            return package.AssignToLot(IdPaquete, IdLote, IdUsuario);
        }

        public static string UnassignFromLot(string IdPaquete)
        {
            if (GetPackage(IdPaquete) == null) return "Paquete no existe!";

            PackageModel package = new PackageModel();

            return package.UnassignFromLot(IdPaquete);
        }

    }
}
