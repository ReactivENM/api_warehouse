using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class WarehouseModel
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
            using (Model model = new Model())
            {
                model.Command.CommandText = $"SELECT * " + $"FROM almacen WHERE id = '{id}'";
                model.Reader = model.Command.ExecuteReader();

                WarehouseModel c = new WarehouseModel();

                if (model.Reader.HasRows)
                {
                    model.Reader.Read();
                    c.Id = Int32.Parse(model.Reader["id"].ToString());
                    c.Descripcion = model.Reader["descripcion"].ToString();
                    c.Calle = model.Reader["calle"].ToString();
                    c.NroPuerta = model.Reader["nro_puerta"].ToString();
                    c.CodPostal = model.Reader["cod_postal"].ToString();
                    c.Capacidad = Int32.Parse(model.Reader["capacidad"].ToString());
                    c.Departamento = model.Reader["departamento"].ToString();
                    return c;
                }

                return null;
            }
        }

        public List<WarehouseModel> GetAllWarehouses()
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = "SELECT * FROM almacen";
                model.Reader = model.Command.ExecuteReader();

                List<WarehouseModel> resultado = new List<WarehouseModel>();

                while (model.Reader.Read())
                {
                    WarehouseModel elemento = new WarehouseModel
                    {
                        Id = Int32.Parse(model.Reader["id"].ToString()),
                        Descripcion = model.Reader["descripcion"].ToString(),
                        Calle = model.Reader["calle"].ToString(),
                        NroPuerta = model.Reader["nro_puerta"].ToString(),
                        CodPostal = model.Reader["cod_postal"].ToString(),
                        Capacidad = Int32.Parse(model.Reader["capacidad"].ToString()),
                        Departamento = model.Reader["departamento"].ToString()
                    };
                    resultado.Add(elemento);
                }

                return resultado;
            }
        }
    }
}
