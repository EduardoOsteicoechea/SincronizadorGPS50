using System.Data.SqlClient;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class WasGestprojectClientSynchronized
   {
      public bool ItIs { get; set; } = false;
      public WasGestprojectClientSynchronized
      (
         System.Data.SqlClient.SqlConnection connection,
         GestprojectDataManager.GestprojectClient client
      )
      {
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
            ";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     if(reader.GetValue(0).GetType().Name != "DBNull" || System.Convert.ToString(reader.GetValue(0)) != "")
                     {
                        ItIs = true;
                        break;
                     }
                  };
               };
            };
         }
         catch(SqlException exception)
         {
            throw new System.Exception(
               $"At:\n\nSincronizadorGPS50\n.GestprojectDataManager\n.WasGestprojectClientSynchronized:\n\n{exception.Message}"
            );
         }
         finally
         {
            connection.Close();
         };
      }
   }
}
