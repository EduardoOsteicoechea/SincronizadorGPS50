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
    internal class RegisterClientAsDesynchronized
    {
        public RegisterClientAsDesynchronized
        (
            GestprojectClient client
        )
        {
            string synchronizationStatus = "Desincronizado";

            string sqlString = $"UPDATE INT_SAGE_SINC_CLIENTE_IMAGEN SET synchronization_status='{synchronizationStatus}';";

            using(SqlCommand SQLCommand = new SqlCommand(sqlString, DataHolder.GestprojectSQLConnection))
            {
                SQLCommand.ExecuteNonQuery();
            };

            string sqlString2 = $"UPDATE INT_SAGE_SINC_CLIENTE SET synchronization_status='{synchronizationStatus}';";

            using(SqlCommand SQLCommand = new SqlCommand(sqlString2, DataHolder.GestprojectSQLConnection))
            {
                SQLCommand.ExecuteNonQuery();
            };
        }
    }
}
