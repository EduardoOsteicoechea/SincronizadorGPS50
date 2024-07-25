using SincronizadorGPS50.GestprojectAPI;
using System.Data;
using SincronizadorGPS50.Sage50API;
using System;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class SynchronizeAllClients
    {
        public SynchronizeAllClients()
        {
            new ConnectToGestprojectDatabase();

            DataHolder.GestprojectSQLConnection.Open();

            GetSage50Clients sage50Clients = new GetSage50Clients();

            GetRegisteredClients registeredClients = new GetRegisteredClients();

            for(int i = 0; i < registeredClients.RegisteredClientsList.Count; i++)
            {

                GestprojectClient registeredClient = registeredClients.RegisteredClientsList[i];

                CreateSage50Customer newSage50Customer = new CreateSage50Customer(
                    registeredClient,
                    Convert.ToInt32(sage50Clients.NextClientCodeAvailable) + i
                );

                if(newSage50Customer.WasSuccessful)
                {
                    new UpdateRegisteredClientModelData(
                        registeredClient,
                        newSage50Customer.ClientCode,
                        newSage50Customer.GUID_ID
                    );
                    new RecordSage50ClientCodeInGestproject(
                        registeredClient.PAR_ID,
                        newSage50Customer.ClientCode
                    );
                    new UpdateClient(
                        registeredClient,
                        SynchronizationStatusOptions.Sincronizado
                    );
                }
                else
                {
                    MessageBox.Show(
                        "El programa se detuvo e el cliente registrado número: " + (i + 1) + "\n" +
                        "registeredClient.synchronization_table_id: " + registeredClient.synchronization_table_id + "\n\n" +
                        "registeredClient.PAR_PAIS_1: " + registeredClient.PAR_PAIS_1.Replace("-", " ").Replace("ñ", "n") + "\n" +
                        "registeredClient.PAR_LOCALIDAD_1: " + registeredClient.PAR_LOCALIDAD_1 + "\n" +
                        "registeredClient.PAR_CP_1: " + registeredClient.PAR_CP_1 + "\n" +
                        "registeredClient.PAR_CIF_NIF: " + registeredClient.PAR_CIF_NIF + "\n" +
                        "registeredClient.PAR_DIRECCION_1: " + registeredClient.PAR_DIRECCION_1.Replace(",", " ") + "\n" +
                        "registeredClient.PAR_PROVINCIA_1: " + registeredClient.PAR_PROVINCIA_1.Replace(",", " ") + "\n"
                    );
                };
            };

            DataHolder.GestprojectSQLConnection.Close();
        }
    }
}