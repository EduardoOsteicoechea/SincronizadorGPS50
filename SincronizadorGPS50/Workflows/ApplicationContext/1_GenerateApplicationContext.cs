using SincronizadorGPS50.Workflows.Sage50Connection.Sage50ConnectionTabUI;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
    internal class GenerateApplicationContext : ApplicationContext
    {
        internal System.Data.SqlClient.SqlConnection GestprojectDatabaseConnection { get; set; } = null;
        internal List<GestprojectDataManager.GestprojectClientModel> GestprojectClientList { get; set; } = null;
        internal List<GestprojectDataManager.GestprojectProviderModel> GestprojectProviderList { get; set; } = null;
        public GenerateApplicationContext() 
        {
            try
            {
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

                // GestprojectConnection
                // GestprojectConnection
                // GestprojectConnection
                // GestprojectConnection

                ApplicationManager.ApplicationGlobalContext = this;

                if(!new ApplyGestprojectGlobalStyle().IsSuccessful)
                {
                    throw new System.Exception("Error applying Gestproject Styles");
                };

                GestprojectDatabaseConnection = new GestprojectDatabaseConnector.ConnectionManager().Connect();

                if(GestprojectDatabaseConnection == null)
                {
                    throw new System.Exception("Error connecting to Gestproject Database");
                };

                GestprojectClientList = new GestprojectDataManager.GestprojectClients().Get(GestprojectDatabaseConnection);

                if (GestprojectClientList == null)
                {
                    throw new System.Exception("Error Obtaining Gestproject Clients");
                };

                GestprojectProviderList = new GestprojectDataManager.GestprojectProviders().Get(GestprojectDatabaseConnection);

                if (GestprojectProviderList == null)
                {
                    throw new System.Exception("Error Obtaining Gestproject Providers");
                };

                // InitialUI
                // InitialUI
                // InitialUI
                // InitialUI

                if(!new GenerateMainWindow().IsSuccessful)
                {
                    throw new System.Exception("Error generating main window");
                };

                if(!new GenerateMainWindowUI().IsSuccessful)
                {
                    throw new System.Exception("Error generating main window UI");
                };

                if(!new GenerateSage50ConnectionTabPageUI().IsSuccessful)
                {
                    throw new System.Exception("Error generating main window");
                };

                if(!new CenterRowUI().IsSuccessful)
                {
                    throw new System.Exception("Error generating main window");
                };

                if(!new CenterRowCenterPanelControls().IsSuccessful)
                {
                    throw new System.Exception("Error generating main window");
                };

                // InitialLaunchForSage50Connection
                // InitialLaunchForSage50Connection
                // InitialLaunchForSage50Connection
                // InitialLaunchForSage50Connection

                MainWindowUIHolder.MainWindow.Show();
            }
            catch (System.Exception e)
            {
                MessageBox.Show($"Error: \n\n{e.Message}");
            };
        }
    }
}