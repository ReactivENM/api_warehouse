using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class TruckController
    {
        public static TruckModel GetTruck(string id)
        {
            TruckModel t = new TruckModel();
            return t.GetTruck(id);
        }

        public static List<Dictionary<string, string>> GetAll()
        {
            TruckModel truck = new TruckModel();
            List<TruckModel> _t = truck.GetAllTrucks();

            List<Dictionary<string, string>> resultado = new List<Dictionary<string, string>>();

            foreach (TruckModel t in _t)
            {
                Dictionary<string, string> elemento = new Dictionary<string, string>();
                elemento.Add("Id", t.Id.ToString());
                elemento.Add("Marca", t.Marca.ToString());
                elemento.Add("Modelo", t.Modelo.ToString());
                elemento.Add("Matricula", t.Matricula.ToString());
                elemento.Add("Capacidad", t.Capacidad.ToString());

                resultado.Add(elemento);
            }
            return resultado;
        }
    }
}
