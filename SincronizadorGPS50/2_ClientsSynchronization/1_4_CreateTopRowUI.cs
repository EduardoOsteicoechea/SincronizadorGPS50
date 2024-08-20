using Infragistics.Win.Misc;
using Sage50ConnectionManager;
using SincronizadorGPS50.Sage50Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Clients
{
   internal class CreateTopRowUI
   {
      internal CreateTopRowUI()
      {
         try
         {
            //////////////////////////////////
            // create row layout control
            //////////////////////////////////
            
            ClientsUIHolder.TopRowTableLayoutPanel = new TableLayoutPanel();
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnCount = 4;
            ClientsUIHolder.TopRowTableLayoutPanel.RowCount = 1;
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 84f));
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 110));
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 110));
            ClientsUIHolder.TopRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 110));
            ClientsUIHolder.TopRowTableLayoutPanel.Dock = DockStyle.Fill;

            //////////////////////////////////
            // create row buttons
            //////////////////////////////////
            
            ClientsUIHolder.TopRowRefreshTableButton = new UltraButton();
            ClientsUIHolder.TopRowRefreshTableButton.Text = "Refrescar";
            ClientsUIHolder.TopRowRefreshTableButton.Dock = DockStyle.Fill;

            ClientsUIHolder.TopRowSelectAllButton = new UltraButton();
            ClientsUIHolder.TopRowSelectAllButton.Text = "Seleccionar todo";
            ClientsUIHolder.TopRowSelectAllButton.Dock = DockStyle.Fill;

            ClientsUIHolder.TopRowSynchronizeButton = new UltraButton();
            ClientsUIHolder.TopRowSynchronizeButton.Text = "Sincronizar";
            ClientsUIHolder.TopRowSynchronizeButton.Dock = DockStyle.Fill;

            //////////////////////////////////
            // Manage buttons events
            //////////////////////////////////

            ClientsUIHolder.TopRowRefreshTableButton.Click += (object sender, System.EventArgs e) =>
            {
               ManageUserInteractionWithUI.RefreshTable();
            };

            ClientsUIHolder.TopRowSelectAllButton.Click += (object sender, System.EventArgs e) =>
            {
               ManageUserInteractionWithUI.SelectNonfiltered();
            };

            ClientsUIHolder.TopRowSynchronizeButton.Click += (object sender, System.EventArgs e) =>
            {
               List<int> selectedIdList = ManageUserInteractionWithUI.GetSelectedOrAllIfNoSelectFound();

               new RunSynchronizeCustomersWorkflow(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  selectedIdList
               );

               ManageUserInteractionWithUI.RefreshTable();

               new ProviderSynchronizationManager();
            };

            //////////////////////////////////
            // add buttons to ui
            //////////////////////////////////
            
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
