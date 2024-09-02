using System;
using System.Data.SqlClient;
using System.Reflection;

namespace SincronizadorGPS50
{
   public class DeleteEntityFromSynchronizationTable
   {
      public DeleteEntityFromSynchronizationTable
      (
         System.Data.SqlClient.SqlConnection connection,
         int? entityId,
         string entitySageGuid,
         string tableName,
         string entityIdColumnName,
         string entitySage50GuidColumnName
      )
      {
         try
         {
            connection.Open();

            string sqlString = $@"
            DELETE FROM 
               {tableName} 
            WHERE 
               {entityIdColumnName}={entityId}
            AND
               {entitySage50GuidColumnName}='{entitySageGuid}'
            ;";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               sqlCommand.ExecuteNonQuery();
            };
         }
         catch(System.Exception exception)
         {
            throw ApplicationLogger.ReportError(
               MethodBase.GetCurrentMethod().DeclaringType.Namespace,
               MethodBase.GetCurrentMethod().DeclaringType.Name,
               MethodBase.GetCurrentMethod().Name,
               exception
            );
         }
         finally
         {
            connection.Close();
         };
      }
   }
}
