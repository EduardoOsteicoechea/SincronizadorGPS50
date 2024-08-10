using SincronizadorGPS50.Workflows.Sage50Connection;
using SincronizadorGPS50.Workflows.Sage50Connection.Sage50ConnectionTabUI;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
    internal class GenerateApplicationContext : ApplicationContext
    {
        public GenerateApplicationContext() 
        {
            try
            {
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

                // Connect to Gestproject
                // Connect to Gestproject
                // Connect to Gestproject
                // Connect to Gestproject
                // Connect to Gestproject

                ApplicationManager.ApplicationGlobalContext = this;

                if(!new ApplyGestprojectGlobalStyle().IsSuccessful)
                {
                    throw new System.Exception("Error applying Gestproject Styles");
                };

                GestprojectDataHolder.GestprojectDatabaseConnection = new GestprojectDatabaseConnector.ConnectionManager().Connect();

                if(GestprojectDataHolder.GestprojectDatabaseConnection == null)
                {
                    throw new System.Exception("Error connecting to Gestproject Database");
                };

                GestprojectDataHolder.GestprojectClientList = new GestprojectDataManager.GestprojectClients().Get(GestprojectDataHolder.GestprojectDatabaseConnection);

                if (GestprojectDataHolder.GestprojectClientList == null)
                {
                    throw new System.Exception("Error Obtaining Gestproject Clients");
                };

                GestprojectDataHolder.GestprojectProviderList = new GestprojectDataManager.GestprojectProviders().Get(GestprojectDataHolder.GestprojectDatabaseConnection);

                if (GestprojectDataHolder.GestprojectProviderList == null)
                {
                    throw new System.Exception("Error Obtaining Gestproject Providers");
                };

                // Create Global UI
                // Create Global UI
                // Create Global UI
                // Create Global UI
                // Create Global UI

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
                    throw new System.Exception("Error generating Sage50 Connection tab page");
                };

                if(!new CenterRowUI().IsSuccessful)
                {
                    throw new System.Exception("Error generating Sage50 Connection UI elements");
                };

                // Create Sage50Connection controls
                // Create Sage50Connection controls
                // Create Sage50Connection controls
                // Create Sage50Connection controls
                // Create Sage50Connection controls

                if(GestprojectDataManager.ManageUserData.CheckIfGestprojectUserDataTableExists(GestprojectDataHolder.GestprojectDatabaseConnection))
                {
                    if(!GestprojectDataManager.ManageUserData.CheckIfRememberUserDataOptionWasActivated(GestprojectDataHolder.GestprojectDatabaseConnection))
                    {
                        //MessageBox.Show("Stateless");
                        
                        new Sage50ConnectionUIManager(Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls, "stateless");

                        //if(!new StatelessCenterRowCenterPanelControls().IsSuccessful)
                        //{
                        //    throw new System.Exception("Error generating main window");
                        //};
                    }
                    else
                    {
                        //MessageBox.Show("Stateful");

                        new Sage50ConnectionUIManager(Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls, "stateful");

                        //if(!new StatefulCenterRowCenterPanelControls().IsSuccessful)
                        //{
                        //    throw new System.Exception("Error generating main window");
                        //};
                    };
                }
                else
                {
                    //MessageBox.Show("Creating User Rememberable Data Table");
                    GestprojectDataManager.ManageUserData.CreateGestprojectUserDataTable(GestprojectDataHolder.GestprojectDatabaseConnection);

                    GestprojectDataManager.ManageUserData.PopulateGestprojectUserDataTable(GestprojectDataHolder.GestprojectDatabaseConnection,"", "", "", "");

                    new Sage50ConnectionUIManager(Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls, "stateless");

                    //if(!new StatelessCenterRowCenterPanelControls().IsSuccessful)
                    //{
                    //    throw new System.Exception("Error generating main window");
                    //};
                };  

                // InitialLaunchForSage50Connection
                // InitialLaunchForSage50Connection
                // InitialLaunchForSage50Connection
                // InitialLaunchForSage50Connection

                MainWindowUIHolder.MainWindow.Show();
            }
            catch(System.Exception e)
            {
                MessageBox.Show($"Error: \n\n{e.Message}");
            };
        }
    }
}