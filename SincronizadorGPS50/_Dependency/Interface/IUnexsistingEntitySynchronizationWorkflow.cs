using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
   internal interface IUnexsistingEntitySynchronizationWorkflow<T>
   {
      void Execute
      (
         IGestprojectConnectionManager GestprojectConnectionManager,
         ISage50ConnectionManager Sage50ConnectionManager,
         List<T> entityList,
         ISynchronizationTableSchemaProvider tableSchemaProvider
      );
   }
}
