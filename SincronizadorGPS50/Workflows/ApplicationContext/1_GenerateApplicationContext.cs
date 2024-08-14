using GestprojectDataManager;
using Infragistics.Win.DataVisualization;
using Infragistics.Win.UltraWinTabControl;
using SincronizadorGPS50.Workflows.Clients;
using SincronizadorGPS50.Workflows.Sage50Connection;
using System.Security.Principal;
using System.Windows.Forms;

namespace SincronizadorGPS50 {
   internal class GenerateApplicationContext : ApplicationContext {
      public GenerateApplicationContext() {
         try {

            ///////////////////////////////////////
            // Set initial windows forms application settings
            ///////////////////////////////////////

            if(!new SetInitialWindowsFormSettings().IsSuccessful) {
               throw new System.Exception("Error at SetInitialWindowsFormSettings");
            };

            ///////////////////////////////////////
            // Get and leverage user device Gestproject data For Global Styling and Database Connection
            ///////////////////////////////////////

            ApplicationManager.ApplicationGlobalContext = this;

            if(!new ApplyGestprojectGlobalStyle().IsSuccessful) {
               throw new System.Exception("Error at ApplyGestprojectGlobalStyle");
            };

            try {
               GestprojectDataHolder.GestprojectDatabaseConnection = new GestprojectDatabaseConnector.ConnectionManager().Connect();
            } 
            catch {
               throw new System.Exception("Error at GestprojectDatabaseConnector.ConnectionManager().Connect()");
            };

            ///////////////////////////////////////
            // Create Global UI
            ///////////////////////////////////////

            if(!new GenerateMainWindow().IsSuccessful) {
               throw new System.Exception("Error at GenerateMainWindow");
            };

            if(!new GenerateMainWindowUI().IsSuccessful) {
               throw new System.Exception("Error at GenerateMainWindowUI");
            };

            if(!new GenerateSage50ConnectionTabPageUI().IsSuccessful) {
               throw new System.Exception("Error at GenerateSage50ConnectionTabPageUI");
            };

            if(!new GenerateCenterRowUI().IsSuccessful) {
               throw new System.Exception("Error at GenerateCenterRowUI");
            };

            ///////////////////////////////////////
            // Evaluate Basic User Rememberlable assets
            ///////////////////////////////////////

            bool gestprojectUserDataTableExists = false;
            try {
               gestprojectUserDataTableExists =
                  GestprojectDataManager
                  .ManageRememberableUserData
                  .CheckIfGestprojectUserDataTableExists(
                     GestprojectDataHolder.GestprojectDatabaseConnection
                  );
            }
            catch {
               throw new System.Exception("Error at gestprojectUserDataTableExists");
            };

            bool rememberUserDataOptionWasActivated = false;
            if(gestprojectUserDataTableExists) {
               try {
                  rememberUserDataOptionWasActivated =
                  GestprojectDataManager
                     .ManageRememberableUserData
                     .CheckIfRememberUserDataOptionWasActivated(
                        GestprojectDataHolder.GestprojectDatabaseConnection
                     );
               }
               catch {
                  throw new System.Exception("Error at rememberUserDataOptionWasActivated");
               };
            };

            ///////////////////////////////////////
            // Get user Rememberlable Data
            ///////////////////////////////////////
            
            try {
               GestprojectDataHolder.LocalDeviceUserSessionData = new GestprojectStyleManager.GestprojectSessionSettings(GestprojectDataHolder.GestprojectDatabaseConnection).userSessionData;
            }
            catch {
               MessageBox.Show("Error at GestprojectStyleManager.GestprojectSessionSettings(GestprojectDataHolder.GestprojectDatabaseConnection).userSessionData");
               throw new System.Exception("Error at GestprojectStyleManager.GestprojectSessionSettings(GestprojectDataHolder.GestprojectDatabaseConnection).userSessionData");
            };

            string currentLocalDevice = WindowsIdentity.GetCurrent().Name.Split('\\')[0];
            string gestprojectSessionDevice = GestprojectDataHolder.LocalDeviceUserSessionData.CNX_EQUIPO;

            bool userIsInRememberedAndApprovedDevice = gestprojectSessionDevice == currentLocalDevice;

            ///////////////////////////////////////
            // Create Sage50Connection conditional controls
            ///////////////////////////////////////

            if(gestprojectUserDataTableExists) {
               MessageBox.Show("gestprojectUserDataTableExists");
               if(rememberUserDataOptionWasActivated) {
                  MessageBox.Show("rememberUserDataOptionWasActivated");
                  if(userIsInRememberedAndApprovedDevice) {
                     MessageBox.Show("userIsInRememberedAndApprovedDevice");

                     if(!new Sage50ConnectionUIManager(
                           Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls,
                           "stateful"
                        ).IsSuccessful
                     ) {
                        MessageBox.Show("gestprojectUserDataTableExists, rememberUserDataOptionWasActivated, userIsInRememberedAndApprovedDevice");
                        throw new System.Exception("Error at Sage50ConnectionUIManager");
                     }
                     else {
                        foreach(UltraTab tab in MainWindowUIHolder.MainTabControl.Tabs) {
                           tab.Enabled = true;
                        };
                        MainWindowUIHolder.MainTabControl.SelectedTab = MainWindowUIHolder.ClientsTab;
                        new ClientsTabPageUI();
                     };
                  }
                  else {
                     if(!new Sage50ConnectionUIManager(
                           Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls,
                           "stateless"
                        ).IsSuccessful
                     ) {
                        MessageBox.Show("gestprojectUserDataTableExists, rememberUserDataOptionWasActivated");
                        throw new System.Exception("Error at Sage50ConnectionUIManager");
                     };
                  };                  
               }
               else {
                  if(!new Sage50ConnectionUIManager(
                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls,
                        "stateless"
                     ).IsSuccessful
                  ) {
                     MessageBox.Show("gestprojectUserDataTableExists");
                     throw new System.Exception("Error at Sage50ConnectionUIManager");
                  };
               };
            }
            else {
               if(!GestprojectDataManager
                  .ManageRememberableUserData
                  .CreateGestprojectUserDataTable(
                     GestprojectDataHolder.GestprojectDatabaseConnection
                  )
               ) {
                  MessageBox.Show("Error at GestprojectDataManager.ManageRememberableUserData.CreateGestprojectUserDataTable");
                  throw new System.Exception("Error at GestprojectDataManager.ManageRememberableUserData.CreateGestprojectUserDataTable");
               };

               if(!new Sage50ConnectionUIManager(
                     Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls,
                     "stateless"
                  ).IsSuccessful
               ) {
                  MessageBox.Show("Error at Sage50ConnectionUIManager");
                  throw new System.Exception("Error at Sage50ConnectionUIManager");
               };
            };

            ///////////////////////////////////////
            // InitialLaunchForSage50Connection
            ///////////////////////////////////////
            
            MainWindowUIHolder.MainWindow.Show();
         }
         catch(System.Exception e) {
            MessageBox.Show($"Error: \n\n{e.Message}");
         };
      }
   }
}