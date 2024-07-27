using System.Data.SqlClient;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
    internal class WasGestprojectClientSynchronized
    {
        internal bool ItIs {  get; set; } = false;
        internal WasGestprojectClientSynchronized(GestprojectClient client) 
        {
            using(System.Data.SqlClient.SqlConnection connection = GestprojectDatabase.Connect())
            {
                try
                {
                    connection.Open();

                    string sqlString = $"SELECT sage50_guid_id FROM INT_SAGE_SINC_CLIENTE WHERE gestproject_id={client.PAR_ID};";

                    using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
                    {
                        using(SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                if((string)reader.GetValue(0) != "" && (string)reader.GetValue(0) != null)
                                {
                                    ItIs = true;
                                    break;
                                }
                            };
                        };
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
