using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   public class EntitySynchronizationTable<T> where T : new()
   {
      T Entity { get; set; }
      public T AppendTableDataToEntity
      (
         SqlConnection connection,
         string tableName,
         List<(string columnName, System.Type columnType)> fieldsToBeRetrieved,
         (string condition1ColumnName, dynamic condition1Value) condition1Data,
         T entity
      )
      {
         try
         {
            connection.Open();

            string fieldNamesForSqlStatement = string.Empty;
            for(global::System.Int32 i = 0; i < fieldsToBeRetrieved.Count; i++)
            {
               fieldNamesForSqlStatement += $"{fieldsToBeRetrieved[i].columnName},";
            };
            fieldNamesForSqlStatement = fieldNamesForSqlStatement.TrimEnd(',');

            string sqlString = $@"
            SELECT
               {fieldNamesForSqlStatement}
            FROM
               {tableName}
            WHERE
               {condition1Data.condition1ColumnName}={DynamicValuesFormatters.Formatters[condition1Data.condition1Value.GetType()](condition1Data.condition1Value)}
            ;";

            //MessageBox.Show(sqlString);

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     Entity = new T(); 

                     PropertyInfo[] properties = Entity.GetType().GetProperties();

                     for(global::System.Int32 i = 0; i < fieldsToBeRetrieved.Count; i++)
                     {
                        TypeRevisor<T>.Check(
                           fieldsToBeRetrieved[i].columnType,
                           fieldsToBeRetrieved[i].columnName,
                           Entity,
                           reader,
                           i,
                           properties
                        );
                     };
                  };
               };
            };

            return Entity;
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
