using System.Data.SqlClient;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class WasGestprojectClientRegistered
   {
      public bool ItIs { get; set; } = false;
      public int? GP_USU_ID { get; set; } = null;
      public WasGestprojectClientRegistered
      (
         System.Data.SqlClient.SqlConnection connection, 
         GestprojectDataManager.GestprojectCustomer client,
         string currentCompanyGroupGuid
      )
      {
         try
         {
            connection.Open();

            string whereClause = "";

            if(client.sage50_guid_id != "" && client.sage50_guid_id != null) 
            {
               whereClause = $@"
                  {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName}={client.PAR_ID}
                  AND
                  {ClientSynchronizationTableSchema.Sage50ClientGuidIdColumn.ColumnDatabaseName}='{client.sage50_guid_id}'
               ";
            }
            else 
            {
               whereClause = $@"
               {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName}={client.PAR_ID}
               AND
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupGuidIdColumn.ColumnDatabaseName}='{currentCompanyGroupGuid}'
               ";
            };

            string sqlString = $@"
               SELECT 
                  {ClientSynchronizationTableSchema.SynchronizationTableClientIdColumn.ColumnDatabaseName}
               FROM 
                  {ClientSynchronizationTableSchema.TableName}
               WHERE 
                  {whereClause}
            ;";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     if(reader.GetValue(0).GetType().Name != "DBNull")
                     {
                        ItIs = true;
                        break;
                     }
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
