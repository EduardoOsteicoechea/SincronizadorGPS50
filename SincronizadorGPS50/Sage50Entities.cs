using sage.ew.db;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace SincronizadorGPS50
{
   internal class Sage50Entities<T> where T : new()
   {
      public List<T> EntityList { get; set; }
      public List<T> GetAll
      (
         string tableName,
         List<(string columName, System.Type columnType)> fieldsToBeRetrieved
      ) 
      {
         try
         {
            string fieldNamesForSqlStatement = string.Empty;
            for(global::System.Int32 i = 0; i < fieldsToBeRetrieved.Count; i++)
            {
               fieldNamesForSqlStatement += $"{fieldsToBeRetrieved[i].columName},";
            };
            fieldNamesForSqlStatement = fieldNamesForSqlStatement.TrimEnd(',');

            string getSage50ProviderSQLQuery = $@"
            SELECT 
               {fieldNamesForSqlStatement}
            FROM 
               {DB.SQLDatabase("gestion",tableName)}";

            DataTable entityDataTable = new DataTable();

            DB.SQLExec(getSage50ProviderSQLQuery, ref entityDataTable);

            if(entityDataTable.Rows.Count > 0)
            {
               for(int i = 0; i < entityDataTable.Rows.Count; i++)
               {
                  T entity = new T();

                  for(global::System.Int32 j = 0; j < fieldsToBeRetrieved.Count; j++)
                  {
                     var entityColumnValue = entityDataTable.Rows[i].ItemArray[j].ToString().Trim();
                     typeof(T).GetProperty(fieldsToBeRetrieved[j].columName).SetValue(entity, entityColumnValue);
                  };

                  EntityList.Add(entity);
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
         };
      }
   }
}
