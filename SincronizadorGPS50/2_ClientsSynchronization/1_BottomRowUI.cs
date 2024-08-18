using Infragistics.Win.Misc;
using System;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal class BottomRowUI
   {
      internal BottomRowUI()
      {
         try
         {
            ClientsUIHolder.BottomRowTableLayoutPanel = new TableLayoutPanel();
            ClientsUIHolder.BottomRowTableLayoutPanel.ColumnCount = 4;
            ClientsUIHolder.BottomRowTableLayoutPanel.RowCount = 1;
            ClientsUIHolder.BottomRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 80f));
            ClientsUIHolder.BottomRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 110));
            ClientsUIHolder.BottomRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 110));
            ClientsUIHolder.BottomRowTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 110));
            ClientsUIHolder.BottomRowTableLayoutPanel.Dock = DockStyle.Fill;


            //ClientsUIHolder.BottomRowMainInstructionLabel = new UltraLabel();
            //ClientsUIHolder.BottomRowMainInstructionLabel.Text = "Presione sobre un cliente para sincronizar de manera particular o presione el boton de \"Sincronizar todo\" para sobreescribir los valores actuales de Sage50 con los datos de Gestproject.";
            //ClientsUIHolder.BottomRowMainInstructionLabel.Dock = DockStyle.Fill;
            //ClientsUIHolder.BottomRowMainInstructionLabel.Appearance.TextVAlign = VAlign.Middle;


            //ClientsUIHolder.BottomRowSynchronizeSelectedButton = new UltraButton();
            //ClientsUIHolder.BottomRowSynchronizeSelectedButton.Text = "Sincronizar selección";
            //ClientsUIHolder.BottomRowSynchronizeSelectedButton.Dock = DockStyle.Fill;
            //ClientsUIHolder.BottomRowSynchronizeSelectedButton.Click += SinchronizeSelectedButtonEvents.Click;
            //ClientsUIHolder.BottomRowSynchronizeSelectedButton.Enabled = false;


            //ClientsUIHolder.BottomRowSynchronizeFilteredButton = new UltraButton();
            //ClientsUIHolder.BottomRowSynchronizeFilteredButton.Text = "Sincronizar filtrados";
            //ClientsUIHolder.BottomRowSynchronizeFilteredButton.Dock = DockStyle.Fill;
            //ClientsUIHolder.BottomRowSynchronizeFilteredButton.Click += SynchronizeFilteredButtonEvents.Click;
            //ClientsUIHolder.BottomRowSynchronizeFilteredButton.Enabled = false;


            //ClientsUIHolder.BottomRowSynchronizeAllButton = new UltraButton();
            //ClientsUIHolder.BottomRowSynchronizeAllButton.Text = "Sincronizar todo";
            //ClientsUIHolder.BottomRowSynchronizeAllButton.Dock = DockStyle.Fill;
            //ClientsUIHolder.BottomRowSynchronizeAllButton.Click += SynchronizeAllButtonEvents.Click;


            ClientsUIHolder.BottomRowCloseButton = new UltraButton();
            ClientsUIHolder.BottomRowCloseButton.Text = "Salir";
            ClientsUIHolder.BottomRowCloseButton.Dock = DockStyle.Fill;
            ClientsUIHolder.BottomRowCloseButton.Click += (object sender, System.EventArgs e) =>
            {
               DialogResult result = MessageBox.Show(
               "¿Desea cerrar la aplicación?", "Confirmación",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
            );

               if(result == DialogResult.Yes)
                  MainWindowActions.CloseCompletellyAndAbruptly();
            };

            ClientsUIHolder.BottomRow.ClientArea.Controls.Add(ClientsUIHolder.BottomRowTableLayoutPanel);
            //ClientsUIHolder.BottomRowTableLayoutPanel.Controls.Add(ClientsUIHolder.BottomRowMainInstructionLabel, 0, 0);
            //ClientsUIHolder.BottomRowTableLayoutPanel.Controls.Add(ClientsUIHolder.BottomRowSynchronizeSelectedButton, 1, 0);
            //ClientsUIHolder.BottomRowTableLayoutPanel.Controls.Add(ClientsUIHolder.BottomRowSynchronizeFilteredButton, 2, 0);
            ClientsUIHolder.BottomRowTableLayoutPanel.Controls.Add(ClientsUIHolder.BottomRowCloseButton, 3, 0);
         }
         catch(Exception exception)
         {
            throw new Exception($"En:\n\nSincronizadorGPS50\n.BottomRowUI:\n\n{exception.Message}");
         };
      }
   }
}
