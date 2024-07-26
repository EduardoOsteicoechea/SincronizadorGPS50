using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectAPI
{
    internal class RegisterClient
    {
        public RegisterClient( GestprojectClient client, string synchronizationStatus ) 
        {
            using(System.Data.SqlClient.SqlConnection connection = GestprojectDatabase.Connect())
            {
                try
                {
                    connection.Open();

                    string sqlString = @"
                    INSERT INTO 
                        INT_SAGE_SINC_CLIENTE 
                        (
                            synchronization_status, 
                            gestproject_id, 
                            sage50_code, 
                            sage50_guid_id, 
                            sage50_instance
                        ) 
                    VALUES " + $"('{synchronizationStatus}', {client.PAR_ID}, '{client.sage50_client_code}', '{client.sage50_guid_id}', '{client.sage50_instance}');";

                    using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
                    {
                        sqlCommand.ExecuteNonQuery();
                    };
                }
                catch(SqlException ex)
                {
                    MessageBox.Show($"Error during data retrieval: \n\n{ex.Message}");
                }
                finally
                {
                    connection.Close();
                };
            };
        }
    }
}
