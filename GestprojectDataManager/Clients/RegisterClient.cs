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
         string synchronizationStatus
      )
      {
         try
         {
            connection.Open();

            string sqlString = $@"
            INSERT INTO {ClientSynchronizationTableSchema.TableName} 
            (
               {ClientSynchronizationTableSchema.SynchronizationStatusColumn.ColumnDatabaseName}, 
               {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName}
            ) 
            VALUES 
            (
               '{synchronizationStatus}', 
               {client.PAR_ID}
            );";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               sqlCommand.ExecuteNonQuery();
            };
         }
         catch(SqlException ex)
         {
            MessageBox.Show($"Error during data retrieval: \n\n{ex.Message}");
         }
         finally
         {
            connection.Close();
         };
      }
   }
}
