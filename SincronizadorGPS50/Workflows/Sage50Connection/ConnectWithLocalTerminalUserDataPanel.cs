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
    internal class ConnectWithLocalTerminalUserDataPanel : ISage50ConnectionUIStateTracker, ISage50ConnectionUIModifiableControls
    {
        public bool IsConnected { get; set; } = false;
        public bool AreControlsEnabled { get; set; } = false;
        public System.Windows.Forms.ImageList ImageList {  get; set; } = new ImageList();
        public UltraButton ConnectButton {  get; set; } = null;
        public UltraPanel Panel { get; set; } = null;
        public TableLayoutPanel PanelTableLayoutPanel { get; set; } = null;

        public ConnectWithLocalTerminalUserDataPanel
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
            //ConnectButton.HotTrackAppearance.Image = 1;
            //ConnectButton.PressedAppearance.Image = 0;
            //ConnectButton.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;


            PanelTableLayoutPanel.Controls.Add(ConnectButton, 0, 0);
            PanelTableLayoutPanel.SetColumnSpan(ConnectButton, 1);
            PanelTableLayoutPanel.SetRowSpan(ConnectButton, 1);

            Panel.ClientArea.Controls.Add(PanelTableLayoutPanel);

            parentControl.Add(Panel, parentControlColumn, parentControlRow);
        }
        public void SetUIToConnected() => throw new NotImplementedException();
        public void SetUIToDisconnected() => throw new NotImplementedException();
        public void EnableControls() => throw new NotImplementedException();
        public void DisableControls() => throw new NotImplementedException();
        public void Dispose() => throw new NotImplementedException();
    }
}