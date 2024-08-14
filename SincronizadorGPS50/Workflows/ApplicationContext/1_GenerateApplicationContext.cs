using SincronizadorGPS50.Workflows.Sage50Connection;
using System.Security.Principal;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal class GenerateApplicationContext : ApplicationContext
   {
      public GenerateApplicationContext()
      {
         try
         {
            ///////////////////////////////////////
            // Set initial windows forms application settings
            ///////////////////////////////////////

            new WindowsFormsApplicationSettings().SetInitialSettings();

            ///////////////////////////////////////
            // Get and leverage user device Gestproject data For Global Styling and Database Connection
            ///////////////////////////////////////

            GestprojectDataHolder.GestprojectDatabaseConnection =
               new GestprojectDatabaseConnector.ConnectionManager().Connect();

            GestprojectDataHolder.GestprojectLocalDeviceUserData =
               new GestprojectConnector.GestprojectLocalDeviceUserData().Get();

            new ManageApplicationGlobalStyles().ApplyGestprojectCurrentStyle();

            ///////////////////////////////////////
            // Create Global UI
            ///////////////////////////////////////

            new GenerateMainWindow();

            new GenerateMainWindowUI();

            new GenerateSage50ConnectionTabPageUI();

            new GenerateCenterRowUI();

            ///////////////////////////////////////
            // Get user Rememberlable Data
            ///////////////////////////////////////

            GestprojectDataHolder.LocalDeviceUserSessionData =
               new GestprojectStyleManager.GestprojectSessionSettings(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  GestprojectDataHolder.GestprojectLocalDeviceUserData.LastUser
               ).userSessionData;

            string currentLocalDevice = WindowsIdentity.GetCurrent().Name.Split('\\')[0];
            string gestprojectSessionDevice = GestprojectDataHolder.LocalDeviceUserSessionData.CNX_EQUIPO;
            bool userIsInRememberedAndApprovedDevice = gestprojectSessionDevice == currentLocalDevice;

            ///////////////////////////////////////
            // Evaluate Basic User Rememberlable assets
            ///////////////////////////////////////

            bool gestprojectUserDataTableExists =
               GestprojectDataManager
               .ManageRememberableUserData
               .CheckIfGestprojectUserDataTableExists(
                  GestprojectDataHolder.GestprojectDatabaseConnection
               );

            bool rememberUserDataOptionWasActivated = false;
            if(gestprojectUserDataTableExists)
            {
               rememberUserDataOptionWasActivated =
               GestprojectDataManager
               .ManageRememberableUserData
               .CheckIfRememberUserDataOptionWasActivated(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  GestprojectDataHolder.LocalDeviceUserSessionData.CNX_USUARIO,
                  GestprojectDataHolder.LocalDeviceUserSessionData.CNX_EQUIPO,
                  GestprojectDataHolder.LocalDeviceUserSessionData.USU_ID
               );
            };

            ///////////////////////////////////////
            // Create Sage50Connection conditional controls
            ///////////////////////////////////////

            if(!gestprojectUserDataTableExists)
            {
               GestprojectDataManager
               .ManageRememberableUserData
               .CreateGestprojectUserDataTable(
                  GestprojectDataHolder.GestprojectDatabaseConnection
               );

               new Sage50ConnectionUIManager(
                  Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls,
                  "stateless"
               );
            }
            else
            {
               if(!rememberUserDataOptionWasActivated)
               {
                  new Sage50ConnectionUIManager(
                     Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls,
                     "stateless"
                  );
               }
               else
               {
                  if(!userIsInRememberedAndApprovedDevice)
                  {
                     new Sage50ConnectionUIManager(
                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls,
                        "stateless"
                     );
                  }
                  else
                  {
                     new Sage50ConnectionUIManager(
                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls,
                        "stateful"
                     );

                     new ClientSynchronizationManager().Launch();
                  };
               };
            };

            ///////////////////////////////////////
            // InitialLaunchForSage50Connection
            ///////////////////////////////////////

            MainWindowUIHolder.MainWindow.Show();
         }
         catch(System.Exception e)
         {
            MessageBox.Show($"Error: \n\n{e.Message}");
         };
      }
   }
}