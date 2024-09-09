using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
	public static class TabLauncher
	{
		public static void LaunchTabs(string selectedCompanyGroupName) 
		{
			IGestprojectConnectionManager gestprojectConnectionManager = new GestprojectConnectionManager();
			ISage50ConnectionManager sage50ConnectionManager = new Sage50ConnectionManager(selectedCompanyGroupName);

			new ClientSynchronizationManager().Launch
			(
			   GestprojectDataHolder.GestprojectDatabaseConnection,
			   sage50ConnectionManager.CompanyGroupData,
            MainWindowUIHolder.CustomersTab
         );

			new ProviderSynchronizationManager().Launch
			(
			   gestprojectConnectionManager,
			   sage50ConnectionManager,
            MainWindowUIHolder.ProvidersTab
         );

         new ProjectsSynchronizationManager().Launch
         (
            gestprojectConnectionManager,
            sage50ConnectionManager,
            MainWindowUIHolder.ProjectsTab
         );

         new TaxesSynchronizationManager().Launch
         (
            gestprojectConnectionManager,
            sage50ConnectionManager,
            MainWindowUIHolder.TaxesTab
         );
      }
	}
}
