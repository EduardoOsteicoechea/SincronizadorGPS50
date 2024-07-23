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
            string checkIfSage50SincronizationTableExistsSQLQuery = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE \"TABLE_NAME\" = '{tableName}'";

            using(
                SqlCommand checkIfSage50SincronizationTableExistsSQLCommand =
                new SqlCommand(
                    checkIfSage50SincronizationTableExistsSQLQuery,
                    DataHolder.GestprojectSQLConnection
                )
            )
            {
                int? Sage50SincronizationTableCount = (int)checkIfSage50SincronizationTableExistsSQLCommand.ExecuteScalar();
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
    }
}
