using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class MakeSynchronizationTableGoballyAvailable
    {
        internal MakeSynchronizationTableGoballyAvailable(DataTable table) 
        {
            DataHolder.ClientsSynchronizationTable = table;
        }
    }
}
