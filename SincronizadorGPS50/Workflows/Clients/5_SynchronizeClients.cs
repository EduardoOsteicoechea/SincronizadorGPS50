using SincronizadorGPS50.GestprojectAPI;
using System.Data;
using SincronizadorGPS50.Sage50API;
using System;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class SynchronizeClients
    {
        public SynchronizeClients()
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
            };

            DataHolder.GestprojectSQLConnection.Close();
        }
    }
}
