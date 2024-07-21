using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using sage.ew.db;
using SincronizadorGPS50.GestprojectAPI;
using System.Data;
using System.Drawing;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class CenterRowUI
    {
        internal CenterRowUI() 
        {
            ClientsUIHolder.ClientDataTable = new UltraGrid();
            ClientsUIHolder.ClientDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            ClientsUIHolder.ClientDataTable.ShowColumnChooser();
            ClientsUIHolder.ClientDataTable.AllowDrop = true;


            new CreateSynchronizationTable();
            ClientsUIHolder.ClientDataTable.DataSource = DataHolder.ClientsSynchronizationTable;

            ClientsUIHolder.CenterRow.ClientArea.Controls.Add(ClientsUIHolder.ClientDataTable);
        }
    }
}
