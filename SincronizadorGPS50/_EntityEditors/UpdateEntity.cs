using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   public class UpdateEntity
   {
      public UpdateEntity
      (
         System.Data.SqlClient.SqlConnection connection,
         string tableName,
         List<(string columnName, dynamic columnValue)> columnsAndValues,
         (string columnName, dynamic columnValue) conditionKeyValuePair
      )
      {
         try
         {
            connection.Open();

            StringBuilder columnsAndValuesStringBuilder = new StringBuilder();

            for(global::System.Int32 i = 0; i < columnsAndValues.Count; i++)
            {
               string name = columnsAndValues[i].columnName;
               dynamic value = columnsAndValues[i].columnValue;

               columnsAndValuesStringBuilder.Append($"{name}={DynamicValuesFormatters.Formatters[value.GetType()](value)},");
            };

            StringBuilder conditionStringBuilder = new StringBuilder();
            conditionStringBuilder.Append($"{conditionKeyValuePair.columnName}={DynamicValuesFormatters.Formatters[conditionKeyValuePair.columnValue.GetType()](conditionKeyValuePair.columnValue)}");

            string sqlString = $@"
            UPDATE 
               {tableName} 
            SET
               {columnsAndValuesStringBuilder.ToString().TrimEnd(',')}
            WHERE
               {conditionStringBuilder.ToString()}
            ;";

            //MessageBox.Show(sqlString);

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
