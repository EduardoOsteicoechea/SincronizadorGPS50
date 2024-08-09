using Infragistics.Win.Misc;
using System;
using Infragistics.Win.UltraWinEditors;
using System.Windows.Forms;
using System.Drawing;
using SincronizadorGPS50.Workflows.Sage50Connection.Sage50ConnectionTabUI;

namespace SincronizadorGPS50
{
    internal class StatefulCenterRowCenterPanelControls
    {
        internal bool IsSuccessful {  get; set; } = false;
        internal StatefulCenterRowCenterPanelControls() 
        {
            // ConnectionState
            // ConnectionState
            // ConnectionState
            // ConnectionState
            // ConnectionState

            UIHolder.CenterRowCenterPanelStateLabel = new UltraLabel();
            UIHolder.CenterRowCenterPanelStateLabel.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelStateLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelStateLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            UIHolder.CenterRowCenterPanelStateLabel.Margin = new Padding(0, 10, 0, 0);
            UIHolder.CenterRowCenterPanelStateLabel.Text = "Estado de la conexión";

            UIHolder.CenterRowCenterPanelStateStateMessageLabel = new UltraLabel();
            UIHolder.CenterRowCenterPanelStateStateMessageLabel.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelStateStateMessageLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelStateStateMessageLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            UIHolder.CenterRowCenterPanelStateStateMessageLabel.Text = "Desconectado";

            UIHolder.CenterRowCenterPanelStateIcon1 = new UltraPictureBox();
            UIHolder.CenterRowCenterPanelStateIcon1.Image = Resources.SemaforoRojo;
            UIHolder.CenterRowCenterPanelStateIcon1.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelStateIcon1.Height = 20;
            UIHolder.CenterRowCenterPanelStateIcon1.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelStateIcon1.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            // Title
            // Title
            // Title
            // Title
            // Title

            UIHolder.CenterRowCenterPanelTitleLabel = new UltraLabel();
            UIHolder.CenterRowCenterPanelTitleLabel.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelTitleLabel.Font= new Font(new UltraLabel().Font.FontFamily, 11, FontStyle.Bold);
            UIHolder.CenterRowCenterPanelTitleLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelTitleLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            UIHolder.CenterRowCenterPanelTitleLabel.Text = "Datos para conectar a Sage50";

            // LocalInstance
            // LocalInstance
            // LocalInstance
            // LocalInstance
            // LocalInstance

            UIHolder.CenterRowCenterPanelLocalInstanceLabel = new UltraLabel();
            UIHolder.CenterRowCenterPanelLocalInstanceLabel.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelLocalInstanceLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelLocalInstanceLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            UIHolder.CenterRowCenterPanelLocalInstanceLabel.Text = "Especifique el terminal de Sage50";

            UIHolder.CenterRowCenterPanelLocalInstanceTextBox = new UltraTextEditor();
            UIHolder.CenterRowCenterPanelLocalInstanceTextBox.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelLocalInstanceTextBox.Text = "C:\\Sage50_12\\Sage50Term";

            // Username
            // Username
            // Username
            // Username
            // Username

            UIHolder.CenterRowCenterPanelUsernameLabel = new UltraLabel();
            UIHolder.CenterRowCenterPanelUsernameLabel.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelUsernameLabel.Text = "Nombre de Usuario";
            UIHolder.CenterRowCenterPanelUsernameLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelUsernameLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            UIHolder.CenterRowCenterPanelUsernameTextBox = new UltraTextEditor();
            UIHolder.CenterRowCenterPanelUsernameTextBox.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelUsernameTextBox.Text = "SUPERVISOR";

            // Password
            // Password
            // Password
            // Password
            // Password

            UIHolder.CenterRowCenterPanelPasswordLabel = new UltraLabel();
            UIHolder.CenterRowCenterPanelPasswordLabel.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelPasswordLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelPasswordLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            UIHolder.CenterRowCenterPanelPasswordLabel.Text = "Contraseña";

            UIHolder.CenterRowCenterPanelPasswordTextBox = new UltraTextEditor();
            UIHolder.CenterRowCenterPanelPasswordTextBox.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelPasswordTextBox.PasswordChar = '*';
            UIHolder.CenterRowCenterPanelPasswordTextBox.Text = "Prueba123!";

            UIHolder.CenterRowCenterPanelValidateUserDataButton = new UltraButton();
            UIHolder.CenterRowCenterPanelValidateUserDataButton.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelValidateUserDataButton.AutoSize = true;
            UIHolder.CenterRowCenterPanelValidateUserDataButton.Margin = new Padding(75, 20, 75, 0);
            UIHolder.CenterRowCenterPanelValidateUserDataButton.Text = "Verificar Datos";

            UIHolder.CenterRowCenterPanelSesionDataValidationLabel = new UltraLabel();
            UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Margin = new Padding(0, 10, 0, 0);
            UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos inválidos para iniciar sesión";

            // RememberData
            // RememberData
            // RememberData
            // RememberData
            // RememberData

            UIHolder.CenterRowCenterPanelRememberDataLabel = new UltraLabel();
            UIHolder.CenterRowCenterPanelRememberDataLabel.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelRememberDataLabel.Margin = new Padding(0, 10, 0, 0);
            UIHolder.CenterRowCenterPanelRememberDataLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelRememberDataLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            UIHolder.CenterRowCenterPanelRememberDataLabel.Text = "¿Desea recordar sus datos?";

            UIHolder.CenterRowCenterPanelRememberDataCheckBox = new UltraCheckEditor();
            UIHolder.CenterRowCenterPanelRememberDataCheckBox.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelRememberDataCheckBox.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelRememberDataCheckBox.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            // EnterpryseGroup
            // EnterpryseGroup
            // EnterpryseGroup
            // EnterpryseGroup
            // EnterpryseGroup

            UIHolder.CenterRowCenterPanelEnterpryseGroupLabel = new UltraLabel();
            UIHolder.CenterRowCenterPanelEnterpryseGroupLabel.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelEnterpryseGroupLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelEnterpryseGroupLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            UIHolder.CenterRowCenterPanelEnterpryseGroupLabel.Text = "Seleccione el grupo de empresa";

            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu = new UltraComboEditor();
            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Margin = new Padding(0, 10, 0, 0);
            
            // ConnectButton
            // ConnectButton
            // ConnectButton
            // ConnectButton
            // ConnectButton

            UIHolder.CenterRowCenterPanelConnectButton = new UltraButton();
            UIHolder.CenterRowCenterPanelConnectButton.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelConnectButton.AutoSize = true;
            UIHolder.CenterRowCenterPanelConnectButton.Margin = new Padding(75,20,75,0);
            UIHolder.CenterRowCenterPanelConnectButton.Text = "Conectar";

            // DisConnectButton
            // DisConnectButton
            // DisConnectButton
            // DisConnectButton
            // DisConnectButton

            UIHolder.CenterRowCenterPanelDisconnectButton = new UltraButton();
            UIHolder.CenterRowCenterPanelDisconnectButton.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelDisconnectButton.AutoSize = true;
            UIHolder.CenterRowCenterPanelDisconnectButton.Margin = new Padding(75, 20, 75, 0);
            UIHolder.CenterRowCenterPanelDisconnectButton.Text = "Desconectar";

            // HanddleEvents
            // HanddleEvents
            // HanddleEvents
            // HanddleEvents
            // HanddleEvents

            UIHolder.CenterRowCenterPanelValidateUserDataButton.Click +=
                new EventHandler(Sage50ConnectionActions.VeryfyUserData);

            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectionChanged +=
                new EventHandler(Sage50ConnectionActions.ChangeCompanyGroup);

            UIHolder.CenterRowCenterPanelConnectButton.Click +=
                new EventHandler(Sage50ConnectionActions.Connect);

            UIHolder.CenterRowCenterPanelDisconnectButton.Click +=
                new EventHandler(Sage50ConnectionActions.Disconnect);

            try
            {
                GestprojectDataManager.SynchronizerUserRememberableDataModel userRememberabledata = GestprojectDataManager.ManageUserData.GetSynchronizerUserRememberableDataForConnection(GestprojectDataHolder.GestprojectDatabaseConnection);

                MessageBox.Show(
                    userRememberabledata.Sage50LocalTerminalPath + "\n" + 
                    userRememberabledata.Sage50Username + "\n" + 
                    userRememberabledata.Sage50Password + "\n" +
                    userRememberabledata.Sage50CompanyGroupName
                );

                if(userRememberabledata != null)
                {
                    if(
                        Sage50ConnectionManager.ConnectionActions.Connect(
                            userRememberabledata.Sage50LocalTerminalPath,
                            userRememberabledata.Sage50Username,
                            userRememberabledata.Sage50Password
                        )
                    )
                    {
                        // Add controls to tab page
                        // Add controls to tab page
                        // Add controls to tab page
                        // Add controls to tab page
                        // Add controls to tab page

                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelStateLabel, 0, 0);
                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelStateStateMessageLabel, 0, 1);
                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelStateIcon1, 0, 2);

                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelTitleLabel, 0, 3);

                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelLocalInstanceLabel, 0, 4);
                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelLocalInstanceTextBox, 0, 5);

                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelUsernameLabel, 0, 6);
                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelUsernameTextBox, 0, 7);

                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelPasswordLabel, 0, 8);
                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelPasswordTextBox, 0, 9);
                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelValidateUserDataButton, 0, 10);

                        // Aditional Controls And Actions for the Stateful UI
                        // Aditional Controls And Actions for the Stateful UI
                        // Aditional Controls And Actions for the Stateful UI
                        // Aditional Controls And Actions for the Stateful UI
                        // Aditional Controls And Actions for the Stateful UI

                        //UIHolder.CenterRowCenterPanelValidateUserDataButton.Enabled = false;
                        UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos correctos";
                        UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.ForeColor = StyleHolder.c_green_1;

                        //UIHolder.CenterRowCenterPanelLocalInstanceTextBox.Enabled = false;
                        //UIHolder.CenterRowCenterPanelUsernameTextBox.Enabled = false;
                        //UIHolder.CenterRowCenterPanelPasswordTextBox.Enabled = false;

                        DataHolder.Sage50CompanyGroupsList = Sage50ConnectionManager.Sage50CompanyGroupActions.GetCompanyGroups();

                        if(UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Count < 1)
                        {
                            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Add("");
                        };

                        for(global::System.Int32 i = 0; i < DataHolder.Sage50CompanyGroupsList.Count; i++)
                        {
                            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Add(DataHolder.Sage50CompanyGroupsList[i].CompanyName);
                            if(userRememberabledata.Sage50CompanyGroupName == DataHolder.Sage50CompanyGroupsList[i].CompanyName)
                            {
                                UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedIndex = i;
                            };
                        };



                        Sage50ConnectionManager.Sage50CompanyGroupActions.ChangeCompanyGroup(
                            userRememberabledata.Sage50CompanyGroupName
                        );

                        UIHolder.CenterRowCenterPanelRememberDataCheckBox.Checked =
                            Convert.ToInt32(userRememberabledata.Remember) == 1 ? true : false;

                        UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedText = userRememberabledata.Sage50CompanyGroupName;
                        UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Text = userRememberabledata.Sage50CompanyGroupName;


                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelSesionDataValidationLabel, 0, 11);

                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelEnterpryseGroupLabel, 0, 12);

                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelEnterpryseGroupMenu, 0, 13);

                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelConnectButton, 0, 16);

                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelRememberDataLabel, 0, 17);

                        UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelRememberDataCheckBox, 0, 18);

                        UIHolder.CenterRowCenterPanelConnectButton.Focus();

                        // Notify Success
                        // Notify Success
                        // Notify Success
                        // Notify Success
                        // Notify Success

                        IsSuccessful = true;
                    }
                    else
                    {
                        MessageBox.Show("Encontramos un error en los datos guardados durante la última sesión");
                        MessageBox.Show("Stateless");
                        if(!new StatelessCenterRowCenterPanelControls().IsSuccessful)
                        {
                            throw new System.Exception("Error generating main window");
                        };

                        IsSuccessful = true;
                    };
                }
                else
                {
                    throw new Exception("Error al obtener la información recordada en la última sesión.");
                };
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error: \n\n{ex.Message}");
            };
        }
    }
}
