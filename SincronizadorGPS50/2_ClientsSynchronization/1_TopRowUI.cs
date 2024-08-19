using Infragistics.Win.Misc;
using Sage50ConnectionManager;
using SincronizadorGPS50.Sage50Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Clients
{
   internal class TopRowUI
   {
      internal TopRowUI()
      {
         try
         {
            ClientsUIHolder.TopRowTableLayoutPanel = new TableLayoutPanel();
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnCount = 4;
            ClientsUIHolder.TopRowTableLayoutPanel.RowCount = 1;
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 84f));
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 110));
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 110));
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 110));
            ClientsUIHolder.TopRowTableLayoutPanel.Dock = DockStyle.Fill;


            ClientsUIHolder.TopRowRefreshTableButton = new UltraButton();
            ClientsUIHolder.TopRowRefreshTableButton.Text = "Refrescar";
            ClientsUIHolder.TopRowRefreshTableButton.Dock = DockStyle.Fill;
            ClientsUIHolder.TopRowRefreshTableButton.Click += (object sender, System.EventArgs e) =>
            {
               ClientsUIHolder.ClientDataTable.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
               ClientsUIHolder.ClientDataTable.DataSource = ClientSynchronizationTable.Create();
            };

            ClientsUIHolder.TopRowSelectAllButton = new UltraButton();
            ClientsUIHolder.TopRowSelectAllButton.Text = "Seleccionar todo";
            ClientsUIHolder.TopRowSelectAllButton.Dock = DockStyle.Fill;
            ClientsUIHolder.TopRowSelectAllButton.Click += (object sender, System.EventArgs e) =>
            {
               foreach(Infragistics.Win.UltraWinGrid.UltraGridRow row in ClientsUIHolder.ClientDataTable.Rows)
               {
                  if(!row.IsFilteredOut)
                  {
                     row.Selected = true;
                  };
               };
            };

            ClientsUIHolder.TopRowSynchronizeButton = new UltraButton();
            ClientsUIHolder.TopRowSynchronizeButton.Text = "Sincronizar";
            ClientsUIHolder.TopRowSynchronizeButton.Dock = DockStyle.Fill;
            ClientsUIHolder.TopRowSynchronizeButton.Click += (object sender, System.EventArgs e) =>
            {
               //////////////////////////////////
               // If a row is selected, take all selected, If none, take non-filtered
               //////////////////////////////////

               int counter = 0;
               System.Collections.Generic.List<int> selectedIdList = new System.Collections.Generic.List<int>();

               foreach(Infragistics.Win.UltraWinGrid.UltraGridRow row in ClientsUIHolder.ClientDataTable.Rows)
               {
                  if(!row.IsFilteredOut)
                  {
                     if(row.Selected)
                     {
                        row.Selected = true;
                        selectedIdList.Add(Convert.ToInt32(row.Cells[0].Value));
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
                        selectedIdList.Add(Convert.ToInt32(row.Cells[0].Value));
                        counter++;
                     };
                  };
               };

               //////////////////////////////////
               // Synchronize selected id's
               //////////////////////////////////

               new SynchronizeCustomersWorkflow(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  selectedIdList
               );

               //////////////////////////////////
               // Update UI
               //////////////////////////////////

               ClientsUIHolder.ClientDataTable.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
               ClientsUIHolder.ClientDataTable.DataSource = ClientSynchronizationTable.Create();
               new ProviderSynchronizationManager();
            };

            ClientsUIHolder.TopRow.ClientArea.Controls.Add(ClientsUIHolder.TopRowTableLayoutPanel);
            ClientsUIHolder.TopRowTableLayoutPanel.Controls.Add(ClientsUIHolder.TopRowRefreshTableButton, 1, 0);
            ClientsUIHolder.TopRowTableLayoutPanel.Controls.Add(ClientsUIHolder.TopRowSelectAllButton, 2, 0);
            ClientsUIHolder.TopRowTableLayoutPanel.Controls.Add(ClientsUIHolder.TopRowSynchronizeButton, 3, 0);
         }
         catch(Exception exception)
         {
            throw new Exception($"En:\n\nSincronizadorGPS50\n.TopRowUI:\n\n{exception.Message}");
         };
      }
   }
}
