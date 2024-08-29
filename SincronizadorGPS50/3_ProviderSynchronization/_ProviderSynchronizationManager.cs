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
               new SynchronizationTabGenerator(),
               MainWindowUIHolder.ProvidersTab.TabPage.Controls,
               new TabPageMainPanelTableLayoutPanelGenerator(),
               new TabPageLayoutPanelRowGenerator(),

               new ProvidersMiddleRowControlsGenerator(),
               new ProvidersTopRowControlsGenerator(),
               new ProvidersBottomRowControlsGenerator(),

               gestprojectConnectionManager,
               sage50ConnectionManager,

               new ProvidersSynchronizationTableSchemaProvider(),
               new ProvidersDataTableManager()
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
