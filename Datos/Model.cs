using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Datos
{
    public class Model
    {
        protected string DatabaseIp;
        protected string DatabaseUser;
        protected string DatabasePassword;
        protected string DatabaseName;

        protected MySqlConnection Connection;
        protected MySqlCommand Command;
        protected MySqlDataReader Reader;

        protected Model()
        {
            this.DatabaseIp = "localhost";
            this.DatabaseUser = "root";
            this.DatabasePassword = "";
            this.DatabaseName = "quickcarry";

            this.Connection = new MySqlConnection(
                $"server={this.DatabaseIp};" +
                $"user={this.DatabaseUser};" +
                $"password={this.DatabasePassword};" +
                $"database={this.DatabaseName};"
            );

            this.Connection.Open();

            this.Command = new MySqlCommand();
            this.Command.Connection = this.Connection;
        }
    }
}
