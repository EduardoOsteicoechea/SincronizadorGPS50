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
         bool isSynchronized
      ) 
      {
         try
         {
            connection.Open();

            string synchronizationStatus = isSynchronized ? "Sincronizado" : "Desincronizado";

            string sqlString1 = $@"
            UPDATE 
               {ClientSynchronizationTableSchema.TableName} 
            SET 
               {ClientSynchronizationTableSchema.SynchronizationStatusColumn.ColumnDatabaseName}='{synchronizationStatus}',
               {ClientSynchronizationTableSchema.CommentsColumn.ColumnDatabaseName}=''
            WHERE
               {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName}={gestprojectClientId}
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
