using Infragistics.Win.UltraWinTabControl;
using System.Reflection;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   public class ProjectsSynchronizationManager : IEntitySynchronizationManager
   {
      public void Launch
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         UltraTab hostTab
      )
      {
         try
         {
            hostTab.Enabled = true;
            MainWindowUIHolder.MainTabControl.SelectedTab = hostTab;

            UIFactory<GestprojectProjectModel, Sage50ProjectModel>.GenerateTabPage
            (
               // Application Constructor
               new SynchronizationTabGenerator<GestprojectProjectModel, Sage50ProjectModel>(),

               // UI comon
               hostTab.TabPage.Controls,
               new TabPageMainPanelTableLayoutPanelGenerator(),
               new TabPageLayoutPanelRowGenerator(),
               new MiddleRowControlsGenerator<GestprojectProjectModel, Sage50ProjectModel>(),
               new TopRowControlsGenerator<GestprojectProjectModel, Sage50ProjectModel>(),
               new BottomRowControlsGenerator<GestprojectProjectModel, Sage50ProjectModel>(),

               // Connectors
               gestprojectConnectionManager,
               sage50ConnectionManager,

               // Data Managers
               new ProjectsSynchronizationTableSchemaProvider(),
               new ProjectsDataTableManager(),
               new ProjectsSynchronizer()
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
