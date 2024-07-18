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

            new ConnectToGestprojectDatabase();
            GestprojectClientsActions.GetGestProjectClientsAndProviders();
            GestprojectClientsActions.GetGestprojectClients();
            ClientsUIHolder.ClientDataTable.DataSource = DataHolder.GestprojectClientsTable;

            ClientsUIHolder.CenterRow.ClientArea.Controls.Add(ClientsUIHolder.ClientDataTable);
        }
    }
}
