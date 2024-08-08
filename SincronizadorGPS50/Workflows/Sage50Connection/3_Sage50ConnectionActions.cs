using Infragistics.Win.UltraWinTabControl;
using SincronizadorGPS50.Workflows.Clients;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
    internal class Sage50ConnectionActions
    {
        internal void VeryfyUserData(object sender, System.EventArgs e)
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
                UIHolder.CenterRowCenterPanelValidateUserDataButton.Enabled = false;
                UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos correctos";
                UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.ForeColor = StyleHolder.c_green_1;

                UIHolder.CenterRowCenterPanelLocalInstanceTextBox.Enabled = false;
                UIHolder.CenterRowCenterPanelUsernameTextBox.Enabled = false;
                UIHolder.CenterRowCenterPanelPasswordTextBox.Enabled = false;

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

        internal void Disconnect(object sender, System.EventArgs e) 
        {
            Sage50ConnectionManager.ConnectionActions.Disconnect();

             UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelDisconnectButton);
             
             UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelConnectButton);

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelSesionDataValidationLabel);

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelEnterpryseGroupLabel);

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelEnterpryseGroupMenu);

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

        internal async void Connect(object sender, System.EventArgs e)
        {
            UIHolder.CenterRowCenterPanelStateIcon1.Image = Resources.Semaforo_verde;

            UIHolder.CenterRowCenterPanelStateStateMessageLabel.Text = "Conectado";

            UIHolder.CenterRowCenterPanelConnectButton.Enabled = false;

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelDisconnectButton, 0, 17);

            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Enabled = false;

            foreach(UltraTab tab in MainWindowUIHolder.MainTabControl.Tabs)
            {
                tab.Enabled = true;
            };

            await Task.Delay(1000);

            MainWindowUIHolder.MainTabControl.SelectedTab = UIHolder.ClientsTab;

            new ClientsTabPageUI();
        }
    }
}