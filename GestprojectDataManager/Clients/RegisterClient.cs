using System.Data.SqlClient;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class RegisterClient
   {
      public RegisterClient
      (
         System.Data.SqlClient.SqlConnection connection, 
         GestprojectDataManager.GestprojectCustomer client, 
         string synchronizationStatus,
         string companyGroupName,
         string companyGroupMainCode,
         string companyGroupCode,
         string companyGroupGuid

      )
      {
         try
         {
            connection.Open();
            string sqlString2 = $@"
            INSERT INTO {ClientSynchronizationTableSchema.TableName} 
            (
               {ClientSynchronizationTableSchema.SynchronizationStatusColumn.ColumnDatabaseName}, 
               {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientNameColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientCIFNIFColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientAddressColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientPostalCodeColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientLocalityColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientProvinceColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientCountryColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupNameColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupMainCodeColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupCodeColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupGuidIdColumn.ColumnDatabaseName}
            ) 
            VALUES 
            (
               '{synchronizationStatus}',
               {client.PAR_ID},
               '{client.fullName}',
               '{client.PAR_CIF_NIF}', 
               '{client.PAR_DIRECCION_1}', 
               '{client.PAR_CP_1}', 
               '{client.PAR_LOCALIDAD_1}', 
               '{client.PAR_PROVINCIA_1}', 
               '{client.PAR_PAIS_1}',               
               '{companyGroupName}',
               '{companyGroupMainCode}',
               '{companyGroupCode}',
               '{companyGroupGuid}'
            );";

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
