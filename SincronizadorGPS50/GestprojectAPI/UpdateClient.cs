using System.Data.SqlClient;

namespace SincronizadorGPS50.GestprojectAPI
{
    internal class UpdateClient
    {
        public UpdateClient( GestprojectClient client, string synchronizationStatus ) 
        {
            string sqlString2 = $"UPDATE INT_SAGE_SINC_CLIENTE SET synchronization_status='{synchronizationStatus}', sage50_code='{client.sage50_client_code}', sage50_guid_id='{client.sage50_guid_id}', sage50_instance='{client.sage50_instance}' WHERE gestproject_id={client.PAR_ID};";

            using(SqlCommand SQLCommand = new SqlCommand(sqlString2, DataHolder.GestprojectSQLConnection))
            {
                SQLCommand.ExecuteNonQuery();
            };
        }
    }
}
