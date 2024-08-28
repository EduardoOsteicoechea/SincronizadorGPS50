
using System.Data;

namespace SincronizadorGPS50
{
   public class ProvidersDataTableManager : IGridDataSourceGenerator
   {
      public DataTable DataSource { get; set; }
      public System.Data.DataTable GenerateDataTable
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider synchronizationTableSchemaProvider
      ) 
      {
         DataTable dataTable = new DataTable();

         foreach (var item in synchronizationTableSchemaProvider.ColumnsTuplesList)
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
