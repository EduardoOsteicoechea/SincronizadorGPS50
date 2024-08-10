using Infragistics.Win.Misc;
using System;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Sage50Connection
{
    internal class ManageFullConnectionPanel : ISage50ConnectionUIStateTracker, ISage50ConnectionUIComponent
    {
        public bool IsConnected { get; set; } = false;
        public UltraPanel Panel { get; set; } = null;
        public TableLayoutPanel PanelTableLayoutPanel { get; set; } = null;
        public UltraButton ConnectButton1 { get; set; } = null;
        public UltraButton ConnectButton2 { get; set; } = null;
        public UltraButton ConnectButton3 { get; set; } = null;

        public bool IsDataCleared => throw new NotImplementedException();

        public event EventHandler ConnectionStateChanged;
        public event EventHandler DataCleared;

        public ManageFullConnectionPanel
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
            PanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
            PanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
            PanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
            PanelTableLayoutPanel.Dock = DockStyle.Fill;


            ConnectButton1 = new UltraButton();
            ConnectButton1.Dock = DockStyle.Fill;
            ConnectButton1.AutoSize = true;
            ConnectButton1.Appearance.Image = 0;
            ConnectButton1.Text = "Desconectar";

            ConnectButton2 = new UltraButton();
            ConnectButton2.Dock = DockStyle.Fill;
            ConnectButton2.AutoSize = true;
            ConnectButton2.Appearance.Image = 0;
            ConnectButton2.Text = "Editar datos";

            ConnectButton3 = new UltraButton();
            ConnectButton3.Dock = DockStyle.Fill;
            ConnectButton3.AutoSize = true;
            ConnectButton3.Appearance.Image = 0;
            ConnectButton3.Text = "Resetear";

            PanelTableLayoutPanel.Controls.Add(ConnectButton1, 0, 0);
            PanelTableLayoutPanel.Controls.Add(ConnectButton2, 0, 1);
            PanelTableLayoutPanel.Controls.Add(ConnectButton3, 0, 2);

            Panel.ClientArea.Controls.Add(PanelTableLayoutPanel);

            parentControl.Add(Panel, parentControlColumn, parentControlRow);

        }

        public void ClearData() => throw new NotImplementedException();
        public void DisableControls() => throw new NotImplementedException();
        public void EnableControls() => throw new NotImplementedException();
        public void Forget() => throw new NotImplementedException();
        public void KeepData() => throw new NotImplementedException();
        public void Remember() => throw new NotImplementedException();
        public void SetUIToConnected() => throw new NotImplementedException();
        public void SetUIToDisconnected() => throw new NotImplementedException();
        public void Dispose() => throw new NotImplementedException();
    }
}