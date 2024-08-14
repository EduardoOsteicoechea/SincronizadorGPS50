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
            foreach(UltraTab tab in MainWindowUIHolder.MainTabControl.Tabs)
            {
               tab.Enabled = true;
            };
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