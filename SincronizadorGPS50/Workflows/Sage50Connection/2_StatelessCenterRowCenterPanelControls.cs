using Infragistics.Win.Misc;
using System;
using Infragistics.Win.UltraWinEditors;
using System.Windows.Forms;
using System.Drawing;

namespace SincronizadorGPS50.Workflows.Sage50Connection.Sage50ConnectionTabUI
{
    internal class StatelessCenterRowCenterPanelControls
    {
        internal bool IsSuccessful {  get; set; } = false;
        internal StatelessCenterRowCenterPanelControls() 
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
            Sage50ConnectionUIHolder.CenterRowCenterPanelLocalInstanceTextBox.Text = "C:\\Sage50_13\\Sage50Term";

            // Username
            // Username
            // Username
            // Username
            // Username

            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameLabel = new UltraLabel();
            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameLabel.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameLabel.Text = "Nombre de Usuario";

            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameTextBox = new UltraTextEditor();
            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameTextBox.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelUsernameTextBox.Text = "SUPERVISOR";

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
            Sage50ConnectionUIHolder.CenterRowCenterPanelPasswordTextBox.Text = "Prueba123!";

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

            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataLabel = new UltraLabel();
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataLabel.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataLabel.Margin = new Padding(0, 10, 0, 0);
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataLabel.Text = "¿Desea recordar sus datos?";

            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataCheckBox = new UltraCheckEditor();
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataCheckBox.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataCheckBox.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            Sage50ConnectionUIHolder.CenterRowCenterPanelRememberDataCheckBox.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

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
            Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton.Margin = new Padding(75,20,75,0);
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

            Sage50ConnectionUIHolder.CenterRowCenterPanelValidateUserDataButton.Click +=
                new EventHandler(Sage50ConnectionActions.VeryfyUserData);

            Sage50ConnectionUIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectionChanged +=
                new EventHandler(Sage50ConnectionActions.ChangeCompanyGroup);

            Sage50ConnectionUIHolder.CenterRowCenterPanelConnectButton.Click +=
                new EventHandler(Sage50ConnectionActions.Connect);

            Sage50ConnectionUIHolder.CenterRowCenterPanelDisconnectButton.Click +=
                new EventHandler(Sage50ConnectionActions.Disconnect);

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

            IsSuccessful = true;
        }
    }
}
