using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class TruckModel : Model
    {

        public string Marca;
        public string Modelo;
        public string Matricula;
        public string Capacidad;

        public int Id;

        public TruckModel GetTruck(String id)
        {
            this.Command.CommandText = $"SELECT * " + $"FROM camion WHERE id = '{id}'";
            this.Reader = this.Command.ExecuteReader();

            TruckModel t = new TruckModel();

            if (this.Reader.HasRows)
            {
                this.Reader.Read();
                t.Id = Int32.Parse(this.Reader["id"].ToString());
                t.Marca = this.Reader["marca"].ToString();
                t.Modelo = this.Reader["modelo"].ToString();
                t.Matricula = this.Reader["matricula"].ToString();
                t.Capacidad = this.Reader["capacidad"].ToString();
                return t;
            }

            return null;

        }

        public List<TruckModel> GetAllTrucks()
        {
            this.Command.CommandText = "SELECT * FROM camion";
            this.Reader = this.Command.ExecuteReader();

            List<TruckModel> resultado = new List<TruckModel>();

            while (this.Reader.Read())
            {
                TruckModel elemento = new TruckModel
                {
                    Id = Int32.Parse(this.Reader["id"].ToString()),
                    Marca = this.Reader["marca"].ToString(),
                    Modelo = this.Reader["modelo"].ToString(),
                    Matricula = this.Reader["matricula"].ToString(),
                    Capacidad = this.Reader["capacidad"].ToString(),
                };
                resultado.Add(elemento);
            }

            return resultado;
        }
    }
}
