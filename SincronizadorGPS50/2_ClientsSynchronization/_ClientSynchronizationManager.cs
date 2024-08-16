using SincronizadorGPS50.Workflows.Clients;

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

            MainWindowUIHolder.ClientsTab.Enabled = true;
            MainWindowUIHolder.MainTabControl.SelectedTab = MainWindowUIHolder.ClientsTab;

            /////////////////////////////////////////////
            // Launch Clients Tab page generation
            /////////////////////////////////////////////

            new ClientsTabPageUI();
         }
         catch (System.Exception exception) 
         {
            throw exception;
         };
      }
   }
}