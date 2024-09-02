using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace SincronizadorGPS50
{
   internal class ProvidersDataTableGenerator : IDataTableGenerator
   {
      public DataTable CreateDataTable(List<(string columnName, string friendlyName, Type columnType, string columnDefinition)> tableFieldsTupleList)
      {
         try
         {
            DataTable dataTable = new DataTable();

            foreach(var item in tableFieldsTupleList)
            {
               string columnName = item.friendlyName;
               System.Type columnType = item.columnType;

               dataTable.Columns.Add(
                  columnName,
                  columnType
               );
            };

            return dataTable;
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
