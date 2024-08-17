using System.Data.SqlClient;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class WasGestprojectClientRegistered
   {
      public bool ItIs { get; set; } = false;
      public WasGestprojectClientRegistered
      (
         System.Data.SqlClient.SqlConnection connection, 
         GestprojectDataManager.GestprojectClient client
      ){
         try
         {
            connection.Open();

            string sqlString = $@"
               SELECT 
                  {ClientSynchronizationTableSchema.Sage50ClientGuidIdColumn.ColumnDatabaseName}
               FROM 
                  {ClientSynchronizationTableSchema.TableName}
               WHERE 
                  {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName}={client.PAR_ID}
            ;";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     ItIs = true;
                     break;
                  };
               };
            };
         }
         catch(System.Exception exception)
         {
            throw new System.Exception(
               $"At:\n\nSincronizadorGPS50.GestprojectDataManager\n.WasGestprojectClientRegistered:\n\n{exception.Message}"
            );
         }
         finally
         {
            connection.Close();
         };
      }
   }
}
