using SincronizadorGPS50.Workflows.Clients;
using System;

namespace SincronizadorGPS50
{
   internal class ClientSynchronizationManager
   {
      public void Launch()
      {
         try
         {
            /////////////////////////////////////////////
            // Enable and set as selected, ClientsTab
            /////////////////////////////////////////////

            MainWindowUIHolder.CustomersTab.Enabled = true;
            MainWindowUIHolder.MainTabControl.SelectedTab = MainWindowUIHolder.CustomersTab;

            /////////////////////////////////////////////
            // Launch Clients Tab page generation
            /////////////////////////////////////////////

            new CreateCustomersTabPageUI();
         }
         catch(Exception exception)
         {
            throw new Exception($"En:\n\nSincronizadorGPS50\n.ClientSynchronizationManager:\n\n{exception.Message}");
         };
      }
   }
}