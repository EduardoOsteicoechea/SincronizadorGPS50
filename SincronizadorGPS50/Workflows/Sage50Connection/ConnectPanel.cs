using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Sage50Connection
{
    internal class ConnectPanel : ISage50ConnectionUIStateTracker, ISage50ConnectionUIComponent
    {
        public bool IsConnected { get; set; } = false;
        public UltraPanel Panel { get; set; } = null;
        public TableLayoutPanel PanelTableLayoutPanel { get; set; } = null;
        public UltraButton ConnectButton { get; set; } = null;
        public System.Windows.Forms.ImageList ImageList { get; set; } = new ImageList();

        public bool IsDataCleared => throw new NotImplementedException();

        public event EventHandler ConnectionStateChanged;
        public event EventHandler DataCleared;

        public ConnectPanel
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
            ConnectButton.Text = "Conectar";

            PanelTableLayoutPanel.Controls.Add(ConnectButton, 0, 0);

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