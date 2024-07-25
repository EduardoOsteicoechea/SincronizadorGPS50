using SincronizadorGPS50.GestprojectAPI;
using System.Data;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class FreshSynchronizationTable
    {
        internal DataTable Table { get; set; } = null;
        public DataTable Create()
        {
            new ConnectToGestprojectDatabase();

            DataHolder.GestprojectSQLConnection.Open();

            new GetGestprojectParticipants();

            new GetGestprojectClients();

            Table = new CreateTableControl().Table;

            bool Sage50SincronizationTableExists = new CheckIfTableExistsOnGestproject("INT_SAGE_SINC_CLIENTE").Exists;

            if(!Sage50SincronizationTableExists) 
            {
                new CreateGestprojectSage50SynchronizationTable();
            };

            bool Sage50SynchronizationTableWasJustCreated = !Sage50SincronizationTableExists;

            for(int i = 0; i < DataHolder.GestprojectClientClassList.Count; i++)
            {
                GestprojectClient gestprojectClient = DataHolder.GestprojectClientClassList[i];

                new PopulateGestprojectClientSynchronizationData(gestprojectClient);

                if(Sage50SynchronizationTableWasJustCreated)
                {
                    new RegisterClient(
                        gestprojectClient,
                        gestprojectClient.synchronization_status
                    );

                    new PopulateGestprojectClientSynchronizationData(gestprojectClient);

                    new AddClientToSyncronizationUITable(
                        gestprojectClient,
                        Table,
                        gestprojectClient.synchronization_status
                    );
                }
                else
                {
                    bool GestprojectClientWasRegistered = new IsGestprojectClientRegistered(gestprojectClient).ItIs;

                    if(GestprojectClientWasRegistered)
                    {
                        new PopulateGestprojectClientSynchronizationData(gestprojectClient);

                        bool GestprojectClientWasSynchronized = new CheckIfGestprojectClientWasSynchronized(gestprojectClient).ItIs;

                        if(!GestprojectClientWasSynchronized)
                        {
                            new UpdateClientSynchronizationStatus(
                                gestprojectClient,
                                SynchronizationStatusOptions.Nunca_ha_sido_sincronizado
                            );

                            new PopulateGestprojectClientSynchronizationData(gestprojectClient);

                            new AddClientToSyncronizationUITable(
                                gestprojectClient,
                                Table,
                                gestprojectClient.synchronization_status
                            );
                        }
                        else
                        {
                            IsGestprojectClientSynchronized isGestprojectClientSynchronized =  new IsGestprojectClientSynchronized(gestprojectClient);

                            if(isGestprojectClientSynchronized.ItIs)
                            {
                                new UpdateClientSynchronizationStatus(
                                    gestprojectClient,
                                    gestprojectClient.synchronization_status
                                );

                                new PopulateGestprojectClientSynchronizationData(gestprojectClient);

                                new AddClientToSyncronizationUITable(
                                    gestprojectClient,
                                    Table,
                                    gestprojectClient.synchronization_status
                                );
                            }
                            else
                            {
                                new UpdateClientSynchronizationStatus(
                                    gestprojectClient,
                                    gestprojectClient.synchronization_status
                                );

                                new PopulateGestprojectClientSynchronizationData(gestprojectClient);

                                new AddClientToSyncronizationUITable(
                                    gestprojectClient,
                                    Table,
                                    gestprojectClient.synchronization_status,
                                    isGestprojectClientSynchronized.Comment
                                );
                            };
                        };
                    }
                    else
                    {
                        new RegisterClient(
                            gestprojectClient,
                            SynchronizationStatusOptions.Nunca_ha_sido_sincronizado
                        );

                        new PopulateGestprojectClientSynchronizationData(gestprojectClient);

                        new AddClientToSyncronizationUITable(
                            gestprojectClient,
                            Table,
                            SynchronizationStatusOptions.Nunca_ha_sido_sincronizado
                        );
                    };
                };
            };

            DataHolder.GestprojectSQLConnection.Close();

            return Table;
        }
    }
}
