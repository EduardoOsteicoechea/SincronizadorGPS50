using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class RegisterUpdatedSage50ClientData
   {
      public RegisterUpdatedSage50ClientData
      (
         System.Data.SqlClient.SqlConnection connection,
         int gestprojectClientId,
         string sage50ClientCode,
         string country,
         string name,
         string cif,
         string postalCode,
         string address,
         string province
      ) 
      {
         try
         {
            connection.Open();

            string sqlString1 = $@"
            UPDATE {ClientSynchronizationTableSchema.TableName} 
            SET 
               {ClientSynchronizationTableSchema.SynchronizationStatusColumn.ColumnDatabaseName}='Sincronizado', 
               {ClientSynchronizationTableSchema.GestprojectClientCountryColumn.ColumnDatabaseName}='{country}', 
               {ClientSynchronizationTableSchema.GestprojectClientNameColumn.ColumnDatabaseName}='{name}', 
               {ClientSynchronizationTableSchema.GestprojectClientCIFNIFColumn.ColumnDatabaseName}='{cif}',
               {ClientSynchronizationTableSchema.GestprojectClientPostalCodeColumn.ColumnDatabaseName}='{postalCode}',
               {ClientSynchronizationTableSchema.GestprojectClientAddressColumn.ColumnDatabaseName}='{address}',
               {ClientSynchronizationTableSchema.GestprojectClientProvinceColumn.ColumnDatabaseName}='{province}',
               {ClientSynchronizationTableSchema.GestprojectClientAccountableSubaccountColumn.ColumnDatabaseName}='{sage50ClientCode}',
               {ClientSynchronizationTableSchema.Sage50ClientCodeColumn.ColumnDatabaseName}='{sage50ClientCode}'
            WHERE
               {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName}={gestprojectClientId}
            ;";


            using(SqlCommand sqlCommand = new SqlCommand(sqlString1, connection))
            {
               sqlCommand.ExecuteNonQuery();
            };

            string sqlString2 = $@"
            UPDATE {"PARTICIPANTE"}
            SET
               {ClientSynchronizationTableSchema.GestprojectClientAccountableSubaccountColumn.ColumnDatabaseName}='{sage50ClientCode}'
            WHERE
               {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName}={gestprojectClientId}
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
