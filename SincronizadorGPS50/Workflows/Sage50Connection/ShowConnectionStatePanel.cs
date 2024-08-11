﻿using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using sage.ew.objetos;
using System;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Sage50Connection
{
    internal class ShowConnectionStatePanel : ISage50ConnectionUIStateTracker
    {
        public bool IsConnected { get; set; } = false;
        public UltraPanel Panel { get; set; } = null;
        public TableLayoutPanel PanelTableLayoutPanel { get; set; } = null;
        public UltraLabel StateLabel { get; set; } = null;
        public UltraLabel StateMessageLabel { get; set; } = null;
        public UltraPictureBox StateImage1 { get; set; } = null;
        public UltraPictureBox StateImage2 { get; set; } = null;
        public UltraPictureBox StateImage3 { get; set; } = null;
        public System.Windows.Forms.ImageList ImageList { get; set; } = new ImageList();
        public Sage50ConnectionUIManager Sage50ConnectionUIManager { get; set; } = null;

        public ShowConnectionStatePanel
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
            PanelTableLayoutPanel.RowCount = 3;
            //PanelTableLayoutPanel.RowCount = 2;
            PanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            PanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            //PanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
            PanelTableLayoutPanel.Dock = DockStyle.Fill;

            ImageList.Images.Add(Resources.SemaforoRojo);
            ImageList.Images.Add(Resources.Semaforo_verde);


            StateLabel = new UltraLabel();
            StateLabel.Dock = DockStyle.Fill;
            StateLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            StateLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            StateMessageLabel = new UltraLabel();
            StateMessageLabel.Dock = DockStyle.Fill;
            StateMessageLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            StateMessageLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;



            StateImage1 = new UltraPictureBox();
            StateImage1.Height = 15;
            StateImage1.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            StateImage1.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            StateImage2 = new UltraPictureBox();
            StateImage2.Height = 15;
            StateImage2.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            StateImage2.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            StateImage3 = new UltraPictureBox();
            StateImage3.Height = 15;
            StateImage3.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            StateImage3.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;

            StateImage1.Image = ImageList.Images[0];
            StateImage2.Image = ImageList.Images[0];
            StateImage3.Image = ImageList.Images[0];

            UltraPanel StateIconsPanel = new UltraPanel();
            StateIconsPanel.Dock = DockStyle.Fill;

            TableLayoutPanel StateIconsPanelTableLayoutPanel = new TableLayoutPanel();
            StateIconsPanelTableLayoutPanel.RowCount = 1;
            StateIconsPanelTableLayoutPanel.ColumnCount = 5;
            StateIconsPanelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f));
            StateIconsPanelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
            StateIconsPanelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
            StateIconsPanelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
            StateIconsPanelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35f));
            StateIconsPanelTableLayoutPanel.Dock = DockStyle.Fill;

            StateIconsPanelTableLayoutPanel.Controls.Add(StateImage1, 1, 0);
            StateIconsPanelTableLayoutPanel.Controls.Add(StateImage2, 2, 0);
            StateIconsPanelTableLayoutPanel.Controls.Add(StateImage3, 3, 0);

            StateIconsPanel.ClientArea.Controls.Add(StateIconsPanelTableLayoutPanel);

            SetUIToDisconnected();

            PanelTableLayoutPanel.Controls.Add(StateLabel, 0, 0);
            PanelTableLayoutPanel.Controls.Add(StateIconsPanel, 0, 1);
            //PanelTableLayoutPanel.Controls.Add(StateMessageLabel, 0, 2);

            Panel.ClientArea.Controls.Add(PanelTableLayoutPanel);

            parentControl.Add(Panel, parentControlColumn, parentControlRow);
        }

        public void SetUIToConnected()
        {
            IsConnected = true;
            StateLabel.Text = "Conectado";
            StateMessageLabel.Text = "Iniciamos una sesión de Sage50 con los datos provistos";
            StateMessageLabel.Appearance.ForeColor = StyleHolder.c_green_1;

            StateImage1.Image = ImageList.Images[1];
            StateImage2.Image = ImageList.Images[1];
            StateImage3.Image = ImageList.Images[1];
        }
        public void SetUIToDisconnected()
        {
            IsConnected = false;
            StateLabel.Text = "Desconectado";
            StateMessageLabel.Text = "Ingrese sus datos de Sage50 para iniciar sesión";

            StateImage1.Image = ImageList.Images[0];
            StateImage2.Image = ImageList.Images[0];
            StateImage3.Image = ImageList.Images[0];
        }

        public void Dispose() 
        {
            foreach(Control control in PanelTableLayoutPanel.Controls)
            {
                if(control is IDisposable disposableControl)
                {
                    disposableControl.Dispose();
                    GC.SuppressFinalize(control);
                }
            };

            PanelTableLayoutPanel.Dispose();
            GC.SuppressFinalize(PanelTableLayoutPanel);

            Panel.Dispose();
            GC.SuppressFinalize(Panel);
        }
    }
}