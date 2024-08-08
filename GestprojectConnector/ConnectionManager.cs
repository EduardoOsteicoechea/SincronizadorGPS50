using GestprojectConnector;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GestprojectDatabaseConnector
{
    public class ConnectionManager
    {
        public bool IsSuccessful { get; set; } = false;

        public System.Data.SqlClient.SqlConnection Connect()
        {
            try
            {
                if(!new GetLocalDeviceData().IsSuccessfull)
                {
                    throw new System.Exception("Error getting local device data");
                };

                if(!new GetConnectionData().IsSuccessfull)
                {
                    throw new System.Exception("Error getting connection data");
                };

                if(!new GenerateConnectionString().IsSuccessfull)
                {
                    throw new System.Exception("Error generating connection string");
                };

                if(!new ValidateDatabaseConnectionString().IsSuccessfull)
                {
                    throw new System.Exception("Error connecting to database");
                };

                ConnectionDataHolder.DisposeSensitiveData();

                IsSuccessful = true;

                return new SqlConnection(GestprojectConnectionString.ConnectionString);
            }
            catch (System.Exception exception)
            {
                ConnectionDataHolder.DisposeSensitiveData();
                MessageBox.Show($"Error: \n\n{exception.Message}");
                return null;
            };
        }
    }
}
