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
         GestprojectDataManager.GestprojectCustomer client
      ){
         try
         {
            connection.Open();

            string clientInitalSyncronizationStatus = client.synchronization_status;
            string clientInitalComments = client.comments;

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
                     client.synchronization_table_id = Convert.ToInt32(reader.GetValue(0).GetType().Name == "DBNull" ? -1 : reader.GetValue(0));
                     client.last_record = System.Convert.ToDateTime(reader.GetValue(1));
                     client.synchronization_status = Convert.ToString(reader.GetValue(2).GetType().Name == "DBNull" ? "" : reader.GetValue(2));
                     client.parent_gesproject_user_id = Convert.ToInt32(reader.GetValue(3).GetType().Name == "DBNull" ? -1 : reader.GetValue(3));
                     client.sage50_client_code = Convert.ToString(reader.GetValue(4).GetType().Name == "DBNull" ? "" : reader.GetValue(4));
                     client.sage50_guid_id = Convert.ToString(reader.GetValue(5).GetType().Name == "DBNull" ? "" : reader.GetValue(5));
                     client.comments = Convert.ToString(reader.GetValue(6).GetType().Name == "DBNull" ? "" : reader.GetValue(6));
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
                     client.sage50_company_group_name = Convert.ToString(reader.GetValue(0).GetType().Name == "DBNull" ? "" : reader.GetValue(0));
                     client.sage50_company_group_main_code = Convert.ToString(reader.GetValue(1).GetType().Name == "DBNull" ? "" : reader.GetValue(1));
                     client.sage50_company_group_code = Convert.ToString(reader.GetValue(2).GetType().Name == "DBNull" ? "" : reader.GetValue(2));
                     client.sage50_company_group_guid_id = Convert.ToString(reader.GetValue(3).GetType().Name == "DBNull" ? "" : reader.GetValue(3));
                  };
               };
            };

            string clientSynchronizationStatus = "Desincronizado";
            string clientComments = "";
            if(clientInitalSyncronizationStatus != "Sincronizado")
            {
               clientSynchronizationStatus = clientInitalSyncronizationStatus;
               client.synchronization_status = clientSynchronizationStatus;
               clientComments = clientInitalComments;
               client.comments = clientComments;
            } 
            else
            {
               clientSynchronizationStatus = client.synchronization_status;
               clientComments = client.comments;
            };

            if(client.synchronization_status == "") 
            {
               client.synchronization_status = "Desincronizado";
            };

            string sqlString1 = $@"
            UPDATE {ClientSynchronizationTableSchema.TableName} 
            SET 
               {ClientSynchronizationTableSchema.SynchronizationStatusColumn.ColumnDatabaseName}='{clientSynchronizationStatus}',
               {ClientSynchronizationTableSchema.GestprojectClientCountryColumn.ColumnDatabaseName}='{client.PAR_PAIS_1}',
               {ClientSynchronizationTableSchema.GestprojectClientNameColumn.ColumnDatabaseName}='{client.fullName}', 
               {ClientSynchronizationTableSchema.GestprojectClientCIFNIFColumn.ColumnDatabaseName}='{client.PAR_CIF_NIF}',
               {ClientSynchronizationTableSchema.GestprojectClientPostalCodeColumn.ColumnDatabaseName}='{client.PAR_CP_1}',
               {ClientSynchronizationTableSchema.GestprojectClientAddressColumn.ColumnDatabaseName}='{client.PAR_DIRECCION_1}',
               {ClientSynchronizationTableSchema.GestprojectClientProvinceColumn.ColumnDatabaseName}='{client.PAR_PROVINCIA_1}',
               {ClientSynchronizationTableSchema.CommentsColumn.ColumnDatabaseName}='{clientComments}'
            WHERE
               {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName}={client.PAR_ID}
            ;";


            using(SqlCommand sqlCommand = new SqlCommand(sqlString1, connection))
            {
               sqlCommand.ExecuteNonQuery();
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
