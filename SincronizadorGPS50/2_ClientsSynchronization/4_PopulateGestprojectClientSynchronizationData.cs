using System.Data.SqlClient;

namespace SincronizadorGPS50.GestprojectDataManager
{
   internal class PopulateGestprojectClientSynchronizationData
   {
      internal PopulateGestprojectClientSynchronizationData
      (
         System.Data.SqlClient.SqlConnection connection,
         GestprojectDataManager.GestprojectClient client,
         string synchronizationStatus
      )
      {
         try
         {
            connection.Open();

            string sqlString = $@"
            SELECT 
               {ClientSynchronizationTableSchema.SynchronizationTableClientIdColumn.ColumnDatabaseName}, 
               synchronization_status, 
               sage50_code, 
               sage50_guid_id 
            FROM 
               {ClientSynchronizationTableSchema.TableName} 
            WHERE 
               {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName}={client.PAR_ID};";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     client.synchronization_table_id = (int)reader.GetValue(0);
                     client.synchronization_status = synchronizationStatus;
                     client.PAR_SUBCTA_CONTABLE = (string)reader.GetValue(2);
                     client.sage50_client_code = (string)reader.GetValue(2);
                     client.sage50_guid_id = (string)reader.GetValue(3);
                  };
               };
            };
         }
         catch(SqlException exception)
         {
            throw exception;
         }
         finally
         {
            connection.Close();
         };
      }
   }
}
