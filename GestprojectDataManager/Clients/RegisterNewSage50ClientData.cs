using System.Data.SqlClient;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class RegisterNewSage50ClientData
   {
      public RegisterNewSage50ClientData
      (
         System.Data.SqlClient.SqlConnection connection,
         int? gestprojectClientId,
         string newSage50ClientCode,
         string newSage50ClientGuidId,
         string companyGroupName,
         string companyGroupMainCode,
         string companyGroupCode,
         string companyGroupGuid,
         int? parentUserId,
         CustomerSyncronizationTableSchema tableSchema
      ) 
      {
         try
         {
            connection.Open();

            string sqlString1 = $@"
            UPDATE {tableSchema.TableName} 
            SET 
               {tableSchema.SynchronizationStatusColumn.ColumnDatabaseName}='Sincronizado', 
               {tableSchema.Sage50ClientCodeColumn.ColumnDatabaseName}='{newSage50ClientCode}', 
               {tableSchema.GestprojectClientAccountableSubaccountColumn.ColumnDatabaseName}='{newSage50ClientCode}', 
               {tableSchema.Sage50ClientGuidIdColumn.ColumnDatabaseName}='{newSage50ClientGuidId}',
               {tableSchema.Sage50ClientCompanyGroupNameColumn.ColumnDatabaseName}='{companyGroupName}',
               {tableSchema.Sage50ClientCompanyGroupMainCodeColumn.ColumnDatabaseName}='{companyGroupMainCode}',
               {tableSchema.Sage50ClientCompanyGroupCodeColumn.ColumnDatabaseName}='{companyGroupCode}',
               {tableSchema.Sage50ClientCompanyGroupGuidIdColumn.ColumnDatabaseName}='{companyGroupGuid}',
               {tableSchema.GestprojectClientParentUserIdColumn.ColumnDatabaseName}={parentUserId}
            WHERE
               {tableSchema.GestprojectClientIdColumn.ColumnDatabaseName}={gestprojectClientId}
            ;";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString1, connection))
            {
               sqlCommand.ExecuteNonQuery();
            };

            string sqlString2 = $@"
            UPDATE {"PARTICIPANTE"}
            SET
               {tableSchema.GestprojectClientAccountableSubaccountColumn.ColumnDatabaseName}='{newSage50ClientCode}'
            WHERE
               {tableSchema.GestprojectClientIdColumn.ColumnDatabaseName}={gestprojectClientId}
            ;";

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
