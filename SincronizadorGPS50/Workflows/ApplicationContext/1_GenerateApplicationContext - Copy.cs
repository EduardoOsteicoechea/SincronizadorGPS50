//using SincronizadorGPS50.Workflows.Sage50Connection;
//using System.Windows.Forms;

//namespace SincronizadorGPS50 {
//   internal class GenerateApplicationContext : ApplicationContext {
//      public GenerateApplicationContext() {
//         try {

//            // Set Initial Windows Forms Settiongs

//            if(!new SetInitialWindowsFormSettings().IsSuccessful) {
//               throw new System.Exception("Error at SetInitialWindowsFormSettings");
//            };

//            // Connect to Gestproject

//            ApplicationManager.ApplicationGlobalContext = this;

//            if(!new ApplyGestprojectGlobalStyle().IsSuccessful) {
//               throw new System.Exception("Error at ApplyGestprojectGlobalStyle");
//            };

//            GestprojectDataHolder.GestprojectDatabaseConnection = new GestprojectDatabaseConnector.ConnectionManager().Connect();

//            if(GestprojectDataHolder.GestprojectDatabaseConnection == null) {
//               throw new System.Exception("Error connecting to Gestproject Database");
//            };

//            GestprojectDataHolder.GestprojectClientList = new GestprojectDataManager.GestprojectClients().Get(GestprojectDataHolder.GestprojectDatabaseConnection);

//            if(GestprojectDataHolder.GestprojectClientList == null) {
//               throw new System.Exception("Error Obtaining Gestproject Clients");
//            };

//            GestprojectDataHolder.GestprojectProviderList = new GestprojectDataManager.GestprojectProviders().Get(GestprojectDataHolder.GestprojectDatabaseConnection);

//            if(GestprojectDataHolder.GestprojectProviderList == null) {
//               throw new System.Exception("Error Obtaining Gestproject Providers");
//            };

//            // Create Global UI

//            if(!new GenerateMainWindow().IsSuccessful) {
//               throw new System.Exception("Error generating main window");
//            };

//            if(!new GenerateMainWindowUI().IsSuccessful) {
//               throw new System.Exception("Error generating main window UI");
//            };

//            if(!new GenerateSage50ConnectionTabPageUI().IsSuccessful) {
//               throw new System.Exception("Error generating Sage50 Connection tab page");
//            };

//            if(!new CenterRowUI().IsSuccessful) {
//               throw new System.Exception("Error generating Sage50 Connection UI elements");
//            };

//            // Create Sage50Connection controls

//            if(GestprojectDataManager.ManageRememberableUserData.CheckIfGestprojectUserDataTableExists(GestprojectDataHolder.GestprojectDatabaseConnection)) {
//               if(!GestprojectDataManager.ManageRememberableUserData.CheckIfRememberUserDataOptionWasActivated(GestprojectDataHolder.GestprojectDatabaseConnection)) {
//                  new Sage50ConnectionUIManager(Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls, "stateless");
//               }
//               else {
//                  new Sage50ConnectionUIManager(Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls, "stateful");
//               };
//            }
//            else {
//               GestprojectDataManager.ManageRememberableUserData.CreateGestprojectUserDataTable(GestprojectDataHolder.GestprojectDatabaseConnection);

//               new Sage50ConnectionUIManager(Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls, "stateless");
//            };

//            // InitialLaunchForSage50Connection

//            MainWindowUIHolder.MainWindow.Show();
//         }
//         catch(System.Exception e) {
//            MessageBox.Show($"Error: \n\n{e.Message}");
//         };
//      }
//   }
//}