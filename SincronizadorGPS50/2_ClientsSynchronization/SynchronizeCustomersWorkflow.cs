using SincronizadorGPS50.GestprojectDataManager;
using SincronizadorGPS50.Sage50Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal class SynchronizeCustomersWorkflow
   {
      public SynchronizeCustomersWorkflow
      (
         System.Data.SqlClient.SqlConnection connection,
         List<int> selectedIdList
      )
      {
         ////////////////////////////////////
         /// get gestproject and sage50 clients
         ////////////////////////////////////

         List<GestprojectDataManager.GestprojectClient> gestProjectClientList =
         new SincronizadorGPS50
         .GestprojectDataManager
         .GetClientsFromSynchronizationTable(
            connection,
            selectedIdList
         ).GestprojectClientList;

         List<Sage50Customer> sage50CustomerList = new GetSage50Customer().CustomerList;

         ////////////////////////////////////
         /// get clients distributon in sage50 (any exist, some exist, all exist) to determine application flow
         ////////////////////////////////////

         List<GestprojectDataManager.GestprojectClient> existingClientsList = new List<GestprojectDataManager.GestprojectClient>();
         List<GestprojectDataManager.GestprojectClient> nonExistingClientsList = new List<GestprojectDataManager.GestprojectClient>();
         List<GestprojectDataManager.GestprojectClient> unsynchronizedClientList = new List<GestprojectDataManager.GestprojectClient>();

         for(int i = 0; i < gestProjectClientList.Count; i++)
         {
            GestprojectDataManager.GestprojectClient gestprojectClient = gestProjectClientList[i];
            bool found = false;

            for(global::System.Int32 j = 0; j < sage50CustomerList.Count; j++)
            {
               Sage50Customer sage50Customer = sage50CustomerList[j];
               if(gestprojectClient.sage50_guid_id == sage50Customer.GUID_ID)
               {
                  existingClientsList.Add(gestprojectClient);
                  found = true;
                  break;
               };
            };

            if(!found)
            {
               nonExistingClientsList.Add(gestprojectClient);
            };

            if(gestprojectClient.synchronization_status != "Sincronizado")
            {
               unsynchronizedClientList.Add(gestprojectClient);
            };
         };

         bool someGestprojectClientsExistsInSage50 = existingClientsList.Count > 0;
         bool allGestprojectClientsExistsInSage50 = existingClientsList.Count == gestProjectClientList.Count;
         bool noGestprojectClientsExistsInSage50 = existingClientsList.Count == 0;
         bool unsynchronizedClientsExists = unsynchronizedClientList.Count > 0;

         ////////////////////////////////////
         /// execute client synchornization flow according to client distribution
         ////////////////////////////////////

         if(noGestprojectClientsExistsInSage50)
         {
            for(global::System.Int32 i = 0; i < gestProjectClientList.Count; i++)
            {
               if(
                  Sage50ConnectionManager.CustomerManager.ClientExists(
                     gestProjectClientList[i].sage50_guid_id,
                     gestProjectClientList[i].PAR_PAIS_1,
                     gestProjectClientList[i].PAR_NOMBRE,
                     gestProjectClientList[i].PAR_CIF_NIF,
                     gestProjectClientList[i].PAR_CP_1,
                     gestProjectClientList[i].PAR_DIRECCION_1,
                     gestProjectClientList[i].PAR_PROVINCIA_1
                  )
               )
               {
                  DialogResult result = MessageBox.Show("Desea crear este cliente de igual manera?", "Confirmación para creación", MessageBoxButtons.OKCancel);
                  if(result == DialogResult.Cancel)
                  {
                     continue;
                  }
               };

               CreateSage50Customer newSage50Client = new SincronizadorGPS50.Sage50Connector.CreateSage50Customer(
                  gestProjectClientList[i].PAR_PAIS_1,
                  gestProjectClientList[i].PAR_NOMBRE ,
                  gestProjectClientList[i].PAR_CIF_NIF,
                  gestProjectClientList[i].PAR_CP_1,
                  gestProjectClientList[i].PAR_DIRECCION_1,
                  gestProjectClientList[i].PAR_PROVINCIA_1
               );

               new GestprojectDataManager.RegisterNewSage50ClientData(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  gestProjectClientList[i].PAR_ID,
                  newSage50Client.ClientCode,
                  newSage50Client.GUID_ID
               );
            }
         }

         if(allGestprojectClientsExistsInSage50)
         {
            if(unsynchronizedClientsExists)
            {
               DialogResult result = MessageBox.Show("Desea actualizar los datos de Sage50 de los clientes desincronizados?", "Confirmación para actualización", MessageBoxButtons.OKCancel);

               if(result == DialogResult.OK)
               {
                  for(global::System.Int32 i = 0; i < gestProjectClientList.Count; i++)
                  {
                     if(unsynchronizedClientList.Contains(gestProjectClientList[i]))
                     {
                        MessageBox.Show(gestProjectClientList[i].PAR_NOMBRE);
                        new SincronizadorGPS50.Sage50Connector.UpdateSage50Customer(
                           gestProjectClientList[i].sage50_guid_id,
                           gestProjectClientList[i].PAR_PAIS_1,
                           gestProjectClientList[i].PAR_NOMBRE,
                           gestProjectClientList[i].PAR_CIF_NIF,
                           gestProjectClientList[i].PAR_CP_1,
                           gestProjectClientList[i].PAR_DIRECCION_1,
                           gestProjectClientList[i].PAR_PROVINCIA_1
                        );

                        new GestprojectDataManager.UpdateClientSyncronizationStatus(
                           GestprojectDataHolder.GestprojectDatabaseConnection, 
                           gestProjectClientList[i].PAR_ID,
                           true
                        );

                        new GestprojectDataManager.RegisterNewSage50ClientData(
                           GestprojectDataHolder.GestprojectDatabaseConnection,
                           gestProjectClientList[i].PAR_ID,
                           gestProjectClientList[i].sage50_client_code,
                           gestProjectClientList[i].sage50_guid_id
                        );
                     };
                  };
               };
            };
         }

         if(someGestprojectClientsExistsInSage50 && !allGestprojectClientsExistsInSage50)
         {
            if(unsynchronizedClientsExists)
            {
               DialogResult result = MessageBox.Show("Desea actualizar los datos de Sage50 de los clientes desincronizados?", "Confirmación para actualización", MessageBoxButtons.OKCancel);

               if(result == DialogResult.OK)
               {
                  for(global::System.Int32 i = 0; i < existingClientsList.Count; i++)
                  {
                     if(unsynchronizedClientList.Contains(existingClientsList[i]))
                     {
                        new SincronizadorGPS50.Sage50Connector.UpdateSage50Customer(
                           existingClientsList[i].sage50_guid_id,
                           existingClientsList[i].PAR_PAIS_1,
                           existingClientsList[i].PAR_NOMBRE,
                           existingClientsList[i].PAR_CIF_NIF,
                           existingClientsList[i].PAR_CP_1,
                           existingClientsList[i].PAR_DIRECCION_1,
                           existingClientsList[i].PAR_PROVINCIA_1
                        );

                        new GestprojectDataManager.UpdateClientSyncronizationStatus(
                           GestprojectDataHolder.GestprojectDatabaseConnection,
                           existingClientsList[i].PAR_ID,
                           true
                        );

                        new GestprojectDataManager.RegisterNewSage50ClientData(
                           GestprojectDataHolder.GestprojectDatabaseConnection,
                           gestProjectClientList[i].PAR_ID,
                           gestProjectClientList[i].sage50_client_code,
                           gestProjectClientList[i].sage50_guid_id
                        );
                     };
                  };
               };
            };

            for(global::System.Int32 i = 0; i < nonExistingClientsList.Count; i++)
            {
               if(
                  Sage50ConnectionManager.CustomerManager.ClientExists(
                     nonExistingClientsList[i].sage50_guid_id,
                     nonExistingClientsList[i].PAR_PAIS_1,
                     nonExistingClientsList[i].PAR_NOMBRE,
                     nonExistingClientsList[i].PAR_CIF_NIF,
                     nonExistingClientsList[i].PAR_CP_1,
                     nonExistingClientsList[i].PAR_DIRECCION_1,
                     nonExistingClientsList[i].PAR_PROVINCIA_1
                  )
               )
               {
                  DialogResult result2 = MessageBox.Show("Desea crear este cliente de igual manera?", "Confirmación para creación", MessageBoxButtons.OKCancel);
                  if(result2 == DialogResult.Cancel)
                     continue;
               };

               CreateSage50Customer newSage50Client = new SincronizadorGPS50.Sage50Connector.CreateSage50Customer(
                  nonExistingClientsList[i].PAR_PAIS_1,
                  nonExistingClientsList[i].PAR_NOMBRE ,
                  nonExistingClientsList[i].PAR_CIF_NIF,
                  nonExistingClientsList[i].PAR_CP_1,
                  nonExistingClientsList[i].PAR_DIRECCION_1,
                  nonExistingClientsList[i].PAR_PROVINCIA_1
               );

               new GestprojectDataManager.RegisterNewSage50ClientData(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  nonExistingClientsList[i].PAR_ID,
                  newSage50Client.ClientCode,
                  newSage50Client.GUID_ID
               );
            };
         };
      }
   }
}
