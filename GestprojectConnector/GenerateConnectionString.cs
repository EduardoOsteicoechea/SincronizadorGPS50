using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GestprojectDatabaseConnector
{
    internal class GenerateConnectionString
    {
        internal bool IsSuccessfull { get; set; } = false;
        public GenerateConnectionString()
        {
            try
            {
                string connectionString = "";
                connectionString += $@"Data Source={ConnectionDataHolder.Server.Trim()}\{ConnectionDataHolder.DatabaseInstance.Trim().ToUpper()};";
                connectionString += $"Initial Catalog={ConnectionDataHolder.DatabaseName.Trim()};";
                connectionString += $"Persist Security Info=True;";
                connectionString += $"User ID={ConnectionDataHolder.DatabaseUser.Trim()};";
                connectionString += $"Password={ConnectionDataHolder.DatabasePassword.Trim()};";
                connectionString += $"Connection Timeout=5;";

                ConnectionDataHolder.GestprojectConnectionString = connectionString;

                connectionString = null;

                //MessageBox.Show(ConnectionDataHolder.GestprojectConnectionString);

                IsSuccessfull = true;
            }
            catch(System.Exception e)
            {
                MessageBox.Show($"Error: \n\n{e.ToString()}. \n\nProcederemos a detener la aplicación. Contacte a nuestro servicio de atención al cliente para reportar el error y recibir servicio técnico al respecto.");
            };
        }
    }
}