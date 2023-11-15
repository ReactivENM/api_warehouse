using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class TruckModel
    {

        public string Marca;
        public string Modelo;
        public string Matricula;
        public string Capacidad;

        public int Id;

        public TruckModel GetTruck(String id)
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = $"SELECT * " + $"FROM camion WHERE id = '{id}'";
                model.Reader = model.Command.ExecuteReader();

                TruckModel t = new TruckModel();

                if (model.Reader.HasRows)
                {
                    model.Reader.Read();
                    t.Id = Int32.Parse(model.Reader["id"].ToString());
                    t.Marca = model.Reader["marca"].ToString();
                    t.Modelo = model.Reader["modelo"].ToString();
                    t.Matricula = model.Reader["matricula"].ToString();
                    t.Capacidad = model.Reader["capacidad"].ToString();
                    return t;
                }

                return null;
            }
        }

        public List<TruckModel> GetAllTrucks()
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = "SELECT * FROM camion";
                model.Reader = model.Command.ExecuteReader();

                List<TruckModel> resultado = new List<TruckModel>();

                while (model.Reader.Read())
                {
                    TruckModel elemento = new TruckModel
                    {
                        Id = Int32.Parse(model.Reader["id"].ToString()),
                        Marca = model.Reader["marca"].ToString(),
                        Modelo = model.Reader["modelo"].ToString(),
                        Matricula = model.Reader["matricula"].ToString(),
                        Capacidad = model.Reader["capacidad"].ToString(),
                    };
                    resultado.Add(elemento);
                }

                return resultado;
            }
        }

        public TruckModel GetTruckByUser(int IdUsuario)
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = $"SELECT * " + $"FROM camion WHERE id IN(SELECT id_camion FROM usuariocamion WHERE id_usuario = '{IdUsuario}')";
                model.Reader = model.Command.ExecuteReader();

                TruckModel t = new TruckModel();

                if (model.Reader.HasRows)
                {
                    model.Reader.Read();
                    t.Id = Int32.Parse(model.Reader["id"].ToString());
                    t.Marca = model.Reader["marca"].ToString();
                    t.Modelo = model.Reader["modelo"].ToString();
                    t.Matricula = model.Reader["matricula"].ToString();
                    t.Capacidad = model.Reader["capacidad"].ToString();
                    return t;
                }

                return null;
            }
        }
    }
}
