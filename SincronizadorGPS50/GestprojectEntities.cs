using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;

namespace SincronizadorGPS50
{
   internal class GestprojectEntities<T> where T : new()
   {
      public List<T> EntityList { get; set; }
      public List<T> GetAll
      (
         System.Data.SqlClient.SqlConnection connection,
         List<int> selectedIdList,
         string tableName,
         List<(string columName, System.Type columnType)> fieldsToBeRetrieved,
         (string condition1ColumnName, string condition1Value) condition1Data
      ) 
      {
         try
         {
            connection.Open();

            string fieldNamesForSqlStatement = string.Empty;
            for (global::System.Int32 i = 0; i < fieldsToBeRetrieved.Count; i++)
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
               {condition1Data.condition1ColumnName} IN ({condition1Data.condition1Value})
            ;";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     T entity = new T();

                     for (global::System.Int32 i = 0; i < fieldsToBeRetrieved.Count; i++)
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
                           typeof(T).GetProperty(fieldsToBeRetrieved[i].columName).SetValue(entity,scrutinizedValue);
                        }
                        else 
                        {
                           throw new Exception($"Unallowed type \"{reader.GetValue(i).GetType().Name}\" on \"{typeof(T).Name}\", please check the data schema you're using.");
                        };
                     };

                     EntityList.Add(entity);
                  };
               };
            };

            return EntityList;
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
