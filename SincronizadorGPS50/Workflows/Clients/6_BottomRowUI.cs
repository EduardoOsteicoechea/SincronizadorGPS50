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
            ClientsUIHolder.BottomRowTableLayoutPanel.ColumnCount = 4;
            ClientsUIHolder.BottomRowTableLayoutPanel.RowCount = 1;
            ClientsUIHolder.BottomRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 84f));
            ClientsUIHolder.BottomRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 150));
            ClientsUIHolder.BottomRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 150));
            ClientsUIHolder.BottomRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 150));
            ClientsUIHolder.BottomRowTableLayoutPanel.Dock = DockStyle.Fill;

            ClientsUIHolder.BottomRowMainInstructionLabel = new UltraLabel();
            ClientsUIHolder.BottomRowMainInstructionLabel.Text = MainMessage;
            ClientsUIHolder.BottomRowMainInstructionLabel.Dock = DockStyle.Fill;
            ClientsUIHolder.BottomRowMainInstructionLabel.Appearance.TextVAlign = VAlign.Middle;


            ClientsUIHolder.BottomRowSynchronizeFilteredButton = new UltraButton();
            ClientsUIHolder.BottomRowSynchronizeFilteredButton.Text = "Sincronizar filtrados";
            ClientsUIHolder.BottomRowSynchronizeFilteredButton.Dock = DockStyle.Fill;
            ClientsUIHolder.BottomRowSynchronizeFilteredButton.Click += BottomRowSynchronizeFilteredButton_Click;
            ClientsUIHolder.BottomRowSynchronizeFilteredButton.Enabled = false;
            ;

            ClientsUIHolder.BottomRowSynchronizeAllButton = new UltraButton();
            ClientsUIHolder.BottomRowSynchronizeAllButton.Text = "Sincronizar todo";
            ClientsUIHolder.BottomRowSynchronizeAllButton.Dock = DockStyle.Fill;
            ClientsUIHolder.BottomRowSynchronizeAllButton.Click += BottomRowSynchronizeAllButton_Click;

            ClientsUIHolder.BottomRowCloseButton = new UltraButton();
            ClientsUIHolder.BottomRowCloseButton.Text = "Salir";
            ClientsUIHolder.BottomRowCloseButton.Dock = DockStyle.Fill;
            ClientsUIHolder.BottomRowCloseButton.Click += BottomRowCloseButton_Click;



            ClientsUIHolder.BottomRow.ClientArea.Controls.Add(ClientsUIHolder.BottomRowTableLayoutPanel);
            ClientsUIHolder.BottomRowTableLayoutPanel.Controls.Add(ClientsUIHolder.BottomRowMainInstructionLabel, 0, 0);
            ClientsUIHolder.BottomRowTableLayoutPanel.Controls.Add(ClientsUIHolder.BottomRowSynchronizeFilteredButton, 1, 0);
            ClientsUIHolder.BottomRowTableLayoutPanel.Controls.Add(ClientsUIHolder.BottomRowSynchronizeAllButton, 2, 0);
            ClientsUIHolder.BottomRowTableLayoutPanel.Controls.Add(ClientsUIHolder.BottomRowCloseButton, 3, 0);
        }

        internal void ChangeMainMessageText(object control, string newText)
        {
            ClientsUIHolder.BottomRowMainInstructionLabel.Text = newText;
        }

        private void BottomRowSynchronizeFilteredButton_Click(object sender, EventArgs e)
        {
            TableUISynchronizationActions.CollectFilteredInTableUI(ClientsUIHolder.ClientDataTable);

            DataHolder.GestprojectSQLConnection.Open();
            GetSelectedClientsInUITable selectedClientsInUITable = new GetSelectedClientsInUITable(DataHolder.ListOfSelectedClientIdInTable);
            DataHolder.GestprojectSQLConnection.Close();

            new RemoveClientsSynchronizationTable();

            new SynchronizeClients(selectedClientsInUITable.Clients);

            new CenterRowUI(() => new FilteredSynchronizationTable().Create(selectedClientsInUITable.Clients));

            ClientsUIHolder.TopRowMainInstructionLabel.Text = MainMessage;

            DataHolder.ListOfSelectedClientIdInTable.Clear();

            ClientsUIHolder.TopRowSynchronizeClientsButton.Enabled = false;
        }

        private void BottomRowSynchronizeAllButton_Click(object sender, System.EventArgs e)
        {
            DataHolder.GestprojectSQLConnection.Open();
            new GetGestprojectParticipants();
            new GetGestprojectClients();
            DataHolder.GestprojectSQLConnection.Close();

            new RemoveClientsSynchronizationTable();
            new SynchronizeAllClients();
            new CenterRowUI(() => new RefreshSynchronizationTable().Create());
            ClientsUIHolder.TopRowMainInstructionLabel.Text = MainMessage;

            ClientsUIHolder.TopRowSynchronizeClientsButton.Enabled = false;
        }

        private void BottomRowCloseButton_Click(object sender, System.EventArgs e)
        {
            UIHolder.MainWindow.Close();
        }
    }
}
