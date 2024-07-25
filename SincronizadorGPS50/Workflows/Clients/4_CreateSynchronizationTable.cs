using SincronizadorGPS50.GestprojectAPI;
using System.Data;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class CreateSynchronizationTable
    {
        internal DataTable Table { get; set; } = null;
        public CreateSynchronizationTable()
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

                if(Sage50SynchronizationTableWasJustCreated)
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
                                SynchronizationStatusOptions.Nunca_ha_sido_sincronizado
                            );
                        }
                        else
                        {
                            IsGestprojectClientSynchronized isGestprojectClientSynchronized =  new IsGestprojectClientSynchronized(gestprojectClient);

                            if(isGestprojectClientSynchronized.ItIs)
                            {
                                new UpdateClientSynchronizationStatus(
                                    gestprojectClient,
                                    SynchronizationStatusOptions.Sincronizado
                                );

                                new PopulateGestprojectClientSynchronizationData(gestprojectClient);

                                new AddClientToSyncronizationUITable(
                                    gestprojectClient,
                                    Table,
                                    SynchronizationStatusOptions.Sincronizado
                                );
                            }
                            else
                            {
                                new UpdateClientSynchronizationStatus(
                                    gestprojectClient,
                                    SynchronizationStatusOptions.Desincronizado
                                );

                                new PopulateGestprojectClientSynchronizationData(gestprojectClient);

                                new AddClientToSyncronizationUITable(
                                    gestprojectClient,
                                    Table,
                                    "",
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

            new MakeSynchronizationTableGoballyAvailable(Table);

            DataHolder.GestprojectSQLConnection.Close();
        }
    }
}
