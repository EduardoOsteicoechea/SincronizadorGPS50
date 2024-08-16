using System;
using System.Data.SqlClient;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class PopulateUnsynchronizedClientRegistrationData
   {
      public PopulateUnsynchronizedClientRegistrationData
      (
         System.Data.SqlClient.SqlConnection connection,
         GestprojectDataManager.GestprojectClient client,
         string synchronizationStatus,
         int? GP_USU_ID
      ){
         try
         {
            connection.Open();

            string sqlString = $@"
            SELECT 
               {ClientSynchronizationTableSchema.SynchronizationTableClientIdColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.ClientLastUpdateTerminalColumn.ColumnDatabaseName}
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
                     client.last_record = (DateTime)reader.GetValue(1);
                  };
               };
            };
            // Check here
            string sqlString2 = $@"
            SELECT 
               SAGE_50_COMPANY_GROUP_NAME,
               SAGE_50_COMPANY_GROUP_MAIN_CODE,
               SAGE_50_COMPANY_GROUP_CODE,
               SAGE_50_COMPANY_GROUP_GUID_ID
            FROM 
               INT_SAGE_USERDATA 
            WHERE 
               GP_USU_ID={GP_USU_ID};";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString2, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     client.parent_gesproject_user_id = GP_USU_ID;
                     client.sage50_company_group_name = (string)reader.GetValue(0);
                     client.sage50_company_group_main_code = (string)reader.GetValue(1);
                     client.sage50_company_group_code = (string)reader.GetValue(2);
                     client.sage50_company_group_guid_id = (string)reader.GetValue(3);
                  };
               };
            };

            client.synchronization_status = synchronizationStatus;
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
