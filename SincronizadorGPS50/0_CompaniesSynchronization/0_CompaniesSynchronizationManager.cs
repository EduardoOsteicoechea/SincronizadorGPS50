using Infragistics.Win.UltraWinTabControl;
using System.Reflection;

namespace SincronizadorGPS50
{
   public class CompaniesSynchronizationManager : IEntitySynchronizationManager
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

            UIFactory<SincronizadorGP50CompanyModel, SageCompanyModel>.GenerateTabPage
            (
               // Application Constructor
               new SynchronizationTabGenerator<SincronizadorGP50CompanyModel, SageCompanyModel>(),

               // UI comon
               hostTab.TabPage.Controls,
               new TabPageMainPanelTableLayoutPanelGenerator(),
               new TabPageLayoutPanelRowGenerator(),
               new MiddleRowControlsGenerator<SincronizadorGP50CompanyModel, SageCompanyModel>(),
               new TopRowControlsGenerator<SincronizadorGP50CompanyModel, SageCompanyModel>(false),
               new BottomRowControlsGenerator<SincronizadorGP50CompanyModel, SageCompanyModel>(),

               // Connectors
               gestprojectConnectionManager,
               sage50ConnectionManager,

               // Data Managers
               new CompaniesSynchronizationTableSchemaProvider(),
               new CompaniesDataTableManager(),
               new CompaniesSynchronizer()
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
