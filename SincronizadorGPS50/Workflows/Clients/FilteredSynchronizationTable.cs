using SincronizadorGPS50.GestprojectAPI;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class FilteredSynchronizationTable
    {
        internal DataTable Table { get; set; } = null;
        public DataTable Create(List<GestprojectClient> clientList)
        {
            DataHolder.GestprojectSQLConnection.Open();

            Table = new CreateTableControl().Table;

            DataHolder.GestprojectClientClassList.Clear();

            for(int i = 0; i < clientList.Count; i++)
            {
                GestprojectClient client = clientList[i];

                new UpdateClientSynchronizationStatus(
                    client,
                    SynchronizationStatusOptions.Sincronizado
                );

                new PopulateGestprojectClientSynchronizationData(client);

                new AddClientToSyncronizationUITable(
                    client,
                    Table,
                    client.synchronization_status
                );
            };

            DataHolder.GestprojectSQLConnection.Close();

            return Table;
        }
    }
}
