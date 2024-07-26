using SincronizadorGPS50.GestprojectAPI;
using System.Data;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal static class RefreshSynchronizationTable
    {
        internal static DataTable Table { get; set; } = null;
        public static DataTable Create()
        {
            using(System.Data.SqlClient.SqlConnection connection = GestprojectDatabase.Connect()) 
            {
            
            };
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

            return Table;
        }
    }
}
