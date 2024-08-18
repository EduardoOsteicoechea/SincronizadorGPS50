using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class RegisterNewSage50ClientData
   {
      public RegisterNewSage50ClientData
      (
         System.Data.SqlClient.SqlConnection connection,
         int gestprojectClientId,
         string newSage50ClientCode,
         string newSage50ClientGuidId
      ) 
      {
         try
         {
            connection.Open();

            string sqlString1 = $@"
            UPDATE {ClientSynchronizationTableSchema.TableName} 
            SET 
               {ClientSynchronizationTableSchema.SynchronizationStatusColumn.ColumnDatabaseName}='Sincronizado', 
               {ClientSynchronizationTableSchema.Sage50ClientCodeColumn.ColumnDatabaseName}='{newSage50ClientCode}', 
               {ClientSynchronizationTableSchema.GestprojectClientAccountableSubaccountColumn.ColumnDatabaseName}='{newSage50ClientCode}', 
               {ClientSynchronizationTableSchema.Sage50ClientGuidIdColumn.ColumnDatabaseName}='{newSage50ClientGuidId}'
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
               {ClientSynchronizationTableSchema.GestprojectClientAccountableSubaccountColumn.ColumnDatabaseName}='{newSage50ClientCode}'
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
