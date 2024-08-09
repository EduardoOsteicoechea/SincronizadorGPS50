using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinTabControl;
using SincronizadorGPS50.Workflows.Clients;
using SincronizadorGPS50.Workflows.Sage50Connection.Sage50ConnectionTabUI;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
    internal static class Sage50ConnectionActions
    {
        internal static void VeryfyUserData(object sender, System.EventArgs e)
        {
            DataHolder.Sage50LocalTerminalPath = UIHolder.CenterRowCenterPanelLocalInstanceTextBox.Text;
            DataHolder.Sage50Username = UIHolder.CenterRowCenterPanelUsernameTextBox.Text;
            DataHolder.Sage50Password = UIHolder.CenterRowCenterPanelPasswordTextBox.Text;

            if(
                Sage50ConnectionManager.ConnectionActions.Connect(
                    DataHolder.Sage50LocalTerminalPath,
                    DataHolder.Sage50Username,
                    DataHolder.Sage50Password
                )
            )
            {
                //UIHolder.CenterRowCenterPanelValidateUserDataButton.Enabled = false;
                UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos correctos";
                UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.ForeColor = StyleHolder.c_green_1;

                //UIHolder.CenterRowCenterPanelLocalInstanceTextBox.Enabled = false;
                //UIHolder.CenterRowCenterPanelUsernameTextBox.Enabled = false;
                //UIHolder.CenterRowCenterPanelPasswordTextBox.Enabled = false;

                UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelSesionDataValidationLabel, 0, 11);

                UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelEnterpryseGroupLabel, 0, 12);

                UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelEnterpryseGroupMenu, 0, 13);

                DataHolder.Sage50CompanyGroupsList = Sage50ConnectionManager.Sage50CompanyGroupActions.GetCompanyGroups();

                if(UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Count < 1)
                {
                    UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Add("");
                };

                for(global::System.Int32 i = 0; i < DataHolder.Sage50CompanyGroupsList.Count; i++)
                {
                    UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Add(DataHolder.Sage50CompanyGroupsList[i].CompanyName);
                };
            }
            else
            {
                UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos inválidos para iniciar sesión";
                UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.ForeColor = StyleHolder.c_red_1;
                UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelSesionDataValidationLabel, 0, 11);
            };
        }

        internal static void InforceDataValidation(object sender, System.EventArgs e) 
        {
           Sage50ConnectionManager.ConnectionActions.Disconnect();

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelDisconnectButton);

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelConnectButton);

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelSesionDataValidationLabel);

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelEnterpryseGroupLabel);

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelEnterpryseGroupMenu);

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelRememberDataPanel);

            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedIndex = 0;

            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Clear();

            DataHolder.Sage50CompanyGroupsList.Clear();

            UIHolder.CenterRowCenterPanelLocalInstanceTextBox.Enabled = true;

            UIHolder.CenterRowCenterPanelUsernameTextBox.Enabled = true;
            UIHolder.CenterRowCenterPanelPasswordTextBox.Enabled = true;

            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Enabled = true;

            UIHolder.CenterRowCenterPanelStateIcon1.Image = Resources.SemaforoRojo;

            UIHolder.CenterRowCenterPanelStateStateMessageLabel.Text = "Desconectado";

            UIHolder.CenterRowCenterPanelConnectButton.Enabled = true;

            UIHolder.CenterRowCenterPanelValidateUserDataButton.Enabled = true;

            UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos Incorrectos";
            UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.ForeColor = StyleHolder.c_red_1;

            foreach(UltraTab tab in MainWindowUIHolder.MainTabControl.Tabs)
            {
                tab.Enabled = false;
            };

            MainWindowUIHolder.MainTabControl.SelectedTab.Enabled = true;
        }

        internal static void Disconnect(object sender, System.EventArgs e) 
        {
            Sage50ConnectionManager.ConnectionActions.Disconnect();

             UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelDisconnectButton);
             
             UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelConnectButton);

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelSesionDataValidationLabel);

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelEnterpryseGroupLabel);

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelEnterpryseGroupMenu);

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelRememberDataLabel);

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelRememberDataCheckBox);

            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedIndex = 0;

            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Clear();

            DataHolder.Sage50CompanyGroupsList.Clear();

            UIHolder.CenterRowCenterPanelLocalInstanceTextBox.Enabled = true;

            UIHolder.CenterRowCenterPanelUsernameTextBox.Enabled = true;
            UIHolder.CenterRowCenterPanelPasswordTextBox.Enabled = true;

            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Enabled = true;

            UIHolder.CenterRowCenterPanelStateIcon1.Image = Resources.SemaforoRojo;

            UIHolder.CenterRowCenterPanelStateStateMessageLabel.Text = "Desconectado";

            UIHolder.CenterRowCenterPanelConnectButton.Enabled = true;

            UIHolder.CenterRowCenterPanelValidateUserDataButton.Enabled = true;

            UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos Incorrectos";
            UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.ForeColor = StyleHolder.c_red_1;

            foreach(UltraTab tab in MainWindowUIHolder.MainTabControl.Tabs)
            {
                tab.Enabled = false;
            };

            MainWindowUIHolder.MainTabControl.SelectedTab.Enabled = true;
        }

        internal static async void Connect(object sender, System.EventArgs e)
        {
            DataHolder.Sage50LocalTerminalPath = UIHolder.CenterRowCenterPanelLocalInstanceTextBox.Text;
            DataHolder.Sage50Username = UIHolder.CenterRowCenterPanelUsernameTextBox.Text;
            DataHolder.Sage50Password = UIHolder.CenterRowCenterPanelPasswordTextBox.Text;
            //DataHolder.Sage50SelectedCompanyGroupName = UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedText;
            DataHolder.Sage50SelectedCompanyGroupName = UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Text;


            Sage50ConnectionManager.ConnectionActions.Disconnect();

            Sage50ConnectionManager.ConnectionActions.Connect(
                DataHolder.Sage50LocalTerminalPath,
                DataHolder.Sage50Username,
                DataHolder.Sage50Password
            );


            if(UIHolder.CenterRowCenterPanelRememberDataCheckBox.Checked)
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

                    UIHolder.CenterRowCenterPanelStateIcon1.Image = Resources.Semaforo_verde;

                    UIHolder.CenterRowCenterPanelStateStateMessageLabel.Text = "Conectado";

                    UIHolder.CenterRowCenterPanelConnectButton.Enabled = false;

                    UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelDisconnectButton, 0, 19);

                    UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Enabled = false;

                    foreach(UltraTab tab in MainWindowUIHolder.MainTabControl.Tabs)
                    {
                        tab.Enabled = true;
                    };

                    await Task.Delay(1000);

                    MainWindowUIHolder.MainTabControl.SelectedTab = UIHolder.ClientsTab;

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

                UIHolder.CenterRowCenterPanelStateIcon1.Image = Resources.Semaforo_verde;

                UIHolder.CenterRowCenterPanelStateStateMessageLabel.Text = "Conectado";

                //UIHolder.CenterRowCenterPanelConnectButton.Enabled = false;

                UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelDisconnectButton, 0, 19);

                //UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Enabled = false;

                foreach(UltraTab tab in MainWindowUIHolder.MainTabControl.Tabs)
                {
                    tab.Enabled = true;
                };

                await Task.Delay(1000);

                MainWindowUIHolder.MainTabControl.SelectedTab = UIHolder.ClientsTab;

                new ClientsTabPageUI();
            };
        }

        internal static void ChangeCompanyGroup(object sender, System.EventArgs e)
        {
            if(UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedText != "")
            {
                if(
                    Sage50ConnectionManager.Sage50CompanyGroupActions.ChangeCompanyGroup(
                        UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedText
                    )
                )
                {
                    UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelConnectButton, 0, 16);

                    UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelRememberDataLabel, 0, 17);

                    UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelRememberDataCheckBox, 0, 18);

                    UIHolder.CenterRowCenterPanelConnectButton.Focus();
                }
                else
                {

                };
            };
        }
    }
}