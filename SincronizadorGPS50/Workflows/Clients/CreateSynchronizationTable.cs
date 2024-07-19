using sage.ew.db;
using SincronizadorGPS50.GestprojectAPI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SincronizadorGPS50.Sage50API;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class CreateSynchronizationTable
    {
        public CreateSynchronizationTable()
        {
            new ConnectToGestprojectDatabase();

            DataHolder.GestprojectSQLConnection.Open();

            new GetGestprojectParticipants();

            new GetGestprojectClients();

            new GetSage50Clients();       

            DataTable sincronizationTable = new CreateClientsSincronizationTable().Table;

            bool Sage50SincronizationTableExists = new CheckIfSage50SynchronizationTableExists().Exists;

            if(!Sage50SincronizationTableExists) 
            {
                new CreateSage50SincronizationTable();
            };

            bool Sage50SincronizationTableWasJustCreated = !Sage50SincronizationTableExists;

            int ExistingSage50ClientCounter = new GetExistingSage50ClientsLastCodeValue().NextValue;

            for(int i = 0; i < DataHolder.GestprojectClientClassList.Count; i++)
            {
                GestprojectClient gestprojectClient = DataHolder.GestprojectClientClassList[i];

                if(Sage50SincronizationTableWasJustCreated)
                {
                    CreateSage50Customer newSage50Customer = new CreateSage50Customer(
                        gestprojectClient,
                        false,
                        ref ExistingSage50ClientCounter
                    );

                    new RegisterNewSage50Client(
                        gestprojectClient.PAR_ID,
                        newSage50Customer.ClientCode,
                        newSage50Customer.GUID_ID,
                        DataHolder.Sage50LocalTerminalPath
                    );

                    new AddAccountableSubacountValueToClient(
                        gestprojectClient.PAR_ID,
                        newSage50Customer.ClientCode
                    );

                    new AddRowToClientsSincronizationTable(
                        sincronizationTable,
                        true,
                        gestprojectClient,
                        newSage50Customer.ClientCode,
                        newSage50Customer.GUID_ID
                    );
                }
                else
                {
                    CheckIfGestprojectClientIsSynchronized checkIfGestprojectClientIsSynchronized =
                        new CheckIfGestprojectClientIsSynchronized(
                            gestprojectClient.PAR_ID,
                            DataHolder.Sage50ClientCodeList,
                            DataHolder.Sage50ClientGUID_IDList,
                            DataHolder.Sage50LocalTerminalPath
                        );

                    if(checkIfGestprojectClientIsSynchronized.IsSynchronized)
                    {
                        new AddRowToClientsSincronizationTable(
                            sincronizationTable,
                            true,
                            gestprojectClient,
                            checkIfGestprojectClientIsSynchronized.SynchronizedClient.Sage50Code,
                            checkIfGestprojectClientIsSynchronized.SynchronizedClient.Sage50GUID_ID
                        );

                        new AddAccountableSubacountValueToClient(
                            gestprojectClient.PAR_ID,
                            checkIfGestprojectClientIsSynchronized.SynchronizedClient.Sage50Code
                        );
                    }
                    else
                    {
                        CreateSage50Customer newSage50Customer = new CreateSage50Customer(
                            gestprojectClient,
                            false,
                            ref ExistingSage50ClientCounter
                        );

                        new RegisterNewSage50Client(
                            gestprojectClient.PAR_ID,
                            newSage50Customer.ClientCode,
                            newSage50Customer.GUID_ID,
                            DataHolder.Sage50LocalTerminalPath
                        );

                        new AddAccountableSubacountValueToClient(
                            gestprojectClient.PAR_ID,
                            newSage50Customer.ClientCode
                        );

                        new AddRowToClientsSincronizationTable(
                            sincronizationTable,
                            true,
                            gestprojectClient,
                            newSage50Customer.ClientCode,
                            newSage50Customer.GUID_ID
                        );
                    };
                };
            };

            new MakeSynchronizationTableGoballyAvailable(sincronizationTable);

            DataHolder.GestprojectSQLConnection.Close();
        }
    }
}
