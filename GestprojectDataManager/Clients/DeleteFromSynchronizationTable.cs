using System.Data.SqlClient;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class DeleteFromSynchronizationTable
   {
      public DeleteFromSynchronizationTable
      (
         System.Data.SqlClient.SqlConnection connection,
         GestprojectCustomer customer
      )
      {
         try
         {
            connection.Open();

            string sqlString = $@"
            DELETE FROM 
               {ClientSynchronizationTableSchema.TableName} 
            WHERE 
               {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName}={customer.PAR_ID}
            AND
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupGuidIdColumn.ColumnDatabaseName}='{customer.sage50_company_group_guid_id}'
            ;";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               sqlCommand.ExecuteNonQuery();
            };
         }
         catch(System.Exception exception)
         {
            throw new System.Exception(
               $"At:\n\nSincronizadorGPS50.GestprojectDataManager\n.DeleteFromSynchronizationTable:\n\n{exception.Message}"
            );
         }
         finally
         {
            connection.Close();
         };
      }
   }
}
