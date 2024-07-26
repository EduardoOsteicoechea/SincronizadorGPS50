using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectAPI
{
    internal class CreateGestprojectSage50SynchronizationTable
    {
        internal CreateGestprojectSage50SynchronizationTable() 
        {
            using(System.Data.SqlClient.SqlConnection connection = GestprojectDatabase.Connect())
            {
                try
                {
                    connection.Open();

                    string sqlString = @"
                    CREATE TABLE 
                        INT_SAGE_SINC_CLIENTE 
                        (
                            id INT PRIMARY KEY IDENTITY(1,1), 
                            synchronization_status VARCHAR(MAX), 
                            gestproject_id INT, 
                            sage50_code VARCHAR(MAX), 
                            sage50_guid_id VARCHAR(MAX), 
                            sage50_instance VARCHAR(MAX),
                            last_record DATETIME DEFAULT GETDATE() NOT NULL
                        )
                    ;";

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
