using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class PackageModel
    {
        public string IdExterno;
        public string IdCliente;
        public string Peso;
        public string DirEnvio;
        public string Estado;

        public int Id;

        public PackageModel Save()
        {
            using (Model model = new Model())
            {
                model.Command.CommandText =
                    $"INSERT INTO paquete (id_externo, id_cliente, peso, dir_envio, estado) " +
                    $"VALUES ('{this.IdExterno}','{this.IdCliente}','{this.Peso}','{this.DirEnvio}','{this.Estado}')";

                model.Command.ExecuteNonQuery();

                PackageModel p = new PackageModel
                {
                    Id = (int)model.Command.LastInsertedId,
                    IdExterno = this.IdExterno,
                    IdCliente = this.IdCliente,
                    Peso = this.Peso,
                    DirEnvio = this.DirEnvio,
                    Estado = this.Estado,
                };

                return p;
            }
        }

        public PackageModel GetPackage(String id)
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = $"SELECT * " + $"FROM paquete WHERE id_interno = '{id}' OR id_externo = '{id}'";
                model.Reader = model.Command.ExecuteReader();

                PackageModel p = new PackageModel();

                if (model.Reader.HasRows)
                {
                    model.Reader.Read();
                    p.Id = Int32.Parse(model.Reader["id_interno"].ToString());
                    p.IdExterno = model.Reader["id_externo"].ToString();
                    p.IdCliente = model.Reader["id_cliente"].ToString();
                    p.Peso = model.Reader["peso"].ToString();
                    p.DirEnvio = model.Reader["dir_envio"].ToString();
                    p.Estado = model.Reader["estado"].ToString();
                    return p;
                }

                return p.Id != 0 ? p : null;
            }
        }

        public List<PackageModel> GetAllPackages()
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = "SELECT * FROM paquete";
                model.Reader = model.Command.ExecuteReader();

                List<PackageModel> resultado = new List<PackageModel>();

                while (model.Reader.Read())
                {
                    PackageModel elemento = new PackageModel
                    {
                        Id = Int32.Parse(model.Reader["id_interno"].ToString()),
                        IdExterno = model.Reader["id_externo"].ToString(),
                        IdCliente = model.Reader["id_cliente"].ToString(),
                        Peso = model.Reader["peso"].ToString(),
                        DirEnvio = model.Reader["dir_envio"].ToString(),
                        Estado = model.Reader["estado"].ToString()
                    };
                    resultado.Add(elemento);
                }

                return resultado;
            }
        }

        public List<PackageModel> GetAllUnassignedPackages()
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = "SELECT * FROM paquete WHERE id_externo NOT IN(SELECT id_externo_paquete FROM paquetelote)";
                model.Reader = model.Command.ExecuteReader();

                List<PackageModel> resultado = new List<PackageModel>();

                while (model.Reader.Read())
                {
                    PackageModel elemento = new PackageModel
                    {
                        Id = Int32.Parse(model.Reader["id_interno"].ToString()),
                        IdExterno = model.Reader["id_externo"].ToString(),
                        IdCliente = model.Reader["id_cliente"].ToString(),
                        Peso = model.Reader["peso"].ToString(),
                        DirEnvio = model.Reader["dir_envio"].ToString(),
                        Estado = model.Reader["estado"].ToString()
                    };
                    resultado.Add(elemento);
                }

                return resultado;
            }
        }

        public List<PackageModel> GetPackagesFromLot(string IdLote)
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = $"SELECT paquete.* FROM paquetelote JOIN paquete ON paquetelote.id_externo_paquete = paquete.id_externo WHERE paquetelote.id_lote = '{IdLote}'";
                model.Reader = model.Command.ExecuteReader();

                List<PackageModel> resultado = new List<PackageModel>();

                while (model.Reader.Read())
                {
                    PackageModel elemento = new PackageModel
                    {
                        Id = Int32.Parse(model.Reader["id_interno"].ToString()),
                        IdExterno = model.Reader["id_externo"].ToString(),
                        IdCliente = model.Reader["id_cliente"].ToString(),
                        Peso = model.Reader["peso"].ToString(),
                        DirEnvio = model.Reader["dir_envio"].ToString(),
                        Estado = model.Reader["estado"].ToString()
                    };
                    resultado.Add(elemento);
                }

                return resultado;
            }
        }

        public string DeletePackage(string IdPaquete)
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = $"SELECT * " + $"FROM paquete WHERE id_interno = '{IdPaquete}'";
                model.Reader = model.Command.ExecuteReader();
                if (model.Reader.HasRows)
                {
                    model.Reader.Close();
                    model.Command.CommandText = $"DELETE FROM paquete WHERE id_interno = '{IdPaquete}'";
                    model.Command.ExecuteNonQuery();
                    return "Paquete eliminado!";
                }

                return "El paquete no existe!";
            }

        }

        public string UpdatePackage(string IdPaquete, PackageModel pkg)
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = $"SELECT * FROM paquete WHERE id_interno = '{IdPaquete}'";
                model.Reader = model.Command.ExecuteReader();
                if (model.Reader.HasRows)
                {
                    model.Reader.Close();
                    model.Command.CommandText = $"UPDATE paquete SET " +
                        $"id_externo = '{pkg.IdExterno}', " +
                        $"id_cliente = '{pkg.IdCliente}', " +
                        $"peso = '{pkg.Peso}', " +
                        $"dir_envio = '{pkg.DirEnvio}', " +
                        $"estado = '{pkg.Estado}' " +
                        $"WHERE id_interno = '{IdPaquete}'";

                    model.Command.ExecuteNonQuery();
                    return "Paquete actualizado!";
                }
                return "El paquete no existe!";
            }
        }

        public string AssignToLot(string IdPaquete, string IdLote, string IdUsuario)
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = $"SELECT * " + $"FROM paquete WHERE id_externo = '{IdPaquete}'";
                model.Reader = model.Command.ExecuteReader();

                if (!model.Reader.HasRows) return "El paquete no existe!";
                model.Reader.Close();

                model.Command.CommandText = $"SELECT * " + $"FROM paquetelote WHERE id_externo_paquete = '{IdPaquete}'";
                model.Reader = model.Command.ExecuteReader();

                if (model.Reader.HasRows) return "El paquete ya está asignado a un lote!";
                model.Reader.Close();

                model.Command.CommandText =
                $"INSERT INTO paquetelote (id_externo_paquete, id_lote, id_usuario) " +
                $"VALUES ('{IdPaquete}','{IdLote}','{IdUsuario}')";

                model.Command.ExecuteNonQuery();
                return "Paquete asignado al lote exitosamente!";
            }

        }

        public string UnassignFromLot(string IdPaquete)
        {
            using (Model model = new Model())
            {
                model.Command.CommandText = $"SELECT * " + $"FROM paquetelote WHERE id_externo_paquete = '{IdPaquete}'";
                model.Reader = model.Command.ExecuteReader();
                if (model.Reader.HasRows)
                {
                    model.Reader.Close();
                    PackageModel p = new PackageModel();

                    model.Command.CommandText =
                    $"DELETE " + $"FROM paquetelote WHERE id_externo_paquete = '{IdPaquete}'";

                    model.Command.ExecuteNonQuery();
                    return "Paquete liberado del lote exitosamente!";
                }
                return "El paquete no está asignado a ningún lote!";
            }
        }

    }
}
