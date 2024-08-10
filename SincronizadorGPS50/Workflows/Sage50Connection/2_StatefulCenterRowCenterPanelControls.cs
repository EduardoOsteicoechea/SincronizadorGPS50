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

            Sage50ConnectionUIHolder.CenterRowCenterPanelStateLabel = new UltraLabel();
            Sage50ConnectionUIHolder.CenterRowCenterPanelStateLabel.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelStateLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Sage50ConnectionUIHolder.CenterRowCenterPanelStateLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            Sage50ConnectionUIHolder.CenterRowCenterPanelStateLabel.Margin = new Padding(0, 10, 0, 0);
            Sage50ConnectionUIHolder.CenterRowCenterPanelStateLabel.Text = "Estado de la conexión";

            Sage50ConnectionUIHolder.CenterRowCenterPanelStateStateMessageLabel = new UltraLabel();
            Sage50ConnectionUIHolder.CenterRowCenterPanelStateStateMessageLabel.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelStateStateMessageLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Sage50ConnectionUIHolder.CenterRowCenterPanelStateStateMessageLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            Sage50ConnectionUIHolder.CenterRowCenterPanelStateStateMessageLabel.Text = "Desconectado";

            Sage50ConnectionUIHolder.CenterRowCenterPanelStateIcon1 = new UltraPictureBox();
            Sage50ConnectionUIHolder.CenterRowCenterPanelStateIcon1.Image = Resources.SemaforoRojo;
            Sage50ConnectionUIHolder.CenterRowCenterPanelStateIcon1.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelStateIcon1.Height = 20;
            Sage50ConnectionUIHolder.CenterRowCenterPanelStateIcon1.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Sage50ConnectionUIHolder.CenterRowCenterPanelStateIcon1.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            // Title
            // Title
            // Title
            // Title
            // Title

            Sage50ConnectionUIHolder.CenterRowCenterPanelTitleLabel = new UltraLabel();
            Sage50ConnectionUIHolder.CenterRowCenterPanelTitleLabel.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelTitleLabel.Font= new Font(new UltraLabel().Font.FontFamily, 11, FontStyle.Bold);
            Sage50ConnectionUIHolder.CenterRowCenterPanelTitleLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Sage50ConnectionUIHolder.CenterRowCenterPanelTitleLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            Sage50ConnectionUIHolder.CenterRowCenterPanelTitleLabel.Text = "Datos para conectar a Sage50";

            // LocalInstance
            // LocalInstance
            // LocalInstance
            // LocalInstance
            // LocalInstance

            Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceLabel = new UltraLabel();
            Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceLabel.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceLabel.Text = "Especifique el terminal de Sage50";

            Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceTextBox = new UltraTextEditor();
            Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceTextBox.Dock = DockStyle.Fill;

            // Username
            // Username
            // Username
            // Username
            // Username

            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameLabel = new UltraLabel();
            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameLabel.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameLabel.Text = "Nombre de Usuario";
            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameTextBox = new UltraTextEditor();
            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameTextBox.Dock = DockStyle.Fill;

            // Password
            // Password
            // Password
            // Password
            // Password

            Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordLabel = new UltraLabel();
            Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordLabel.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordLabel.Text = "Contraseña";

            Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordTextBox = new UltraTextEditor();
            Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordTextBox.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordTextBox.PasswordChar = '*';

            Sage50ConnectionUIHolder.CenterRowCenterPanelValidateUserDataButton = new UltraButton();
            Sage50ConnectionUIHolder.CenterRowCenterPanelValidateUserDataButton.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelValidateUserDataButton.AutoSize = true;
            Sage50ConnectionUIHolder.CenterRowCenterPanelValidateUserDataButton.Margin = new Padding(75, 20, 75, 0);
            Sage50ConnectionUIHolder.CenterRowCenterPanelValidateUserDataButton.Text = "Verificar Datos";

            Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel = new UltraLabel();
            Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel.Margin = new Padding(0, 10, 0, 0);
            Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos inválidos para iniciar sesión";

            // RememberData
            // RememberData
            // RememberData
            // RememberData
            // RememberData

            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataPanel = new UltraPanel();
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataPanel.Height = 25;
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataPanel.Margin = new Padding(0, 20, 0, 0);

            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataLabel = new UltraLabel();
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataLabel.Width = Convert.ToInt32(Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataPanel.Width * .8);
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataLabel.Location = new Point(
                Convert.ToInt32(Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataPanel.Width * 0.2),
                0
            );
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataLabel.Text = "¿Desea recordar sus datos?";

            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataCheckBox = new UltraCheckEditor();
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataCheckBox.Width = Convert.ToInt32(Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataPanel.Width * .1);
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataCheckBox.Location = new Point(
                Convert.ToInt32(Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataPanel.Width * 1.1), 
                0
            );
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataCheckBox.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataCheckBox.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataPanel.ClientArea.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataLabel);
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataPanel.ClientArea.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataCheckBox);

            // EnterpryseGroup
            // EnterpryseGroup
            // EnterpryseGroup
            // EnterpryseGroup
            // EnterpryseGroup

            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupLabel = new UltraLabel();
            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupLabel.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupLabel.Text = "Seleccione el grupo de empresa";

            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu = new UltraComboEditor();
            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Margin = new Padding(0, 10, 0, 0);
            
            // ConnectButton
            // ConnectButton
            // ConnectButton
            // ConnectButton
            // ConnectButton

            Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton = new UltraButton();
            Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton.AutoSize = true;
            Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton.Margin = new Padding(75,25,75,0);
            Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton.Text = "Conectar";

            // DisConnectButton
            // DisConnectButton
            // DisConnectButton
            // DisConnectButton
            // DisConnectButton

            Sage50ConnectionUIHolder.CenterRowCenterPanelDisconnectButton = new UltraButton();
            Sage50ConnectionUIHolder.CenterRowCenterPanelDisconnectButton.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelDisconnectButton.AutoSize = true;
            Sage50ConnectionUIHolder.CenterRowCenterPanelDisconnectButton.Margin = new Padding(75, 20, 75, 0);
            Sage50ConnectionUIHolder.CenterRowCenterPanelDisconnectButton.Text = "Desconectar";

            // HanddleEvents
            // HanddleEvents
            // HanddleEvents
            // HanddleEvents
            // HanddleEvents

            Sage50ConnectionUIHolder.CenterRowCenterPanelValidateUserDataButton.Click += Sage50ConnectionActions.VeryfyUserData;

            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectionChanged += Sage50ConnectionActions.ChangeCompanyGroup;

            Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton.Click += Sage50ConnectionActions.Connect;

            Sage50ConnectionUIHolder.CenterRowCenterPanelDisconnectButton.Click += Sage50ConnectionActions.Disconnect;
            
            // Prevent TextChanged events from firing on MainWindow.Load event
            MainWindowUIHolder.MainWindow.Load += (object sender, System.EventArgs e) => 
            {
                Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceTextBox.TextChanged += Sage50ConnectionActions.InforceDataValidation;

                Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameTextBox.TextChanged += Sage50ConnectionActions.InforceDataValidation;

                Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordTextBox.TextChanged += Sage50ConnectionActions.InforceDataValidation;
            };


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
                        Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceTextBox.Text = userRememberabledata.Sage50LocalTerminalPath;
                        Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameTextBox.Text = userRememberabledata.Sage50Username;
                        Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordTextBox.Text = userRememberabledata.Sage50Password;

                        // Add controls to tab page
                        // Add controls to tab page
                        // Add controls to tab page
                        // Add controls to tab page
                        // Add controls to tab page

                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelStateLabel, 0, 0);
                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelStateStateMessageLabel, 0, 1);
                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelStateIcon1, 0, 2);

                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelTitleLabel, 0, 3);

                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceLabel, 0, 4);
                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceTextBox, 0, 5);

                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameLabel, 0, 6);
                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameTextBox, 0, 7);

                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordLabel, 0, 8);
                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordTextBox, 0, 9);
                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelValidateUserDataButton, 0, 10);

                        // Aditional Controls And Actions for the Stateful UI
                        // Aditional Controls And Actions for the Stateful UI
                        // Aditional Controls And Actions for the Stateful UI
                        // Aditional Controls And Actions for the Stateful UI
                        // Aditional Controls And Actions for the Stateful UI

                        //Sage50ConnectionUIHolder.CenterRowCenterPanelValidateUserDataButton.Enabled = false;
                        Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos correctos";
                        Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.ForeColor = StyleHolder.c_green_1;

                        //Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceTextBox.Enabled = false;
                        //Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameTextBox.Enabled = false;
                        //Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordTextBox.Enabled = false;

                        DataHolder.Sage50CompanyGroupsList = Sage50ConnectionManager.Sage50CompanyGroupActions.GetCompanyGroups();

                        if(Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Count < 1)
                        {
                            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Add("");
                        };

                        for(global::System.Int32 i = 0; i < DataHolder.Sage50CompanyGroupsList.Count; i++)
                        {
                            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Add(DataHolder.Sage50CompanyGroupsList[i].CompanyName);
                            if(userRememberabledata.Sage50CompanyGroupName == DataHolder.Sage50CompanyGroupsList[i].CompanyName)
                            {
                                   Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedIndex = i;
                            };
                        };



                        Sage50ConnectionManager.Sage50CompanyGroupActions.ChangeCompanyGroup(
                            userRememberabledata.Sage50CompanyGroupName
                        );

                        Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataCheckBox.Checked =
                            Convert.ToInt32(userRememberabledata.Remember) == 1 ? true : false;

                        Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedText = userRememberabledata.Sage50CompanyGroupName;

                        Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Text = userRememberabledata.Sage50CompanyGroupName;


                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelSesionDataValidationLabel, 0, 11);

                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupLabel, 0, 12);

                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu, 0, 13);

                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataPanel, 0, 14);

                        Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton, 0, 15);

                        Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton.Focus();

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
