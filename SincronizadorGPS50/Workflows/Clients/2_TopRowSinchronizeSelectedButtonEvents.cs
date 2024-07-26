using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal static class TopRowSinchronizeSelectedButtonEvents
    {
        internal static void Click(object sender, System.EventArgs e)
        {
            DataHolder.GestprojectSQLConnection.Open();

            GetSelectedClientsInUITable selectedClientsInUITable = new GetSelectedClientsInUITable(DataHolder.ListOfSelectedClientIdInTable);

            new RemoveClientsSynchronizationTable();

            new SynchronizeClients(selectedClientsInUITable.Clients);

            //new CenterRowUI(() => new SelectiveSynchronizationTable().Create(selectedClientsInUITable.Clients));
            //new CenterRowUI(() => new SelectiveSynchronizationTable().Create(selectedClientsInUITable.Clients));
            new CenterRowUI(SynchronizationTable.Refresh);

            ClientsUIHolder.TopRowMainInstructionLabel.Text = "Visualize el estado actual de sus clientes respecto a la información de Sage50. Renderizado el " + DateTime.UtcNow.ToShortDateString().ToString() + " en el horario " + DateTime.Now.TimeOfDay.ToString();

            DataHolder.ListOfSelectedClientIdInTable.Clear();

            ClientsUIHolder.BottomRowSynchronizeFilteredButton.Enabled = false;
            ClientsUIHolder.TopRowSynchronizeClientsButton.Enabled = false;
        }
    }
}
