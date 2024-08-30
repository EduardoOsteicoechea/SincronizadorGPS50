using SincronizadorGPS50.GestprojectDataManager;
using SincronizadorGPS50.Sage50Connector;
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

            UIFactory.GenerateTabPage
            (
               MainWindowUIHolder.ProvidersTab.TabPage.Controls,

               new SynchronizationTabGenerator<GestprojectProviderModel, Sage50ProviderModel>(),

               new TabPageMainPanelTableLayoutPanelGenerator(),
               new TabPageLayoutPanelRowGenerator(),

               new ProvidersMiddleRowControlsGenerator(),
               new ProvidersTopRowControlsGenerator<GestprojectProviderModel, Sage50ProviderModel>(),
               new ProvidersBottomRowControlsGenerator(),

               gestprojectConnectionManager,
               sage50ConnectionManager,

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
