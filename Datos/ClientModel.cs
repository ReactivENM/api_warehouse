using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ClientModel
    {

        public string Nombre;
        public string Calle;
        public string Telefono;

        public int Id;

        public ClientModel GetClient(String id)
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = $"SELECT * " + $"FROM cliente WHERE id = '{id}'";
                model.Reader = model.Command.ExecuteReader();

                ClientModel c = new ClientModel();

                if (model.Reader.HasRows)
                {
                    model.Reader.Read();
                    c.Id = Int32.Parse(model.Reader["id"].ToString());
                    c.Nombre = model.Reader["nombre"].ToString();
                    c.Calle = model.Reader["calle"].ToString();
                    c.Telefono = model.Reader["telefono"].ToString();
                    return c;
                }

                return null;
            }
        }

        public List<ClientModel> GetAllClients()
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = "SELECT * FROM cliente";
                model.Reader = model.Command.ExecuteReader();

                List<ClientModel> resultado = new List<ClientModel>();

                while (model.Reader.Read())
                {
                    ClientModel elemento = new ClientModel
                    {
                        Id = Int32.Parse(model.Reader["id"].ToString()),
                        Nombre = model.Reader["nombre"].ToString(),
                        Calle = model.Reader["calle"].ToString(),
                        Telefono = model.Reader["telefono"].ToString()
                    };
                    resultado.Add(elemento);
                }

                return resultado;
            }
        }
    }
}
