using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal static class BottomRowSynchronizeFilteredButtonEvents
    {
        internal static void Click(object sender, System.EventArgs e) 
        {
            TableUISynchronizationActions.CollectFilteredInTableUI(ClientsUIHolder.ClientDataTable);

            GetSelectedClientsInUITable selectedClientsInUITable = new GetSelectedClientsInUITable(DataHolder.ListOfSelectedClientIdInTable);

            new RemoveClientsSynchronizationTable();

            new SynchronizeClients(selectedClientsInUITable.Clients);

            new CenterRowUI(SynchronizationTable.Refresh);

            DataHolder.ListOfSelectedClientIdInTable.Clear();

            ClientsUIHolder.TopRowSynchronizeClientsButton.Enabled = false;
        }
    }
}
