using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectAPI
{
    internal class CheckIfTableExistsOnGestproject
    {
        internal bool Exists { get; set; } = false;
        internal CheckIfTableExistsOnGestproject(string tableName)
        {
            using(System.Data.SqlClient.SqlConnection connection = GestprojectDatabase.Connect())
            {
                try
                {
                    connection.Open();

                    string sqlString = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE \"TABLE_NAME\" = '{tableName}'";

                    using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
                    {
                        int? Sage50SincronizationTableCount = (int)sqlCommand.ExecuteScalar();
                        if(Sage50SincronizationTableCount != null)
                        {
                            Exists = Sage50SincronizationTableCount > 0;
                        }
                        else
                        {
                            MessageBox.Show($"La búsqueda de la tabla \"{tableName}\" retornó un valor nulo.");
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
