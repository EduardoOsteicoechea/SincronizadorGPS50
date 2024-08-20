using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class WasGestprojectClientRegistered
   {
      public bool ItIs { get; set; } = false;
      public int? GP_USU_ID { get; set; } = null;
      public WasGestprojectClientRegistered
      (
         System.Data.SqlClient.SqlConnection connection, 
         GestprojectDataManager.GestprojectCustomer client
         //int? GP_USU_ID
      )
      {
         try
         {
            connection.Open();

            string sqlString = $@"
               SELECT 
                  {ClientSynchronizationTableSchema.Sage50ClientGuidIdColumn.ColumnDatabaseName},
                  {ClientSynchronizationTableSchema.GestprojectClientParentUserIdColumn.ColumnDatabaseName}
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
                     if(reader.GetValue(1).GetType().Name != "DBNull")
                     {
                        GP_USU_ID = Convert.ToInt32(reader.GetValue(1));
                     }
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
