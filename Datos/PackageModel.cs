using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class PackageModel : Model
    {
        public string IdExterno;
        public string IdCliente;
        public string Peso;
        public string DirEnvio;
        public string Estado;

        public int Id;

        public PackageModel Save()
        {
            using (this.Connection)
            {
                this.Command.CommandText =
                    $"INSERT INTO paquete (id_externo, id_cliente, peso, dir_envio, estado) " +
                    $"VALUES ('{this.IdExterno}','{this.IdCliente}','{this.Peso}','{this.DirEnvio}','{this.Estado}')";

                this.Command.ExecuteNonQuery();

                PackageModel p = new PackageModel
                {
                    Id = (int)this.Command.LastInsertedId,
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
            using (this.Connection)
            {
                this.Command.CommandText = $"SELECT * " + $"FROM paquete WHERE id_interno = '{id}' OR id_externo = '{id}'";
                this.Reader = this.Command.ExecuteReader();

                PackageModel p = new PackageModel();

                if (this.Reader.HasRows)
                {
                    this.Reader.Read();
                    p.Id = Int32.Parse(this.Reader["id_interno"].ToString());
                    p.IdExterno = this.Reader["id_externo"].ToString();
                    p.IdCliente = this.Reader["id_cliente"].ToString();
                    p.Peso = this.Reader["peso"].ToString();
                    p.DirEnvio = this.Reader["dir_envio"].ToString();
                    p.Estado = this.Reader["estado"].ToString();
                    return p;
                }

                return p.Id != 0 ? p : null;
            }
        }

        public List<PackageModel> GetAllPackages()
        {
            using (this.Connection)
            {
                this.Command.CommandText = "SELECT * FROM paquete";
                this.Reader = this.Command.ExecuteReader();

                List<PackageModel> resultado = new List<PackageModel>();

                while (this.Reader.Read())
                {
                    PackageModel elemento = new PackageModel
                    {
                        Id = Int32.Parse(this.Reader["id_interno"].ToString()),
                        IdExterno = this.Reader["id_externo"].ToString(),
                        IdCliente = this.Reader["id_cliente"].ToString(),
                        Peso = this.Reader["peso"].ToString(),
                        DirEnvio = this.Reader["dir_envio"].ToString(),
                        Estado = this.Reader["estado"].ToString()
                    };
                    resultado.Add(elemento);
                }

                return resultado;
            }
        }

        public List<PackageModel> GetAllUnassignedPackages()
        {
            using (this.Connection)
            {
                this.Command.CommandText = "SELECT * FROM paquete WHERE id_externo NOT IN(SELECT id_externo_paquete FROM paquetelote)";
                this.Reader = this.Command.ExecuteReader();

                List<PackageModel> resultado = new List<PackageModel>();

                while (this.Reader.Read())
                {
                    PackageModel elemento = new PackageModel
                    {
                        Id = Int32.Parse(this.Reader["id_interno"].ToString()),
                        IdExterno = this.Reader["id_externo"].ToString(),
                        IdCliente = this.Reader["id_cliente"].ToString(),
                        Peso = this.Reader["peso"].ToString(),
                        DirEnvio = this.Reader["dir_envio"].ToString(),
                        Estado = this.Reader["estado"].ToString()
                    };
                    resultado.Add(elemento);
                }

                return resultado;
            }
        }

        public List<PackageModel> GetPackagesFromLot(string IdLote)
        {
            using (this.Connection)
            {
                this.Command.CommandText = $"SELECT paquete.* FROM paquetelote JOIN paquete ON paquetelote.id_externo_paquete = paquete.id_externo WHERE paquetelote.id_lote = '{IdLote}'";
                this.Reader = this.Command.ExecuteReader();

                List<PackageModel> resultado = new List<PackageModel>();

                while (this.Reader.Read())
                {
                    PackageModel elemento = new PackageModel
                    {
                        Id = Int32.Parse(this.Reader["id_interno"].ToString()),
                        IdExterno = this.Reader["id_externo"].ToString(),
                        IdCliente = this.Reader["id_cliente"].ToString(),
                        Peso = this.Reader["peso"].ToString(),
                        DirEnvio = this.Reader["dir_envio"].ToString(),
                        Estado = this.Reader["estado"].ToString()
                    };
                    resultado.Add(elemento);
                }

                return resultado;
            }
        }

        public string DeletePackage(string IdPaquete)
        {
            using (this.Connection)
            {
                this.Command.CommandText = $"SELECT * " + $"FROM paquete WHERE id_interno = '{IdPaquete}'";
                this.Reader = this.Command.ExecuteReader();
                if (this.Reader.HasRows)
                {
                    this.Reader.Close();
                    this.Command.CommandText = $"DELETE FROM paquete WHERE id_interno = '{IdPaquete}'";
                    this.Command.ExecuteNonQuery();
                    return "Paquete eliminado!";
                }

                return "El paquete no existe!";
            }

        }

        public string UpdatePackage(string IdPaquete, PackageModel pkg)
        {
            using (this.Connection)
            {
                this.Command.CommandText = $"SELECT * FROM paquete WHERE id_interno = '{IdPaquete}'";
                this.Reader = this.Command.ExecuteReader();
                if (this.Reader.HasRows)
                {
                    this.Reader.Close();
                    this.Command.CommandText = $"UPDATE paquete SET " +
                        $"id_externo = '{pkg.IdExterno}', " +
                        $"id_cliente = '{pkg.IdCliente}', " +
                        $"peso = '{pkg.Peso}', " +
                        $"dir_envio = '{pkg.DirEnvio}', " +
                        $"estado = '{pkg.Estado}' " +
                        $"WHERE id_interno = '{IdPaquete}'";

                    this.Command.ExecuteNonQuery();
                    return "Paquete actualizado!";
                }
                return "El paquete no existe!";
            }
        }

        public string AssignToLot(string IdPaquete, string IdLote, string IdUsuario)
        {
            using (this.Connection)
            {
                this.Command.CommandText = $"SELECT * " + $"FROM paquete WHERE id_externo = '{IdPaquete}'";
                this.Reader = this.Command.ExecuteReader();

                if (!this.Reader.HasRows) return "El paquete no existe!";
                this.Reader.Close();

                this.Command.CommandText = $"SELECT * " + $"FROM paquetelote WHERE id_externo_paquete = '{IdPaquete}'";
                this.Reader = this.Command.ExecuteReader();

                if (this.Reader.HasRows) return "El paquete ya está asignado a un lote!";
                this.Reader.Close();

                this.Command.CommandText =
                $"INSERT INTO paquetelote (id_externo_paquete, id_lote, id_usuario) " +
                $"VALUES ('{IdPaquete}','{IdLote}','{IdUsuario}')";

                this.Command.ExecuteNonQuery();
                return "Paquete asignado al lote exitosamente!";
            }

        }

        public string UnassignFromLot(string IdPaquete)
        {
            using (this.Connection)
            {
                this.Command.CommandText = $"SELECT * " + $"FROM paquetelote WHERE id_externo_paquete = '{IdPaquete}'";
                this.Reader = this.Command.ExecuteReader();
                if (this.Reader.HasRows)
                {
                    this.Reader.Close();
                    PackageModel p = new PackageModel();

                    this.Command.CommandText =
                    $"DELETE " + $"FROM paquetelote WHERE id_externo_paquete = '{IdPaquete}'";

                    this.Command.ExecuteNonQuery();
                    return "Paquete liberado del lote exitosamente!";
                }
                return "El paquete no está asignado a ningún lote!";
            }
        }

    }
}
