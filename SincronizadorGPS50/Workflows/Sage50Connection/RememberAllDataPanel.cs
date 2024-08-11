﻿using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Sage50Connection
{
    internal class RememberAllDataPanel : ISage50ConnectionUIStateTracker, ISage50ConnectionUIComponent
    {
        public bool IsConnected { get; set; } = false;
        public UltraPanel Panel { get; set; } = null;
        public TableLayoutPanel PanelTableLayoutPanel { get; set; } = null;
        public UltraLabel RememberLabel { get; set; } = null;
        public UltraCheckEditor CheckBox { get; set; } = null;

        public bool IsDataCleared => throw new NotImplementedException();

        public event EventHandler ConnectionStateChanged;
        public event EventHandler DataCleared;

        public RememberAllDataPanel
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
            PanelTableLayoutPanel.ColumnCount = 4;
            PanelTableLayoutPanel.RowCount = 0;
            PanelTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 30f));
            PanelTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 25f));
            PanelTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 15f));
            PanelTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 30f));
            PanelTableLayoutPanel.Dock = DockStyle.Fill;

            RememberLabel = new UltraLabel();
            RememberLabel.Dock = DockStyle.Fill;
            RememberLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            RememberLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            RememberLabel.Text = "¿Recordar?";

            CheckBox = new UltraCheckEditor();
            CheckBox.Width = 15;
            CheckBox.Height = 20;
            CheckBox.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            CheckBox.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            PanelTableLayoutPanel.Controls.Add(RememberLabel, 1, 0);
            PanelTableLayoutPanel.Controls.Add(CheckBox, 2, 0);

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