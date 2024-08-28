using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
   public interface ISynchronizationTableSchemaProvider
   {
      string TableName { get; set; }
      List<(string columnName, string friendlyName, Type columnType)> ColumnsTuplesList { get; set; }
   }
}
