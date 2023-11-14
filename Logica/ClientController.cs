using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class ClientController
    {
        public static ClientModel GetClient(string id)
        {
            ClientModel c = new ClientModel();
            return c.GetClient(id);
        }

        public static List<Dictionary<string, string>> GetAll()
        {
            ClientModel client = new ClientModel();
            List<ClientModel> _c = client.GetAllClients();

            List<Dictionary<string, string>> resultado = new List<Dictionary<string, string>>();

            foreach (ClientModel c in _c)
            {
                Dictionary<string, string> elemento = new Dictionary<string, string>();
                elemento.Add("Id", c.Id.ToString());
                elemento.Add("Nombre", c.Nombre.ToString());
                elemento.Add("Calle", c.Calle.ToString());
                elemento.Add("Telefono", c.Telefono.ToString());

                resultado.Add(elemento);
            }
            return resultado;
        }
    }
}
