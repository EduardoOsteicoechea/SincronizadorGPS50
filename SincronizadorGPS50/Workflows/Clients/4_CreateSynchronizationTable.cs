using SincronizadorGPS50.GestprojectAPI;
using System.Data;
using System.Windows.Forms;

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

            bool GestprojectSynchronizationImageTableExists = new CheckIfTableExistsOnGestproject("INT_SAGE_SINC_CLIENTE_IMAGEN").Exists;

            if(!Sage50SincronizationTableExists) 
            {
                new CreateGestprojectSage50SynchronizationTable();
            };   
            
            if(!GestprojectSynchronizationImageTableExists) 
            {
                new CreateGestprojectSynchronizationImageTable();
            };

            bool Sage50SynchronizationTableWasJustCreated = !Sage50SincronizationTableExists;
            bool GestprojectSynchronizationImageTableWasJustCreated = !Sage50SincronizationTableExists;


            for(int i = 0; i < DataHolder.GestprojectClientClassList.Count; i++)
            {
                GestprojectClient gestprojectClient = DataHolder.GestprojectClientClassList[i];

                if(Sage50SynchronizationTableWasJustCreated)
                {
                    MessageBox.Show("Se acaba de crear la tabla de sincronización. Registraremos a todos los clientes de Gestproject y les asignaremos un estado de Nunca sincronizado.");

                    new RegisterClientForTheFirstTime(
                        gestprojectClient
                    );

                    new AddClientToTable(
                        Table,
                        gestprojectClient
                    );
                }
                else
                {
                    bool GestprojectClientWasRegistered = new CheckIfGestprojectClientWasRegistered(gestprojectClient).ItIs;

                    if(GestprojectClientWasRegistered)
                    {
                        bool GestprojectClientWasSynchronized = new CheckIfGestprojectClientWasSynchronized(gestprojectClient).ItIs;

                        MessageBox.Show("La tabla de sincronización ya había sido creada y el cliente estaba registrado. Procederemos a verificar si fue sincronizado alguna vez.");

                        if(!GestprojectClientWasSynchronized)
                        {
                            MessageBox.Show("La tabla de sincronización ya había sido creada, el cliente estaba registrado y tambien se había sincronizado por lo menos una vez. Procederemos a verificar si sus datos coinciden con el último registro de sincronización.");

                            IsClientUpToDateWithLastGestProjectRecord isClientUpToDateWithLastGestProjectRecord =  new IsClientUpToDateWithLastGestProjectRecord(gestprojectClient);
                            bool GestprojectClientIsUpToDateWithLastGestProjectRecord =  isClientUpToDateWithLastGestProjectRecord.ItIs;

                            if(GestprojectClientIsUpToDateWithLastGestProjectRecord)
                            {
                                IsClientUpToDateWithSage50 isClientUpToDateWithSage50 =  new IsClientUpToDateWithSage50(gestprojectClient);
                                bool GestprojectClientIsUpToDateWithSage50 =  isClientUpToDateWithSage50.ItIs;

                                if(!GestprojectClientIsUpToDateWithSage50)
                                {
                                    MessageBox.Show("La tabla de sincronización ya había sido creada, el cliente estaba registrado, se había sincronizado por lo menos una vez, sus datos coinciden con el último registro de sincronización en Gestproject pero está desactualizado con Respecto al número de cuenta contable de Sage50. Verifique estos datos para determinar si modificará los registros manualmente o procederá a sincronizar.");

                                    new RegisterClientAsDesynchronized(
                                        gestprojectClient
                                    );

                                    new AddClientToTable(
                                        Table,
                                        gestprojectClient,
                                        isClientUpToDateWithSage50.Comment
                                    );
                                }
                                else
                                {
                                    MessageBox.Show("Sincronización total. El cliente está registrado, sincronizado, actualizado con Gestproject y también actualizado con los registros de Sage50.");

                                    new RegisterClientAsSynchronized(
                                        gestprojectClient
                                    );

                                    new AddClientToTable(
                                        Table,
                                        gestprojectClient
                                    );
                                };
                            }
                            else
                            {
                                MessageBox.Show("La tabla de sincronización ya había sido creada, el cliente estaba registrado y se había sincronizado por lo menos una vez pero sus datos no coinciden con el último registro de sincronización. Esto implica que sus datos desactualizados con respecto a los registros de Gestproject. Por favor verifique la columna de comentarios de la tabla de sincronización para decidir si modificará los datos del cliente en Gestproject o procederá a sincronizarlos.");

                                new RegisterClientAsDesynchronized(
                                    gestprojectClient
                                );

                                new AddClientToTable(
                                    Table,
                                    gestprojectClient,
                                    isClientUpToDateWithLastGestProjectRecord.Comment
                                );
                            }
                        }
                        else
                        {
                            MessageBox.Show("La tabla de sincronización ya había sido creada y el cliente estaba registrado pero nunca fue sincronizado. Estableceremos el estado del cliente como \"No sincronizado\".");

                            new RegisterClientAsDesynchronized(
                                gestprojectClient
                            );

                            new AddClientToTable(
                                Table,
                                gestprojectClient
                            );
                        }
                    }
                    else
                    {
                        MessageBox.Show("La tabla de sincronización ya había sido creada pero el cliente no se había registrado. Se procederá a registrar al cliente por lo que su estado será registrado pero no sincronizado.");

                        new RegisterClientForTheFirstTime(
                            gestprojectClient
                        );

                        new AddClientToTable(
                            Table,
                            gestprojectClient
                        );
                    };
                };
            };

            new MakeSynchronizationTableGoballyAvailable(Table);

            DataHolder.GestprojectSQLConnection.Close();
        }
    }
}
