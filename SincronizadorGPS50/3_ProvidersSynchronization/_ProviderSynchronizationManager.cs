using System.Reflection;

namespace SincronizadorGPS50
{
   public class ProviderSynchronizationManager
   {
      public void Launch
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager
      )
      {
         try
         {
            MainWindowUIHolder.ProvidersTab.Enabled = true;
            MainWindowUIHolder.MainTabControl.SelectedTab = MainWindowUIHolder.ProvidersTab;

            UIFactory<GestprojectProviderModel, Sage50ProviderModel>.GenerateTabPage
            (
               // Application Constructor
               new SynchronizationTabGenerator<GestprojectProviderModel, Sage50ProviderModel>(),

               // UI Components
               MainWindowUIHolder.ProvidersTab.TabPage.Controls,
               new TabPageMainPanelTableLayoutPanelGenerator(),
               new TabPageLayoutPanelRowGenerator(),
               new MiddleRowControlsGenerator<GestprojectProviderModel, Sage50ProviderModel>(),
               new TopRowControlsGenerator<GestprojectProviderModel, Sage50ProviderModel>(),
               new BottomRowControlsGenerator<GestprojectProviderModel, Sage50ProviderModel>(),

               // Connectors
               gestprojectConnectionManager,
               sage50ConnectionManager,

               // Data Managers
               new ProvidersSynchronizationTableSchemaProvider(),
               new ProvidersDataTableManager(),
               new ProvidersSynchronizer()
            );
         }
         catch(System.Exception exception)
         {
            throw ApplicationLogger.ReportError(
               MethodBase.GetCurrentMethod().DeclaringType.Namespace,
               MethodBase.GetCurrentMethod().DeclaringType.Name,
               MethodBase.GetCurrentMethod().Name,
               exception
            );
         };
      }
   }
}
