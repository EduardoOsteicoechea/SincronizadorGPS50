

namespace SincronizadorGPS50
{
   internal interface IGridDataSourceGenerator
   {
      System.Data.DataTable GenerateDataTable
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider synchronizationTableSchemaProvider
      );
      
   }
}
