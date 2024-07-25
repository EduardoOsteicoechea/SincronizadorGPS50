using SincronizadorGPS50.GestprojectAPI;
using System.Collections.Generic;
using System.Data;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class PartialSynchronizationTable
    {
        internal DataTable Table { get; set; } = null;
        public DataTable Create(List<GestprojectClient> clientList)
        {
            DataHolder.GestprojectSQLConnection.Open();

            Table = new CreateTableControl().Table;

            DataHolder.GestprojectClientClassList.Clear();
            new GetGestprojectClients();

            for(int i = 0; i < DataHolder.GestprojectClientClassList.Count; i++)
            {
                //GestprojectClient synchronizedClient = clientList[i];
                GestprojectClient client = DataHolder.GestprojectClientClassList[i];

                for (global::System.Int32 j = 0; j < clientList.Count; j++)
                {
                    GestprojectClient selectClient = clientList[j];
                    if(client.PAR_ID == selectClient.PAR_ID)
                    {
                        new PopulateGestprojectClientSynchronizationData(selectClient);
                        client = selectClient;
                        break;
                    };
                };

                //new PopulateGestprojectClientSynchronizationData(synchronizedClient);
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
