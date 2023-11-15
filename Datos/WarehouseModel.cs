using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class WarehouseModel : Model
    {

        public string Descripcion;
        public string Calle;
        public string NroPuerta;
        public string CodPostal;
        public int Capacidad;
        public string Departamento;

        public int Id;

        public WarehouseModel GetWarehouse(String id)
        {
            using (this.Connection)
            {
                this.Command.CommandText = $"SELECT * " + $"FROM almacen WHERE id = '{id}'";
                this.Reader = this.Command.ExecuteReader();

                WarehouseModel c = new WarehouseModel();

                if (this.Reader.HasRows)
                {
                    this.Reader.Read();
                    c.Id = Int32.Parse(this.Reader["id"].ToString());
                    c.Descripcion = this.Reader["descripcion"].ToString();
                    c.Calle = this.Reader["calle"].ToString();
                    c.NroPuerta = this.Reader["nro_puerta"].ToString();
                    c.CodPostal = this.Reader["cod_postal"].ToString();
                    c.Capacidad = Int32.Parse(this.Reader["capacidad"].ToString());
                    c.Departamento = this.Reader["departamento"].ToString();
                    return c;
                }

                return null;
            }
        }

        public List<WarehouseModel> GetAllWarehouses()
        {
            using (this.Connection)
            {
                this.Command.CommandText = "SELECT * FROM almacen";
                this.Reader = this.Command.ExecuteReader();

                List<WarehouseModel> resultado = new List<WarehouseModel>();

                while (this.Reader.Read())
                {
                    WarehouseModel elemento = new WarehouseModel
                    {
                        Id = Int32.Parse(this.Reader["id"].ToString()),
                        Descripcion = this.Reader["descripcion"].ToString(),
                        Calle = this.Reader["calle"].ToString(),
                        NroPuerta = this.Reader["nro_puerta"].ToString(),
                        CodPostal = this.Reader["cod_postal"].ToString(),
                        Capacidad = Int32.Parse(this.Reader["capacidad"].ToString()),
                        Departamento = this.Reader["departamento"].ToString()
                    };
                    resultado.Add(elemento);
                }

                return resultado;
            }
        }
    }
}
