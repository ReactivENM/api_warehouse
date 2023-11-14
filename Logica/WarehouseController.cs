using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class WarehouseController
    {
        public static WarehouseModel GetWarehouse(string id)
        {
            WarehouseModel wh = new WarehouseModel();
            return wh.GetWarehouse(id);
        }

        public static List<Dictionary<string, string>> GetAll()
        {
            WarehouseModel warehouse = new WarehouseModel();
            List<WarehouseModel> _wh = warehouse.GetAllWarehouses();

            List<Dictionary<string, string>> resultado = new List<Dictionary<string, string>>();

            foreach (WarehouseModel wh in _wh)
            {
                Dictionary<string, string> elemento = new Dictionary<string, string>();
                elemento.Add("Id", wh.Id.ToString());
                elemento.Add("Descripcion", wh.Descripcion.ToString());
                elemento.Add("Calle", wh.Calle.ToString());
                elemento.Add("NroPuerta", wh.NroPuerta.ToString());
                elemento.Add("CodPostal", wh.CodPostal.ToString());
                elemento.Add("Capacidad", wh.Capacidad.ToString());
                elemento.Add("Departamento", wh.Departamento.ToString());

                resultado.Add(elemento);
            }
            return resultado;
        }
    }
}
