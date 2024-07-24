using System;
using System.Data.SqlClient;

namespace SincronizadorGPS50.GestprojectAPI
{
    internal class RegisterClient
    {
        public RegisterClient( GestprojectClient client, string synchronizationStatus ) 
        {
            string sqlString2 = @"
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

            using(SqlCommand SQLCommand = new SqlCommand(sqlString2, DataHolder.GestprojectSQLConnection))
            {
                SQLCommand.ExecuteNonQuery();
            };
        }
    }
}
