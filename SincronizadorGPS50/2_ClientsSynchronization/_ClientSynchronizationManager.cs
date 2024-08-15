using Infragistics.Win.UltraWinTabControl;
using SincronizadorGPS50.Workflows.Clients;

namespace SincronizadorGPS50
{
   internal class ClientSynchronizationManager
   {
      public void Launch()
      {
         try
         {
            MainWindowUIHolder.ClientsTab.Enabled = true;
            MainWindowUIHolder.MainTabControl.SelectedTab = MainWindowUIHolder.ClientsTab;
            new ClientsTabPageUI();
         } 
         catch (System.Exception exception) 
         {
            throw exception;
         };
      }
   }
}