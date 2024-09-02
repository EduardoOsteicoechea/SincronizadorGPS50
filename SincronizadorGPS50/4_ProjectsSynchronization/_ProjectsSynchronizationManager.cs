using SincronizadorGPS50._4_ProjectsSynchronization.Models;
using System.Reflection;

namespace SincronizadorGPS50
{
   public class ProjectsSynchronizationManager
   {
      public void Launch
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager
      )
      {
         try
         {
            MainWindowUIHolder.ProjectsTab.Enabled = true;
            MainWindowUIHolder.MainTabControl.SelectedTab = MainWindowUIHolder.ProjectsTab;

            UIFactory<GestprojectProjectModel, Sage50ProjectModel>.GenerateTabPage
            (
               // Application Constructor
               new SynchronizationTabGenerator<GestprojectProjectModel, Sage50ProjectModel>(),

               // UI comon
               MainWindowUIHolder.ProvidersTab.TabPage.Controls,
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
