using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50.GestprojectAPI
{
    internal class CheckIfGestprojectClientWasSynchronized
    {
        internal bool ItIs {  get; set; } = false;
        internal CheckIfGestprojectClientWasSynchronized(GestprojectClient client) 
        {
            string sqlString = $"SELECT sage50_guid_id FROM INT_SAGE_SINC_CLIENTE WHERE gestproject_id={client.PAR_ID};";

            SqlCommand sqlCommand = new SqlCommand(sqlString, DataHolder.GestprojectSQLConnection);

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
        }
    }
}
