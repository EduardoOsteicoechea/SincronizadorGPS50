using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Sage50Connection
{
    internal class ValidateTerminalUserDataPanel : ISage50ConnectionUIStateTracker, ISage50ConnectionUIModifiableControls
    {
        public bool IsConnected { get; set; } = false;
        public bool AreControlsEnabled { get; set; } = false;
        public System.Windows.Forms.ImageList ImageList {  get; set; } = new ImageList();
        public UltraButton ConnectButton {  get; set; } = null;
        public UltraPanel Panel { get; set; } = null;
        public TableLayoutPanel PanelTableLayoutPanel { get; set; } = null;
        public Sage50ConnectionUIManager Sage50ConnectionUIManager { get; set; } = null;

        public ValidateTerminalUserDataPanel
        (
            Sage50ConnectionUIManager sage50ConnectionUIManager,
            System.Windows.Forms.TableLayoutControlCollection parentControl,
            int parentControlColumn,
            int parentControlRow
        )
        {
            Sage50ConnectionUIManager = sage50ConnectionUIManager;

            Panel = new UltraPanel();
            Panel.Dock = DockStyle.Fill;

            PanelTableLayoutPanel = new TableLayoutPanel();
            PanelTableLayoutPanel.ColumnCount = 1;
            PanelTableLayoutPanel.RowCount = 1;
            PanelTableLayoutPanel.Dock = DockStyle.Fill;

            ImageList.Images.Add(Resources.SemaforoRojo);
            ImageList.Images.Add(Resources.Semaforo_verde);

            ConnectButton = new UltraButton();
            ConnectButton.Dock = DockStyle.Fill;
            ConnectButton.AutoSize = true;
            ConnectButton.ShowOutline = false;
            ConnectButton.ImageList = ImageList;
            ConnectButton.Appearance.Image = 0;
            ConnectButton.Text = "Validar Terminal";


            PanelTableLayoutPanel.Controls.Add(ConnectButton, 0, 0);
            PanelTableLayoutPanel.SetColumnSpan(ConnectButton, 1);
            PanelTableLayoutPanel.SetRowSpan(ConnectButton, 1);

            Panel.ClientArea.Controls.Add(PanelTableLayoutPanel);

            parentControl.Add(Panel, parentControlColumn, parentControlRow);

            // Handle Events
            // Handle Events
            // Handle Events
            // Handle Events
            // Handle Events

            ConnectButton.Click += ConnectButton_Click;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Sage50ConnectionUIManager.GetLocalTerminalUserDataUI.SetUIToConnected();
            if(
                Sage50ConnectionManager.ConnectionActions.Connect(
                    Sage50ConnectionUIManager.GetLocalTerminalUserDataUI.LocalInstanceTextBox.Text,
                    Sage50ConnectionUIManager.GetLocalTerminalUserDataUI.UsernameTextBox.Text,
                    Sage50ConnectionUIManager.GetLocalTerminalUserDataUI.PasswordTextBox.Text
                )
            )
            {
                ConnectButton.Appearance.Image = 1;
                Sage50ConnectionUIManager.ShowConnectionStateUI.StateImage1.Image = Sage50ConnectionUIManager.ShowConnectionStateUI.ImageList.Images[1];
                Sage50ConnectionUIManager.SetValidatedTerminalSelectCompanyGroupUI();
            }
            else
            {
                Sage50ConnectionUIManager.GetLocalTerminalUserDataUI.SetUIToDisconnected();
                ConnectButton.Appearance.Image = 0;
                Sage50ConnectionUIManager.ShowConnectionStateUI.StateImage1.Image = Sage50ConnectionUIManager.ShowConnectionStateUI.ImageList.Images[0];
            }
        }

        public void SetUIToAwaitingForData()
        {
            DisableControls();
        }
        public void SetUIToReadyForValidation()
        {
            EnableControls();
        }
        public void SetUIToValidationExecuted()
        {
            DisableControls();
        }
        public void SetUIToConnected()
        {
            IsConnected = true;
            DisableControls();
        }
        public void SetUIToDisconnected()
        {
            IsConnected = false;
            EnableControls();
        }
        public void EnableControls()
        {
            ConnectButton.Enabled = true;
        }
        public void DisableControls()
        {
            ConnectButton.Enabled = false;
        }
        public void Dispose()
        {
            Panel.Dispose();
            GC.SuppressFinalize(Panel);
        }
    }
}