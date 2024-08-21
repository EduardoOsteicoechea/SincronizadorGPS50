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
            /////////////////////////////////////////////
            // start point of the client synchronization workflow
            /////////////////////////////////////////////
            /////////////////////////////////////////////
            
            /////////////////////////////////////////////
            // enable ClientsTab and set it as selected
            /////////////////////////////////////////////

            MainWindowUIHolder.CustomersTab.Enabled = true;
            MainWindowUIHolder.MainTabControl.SelectedTab = MainWindowUIHolder.CustomersTab;

            /////////////////////////////////////////////
            // launch Clients Tab page generation
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