using Infragistics.Win.UltraWinGrid;
using SincronizadorGPS50.Sage50Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal static class ManageUserInteractionWithUI
   {
      internal static List<UltraGridRow> UltraGridRowList { get; set; } = new List<UltraGridRow>();
      internal static List<int> GestprojectClientIdList { get; set; } = new List<int>();
      internal static void ConfigureTable(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
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

               for(global::System.Int32 i = MinIndex; i < MaxIndex; i++)
               {
                  ultraGrid.Rows[i].Selected = true;
                  UltraGridRowList.Add(ultraGrid.Rows[i]);
               };
            };
         }
         else if((Control.ModifierKeys & Keys.Control) == Keys.Control)
         {
            UltraGridRowList.Add(ultraGridRow);
            ultraGridRow.Selected = true;
         }
         else
         {
            UltraGridRowList.Clear();
            UltraGridRowList.Add(ultraGridRow);
            foreach(var item in ultraGrid.Rows)
            {
               item.Selected = false;
            };
            ultraGridRow.Selected = true;
         };

         foreach(var item in UltraGridRowList)
         {
            item.Selected = true;
         };
      }

      internal static void DeselectRows(UltraGrid ultraGrid)
      {
         foreach(var item in UltraGridRowList)
         {
            item.Selected = false;
         };
      }

      internal static void SelectNonfiltered()
      {
         foreach(Infragistics.Win.UltraWinGrid.UltraGridRow row in ClientsUIHolder.ClientDataTable.Rows)
         {
            if(!row.IsFilteredOut)
            {
               row.Selected = true;
            };
         };
      }

      internal static void RefreshTable
      (
         System.Data.SqlClient.SqlConnection connection,
         CompanyGroup sage50CompanyGroupData
      )
      {
         DeselectRows(ClientsUIHolder.ClientDataTable);

         ClientsUIHolder.ClientDataTable.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();

         ClientsUIHolder.ClientDataTable.DataSource = CustomerSynchronizationDataTable.Create(
            connection, 
            sage50CompanyGroupData, 
            new GestprojectDataManager.CustomerSyncronizationTableSchema()
         );

         DeselectRows(ClientsUIHolder.ClientDataTable);
         ClientsUIHolder.ClientDataTable.ClickCell += ConfigureTable;
      }

      internal static List<int> GetSelectedIfAnyOrAll()
      {
         int counter = 0;
         System.Collections.Generic.List<int> selectedIdList = new System.Collections.Generic.List<int>();

         foreach(Infragistics.Win.UltraWinGrid.UltraGridRow row in ClientsUIHolder.ClientDataTable.Rows)
         {
            if(!row.IsFilteredOut)
            {
               if(row.Selected)
               {
                  row.Selected = true;
                  selectedIdList.Add(Convert.ToInt32(row.Cells[2].Value));
                  counter++;
               };
            };
         };

         if(counter == 0)
         {
            foreach(Infragistics.Win.UltraWinGrid.UltraGridRow row in ClientsUIHolder.ClientDataTable.Rows)
            {
               if(!row.IsFilteredOut)
               {
                  selectedIdList.Add(Convert.ToInt32(row.Cells[2].Value));
                  counter++;
               };
            };
         };

         return selectedIdList;
      }
   }
}
