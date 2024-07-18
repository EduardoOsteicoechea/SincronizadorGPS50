using Infragistics.Win.Misc;
using System;
using Infragistics.Win.UltraWinEditors;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using SincronizadorGPS50.Sage50API;

namespace SincronizadorGPS50.Workflows.Sage50Connection.Sage50ConnectionTabUI
{
    internal class CenterRowCenterPanelControls
    {
        //internal int panelWidth {  get; set; } = UIHolder.Sage50ConnectionCenterRowCenterPanel.Width;
        internal int VariableHeight {  get; set; } = 30;
        internal CenterRowCenterPanelControls() 
        {
            // ConnectionState
            // ConnectionState
            // ConnectionState
            // ConnectionState
            // ConnectionState

            UIHolder.CenterRowCenterPanelStateLabel = new UltraLabel();
            UIHolder.CenterRowCenterPanelStateLabel.Location = new System.Drawing.Point(0, VariableHeight);
            UIHolder.CenterRowCenterPanelStateLabel.Text = "Estado de la conexión";
            UIHolder.CenterRowCenterPanelStateLabel.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelStateLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelStateLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            UIHolder.CenterRowCenterPanelStateLabel.Margin = new Padding(0, 10, 0, 0);

            UIHolder.CenterRowCenterPanelStateStateMessageLabel = new UltraLabel();
            UIHolder.CenterRowCenterPanelStateStateMessageLabel.Location = new System.Drawing.Point(0, VariableHeight);
            UIHolder.CenterRowCenterPanelStateStateMessageLabel.Text = "Desconectado";
            UIHolder.CenterRowCenterPanelStateStateMessageLabel.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelStateStateMessageLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelStateStateMessageLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            string CenterRowCenterPanelStateIcon1ImagePath = Application.StartupPath + @"\Media\Image\Semaforo rojo.png";
            Image CenterRowCenterPanelStateIcon1Image = null;
            if(File.Exists(CenterRowCenterPanelStateIcon1ImagePath))
            {
                CenterRowCenterPanelStateIcon1Image = Image.FromFile(CenterRowCenterPanelStateIcon1ImagePath);
            };

            UIHolder.CenterRowCenterPanelStateIcon1 = new UltraPictureBox();
            UIHolder.CenterRowCenterPanelStateIcon1.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelStateIcon1.Image = CenterRowCenterPanelStateIcon1Image;
            UIHolder.CenterRowCenterPanelStateIcon1.Location = new System.Drawing.Point(0, VariableHeight);
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
            UIHolder.CenterRowCenterPanelTitleLabel.Text = "Datos para conectar a Sage50";
            UIHolder.CenterRowCenterPanelTitleLabel.Font= new Font(new UltraLabel().Font.FontFamily, 11, FontStyle.Bold);
            UIHolder.CenterRowCenterPanelTitleLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelTitleLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            // LocalInstance
            // LocalInstance
            // LocalInstance
            // LocalInstance
            // LocalInstance

            UIHolder.CenterRowCenterPanelLocalInstanceLabel = new UltraLabel();
            UIHolder.CenterRowCenterPanelLocalInstanceLabel.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelLocalInstanceLabel.Text = "Especifique la instalación de Sage50";
            UIHolder.CenterRowCenterPanelLocalInstanceLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelLocalInstanceLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            UIHolder.CenterRowCenterPanelLocalInstanceTextBox = new UltraTextEditor();
            UIHolder.CenterRowCenterPanelLocalInstanceTextBox.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelLocalInstanceTextBox.Text = "C:\\Sage50_11\\Sage50Term";

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
            UIHolder.CenterRowCenterPanelPasswordLabel.Text = "Contraseña";
            UIHolder.CenterRowCenterPanelPasswordLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelPasswordLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            UIHolder.CenterRowCenterPanelPasswordTextBox = new UltraTextEditor();
            UIHolder.CenterRowCenterPanelPasswordTextBox.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelPasswordTextBox.Text = "Prueba123!";
            UIHolder.CenterRowCenterPanelPasswordTextBox.PasswordChar = '*';

            UIHolder.CenterRowCenterPanelValidateUserDataButton = new UltraButton();
            UIHolder.CenterRowCenterPanelValidateUserDataButton.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelValidateUserDataButton.AutoSize = true;
            UIHolder.CenterRowCenterPanelValidateUserDataButton.Margin = new Padding(75, 20, 75, 0);
            UIHolder.CenterRowCenterPanelValidateUserDataButton.Text = "Verificar Datos";
            var ValidateUserDataButtonEventHandler = new Sage50ConnectionActions();
            UIHolder.CenterRowCenterPanelValidateUserDataButton.Click += new EventHandler(ValidateUserDataButtonEventHandler.VeryfyUserData);

            UIHolder.CenterRowCenterPanelSesionDataValidationLabel = new UltraLabel();
            UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos inválidos para iniciar sesión";
            UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Margin = new Padding(0, 10, 0, 0);
            UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            // EnterpryseGroup
            // EnterpryseGroup
            // EnterpryseGroup
            // EnterpryseGroup
            // EnterpryseGroup

            UIHolder.CenterRowCenterPanelEnterpryseGroupLabel = new UltraLabel();
            UIHolder.CenterRowCenterPanelEnterpryseGroupLabel.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelEnterpryseGroupLabel.Text = "Seleccione el grupo de empresa";
            UIHolder.CenterRowCenterPanelEnterpryseGroupLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UIHolder.CenterRowCenterPanelEnterpryseGroupLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu = new UltraComboEditor();
            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Dock = DockStyle.Fill;
            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Margin = new Padding(0, 10, 0, 0);
            var Sage50CompanyGroupMenuEventHandler = new Sage50CompanyGroupActions();
            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectionChanged += new EventHandler(Sage50CompanyGroupMenuEventHandler.ChangeCompanyGroup);
            
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
            UIHolder.CenterRowCenterPanelConnectButton.TabStop = false;
            var ConnectButtonEventHandler = new Sage50ConnectionActions();
            UIHolder.CenterRowCenterPanelConnectButton.Click += new EventHandler(ConnectButtonEventHandler.Connect);

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
            UIHolder.CenterRowCenterPanelDisconnectButton.TabStop = false;
            var DisconnectButtonEventHandler = new Sage50ConnectionActions();
            UIHolder.CenterRowCenterPanelDisconnectButton.Click += new EventHandler(DisconnectButtonEventHandler.Disconnect);

            // ProcessingSpinner
            // ProcessingSpinner
            // ProcessingSpinner
            // ProcessingSpinner
            // ProcessingSpinner

            UIHolder.CenterRowCenterPanelConnectingSpinner = new UltraPictureBox();
            UIHolder.CenterRowCenterPanelConnectingSpinner.Dock = DockStyle.Fill;

            string CenterRowCenterPanelConnectingSpinnerImagePath = Application.StartupPath + @"\Media\Image\loading_spinner_1.gif";
            Image CenterRowCenterPanelConnectingSpinnerImage = null;
            if(File.Exists(CenterRowCenterPanelConnectingSpinnerImagePath))
            {
                CenterRowCenterPanelConnectingSpinnerImage = Image.FromFile(CenterRowCenterPanelConnectingSpinnerImagePath);
            };
            UIHolder.CenterRowCenterPanelConnectingSpinner.Image = CenterRowCenterPanelConnectingSpinnerImage;
            UIHolder.CenterRowCenterPanelConnectingSpinner.Margin = new Padding(75, 20, 75, 0);

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

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelConnectingSpinner, 0, 17);
        }
    }
}
