using System.Data.SqlClient;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class CreateClientSynchronizationTable
   {
      public CreateClientSynchronizationTable(System.Data.SqlClient.SqlConnection connection)
      {
         try
         {
            connection.Open();

            string sqlString = $@"
            CREATE TABLE {ClientSynchronizationTableSchema.TableName} 
               (
                  {ClientSynchronizationTableSchema.SynchronizationTableClientIdColumn.ColumnDatabaseName} INT PRIMARY KEY IDENTITY(1,1), 
                  {ClientSynchronizationTableSchema.SynchronizationStatusColumn.ColumnDatabaseName} VARCHAR(MAX), 
                  {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName} INT, 
                  {ClientSynchronizationTableSchema.GestprojectClientAccountableSubaccountColumn.ColumnDatabaseName} VARCHAR(MAX),
                  {ClientSynchronizationTableSchema.GestprojectClientNameColumn.ColumnDatabaseName} VARCHAR(MAX),
                  {ClientSynchronizationTableSchema.GestprojectClientCommercialNameColumn.ColumnDatabaseName} VARCHAR(MAX),
                  {ClientSynchronizationTableSchema.GestprojectClientCIFNIFColumn.ColumnDatabaseName} VARCHAR(MAX),
                  {ClientSynchronizationTableSchema.GestprojectClientAddressColumn.ColumnDatabaseName} VARCHAR(MAX),
                  {ClientSynchronizationTableSchema.GestprojectClientPostalCodeColumn.ColumnDatabaseName} VARCHAR(MAX),
                  {ClientSynchronizationTableSchema.GestprojectClientLocalityColumn.ColumnDatabaseName} VARCHAR(MAX),
                  {ClientSynchronizationTableSchema.GestprojectClientProvinceColumn.ColumnDatabaseName} VARCHAR(MAX),
                  {ClientSynchronizationTableSchema.GestprojectClientCountryColumn.ColumnDatabaseName} VARCHAR(MAX),
                  {ClientSynchronizationTableSchema.Sage50ClientCodeColumn.ColumnDatabaseName} VARCHAR(MAX), 
                  {ClientSynchronizationTableSchema.Sage50ClientGuidIdColumn.ColumnDatabaseName} VARCHAR(MAX), 
                  {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupNameColumn.ColumnDatabaseName} VARCHAR(MAX), 
                  {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupCodeColumn.ColumnDatabaseName} VARCHAR(MAX), 
                  {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupMainCodeColumn.ColumnDatabaseName} VARCHAR(MAX), 
                  {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupGuidIdColumn.ColumnDatabaseName} VARCHAR(MAX), 
                  {ClientSynchronizationTableSchema.CommentsColumn.ColumnDatabaseName} VARCHAR(MAX),
                  {ClientSynchronizationTableSchema.GestprojectClientParentUserIdColumn.ColumnDatabaseName} VARCHAR(MAX),
                  {ClientSynchronizationTableSchema.ClientLastUpdateTerminalColumn.ColumnDatabaseName} DATETIME DEFAULT GETDATE() NOT NULL
               )
            ;";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               sqlCommand.ExecuteNonQuery();
            };
         }
         catch(SqlException exception)
         {
            throw new System.Exception(
               $"At:\n\nSincronizadorGPS50.GestprojectDataManager\n.CreateClientSynchronizationTable:\n\n{exception.Message}"
            );
         }
         finally
         {
            connection.Close();
         };
      }
   }
}
