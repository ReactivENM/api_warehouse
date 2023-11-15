using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class LotController
    {
        public static LotModel Create(string idalmacen, string estado)
        {
            LotModel l = new LotModel
            {
                IdAlmacen = idalmacen,
                Estado = estado,
            };
            return l.Save();
        }

        public static string Delete(string id)
        {
            LotModel l = new LotModel();
            return l.DeleteLot(id);
        }

        public static LotModel GetLot(string id)
        {
            LotModel l = new LotModel();
            return l.GetLot(id);
        }

        public static List<Dictionary<string, string>> GetAll()
        {
            LotModel lot = new LotModel();
            List<LotModel> _l = lot.GetAllLots();

            List<Dictionary<string, string>> resultado = new List<Dictionary<string, string>>();

            foreach (LotModel l in _l)
            {
                Dictionary<string, string> elemento = new Dictionary<string, string>();
                elemento.Add("Id", l.Id.ToString());
                elemento.Add("Id_Almacen", l.IdAlmacen.ToString());
                elemento.Add("Estado", l.Estado.ToString());

                resultado.Add(elemento);
            }
            return resultado;


        }

        public static List<Dictionary<string, string>> GetAllAssignedLots()
        {
            LotModel lot = new LotModel();
            List<LotModel> _l = lot.GetAllAssignedLots();

            List<Dictionary<string, string>> resultado = new List<Dictionary<string, string>>();

            foreach (LotModel l in _l)
            {
                Dictionary<string, string> elemento = new Dictionary<string, string>();
                elemento.Add("Id", l.Id.ToString());
                elemento.Add("Id_Almacen", l.IdAlmacen.ToString());
                elemento.Add("Estado", l.Estado.ToString());

                resultado.Add(elemento);
            }
            return resultado;


        }

        public static List<Dictionary<string, string>> GetAllUnassignedLots()
        {
            LotModel lot = new LotModel();
            List<LotModel> _l = lot.GetAllUnassignedLots();

            List<Dictionary<string, string>> resultado = new List<Dictionary<string, string>>();

            foreach (LotModel l in _l)
            {
                Dictionary<string, string> elemento = new Dictionary<string, string>();
                elemento.Add("Id", l.Id.ToString());
                elemento.Add("Id_Almacen", l.IdAlmacen.ToString());
                elemento.Add("Estado", l.Estado.ToString());

                resultado.Add(elemento);
            }
            return resultado;


        }

        public static object Update(string IdLote, LotModel lot)
        {
            LotModel l = new LotModel();
            return l.UpdateLot(IdLote, lot);
        }

        public static string AssignToTruck(string IdLote, string IdCamion)
        {
            if (GetLot(IdLote) == null) return "Lote no existe!";

            LotModel l = new LotModel();

            return l.AssignLotToTruck(IdLote, IdCamion);
        }

        public static object UnassignFromTruck(string IdLote)
        {
            if (GetLot(IdLote) == null) return "Lote no existe!";

            LotModel l = new LotModel();

            return l.UnassignFromTruck(IdLote);
        }
    }
}
