using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinTabControl;
using SincronizadorGPS50.Workflows.Clients;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
    internal static class Sage50ConnectionActions
    {
        internal static void VeryfyUserData(object sender, System.EventArgs e)
        {
            DataHolder.Sage50LocalTerminalPath = Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceTextBox.Text;
            DataHolder.Sage50Username = Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameTextBox.Text;
            DataHolder.Sage50Password = Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordTextBox.Text;

            if(
                Sage50ConnectionManager.ConnectionActions.Connect(
                    DataHolder.Sage50LocalTerminalPath,
                    DataHolder.Sage50Username,
                    DataHolder.Sage50Password
                )
            )
            {
                Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos correctos";
                Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.ForeColor = StyleHolder.c_green_1;

                Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel, 0, 11);

                Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupLabel, 0, 12);

                Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu, 0, 13);

                DataHolder.Sage50CompanyGroupsList = Sage50ConnectionManager.Sage50CompanyGroupActions.GetCompanyGroups();

                if(Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Count < 1)
                {
                    Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Add("");
                };

                for(global::System.Int32 i = 0; i < DataHolder.Sage50CompanyGroupsList.Count; i++)
                {
                    Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Add(DataHolder.Sage50CompanyGroupsList[i].CompanyName);
                };
            }
            else
            {
                Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos inválidos para iniciar sesión";
                Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.ForeColor = StyleHolder.c_red_1;
                Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel, 0, 11);
            };
        }

        internal static void InforceDataValidation(object sender, System.EventArgs e) 
        {
           Sage50ConnectionManager.ConnectionActions.Disconnect();

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(Sage50ConnectionUIHolder.CenterRowCenterPanelDisconnectButton);

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton);

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel);

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupLabel);

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu);

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataPanel);

            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedIndex = 0;

            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Clear();

            DataHolder.Sage50CompanyGroupsList.Clear();

            Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceTextBox.Enabled = true;

            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameTextBox.Enabled = true;
            Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordTextBox.Enabled = true;

            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Enabled = true;

            Sage50ConnectionUIHolder.CenterRowCenterPanelStateIcon1.Image = Resources.SemaforoRojo;

            Sage50ConnectionUIHolder.CenterRowCenterPanelStateStateMessageLabel.Text = "Desconectado";

            Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton.Enabled = true;

            Sage50ConnectionUIHolder.CenterRowCenterPanelValidateUserDataButton.Enabled = true;

            Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos Incorrectos";
            Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.ForeColor = StyleHolder.c_red_1;

            foreach(UltraTab tab in MainWindowUIHolder.MainTabControl.Tabs)
            {
                tab.Enabled = false;
            };

            MainWindowUIHolder.MainTabControl.SelectedTab.Enabled = true;
        }

        internal static void Disconnect(object sender, System.EventArgs e) 
        {
            Sage50ConnectionManager.ConnectionActions.Disconnect();

             Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(Sage50ConnectionUIHolder.CenterRowCenterPanelDisconnectButton);
             
             Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton);

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel);

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupLabel);

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu);

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataLabel);

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataCheckBox);

            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedIndex = 0;

            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Clear();

            DataHolder.Sage50CompanyGroupsList.Clear();

            Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceTextBox.Enabled = true;

            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameTextBox.Enabled = true;
            Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordTextBox.Enabled = true;

            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Enabled = true;

            Sage50ConnectionUIHolder.CenterRowCenterPanelStateIcon1.Image = Resources.SemaforoRojo;

            Sage50ConnectionUIHolder.CenterRowCenterPanelStateStateMessageLabel.Text = "Desconectado";

            Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton.Enabled = true;

            Sage50ConnectionUIHolder.CenterRowCenterPanelValidateUserDataButton.Enabled = true;

            Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos Incorrectos";
            Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.ForeColor = StyleHolder.c_red_1;

            foreach(UltraTab tab in MainWindowUIHolder.MainTabControl.Tabs)
            {
                tab.Enabled = false;
            };

            MainWindowUIHolder.MainTabControl.SelectedTab.Enabled = true;
        }

        internal static async void Connect(object sender, System.EventArgs e)
        {
            DataHolder.Sage50LocalTerminalPath = Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceTextBox.Text;
            DataHolder.Sage50Username = Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameTextBox.Text;
            DataHolder.Sage50Password = Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordTextBox.Text;
            DataHolder.Sage50SelectedCompanyGroupName = Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Text;


            Sage50ConnectionManager.ConnectionActions.Disconnect();

            Sage50ConnectionManager.ConnectionActions.Connect(
                DataHolder.Sage50LocalTerminalPath,
                DataHolder.Sage50Username,
                DataHolder.Sage50Password
            );


            if(Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataCheckBox.Checked)
            {
                if(
                    DataHolder.Sage50LocalTerminalPath != ""
                    &&
                    DataHolder.Sage50Username != ""
                    &&
                    DataHolder.Sage50Password != ""
                    &&
                    DataHolder.Sage50SelectedCompanyGroupName != ""
                )
                {
                    GestprojectDataManager.ManageUserData.Save(
                        GestprojectDataHolder.GestprojectDatabaseConnection,
                        DataHolder.Sage50LocalTerminalPath,
                        DataHolder.Sage50Username,
                        DataHolder.Sage50Password,
                        DataHolder.Sage50SelectedCompanyGroupName
                    );

                    Sage50ConnectionUIHolder.CenterRowCenterPanelStateIcon1.Image = Resources.Semaforo_verde;

                    Sage50ConnectionUIHolder.CenterRowCenterPanelStateStateMessageLabel.Text = "Conectado";

                    Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton.Enabled = false;

                    Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelDisconnectButton, 0, 19);

                    Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Enabled = false;

                    foreach(UltraTab tab in MainWindowUIHolder.MainTabControl.Tabs)
                    {
                        tab.Enabled = true;
                    };

                    await Task.Delay(1000);

                    MainWindowUIHolder.MainTabControl.SelectedTab = MainWindowUIHolder.ClientsTab;

                    new ClientsTabPageUI();
                }
                else
                {
                    MessageBox.Show(
                        "DataHolder.Sage50LocalTerminalPath: " + "\n" +
                        DataHolder.Sage50LocalTerminalPath + "\n\n" +
                        "DataHolder.Sage50Username: " + "\n" +
                        DataHolder.Sage50Username + "\n\n" +
                        "DataHolder.Sage50Password: " + "\n" +
                        DataHolder.Sage50Password + "\n\n" +
                        "DataHolder.Sage50SelectedCompanyGroupName: " + "\n" +
                        DataHolder.Sage50SelectedCompanyGroupName
                    );
                    MessageBox.Show("Alguno de los campos de conexión está vacío.");
                };
            }
            else
            {
                GestprojectDataManager.ManageUserData.DisableRememberUserDataFeature(GestprojectDataHolder.GestprojectDatabaseConnection);

                Sage50ConnectionUIHolder.CenterRowCenterPanelStateIcon1.Image = Resources.Semaforo_verde;

                Sage50ConnectionUIHolder.CenterRowCenterPanelStateStateMessageLabel.Text = "Conectado";

                Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelDisconnectButton, 0, 19);

                foreach(UltraTab tab in MainWindowUIHolder.MainTabControl.Tabs)
                {
                    tab.Enabled = true;
                };

                await Task.Delay(1000);

                MainWindowUIHolder.MainTabControl.SelectedTab = MainWindowUIHolder.ClientsTab;

                new ClientsTabPageUI();
            };
        }

        internal static void ChangeCompanyGroup(object sender, System.EventArgs e)
        {
            if(Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedText != "")
            {
                if(
                    Sage50ConnectionManager.Sage50CompanyGroupActions.ChangeCompanyGroup(
                        Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedText
                    )
                )
                {
                    Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton, 0, 16);

                    Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataLabel, 0, 17);

                    Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataCheckBox, 0, 18);

                    Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton.Focus();
                }
                else
                {

                };
            };
        }
    }
}