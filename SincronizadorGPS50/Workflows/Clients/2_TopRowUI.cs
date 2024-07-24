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
        internal string MainMessage => "Visualize el estado actual de sus clientes respecto a la información de Sage50. Renderizado el " + DateTime.UtcNow.ToShortDateString().ToString() + " en el horario " + DateTime.Now.TimeOfDay.ToString();
        //Infragistics.Win.UltraWinGrid.ColumnStyle
        //Infragistics.Win.UltraWinGrid.ColumnStyle
        //Infragistics.Win.UltraWinGrid.ColumnStyle
        //Infragistics.Win.UltraWinGrid.ColumnStyle
        //Infragistics.Win.UltraWinGrid.ColumnStyle
        //Infragistics.Win.UltraWinGrid.ColumnStyle
        //Infragistics.Win.UltraWinGrid.ColumnStyle
        //Infragistics.Win.UltraWinGrid.ColumnStyle
        //Infragistics.Win.UltraWinGrid.ColumnStyle // This is the key
        internal TopRowUI()
        {
            ClientsUIHolder.TopRowTableLayoutPanel = new TableLayoutPanel();
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnCount = 3;
            ClientsUIHolder.TopRowTableLayoutPanel.RowCount = 1;
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 84f));
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 110));
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 110));
            ClientsUIHolder.TopRowTableLayoutPanel.Dock = DockStyle.Fill;

            ClientsUIHolder.TopRowMainInstructionLabel = new UltraLabel();
            ClientsUIHolder.TopRowMainInstructionLabel.Text = MainMessage;
            ClientsUIHolder.TopRowMainInstructionLabel.Dock = DockStyle.Fill;
            ClientsUIHolder.TopRowMainInstructionLabel.Appearance.TextVAlign = VAlign.Middle;



            ClientsUIHolder.TopRowRefreshTableButton = new UltraButton();
            ClientsUIHolder.TopRowRefreshTableButton.Text = "Refrescar";
            ClientsUIHolder.TopRowRefreshTableButton.Dock = DockStyle.Fill;
            ClientsUIHolder.TopRowRefreshTableButton.Click += TopRowRefreshTableButton_Click;



            ClientsUIHolder.TopRowSynchronizeClientsButton = new UltraButton();
            ClientsUIHolder.TopRowSynchronizeClientsButton.Text = "Sincronizar";
            ClientsUIHolder.TopRowSynchronizeClientsButton.Dock = DockStyle.Fill;
            ClientsUIHolder.TopRowSynchronizeClientsButton.Click += TopRowSynchronizeClientsButton_Click;



            ClientsUIHolder.TopRow.ClientArea.Controls.Add(ClientsUIHolder.TopRowTableLayoutPanel);

            ClientsUIHolder.TopRowTableLayoutPanel.Controls.Add(ClientsUIHolder.TopRowMainInstructionLabel, 0, 0);
            ClientsUIHolder.TopRowTableLayoutPanel.Controls.Add(ClientsUIHolder.TopRowRefreshTableButton, 1, 0);
            ClientsUIHolder.TopRowTableLayoutPanel.Controls.Add(ClientsUIHolder.TopRowSynchronizeClientsButton, 2, 0);
        }

        private async void TopRowRefreshTableButton_Click(object sender, System.EventArgs e)
        {
            new RemoveClientsSynchronizationTable();

            await Task.Delay(1000);

            new CenterRowUI();

            ClientsUIHolder.TopRowMainInstructionLabel.Text = MainMessage;
        }

        private async void TopRowSynchronizeClientsButton_Click(object sender, System.EventArgs e)
        {
            new RemoveClientsSynchronizationTable();

            await Task.Delay(1000);

            new SynchronizeClients();

            new CenterRowUI();

            ClientsUIHolder.TopRowMainInstructionLabel.Text = MainMessage;
        }

        internal void ChangeMainMessageText(object control, string newText) 
        {
            ClientsUIHolder.TopRowMainInstructionLabel.Text = newText;  
        }
    }
}
