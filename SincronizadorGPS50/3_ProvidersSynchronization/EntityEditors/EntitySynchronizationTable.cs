using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;

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
         (string condition1ColumnName, int? condition1Value) condition1Data
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
               {condition1Data.condition1ColumnName} IN ({condition1Data.condition1Value})
            ;";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  //int c = 0;
                  while(reader.Read())
                  {
                     Entity = new T();

                     //Type type = GetType();
                     //PropertyInfo[] properties = type.GetProperties();
                     //string aa = "";
                     //foreach(PropertyInfo property in properties)
                     //{
                     //   object value = property.GetValue(Entity);

                     //   aa += $"{property.Name}: {value}";
                     //}


                     for(global::System.Int32 i = 0; i < fieldsToBeRetrieved.Count; i++)
                     {
                        if(fieldsToBeRetrieved[i].columnType == typeof(int))
                        {
                           var scrutinizedValue = TypeProtector<int>.Scrutinize(reader, i, 0);
                           typeof(T).GetProperty(fieldsToBeRetrieved[i].columName).SetValue(Entity, scrutinizedValue);
                        }
                        else if(fieldsToBeRetrieved[i].columnType == typeof(string))
                        {
                           var scrutinizedValue = TypeProtector<string>.Scrutinize(reader, i, string.Empty);
                           typeof(T).GetProperty(fieldsToBeRetrieved[i].columName).SetValue(Entity, scrutinizedValue);
                        }
                        else if(fieldsToBeRetrieved[i].columnType == typeof(DateTime))
                        {
                           var scrutinizedValue = TypeProtector<DateTime>.Scrutinize(reader, i, DateTime.Now);
                           typeof(T).GetProperty(fieldsToBeRetrieved[i].columName).SetValue(Entity, scrutinizedValue);
                        }
                        else
                        {
                           throw new Exception($"Unallowed type \"{reader.GetValue(i).GetType().Name}\" on \"{typeof(T).Name}\", please check the data schema you're using.");
                        };
                     };
                     //c++;
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
