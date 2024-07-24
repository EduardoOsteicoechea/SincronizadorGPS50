//using SincronizadorGPS50.GestprojectAPI;
//using System.Data;

//namespace SincronizadorGPS50.Workflows.Clients
//{
//    internal class CreateSynchronizationTable
//    {
//        internal DataTable Table { get; set; } = null;
//        public CreateSynchronizationTable()
//        {
//            new ConnectToGestprojectDatabase();

//            DataHolder.GestprojectSQLConnection.Open();

//            new GetGestprojectParticipants();

//            new GetGestprojectClients();

//            Table = new CreateTableControl().Table;

//            bool Sage50SincronizationTableExists = new CheckIfTableExistsOnGestproject("INT_SAGE_SINC_CLIENTE").Exists;

//            bool GestprojectSynchronizationImageTableExists = new CheckIfTableExistsOnGestproject("INT_SAGE_SINC_CLIENTE_IMAGEN").Exists;

//            if(!Sage50SincronizationTableExists) 
//            {
//                new CreateGestprojectSage50SynchronizationTable();
//            };   
            
//            if(!GestprojectSynchronizationImageTableExists) 
//            {
//                new CreateGestprojectSynchronizationImageTable();
//            };

//            bool Sage50SynchronizationTableWasJustCreated = !Sage50SincronizationTableExists;
//            bool GestprojectSynchronizationImageTableWasJustCreated = !Sage50SincronizationTableExists;

//            for(int i = 0; i < DataHolder.GestprojectClientClassList.Count; i++)
//            {
//                GestprojectClient gestprojectClient = DataHolder.GestprojectClientClassList[i];

//                if(Sage50SynchronizationTableWasJustCreated)
//                {
//                    new RegisterClient(
//                        gestprojectClient,
//                        SynchronizationStatusOptions.Nunca_ha_sido_sincronizado
//                    );
//                    new AddClientToSyncronizationUITable(
//                        Table,
//                        gestprojectClient
//                    );
//                }
//                else
//                {
//                    bool GestprojectClientWasRegistered = new CheckIfGestprojectClientWasRegistered(gestprojectClient).ItIs;

//                    if(GestprojectClientWasRegistered)
//                    {
//                        bool GestprojectClientWasSynchronized = new CheckIfGestprojectClientWasSynchronized(gestprojectClient).ItIs;

//                        if(!GestprojectClientWasSynchronized)
//                        {
//                            new UpdateClientSynchronizationStatus(
//                                gestprojectClient,
//                                SynchronizationStatusOptions.Nunca_ha_sido_sincronizado
//                            );
//                            new AddClientToSyncronizationUITable(
//                                Table,
//                                gestprojectClient
//                            );
//                        }
//                        else
//                        {
//                            IsClientUpToDateWithLastGestProjectRecord isClientUpToDateWithLastGestProjectRecord =  new IsClientUpToDateWithLastGestProjectRecord(gestprojectClient);

//                            bool GestprojectClientIsUpToDateWithLastGestProjectRecord =  isClientUpToDateWithLastGestProjectRecord.ItIs;

//                            if(GestprojectClientIsUpToDateWithLastGestProjectRecord)
//                            {
//                                IsClientUpToDateWithSage50 isClientUpToDateWithSage50 =  new IsClientUpToDateWithSage50(gestprojectClient);
//                                bool GestprojectClientIsUpToDateWithSage50 =  isClientUpToDateWithSage50.ItIs;

//                                if(!GestprojectClientIsUpToDateWithSage50)
//                                {
//                                    new UpdateClientSynchronizationStatus(
//                                        gestprojectClient,
//                                        SynchronizationStatusOptions.Sincronizado_con_Gestproject_y_Desincronizado_con_Sage50
//                                    );
//                                    new AddClientToSyncronizationUITable(
//                                        Table,
//                                        gestprojectClient,
//                                        isClientUpToDateWithSage50.Comment
//                                    );
//                                }
//                                else
//                                {
//                                    new UpdateClientSynchronizationStatus(
//                                        gestprojectClient,
//                                        SynchronizationStatusOptions.Sincronizado_con_ambos_Gestproject_y_Sage50
//                                    );
//                                    new AddClientToSyncronizationUITable(
//                                        Table,
//                                        gestprojectClient
//                                    );
//                                };
//                            }
//                            else
//                            {
//                                new UpdateClientSynchronizationStatus(
//                                    gestprojectClient,
//                                    SynchronizationStatusOptions.Desincronizado_con_ambos_Gestproject_y_Sage50
//                                );
//                                new AddClientToSyncronizationUITable(
//                                    Table,
//                                    gestprojectClient,
//                                    isClientUpToDateWithLastGestProjectRecord.Comment
//                                );
//                            };
//                        };
//                    }
//                    else
//                    {
//                        new RegisterClient(
//                            gestprojectClient,
//                            SynchronizationStatusOptions.Nunca_ha_sido_sincronizado
//                        );
//                        new AddClientToSyncronizationUITable(
//                            Table,
//                            gestprojectClient
//                        );
//                    };
//                };
//            };

//            new MakeSynchronizationTableGoballyAvailable(Table);

//            DataHolder.GestprojectSQLConnection.Close();
//        }
//    }
//}
