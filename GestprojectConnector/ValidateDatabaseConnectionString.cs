using GestprojectConnector;
using System.Windows.Forms;

namespace GestprojectDatabaseConnector
{
    internal class ValidateDatabaseConnectionString
    {
        internal bool IsSuccessfull { get; set; } = false;
        public ValidateDatabaseConnectionString()
        {
            try
            {
                using(System.Data.SqlClient.SqlConnection testConnection = new System.Data.SqlClient.SqlConnection(ConnectionDataHolder.GestprojectConnectionString))
                {
                    testConnection.Open();
                    testConnection.Close();
                };

                IsSuccessfull = true;

                GestprojectConnectionString.ConnectionString = ConnectionDataHolder.GestprojectConnectionString;
            }
            catch(System.Data.SqlClient.SqlException e)
            {
                MessageBox.Show($"Error: \n\n{e.ToString()}. \n\nProcederemos a detener la aplicación. Contacte a nuestro servicio de atención al cliente para reportar el error y recibir servicio técnico al respecto.");
            };
        }
    }
}