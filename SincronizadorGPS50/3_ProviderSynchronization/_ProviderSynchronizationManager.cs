using SincronizadorGPS50.Workflows.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
   public class ProviderSynchronizationManager
   {
      public void Launch()
      {
         try
         {
            /////////////////////////////////////////////
            // Enable and set as selected, ClientsTab
            /////////////////////////////////////////////

            MainWindowUIHolder.ProvidersTab.Enabled = true;

            /////////////////////////////////////////////
            // Launch the Providers Tab page generation
            /////////////////////////////////////////////

            
         }
         catch(Exception exception)
         {
            throw new Exception($"En:\n\nSincronizadorGPS50\n.ProviderSynchronizationManager:\n\n{exception.Message}");
         };
      }
   }
}
