using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class RegisterClient
   {
      public RegisterClient
      (
         System.Data.SqlClient.SqlConnection connection, 
         GestprojectDataManager.GestprojectClient client, 
         string synchronizationStatus,
         int? GP_USU_ID
      )
      {
         try
         {
            connection.Open();

            string sqlString1 = $@"
            SELECT 
               SAGE_50_COMPANY_GROUP_NAME,
               SAGE_50_COMPANY_GROUP_MAIN_CODE,
               SAGE_50_COMPANY_GROUP_CODE,
               SAGE_50_COMPANY_GROUP_GUID_ID
            FROM 
               INT_SAGE_USERDATA 
            WHERE 
               GP_USU_ID={GP_USU_ID};";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString1, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     client.parent_gesproject_user_id = GP_USU_ID;
                     client.sage50_company_group_name = System.Convert.ToString(reader.GetValue(0));
                     client.sage50_company_group_main_code = System.Convert.ToString(reader.GetValue(1));
                     client.sage50_company_group_code = System.Convert.ToString(reader.GetValue(2));
                     client.sage50_company_group_guid_id = System.Convert.ToString(reader.GetValue(3));
                  };
               };
            };

            string sqlString2 = $@"
            INSERT INTO {ClientSynchronizationTableSchema.TableName} 
            (
               {ClientSynchronizationTableSchema.SynchronizationStatusColumn.ColumnDatabaseName}, 
               {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupNameColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupMainCodeColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupCodeColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupGuidIdColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientParentUserIdColumn.ColumnDatabaseName}
            ) 
            VALUES 
            (
               '{synchronizationStatus}', 
               {client.PAR_ID},
               '{client.sage50_company_group_name}', 
               '{client.sage50_company_group_main_code}', 
               '{client.sage50_company_group_code}', 
               '{client.sage50_company_group_guid_id}', 
               {client.parent_gesproject_user_id} 
            );";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString2, connection))
            {
               sqlCommand.ExecuteNonQuery();
            };
         }
         catch(SqlException exception)
         {
            throw new System.Exception(
               $"At:\n\nSincronizadorGPS50.GestprojectDataManager\n.RegisterClient:\n\n{exception.Message}"
            );
         }
         finally
         {
            connection.Close();
         };
      }
   }
}
