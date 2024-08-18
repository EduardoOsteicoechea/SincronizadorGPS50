using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class PopulateUnsynchronizedClientRegistrationData
   {
      public PopulateUnsynchronizedClientRegistrationData
      (
         System.Data.SqlClient.SqlConnection connection,
         GestprojectDataManager.GestprojectClient client
      ){
         try
         {
            connection.Open();

            string sqlString = $@"
            SELECT 
               {ClientSynchronizationTableSchema.SynchronizationTableClientIdColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.ClientLastUpdateTerminalColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.SynchronizationStatusColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientParentUserIdColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCodeColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientGuidIdColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.CommentsColumn.ColumnDatabaseName}
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
                     client.synchronization_table_id = System.Convert.ToInt32(reader.GetValue(0));
                     client.last_record = System.Convert.ToDateTime(reader.GetValue(1));

                     client.synchronization_status = System.Convert.ToString(reader.GetValue(2)) == "" || Convert.ToString(reader.GetValue(2)) == null || Convert.ToString(reader.GetValue(2)) == null ? "Desincronizado" : System.Convert.ToString(reader.GetValue(2));

                     client.parent_gesproject_user_id = System.Convert.ToInt32(reader.GetValue(3));

                     client.sage50_client_code = System.Convert.ToString(reader.GetValue(4)) == "" || Convert.ToString(reader.GetValue(4)) == null || Convert.ToString(reader.GetValue(4)) == null ? "" : System.Convert.ToString(reader.GetValue(4));

                     client.sage50_guid_id = System.Convert.ToString(reader.GetValue(5)) == "" || Convert.ToString(reader.GetValue(5)) == null || Convert.ToString(reader.GetValue(5)) == null ? "" : System.Convert.ToString(reader.GetValue(5));

                     client.comments = System.Convert.ToString(reader.GetValue(6)) == "" || Convert.ToString(reader.GetValue(6)) == null || Convert.ToString(reader.GetValue(6)) == null ? "" : System.Convert.ToString(reader.GetValue(6));
                  };
               };
            };

            string sqlString2 = $@"
            SELECT 
               SAGE_50_COMPANY_GROUP_NAME,
               SAGE_50_COMPANY_GROUP_MAIN_CODE,
               SAGE_50_COMPANY_GROUP_CODE,
               SAGE_50_COMPANY_GROUP_GUID_ID
            FROM 
               INT_SAGE_USERDATA 
            WHERE 
               GP_USU_ID={client.parent_gesproject_user_id};";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString2, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     client.sage50_company_group_name = System.Convert.ToString(reader.GetValue(0));
                     client.sage50_company_group_main_code = System.Convert.ToString(reader.GetValue(1));
                     client.sage50_company_group_code = System.Convert.ToString(reader.GetValue(2));
                     client.sage50_company_group_guid_id = System.Convert.ToString(reader.GetValue(3));
                  };
               };
            };
         }
         catch(System.Exception exception)
         {
            throw new System.Exception(
               $"At:\n\nSincronizadorGPS50.GestprojectDataManager\n.PopulateUnsynchronizedClientRegistrationData:\n\n{exception.Message}"
            );
         }
         finally
         {
            connection.Close();
         };
      }
   }
}
