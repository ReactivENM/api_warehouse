using System;
using MySqlConnector;

namespace Datos
{
    public class Model : IDisposable
    {
        protected string DatabaseIp;
        protected string DatabaseUser;
        protected string DatabasePassword;
        protected string DatabaseName;

        public MySqlConnection Connection;
        public MySqlCommand Command;
        public MySqlDataReader Reader;

        public Model()
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

        // Implementación de IDisposable
        public void Dispose()
        {
            try
            {
                Command?.Dispose();
                Reader?.Dispose();
                Connection?.Dispose();
            }
            catch (Exception ex)
            {
                // Manejar excepciones, por ejemplo, registrarlas o lanzarlas nuevamente si es necesario.
                Console.WriteLine($"Error al liberar recursos: {ex.Message}");
            }
        }
    }
}