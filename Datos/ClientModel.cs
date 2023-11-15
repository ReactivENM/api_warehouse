using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ClientModel : Model
    {

        public string Nombre;
        public string Calle;
        public string Telefono;

        public int Id;

        public ClientModel GetClient(String id)
        {
            using (this.Connection)
            {
                this.Command.CommandText = $"SELECT * " + $"FROM cliente WHERE id = '{id}'";
                this.Reader = this.Command.ExecuteReader();

                ClientModel c = new ClientModel();

                if (this.Reader.HasRows)
                {
                    this.Reader.Read();
                    c.Id = Int32.Parse(this.Reader["id"].ToString());
                    c.Nombre = this.Reader["nombre"].ToString();
                    c.Calle = this.Reader["calle"].ToString();
                    c.Telefono = this.Reader["telefono"].ToString();
                    return c;
                }

                return null;
            }
        }

        public List<ClientModel> GetAllClients()
        {
            using (this.Connection)
            {
                this.Command.CommandText = "SELECT * FROM cliente";
                this.Reader = this.Command.ExecuteReader();

                List<ClientModel> resultado = new List<ClientModel>();

                while (this.Reader.Read())
                {
                    ClientModel elemento = new ClientModel
                    {
                        Id = Int32.Parse(this.Reader["id"].ToString()),
                        Nombre = this.Reader["nombre"].ToString(),
                        Calle = this.Reader["calle"].ToString(),
                        Telefono = this.Reader["telefono"].ToString()
                    };
                    resultado.Add(elemento);
                }

                return resultado;
            }
        }
    }
}
