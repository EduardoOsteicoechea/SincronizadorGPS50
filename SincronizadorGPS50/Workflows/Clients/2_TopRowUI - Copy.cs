﻿//using Infragistics.Win;
//using Infragistics.Win.Misc;
//using Infragistics.Win.UltraWinEditors;
//using Infragistics.Win.UltraWinGrid;
//using sage.ew.db;
//using SincronizadorGPS50.GestprojectAPI;
//using System;
//using System.Data;
//using System.Drawing;
//using System.IO;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace SincronizadorGPS50.Workflows.Clients
//{
//    internal class TopRowUI
//    {
//        internal string MainMessage => "Visualize el estado actual de sus clientes respecto a la información de Sage50. Renderizado el " + DateTime.UtcNow.ToShortDateString().ToString() + " en el horario " + DateTime.Now.TimeOfDay.ToString();
//        internal TopRowUI()
//        {
//            ClientsUIHolder.TopRowTableLayoutPanel = new TableLayoutPanel();
//            ClientsUIHolder.TopRowTableLayoutPanel.ColumnCount = 3;
//            ClientsUIHolder.TopRowTableLayoutPanel.RowCount = 1;
//            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 84f));
//            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 150));
//            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 150));
//            ClientsUIHolder.TopRowTableLayoutPanel.Dock = DockStyle.Fill;

//            ClientsUIHolder.TopRowMainInstructionLabel = new UltraLabel();
//            ClientsUIHolder.TopRowMainInstructionLabel.Text = "Visualize el estado actual de sus clientes respecto a la información de Sage50. Renderizado el " + DateTime.UtcNow.ToShortDateString().ToString() + " en el horario " + DateTime.Now.TimeOfDay.ToString();
//            ;
//            ClientsUIHolder.TopRowMainInstructionLabel.Dock = DockStyle.Fill;
//            ClientsUIHolder.TopRowMainInstructionLabel.Appearance.TextVAlign = VAlign.Middle;


//            ClientsUIHolder.TopRowRefreshTableButton = new UltraButton();
//            ClientsUIHolder.TopRowRefreshTableButton.Text = "Refrescar";
//            ClientsUIHolder.TopRowRefreshTableButton.Dock = DockStyle.Fill;
//            ClientsUIHolder.TopRowRefreshTableButton.Click += TopRowRefreshTableButtonEvents.Click;


//            ClientsUIHolder.TopRowSynchronizeClientsButton = new UltraButton();
//            ClientsUIHolder.TopRowSynchronizeClientsButton.Text = "Sincronizar selección";
//            ClientsUIHolder.TopRowSynchronizeClientsButton.Dock = DockStyle.Fill;
//            ClientsUIHolder.TopRowSynchronizeClientsButton.Click += TopRowSinchronizeSelectedButtonEvents.Click;
//            ClientsUIHolder.TopRowSynchronizeClientsButton.Enabled = false;


//            ClientsUIHolder.TopRow.ClientArea.Controls.Add(ClientsUIHolder.TopRowTableLayoutPanel);


//            ClientsUIHolder.TopRowTableLayoutPanel.Controls.Add(ClientsUIHolder.TopRowMainInstructionLabel, 0, 0);
//            ClientsUIHolder.TopRowTableLayoutPanel.Controls.Add(ClientsUIHolder.TopRowRefreshTableButton, 1, 0);
//            ClientsUIHolder.TopRowTableLayoutPanel.Controls.Add(ClientsUIHolder.TopRowSynchronizeClientsButton, 2, 0);
//        }

//        //private async void TopRowRefreshTableButton_Click(object sender, System.EventArgs e)
//        //{
//        //    new RemoveClientsSynchronizationTable();
//        //    await Task.Delay(0);
//        //    //new CenterRowUI(()=> new FreshSynchronizationTable().Create());
//        //    new CenterRowUI(()=> new RefreshSynchronizationTable().Create());
//        //    ClientsUIHolder.TopRowMainInstructionLabel.Text = MainMessage;

//        //    ClientsUIHolder.BottomRowSynchronizeFilteredButton.Enabled = false;
//        //    ClientsUIHolder.TopRowSynchronizeClientsButton.Enabled = false;
//        //}

//        //private async void TopRowSynchronizeClientsButton_Click(object sender, System.EventArgs e)
//        //{
//        //    DataHolder.GestprojectSQLConnection.Open();
//        //    TableUISynchronizationActions.CollectCurrentlySelected(ClientsUIHolder.ClientDataTable);
//        //    GetSelectedClientsInUITable selectedClientsInUITable = new GetSelectedClientsInUITable(DataHolder.ListOfSelectedClientIdInTable);
//        //    DataHolder.GestprojectSQLConnection.Close();

//        //    new RemoveClientsSynchronizationTable();

//        //    new SynchronizeClients(selectedClientsInUITable.Clients);
//        //    new CenterRowUI(() => new SelectiveSynchronizationTable().Create(selectedClientsInUITable.Clients));
//        //    ClientsUIHolder.TopRowMainInstructionLabel.Text = MainMessage;

//        //    DataHolder.ListOfSelectedClientIdInTable.Clear();

//        //    ClientsUIHolder.BottomRowSynchronizeFilteredButton.Enabled = false;
//        //    ClientsUIHolder.TopRowSynchronizeClientsButton.Enabled = false;
//        //}

//        //internal void ChangeMainMessageText(object control, string newText) 
//        //{
//        //    ClientsUIHolder.TopRowMainInstructionLabel.Text = newText;  
//        //}
//    }
//}