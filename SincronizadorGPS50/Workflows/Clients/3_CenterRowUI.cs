using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class CenterRowUI
    {
        internal List<UltraGridRow> UltraGridRowList { get; set; } = new List<UltraGridRow>();
        internal List<int> GestprojectClientIdList { get; set; } = new List<int>();
        internal CenterRowUI(CreateSynchronizationTableDelegate createTableDelegate)
        {
            ClientsUIHolder.ClientDataTable = new UltraGrid();

            ClientsUIHolder.ClientDataTable.DisplayLayout.Override.FilterUIProvider = new ColumnsFilter();
            ClientsUIHolder.ClientDataTable.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ClientsUIHolder.ClientDataTable.AfterRowFilterChanged += ClientDataTable_AfterRowFilterChanged;

            ClientsUIHolder.ClientDataTable.Dock = System.Windows.Forms.DockStyle.Fill;

            DataTable synchronizationTable = createTableDelegate();

            ClientsUIHolder.ClientDataTable.DataSource = synchronizationTable;

            ClientsUIHolder.CenterRow.ClientArea.Controls.Add(ClientsUIHolder.ClientDataTable);

            ClientsUIHolder.ClientDataTable.ClickCell += TableUISynchronizationActions.Set;
        }

        private void ClientDataTable_AfterRowFilterChanged(object sender, AfterRowFilterChangedEventArgs e) 
        {
            ClientsUIHolder.TopRowSynchronizeClientsButton.Enabled = false;
            ClientsUIHolder.BottomRowSynchronizeFilteredButton.Enabled = true;
            TableUISynchronizationActions.DeselectRows(ClientsUIHolder.ClientDataTable);
        }
    }
}