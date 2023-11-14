using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class LotModel : Model
    {
        public string IdAlmacen;
        public string Estado;

        public int Id;

        public LotModel Save()
        {
            this.Command.CommandText =
                $"INSERT INTO lote (id_almacen, estado) " + 
                $"VALUES ('{this.IdAlmacen}','{this.Estado}')";

            this.Command.ExecuteNonQuery();

            LotModel l = new LotModel
            {
                Id = (int)this.Command.LastInsertedId,
                IdAlmacen = this.IdAlmacen,
                Estado = this.Estado,
            };

            return l;
        }

        public LotModel GetLot(String id)
        {
            this.Command.CommandText = $"SELECT * " + $"FROM lote WHERE id = '{id}'";
            this.Reader = this.Command.ExecuteReader();

            LotModel l = new LotModel();

            if (this.Reader.HasRows)
            {
                this.Reader.Read();
                l.Id = Int32.Parse(this.Reader["id"].ToString());
                l.IdAlmacen = this.Reader["id_almacen"].ToString();
                l.Estado = this.Reader["estado"].ToString();
                return l;
            }

            return null;

        }

        public List<LotModel> GetAllLots()
        {
            this.Command.CommandText = "SELECT * FROM lote";
            this.Reader = this.Command.ExecuteReader();

            List<LotModel> resultado = new List<LotModel>();

            while (this.Reader.Read())
            {
                LotModel elemento = new LotModel
                {
                    Id = Int32.Parse(this.Reader["id"].ToString()),
                    IdAlmacen = this.Reader["id_almacen"].ToString(),
                    Estado = this.Reader["estado"].ToString()
                };
                resultado.Add(elemento);
            }

            return resultado;
        }

        public string AssignLotToTruck(string IdLote, string IdCamion)
        {
            this.Command.CommandText = $"SELECT * " + $"FROM camionlote WHERE id_lote = '{IdLote}'";
            this.Reader = this.Command.ExecuteReader();

            if (this.Reader.HasRows) return "El lote ya está asignado a un camión!";
            this.Reader.Close();

            LotModel L = new LotModel();

            this.Command.CommandText =
            $"INSERT INTO camionlote (id_camion, id_lote) " +
            $"VALUES ('{IdCamion}','{IdLote}')";

            this.Command.ExecuteNonQuery();
            return "Lote asignado al camión exitosamente!";
        }

        public string UnassignFromTruck(string IdLote)
        {
            this.Command.CommandText = $"SELECT * " + $"FROM camionlote WHERE id_lote = '{IdLote}'";
            this.Reader = this.Command.ExecuteReader();
            if (this.Reader.HasRows)
            {
                this.Reader.Close();
                LotModel l = new LotModel();

                this.Command.CommandText =
                $"DELETE " + $"FROM camionlote WHERE id_lote = '{IdLote}'";

                this.Command.ExecuteNonQuery();
                return "Lote liberado del camión exitosamente!";
            }
            return "El lote no está asignado a ningún camión!";
        }

        public string DeleteLot(string IdLote)
        {
            this.Command.CommandText = $"SELECT * " + $"FROM lote WHERE id = '{IdLote}'";
            this.Reader = this.Command.ExecuteReader();
            if (this.Reader.HasRows)
            {
                this.Reader.Close();

                this.Command.CommandText =
                $"DELETE " + $"FROM lote WHERE id = '{IdLote}'";

                this.Command.ExecuteNonQuery();
                return "Lote eliminado!";
            }
            return "El lote no existe!";
        }

        public string UpdateLot(String IdLote, LotModel lot)
        {
            this.Command.CommandText = $"SELECT * FROM almacen WHERE id = '{lot.IdAlmacen}'";
            this.Reader = this.Command.ExecuteReader();
            if (!this.Reader.HasRows) return "El almacen ingresado no existe!";
            this.Reader.Close();

            this.Command.CommandText = $"SELECT * FROM lote WHERE id = '{IdLote}'";
            this.Reader = this.Command.ExecuteReader();
            if (this.Reader.HasRows)
            {
                this.Reader.Close();
                
                this.Command.CommandText = $"UPDATE lote SET " +
                    $"id_almacen = '{lot.IdAlmacen}', " +
                    $"estado = '{lot.Estado}' " +
                    $"WHERE id = '{IdLote}'";

                this.Command.ExecuteNonQuery();

                return "Lote actualizado!";
            }
            return "El lote no existe!";
        }
    }
}
