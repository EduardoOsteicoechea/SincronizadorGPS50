
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Sage50Connection
{
    internal class ValidateCompanyGroupPanel : ISage50ConnectionUIStateTracker, ISage50ConnectionUIComponent
    {
        public bool IsConnected { get; set; } = false;
        public UltraPanel Panel { get; set; } = null;
        public TableLayoutPanel PanelTableLayoutPanel { get; set; } = null;
        public UltraLabel SelectEnterpryseGroupLabel { get; set; } = null;
        public UltraComboEditor SelectEnterpryseGroupMenu { get; set; } = null;
        public UltraButton ConnectButton { get; set; } = null;
        public System.Windows.Forms.ImageList ImageList { get; set; } = new ImageList();
        public Sage50ConnectionUIManager Sage50ConnectionUIManager { get; set; } = null;

        public bool IsDataCleared => throw new NotImplementedException();

        public event EventHandler ConnectionStateChanged;
        public event EventHandler DataCleared;

        public ValidateCompanyGroupPanel
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
            PanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            PanelTableLayoutPanel.Dock = DockStyle.Fill;

            ImageList.Images.Add(Resources.SemaforoRojo);
            ImageList.Images.Add(Resources.Semaforo_verde);

            ConnectButton = new UltraButton();
            ConnectButton.Dock = DockStyle.Fill;
            ConnectButton.AutoSize = true;
            ConnectButton.ImageList = ImageList;
            ConnectButton.Appearance.Image = 0;
            ConnectButton.Text = "Validar Grupo de Empresa";
            ConnectButton.Click += ConnectButton_Click;

            PanelTableLayoutPanel.Controls.Add(ConnectButton, 0, 0);

            Panel.ClientArea.Controls.Add(PanelTableLayoutPanel);

            parentControl.Add(Panel, parentControlColumn, parentControlRow);
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if(
                true
            )
            {
                ConnectButton.Appearance.Image = 1;
                Sage50ConnectionUIManager.ShowConnectionStateUI.StateImage2.Image = Sage50ConnectionUIManager.ShowConnectionStateUI.ImageList.Images[1];
                Sage50ConnectionUIManager.SetValidatedCompanyGroupAwaitingConnectionUI();
            }
            else
            {
                ConnectButton.Appearance.Image = 0;
                Sage50ConnectionUIManager.ShowConnectionStateUI.StateImage2.Image = Sage50ConnectionUIManager.ShowConnectionStateUI.ImageList.Images[0];
            }
        }

        public void EnableControls()
        {
            ConnectButton.Enabled = true;
        }
        public void DisableControls()
        {
            ConnectButton.Enabled = false;
        }
        public void SetUIToConnected()
        {
            DisableControls();
        }
        public void SetUIToDisconnected()
        {
            EnableControls();
        }
        public void ClearData() => throw new NotImplementedException();
        public void Forget() => throw new NotImplementedException();
        public void KeepData() => throw new NotImplementedException();
        public void Remember() {}
        public void Dispose()
        {
            Panel.Dispose();
            GC.SuppressFinalize(Panel);
        }
    }
}