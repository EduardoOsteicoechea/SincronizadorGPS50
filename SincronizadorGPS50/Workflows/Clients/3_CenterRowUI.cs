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
        internal List<UltraGridRow> UltraGridRowList {  get; set; } = new List<UltraGridRow>();
        internal List<int> GestprojectClientIdList {  get; set; } = new List<int>();
        internal CenterRowUI() 
        {
            ClientsUIHolder.ClientDataTable = new UltraGrid();

            ClientsUIHolder.ClientDataTable.Dock = System.Windows.Forms.DockStyle.Fill;

            DataTable synchronizationTable = new CreateSynchronizationTable().Table;

            ClientsUIHolder.ClientDataTable.DataSource = synchronizationTable;

            ClientsUIHolder.CenterRow.ClientArea.Controls.Add(ClientsUIHolder.ClientDataTable);

            ClientsUIHolder.ClientDataTable.ClickCell += ClientDataTable_ClickCell;
        }

        private void ClientDataTable_ClickCell(object sender, ClickCellEventArgs e)
        {
            UltraGrid ultraGrid = sender as UltraGrid;
            UltraGridRow ultraGridRow = ultraGrid.ActiveRow;
            UltraGridRowList.Add(ultraGridRow);
            
            if((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                if(UltraGridRowList.Count > 1)
                {
                    UltraGridRow previousIndex = UltraGridRowList[UltraGridRowList.Count - 2];
                    int selectedIndex1 = previousIndex.Index;
                    int selectedIndex2 = ultraGridRow.Index;
                    int MaxIndex = Math.Max(selectedIndex1, selectedIndex2);
                    int MinIndex = Math.Min(selectedIndex1, selectedIndex2);
                    int indexesDifference = MaxIndex - MinIndex;

                    for(global::System.Int32 i = MinIndex; i < MaxIndex; i++)
                    {
                        ultraGrid.Rows[i].Selected = true;
                        UltraGridRowList.Add(ultraGrid.Rows[i]);
                    };
                };
            };

            foreach (var item in UltraGridRowList)
            {
                item.Selected = true;
                GestprojectClientIdList.Add((int)item.Cells[2].Value);
                DataHolder.ListOfSelectedClientIdInTable.Add((int)item.Cells[2].Value);
            };

            ClientsUIHolder.TopRowSynchronizeClientsButton.Enabled = true;
        }
    }
}