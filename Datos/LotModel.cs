using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class LotModel
    {
        public string IdAlmacen;
        public string Estado;

        public int Id;

        public LotModel Save()
        {
            using (Model model = new Model())
            {
                model.Command.CommandText =
                $"INSERT INTO lote (id_almacen, estado) " +
                $"VALUES ('{this.IdAlmacen}','{this.Estado}')";

                model.Command.ExecuteNonQuery();

                LotModel l = new LotModel
                {
                    Id = (int)model.Command.LastInsertedId,
                    IdAlmacen = this.IdAlmacen,
                    Estado = this.Estado,
                };

                return l;
            }
        }

        public LotModel GetLot(String id)
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = $"SELECT * " + $"FROM lote WHERE id = '{id}'";
                model.Reader = model.Command.ExecuteReader();

                LotModel l = new LotModel();

                if (model.Reader.HasRows)
                {
                    model.Reader.Read();
                    l.Id = Int32.Parse(model.Reader["id"].ToString());
                    l.IdAlmacen = model.Reader["id_almacen"].ToString();
                    l.Estado = model.Reader["estado"].ToString();
                    return l;
                }

                return null;
            }

        }

        public List<LotModel> GetAllLots()
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = "SELECT * FROM lote";
                model.Reader = model.Command.ExecuteReader();

                List<LotModel> resultado = new List<LotModel>();

                while (model.Reader.Read())
                {
                    LotModel elemento = new LotModel
                    {
                        Id = Int32.Parse(model.Reader["id"].ToString()),
                        IdAlmacen = model.Reader["id_almacen"].ToString(),
                        Estado = model.Reader["estado"].ToString()
                    };
                    resultado.Add(elemento);
                }

                return resultado;
            }
        }

        public List<LotModel> GetAllAssignedLots(int IdCamion)
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = $"SELECT * FROM lote WHERE id IN(SELECT id_lote FROM camionlote WHERE id_camion = '{IdCamion}')";
                model.Reader = model.Command.ExecuteReader();

                List<LotModel> resultado = new List<LotModel>();

                while (model.Reader.Read())
                {
                    LotModel elemento = new LotModel
                    {
                        Id = Int32.Parse(model.Reader["id"].ToString()),
                        IdAlmacen = model.Reader["id_almacen"].ToString(),
                        Estado = model.Reader["estado"].ToString()
                    };
                    resultado.Add(elemento);
                }

                return resultado;
            }
        }

        public List<LotModel> GetAllUnassignedLots()
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = "SELECT * FROM lote WHERE id NOT IN(SELECT id_lote FROM camionlote) AND estado = 'en_espera'";
                model.Reader = model.Command.ExecuteReader();

                List<LotModel> resultado = new List<LotModel>();

                while (model.Reader.Read())
                {
                    LotModel elemento = new LotModel
                    {
                        Id = Int32.Parse(model.Reader["id"].ToString()),
                        IdAlmacen = model.Reader["id_almacen"].ToString(),
                        Estado = model.Reader["estado"].ToString()
                    };
                    resultado.Add(elemento);
                }

                return resultado;
            }
        }

        public string AssignLotToTruck(string IdLote, string IdCamion)
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = $"SELECT * " + $"FROM camionlote WHERE id_lote = '{IdLote}'";
                model.Reader = model.Command.ExecuteReader();

                if (model.Reader.HasRows) return "El lote ya está asignado a un camión!";
                model.Reader.Close();

                LotModel L = new LotModel();

                model.Command.CommandText =
                $"INSERT INTO camionlote (id_camion, id_lote) " +
                $"VALUES ('{IdCamion}','{IdLote}')";

                model.Command.ExecuteNonQuery();
                return "Lote asignado al camión exitosamente!";
            }
        }

        public string UnassignFromTruck(string IdLote)
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = $"SELECT * " + $"FROM camionlote WHERE id_lote = '{IdLote}'";
                model.Reader = model.Command.ExecuteReader();
                if (model.Reader.HasRows)
                {
                    model.Reader.Close();
                    LotModel l = new LotModel();

                    model.Command.CommandText =
                    $"DELETE " + $"FROM camionlote WHERE id_lote = '{IdLote}'";

                    model.Command.ExecuteNonQuery();
                    return "Lote liberado del camión exitosamente!";
                }
                return "El lote no está asignado a ningún camión!";
            }
        }

        public string DeleteLot(string IdLote)
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = $"SELECT * " + $"FROM lote WHERE id = '{IdLote}'";
                model.Reader = model.Command.ExecuteReader();
                if (model.Reader.HasRows)
                {
                    model.Reader.Close();

                    model.Command.CommandText = $"DELETE " + $"FROM lote WHERE id = '{IdLote}'";

                    model.Command.ExecuteNonQuery();
                    return "Lote eliminado!";
                }
                return "El lote no existe!";
            }
        }

        public string UpdateLot(String IdLote, LotModel lot)
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = $"SELECT * FROM almacen WHERE id = '{lot.IdAlmacen}'";
                model.Reader = model.Command.ExecuteReader();
                if (!model.Reader.HasRows) return "El almacen ingresado no existe!";
                model.Reader.Close();

                model.Command.CommandText = $"SELECT * FROM lote WHERE id = '{IdLote}'";
                model.Reader = model.Command.ExecuteReader();
                if (model.Reader.HasRows)
                {
                    model.Reader.Close();

                    model.Command.CommandText = $"UPDATE lote SET " +
                        $"id_almacen = '{lot.IdAlmacen}', " +
                        $"estado = '{lot.Estado}' " +
                        $"WHERE id = '{IdLote}'";

                    model.Command.ExecuteNonQuery();

                    return "Lote actualizado!";
                }
                return "El lote no existe!";
            }
        }
    }
}
