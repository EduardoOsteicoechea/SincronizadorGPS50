using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class UpdateClientSyncronizationStatus
   {
      public UpdateClientSyncronizationStatus
      (
         System.Data.SqlClient.SqlConnection connection,
         int gestprojectClientId,
         string sage50ClientGuid,
         string sage50CompanyGroupGuid,
         bool isSynchronized
      ) 
      {
         try
         {
            connection.Open();

            string synchronizationStatus = isSynchronized ? "Sincronizado" : "Desincronizado";

            string whereClause = "";
            if(sage50ClientGuid != null && sage50ClientGuid != "") 
            {
               whereClause = $@"
               {ClientSynchronizationTableSchema.Sage50ClientGuidIdColumn.ColumnDatabaseName}='{sage50ClientGuid}'
               AND
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupGuidIdColumn.ColumnDatabaseName}='{sage50CompanyGroupGuid}'
               ";
            } 
            else
            {
               whereClause = $@"
               {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName}={gestprojectClientId}
               AND
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupGuidIdColumn.ColumnDatabaseName}='{sage50CompanyGroupGuid}'
               ";
            };

            string sqlString1 = $@"
            UPDATE 
               {ClientSynchronizationTableSchema.TableName} 
            SET 
               {ClientSynchronizationTableSchema.SynchronizationStatusColumn.ColumnDatabaseName}='{synchronizationStatus}',
               {ClientSynchronizationTableSchema.CommentsColumn.ColumnDatabaseName}=''
            WHERE
               {whereClause}
            ;";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString1, connection))
            {
               sqlCommand.ExecuteNonQuery();
            };
         }
         catch(System.Exception exception)
         {
            throw new System.Exception(
               $"At:\n\nSincronizadorGPS50.GestprojectDataManager\n.UpdateClientSyncronizationStatus:\n\n{exception.Message}"
            );
         }
         finally
         {
            connection.Close();
         };
      }
   }
}
