using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class UpdateRegisteredClientModelData
    {
        internal UpdateRegisteredClientModelData (GestprojectClient registeredClient, string sage50ClientCode, string sage50_guid_id) 
        {
            registeredClient.sage50_client_code = sage50ClientCode;
            registeredClient.sage50_guid_id = sage50_guid_id;
            registeredClient.sage50_instance = DataHolder.Sage50LocalTerminalPath;
        }
    }
}
