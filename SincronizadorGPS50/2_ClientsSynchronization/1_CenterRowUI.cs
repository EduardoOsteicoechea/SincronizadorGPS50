using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System.Collections.Generic;
using System.Data;

namespace SincronizadorGPS50
{
   internal class CenterRowUI
   {
      internal List<UltraGridRow> UltraGridRowList { get; set; } = new List<UltraGridRow>();
      internal List<int> GestprojectClientIdList { get; set; } = new List<int>();
      internal CenterRowUI(SynchronizationTableDelegate createTableDelegate)
      {
         try
         {
            ClientsUIHolder.ClientDataTable = new UltraGrid();

            ClientsUIHolder.ClientDataTable.DisplayLayout.Override.FilterUIProvider = new ColumnsFilter();
            ClientsUIHolder.ClientDataTable.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ClientsUIHolder.ClientDataTable.AfterRowFilterChanged += ClientDataTable_AfterRowFilterChanged;
            ClientsUIHolder.ClientDataTable.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
            ClientsUIHolder.ClientDataTable.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.False;


            ClientsUIHolder.ClientDataTable.DisplayLayout.Override.AllowColSizing = AllowColSizing.Free;
            ClientsUIHolder.ClientDataTable.DisplayLayout.Override.ColumnSizingArea = ColumnSizingArea.EntireColumn;
            ClientsUIHolder.ClientDataTable.DisplayLayout.Override.DefaultColWidth = 80;
            //ClientsUIHolder.ClientDataTable.DisplayLayout.Bands[0].Override.DefaultColWidth = 100;
            //ClientsUIHolder.ClientDataTable.DisplayLayout.Bands[0].Columns[ClientsUIHolder.ClientDataTable.DisplayLayout.Bands[0].Columns.Count - 2].Width = 300;
            //ClientsUIHolder.ClientDataTable.DisplayLayout.Bands[0].Columns[0].MinWidth = 60;

            ClientsUIHolder.ClientDataTable.Dock = System.Windows.Forms.DockStyle.Fill;

            DataTable synchronizationTable = createTableDelegate();

            ClientsUIHolder.ClientDataTable.DataSource = synchronizationTable;

            ClientsUIHolder.CenterRow.ClientArea.Controls.Add(ClientsUIHolder.ClientDataTable);

            ClientsUIHolder.ClientDataTable.ClickCell += SynchronizationTableUIActions.Set;
         }
         catch(System.Exception exception)
         {
            throw new System.Exception($"En:\n\nSincronizadorGPS50\n.CenterRowUI:\n\n{exception.Message}");
         };
      }

      private void ClientDataTable_AfterRowFilterChanged(object sender, AfterRowFilterChangedEventArgs e)
      {
         SynchronizationTableUIActions.DeselectRows(ClientsUIHolder.ClientDataTable);
      }
   }
}