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
         List<(string columName, System.Type columnType)> fieldsToBeRetrieved,
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
               fieldNamesForSqlStatement += $"{fieldsToBeRetrieved[i].columName},";
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

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     Entity = new T();

                     for(global::System.Int32 i = 0; i < fieldsToBeRetrieved.Count; i++)
                     {
                        if(fieldsToBeRetrieved[i].columnType == typeof(int))
                        {
                           var scrutinizedValue = TypeProtector<int>.Scrutinize(reader, i, 0);
                           typeof(T).GetProperty(fieldsToBeRetrieved[i].columName).SetValue(entity, scrutinizedValue);
                        }
                        else if(fieldsToBeRetrieved[i].columnType == typeof(string))
                        {
                           var scrutinizedValue = TypeProtector<string>.Scrutinize(reader, i, string.Empty);
                           typeof(T).GetProperty(fieldsToBeRetrieved[i].columName).SetValue(entity, scrutinizedValue);
                        }
                        else if(fieldsToBeRetrieved[i].columnType == typeof(DateTime))
                        {
                           var scrutinizedValue = TypeProtector<DateTime>.Scrutinize(reader, i, DateTime.Now);
                           typeof(T).GetProperty(fieldsToBeRetrieved[i].columName).SetValue(entity, scrutinizedValue);
                        }
                        else
                        {
                           throw new Exception($"Unallowed type \"{reader.GetValue(i).GetType().Name}\" on \"{typeof(T).Name}\", please check the data schema you're using.");
                        };
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
