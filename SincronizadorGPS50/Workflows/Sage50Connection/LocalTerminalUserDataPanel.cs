using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using System;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Sage50Connection
{
    internal class LocalTerminalUserDataPanel : ISage50ConnectionUIStateTracker, ISage50ConnectionUIComponent
    {

        public bool IsDataCleared => throw new NotImplementedException();

        public event EventHandler ConnectionStateChanged;
        public event EventHandler DataCleared;
        public bool IsConnected { get; set; } = false;
        public UltraPanel Panel { get; set; } = null;
        public TableLayoutPanel PanelTableLayoutPanel { get; set; } = null;
        public UltraLabel LocalInstanceLabel { get; set; } = null;
        public UltraTextEditor LocalInstanceTextBox { get; set; } = null;
        public UltraLabel UsernameLabel { get; set; } = null;
        public UltraTextEditor UsernameTextBox { get; set; } = null;
        public UltraLabel PasswordLabel { get; set; } = null;
        public UltraTextEditor PasswordTextBox { get; set; } = null;
        public UltraButton ValidateUserDataButton { get; set; } = null;
        public UltraLabel SesionDataValidationLabel { get; set; } = null;


        public LocalTerminalUserDataPanel
        (
            Sage50ConnectionUIManager sage50ConnectionUIManager,
            System.Windows.Forms.TableLayoutControlCollection parentControl,
            int parentControlColumn,
            int parentControlRow
        )
        {
            Panel = new UltraPanel();
            Panel.Dock = DockStyle.Fill;

            PanelTableLayoutPanel = new TableLayoutPanel();
            PanelTableLayoutPanel.ColumnCount = 1;
            PanelTableLayoutPanel.RowCount = 6;
            PanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.5f));
            PanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.5f));
            PanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.5f));
            PanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.5f));
            PanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.5f));
            PanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.5f));
            PanelTableLayoutPanel.Dock = DockStyle.Fill;

            // LocalInstance
            // LocalInstance
            // LocalInstance
            // LocalInstance
            // LocalInstance

            LocalInstanceLabel = new UltraLabel();
            LocalInstanceLabel.Dock = DockStyle.Fill;
            LocalInstanceLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            LocalInstanceLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            LocalInstanceLabel.Text = "Especifique el terminal de Sage50";

            LocalInstanceTextBox = new UltraTextEditor();
            LocalInstanceTextBox.Dock = DockStyle.Fill;

            // Username
            // Username
            // Username
            // Username
            // Username

            UsernameLabel = new UltraLabel();
            UsernameLabel.Dock = DockStyle.Fill;
            UsernameLabel.Text = "Nombre de Usuario";
            UsernameLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            UsernameLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            UsernameTextBox = new UltraTextEditor();
            UsernameTextBox.Dock = DockStyle.Fill;

            // Password
            // Password
            // Password
            // Password
            // Password

            PasswordLabel = new UltraLabel();
            PasswordLabel.Dock = DockStyle.Fill;
            PasswordLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            PasswordLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            PasswordLabel.Text = "Contraseña";

            PasswordTextBox = new UltraTextEditor();
            PasswordTextBox.Dock = DockStyle.Fill;
            PasswordTextBox.PasswordChar = '*';

            ValidateUserDataButton = new UltraButton();
            ValidateUserDataButton.Dock = DockStyle.Fill;
            ValidateUserDataButton.AutoSize = true;
            ValidateUserDataButton.Margin = new Padding(75, 20, 75, 0);
            ValidateUserDataButton.Text = "Verificar Datos";

            SesionDataValidationLabel = new UltraLabel();
            SesionDataValidationLabel.Dock = DockStyle.Fill;
            SesionDataValidationLabel.Margin = new Padding(0, 10, 0, 0);
            SesionDataValidationLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            SesionDataValidationLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            SesionDataValidationLabel.Text = "Datos inválidos para iniciar sesión";

            // AddToPanel
            // AddToPanel
            // AddToPanel
            // AddToPanel
            // AddToPanel

            PanelTableLayoutPanel.Controls.Add(LocalInstanceLabel, 0, 0);
            PanelTableLayoutPanel.Controls.Add(LocalInstanceTextBox, 0, 1);
            PanelTableLayoutPanel.Controls.Add(UsernameLabel, 0, 2);
            PanelTableLayoutPanel.Controls.Add(UsernameTextBox, 0, 3);
            PanelTableLayoutPanel.Controls.Add(PasswordLabel, 0, 4);
            PanelTableLayoutPanel.Controls.Add(PasswordTextBox, 0, 5);

            Panel.ClientArea.Controls.Add(PanelTableLayoutPanel);

            parentControl.Add(Panel, parentControlColumn, parentControlRow);
        }

        public void EnableControls() => throw new NotImplementedException();
        public void ClearData() => throw new NotImplementedException();
        public void DisableControls() => throw new NotImplementedException();
        public void Forget() => throw new NotImplementedException();
        public void KeepData() => throw new NotImplementedException();
        public void Remember() {}
        public void SetUIToConnected() => throw new NotImplementedException();
        public void SetUIToDisconnected() => throw new NotImplementedException();
        public void Dispose() => throw new NotImplementedException();
    }
}