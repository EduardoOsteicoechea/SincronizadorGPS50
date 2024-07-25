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
    internal class BottomRowUI
    {
        internal string MainMessage => "Presione sobre un cliente para sincronizar de manera particular o presione el boton de \"Sincronizar todo\" para sobreescribir los valores actuales de Sage50 con los datos de Gestproject.";
        internal BottomRowUI()
        {
            ClientsUIHolder.BottomRowTableLayoutPanel = new TableLayoutPanel();
            ClientsUIHolder.BottomRowTableLayoutPanel.ColumnCount = 3;
            ClientsUIHolder.BottomRowTableLayoutPanel.RowCount = 1;
            ClientsUIHolder.BottomRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 84f));
            ClientsUIHolder.BottomRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 110));
            ClientsUIHolder.BottomRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 110));
            ClientsUIHolder.BottomRowTableLayoutPanel.Dock = DockStyle.Fill;

            ClientsUIHolder.BottomRowMainInstructionLabel = new UltraLabel();
            ClientsUIHolder.BottomRowMainInstructionLabel.Text = MainMessage;
            ClientsUIHolder.BottomRowMainInstructionLabel.Dock = DockStyle.Fill;
            ClientsUIHolder.BottomRowMainInstructionLabel.Appearance.TextVAlign = VAlign.Middle;



            ClientsUIHolder.BottomRowRefreshTableButton = new UltraButton();
            ClientsUIHolder.BottomRowRefreshTableButton.Text = "Sincronizar todo";
            ClientsUIHolder.BottomRowRefreshTableButton.Dock = DockStyle.Fill;
            ClientsUIHolder.BottomRowRefreshTableButton.Click += BottomRowRefreshTableButton_Click;



            ClientsUIHolder.BottomRowSynchronizeClientsButton = new UltraButton();
            ClientsUIHolder.BottomRowSynchronizeClientsButton.Text = "Salir";
            ClientsUIHolder.BottomRowSynchronizeClientsButton.Dock = DockStyle.Fill;
            ClientsUIHolder.BottomRowSynchronizeClientsButton.Click += BottomRowSynchronizeClientsButton_Click;



            ClientsUIHolder.BottomRow.ClientArea.Controls.Add(ClientsUIHolder.BottomRowTableLayoutPanel);

            ClientsUIHolder.BottomRowTableLayoutPanel.Controls.Add(ClientsUIHolder.BottomRowMainInstructionLabel, 0, 0);
            ClientsUIHolder.BottomRowTableLayoutPanel.Controls.Add(ClientsUIHolder.BottomRowRefreshTableButton, 1, 0);
            ClientsUIHolder.BottomRowTableLayoutPanel.Controls.Add(ClientsUIHolder.BottomRowSynchronizeClientsButton, 2, 0);
        }

        private async void BottomRowRefreshTableButton_Click(object sender, System.EventArgs e)
        {
            DataHolder.GestprojectSQLConnection.Open();
            new GetGestprojectParticipants();
            new GetGestprojectClients();
            DataHolder.GestprojectSQLConnection.Close();

            new RemoveClientsSynchronizationTable();
            await Task.Delay(0);
            new SynchronizeAllClients();
            new CenterRowUI();
            ClientsUIHolder.TopRowMainInstructionLabel.Text = MainMessage;
        }

        private async void BottomRowSynchronizeClientsButton_Click(object sender, System.EventArgs e)
        {
            UIHolder.MainWindow.Close();
        }

        internal void ChangeMainMessageText(object control, string newText)
        {
            ClientsUIHolder.BottomRowMainInstructionLabel.Text = newText;
        }
    }
}
