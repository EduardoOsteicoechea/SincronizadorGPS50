using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using sage.ew.db;
using SincronizadorGPS50.GestprojectAPI;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class TopRowUI
    {
        internal TopRowUI()
        {
            ClientsUIHolder.TopRowTableLayoutPanel = new TableLayoutPanel();
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnCount = 3;
            ClientsUIHolder.TopRowTableLayoutPanel.RowCount = 1;
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 84f));
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 150));
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 150));
            ClientsUIHolder.TopRowTableLayoutPanel.Dock = DockStyle.Fill;

            ClientsUIHolder.TopRowMainInstructionLabel = new UltraLabel();
            ClientsUIHolder.TopRowMainInstructionLabel.Text = "Visualize el estado actual de sus clientes respecto a la información de Sage50. Renderizado el " + DateTime.UtcNow.ToShortDateString().ToString() + " en el horario " + DateTime.Now.TimeOfDay.ToString();
            ;
            ClientsUIHolder.TopRowMainInstructionLabel.Dock = DockStyle.Fill;
            ClientsUIHolder.TopRowMainInstructionLabel.Appearance.TextVAlign = VAlign.Middle;


            ClientsUIHolder.TopRowRefreshTableButton = new UltraButton();
            ClientsUIHolder.TopRowRefreshTableButton.Text = "Refrescar";
            ClientsUIHolder.TopRowRefreshTableButton.Dock = DockStyle.Fill;
            ClientsUIHolder.TopRowRefreshTableButton.Click += TopRowRefreshTableButtonEvents.Click;


            ClientsUIHolder.TopRowCloseButton = new UltraButton();
            ClientsUIHolder.TopRowCloseButton.Text = "Salir";
            ClientsUIHolder.TopRowCloseButton.Dock = DockStyle.Fill;
            ClientsUIHolder.TopRowCloseButton.Click += new EventHandler((object sender, System.EventArgs e) => {
                Application.ExitThread();
                Application.Exit();
                MainWindowUIHolder.MainWindow.Close();
            });


            ClientsUIHolder.TopRow.ClientArea.Controls.Add(ClientsUIHolder.TopRowTableLayoutPanel);


            ClientsUIHolder.TopRowTableLayoutPanel.Controls.Add(ClientsUIHolder.TopRowMainInstructionLabel, 0, 0);
            ClientsUIHolder.TopRowTableLayoutPanel.Controls.Add(ClientsUIHolder.TopRowRefreshTableButton, 1, 0);
            ClientsUIHolder.TopRowTableLayoutPanel.Controls.Add(ClientsUIHolder.TopRowCloseButton, 2, 0);
        }
    }
}
