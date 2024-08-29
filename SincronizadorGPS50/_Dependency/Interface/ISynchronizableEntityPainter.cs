using SincronizadorGPS50.GestprojectDataManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
   internal interface ISynchronizableEntityPainter
   {
      void PaintEntityListOnDataTable(
         List<GestprojectProviderModel> proccessedGestprojectProviders,
         DataTable dataTable,
         List<(string columnName, string friendlyName, Type columnType, string columnDefinition)> tableFieldsTupleList
      );
   }
}
