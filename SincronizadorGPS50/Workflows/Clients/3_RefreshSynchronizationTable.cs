using SincronizadorGPS50.GestprojectAPI;
using System.Data;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class RefreshSynchronizationTable
    {
        internal DataTable Table { get; set; } = null;
        public DataTable Create()
        {
            new ConnectToGestprojectDatabase();

            DataHolder.GestprojectSQLConnection.Open();

            new GetGestprojectParticipants();

            new GetGestprojectClients();

            Table = new CreateTableControl().Table;

            for(int i = 0; i < DataHolder.GestprojectClientClassList.Count; i++)
            {
                GestprojectClient gestprojectClient = DataHolder.GestprojectClientClassList[i];

                new PopulateGestprojectClientSynchronizationData(gestprojectClient);

                new AddClientToSyncronizationUITable(
                    gestprojectClient,
                    Table,
                    gestprojectClient.synchronization_status
                );
            };

            DataHolder.GestprojectSQLConnection.Close();

            return Table;
        }
    }
}
