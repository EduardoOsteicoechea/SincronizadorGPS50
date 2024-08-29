using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
   internal class ProvidersDataTableGenerator : IDataTableGenerator
   {
      public DataTable CreateDataTable(List<(string columnName, string friendlyName, Type columnType, string columnDefinition)> tableFieldsTupleList)
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
   }
}
