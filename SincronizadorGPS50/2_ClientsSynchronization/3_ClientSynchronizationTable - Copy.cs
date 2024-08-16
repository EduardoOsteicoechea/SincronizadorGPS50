//using SincronizadorGPS50.GestprojectAPI;
//using System.Collections.Generic;
//using System.Data;
//using System.Windows.Forms;

//namespace SincronizadorGPS50.Workflows.Clients
//{
//   internal static class ClientSynchronizationTable
//   {
//      public static DataTable Create()
//      {
//         //List<GestprojectClient> gestprojectClientList = GestprojectClients.Get();

//         List<GestprojectDataManager.GestprojectClient> gestprojectClientList =
//            new GestprojectDataManager
//            .GestprojectClientsManager()
//            .GetClients(
//               GestprojectDataHolder.GestprojectDatabaseConnection
//            );

//         DataTable table = new CreateTableControl().Table;

//         bool Sage50SincronizationTableExists = 
//            new GestprojectDataManager
//            .CheckIfTableExistsOnGestproject(
//               GestprojectDataHolder.GestprojectDatabaseConnection, 
//               "INT_SAGE_SINC_CLIENTE"
//            ).Exists;

//         if(!Sage50SincronizationTableExists)
//         {
//            new GestprojectDataManager.CreateGestprojectSage50SynchronizationTable(GestprojectDataHolder.GestprojectDatabaseConnection);
//         };

//         bool Sage50SynchronizationTableWasJustCreated = !Sage50SincronizationTableExists;

//         for(int i = 0; i < gestprojectClientList.Count; i++)
//         {
//            GestprojectDataManager.GestprojectClient gestprojectClient = gestprojectClientList[i];

//            new PopulateGestprojectClientSynchronizationData(gestprojectClient);

//            //if(Sage50SynchronizationTableWasJustCreated)
//            //{
//            //   new RegisterClient(
//            //       gestprojectClient,
//            //       SynchronizationStatusOptions.Nunca_ha_sido_sincronizado
//            //   );

//            //   new PopulateGestprojectClientSynchronizationData(gestprojectClient);

//            //   new AddClientToSynchronizationUITable(
//            //       gestprojectClient,
//            //       table,
//            //       gestprojectClient.synchronization_status,
//            //       gestprojectClient.comments
//            //   );
//            //}
//            //else
//            //{
//            //   bool GestprojectClientWasRegistered = new WasGestprojectClientRegistered(gestprojectClient).ItIs;

//            //   if(GestprojectClientWasRegistered)
//            //   {
//            //      new PopulateGestprojectClientSynchronizationData(gestprojectClient);

//            //      bool GestprojectClientWasSynchronized = new WasGestprojectClientSynchronized(gestprojectClient).ItIs;

//            //      if(!GestprojectClientWasSynchronized)
//            //      {
//            //         new UpdateClientSynchronizationStatus(
//            //             gestprojectClient,
//            //             SynchronizationStatusOptions.Nunca_ha_sido_sincronizado
//            //         );

//            //         new PopulateGestprojectClientSynchronizationData(gestprojectClient);

//            //         new AddClientToSynchronizationUITable(
//            //             gestprojectClient,
//            //             table,
//            //             gestprojectClient.synchronization_status,
//            //             gestprojectClient.comments
//            //         );
//            //      }
//            //      else
//            //      {
//            //         new UpdateClientSynchronizationStatus(
//            //             gestprojectClient,
//            //             SynchronizationStatusOptions.Sincronizado
//            //         );

//            //         new PopulateGestprojectClientSynchronizationData(gestprojectClient);

//            //         if(!new EndPointsData(gestprojectClient).Matches)
//            //         {
//            //            new UpdateClientSynchronizationStatus(
//            //                gestprojectClient,
//            //                SynchronizationStatusOptions.Desincronizado
//            //            );
//            //         };

//            //         new AddClientToSynchronizationUITable(
//            //             gestprojectClient,
//            //             table,
//            //             gestprojectClient.synchronization_status,
//            //             gestprojectClient.comments
//            //         );
//            //      };
//            //   }
//            //   else
//            //   {
//            //      new RegisterClient(
//            //          gestprojectClient,
//            //          SynchronizationStatusOptions.Nunca_ha_sido_sincronizado
//            //      );

//            //      new PopulateGestprojectClientSynchronizationData(gestprojectClient);

//            //      new AddClientToSynchronizationUITable(
//            //          gestprojectClient,
//            //          table,
//            //          gestprojectClient.synchronization_status,
//            //          gestprojectClient.comments
//            //      );
//            //   };
//            //};
//         };

//         return table;
//      }

//      public static DataTable Refresh()
//      {
//         List<GestprojectClient> gestprojectClientList = GestprojectClients.Get();

//         DataTable table = new CreateTableControl().Table;

//         for(int i = 0; i < gestprojectClientList.Count; i++)
//         {
//            GestprojectClient gestprojectClient = gestprojectClientList[i];

//            bool GestprojectClientWasSynchronized = new WasGestprojectClientSynchronized(gestprojectClient).ItIs;

//            if(!GestprojectClientWasSynchronized)
//            {
//               new UpdateClientSynchronizationStatus(
//                   gestprojectClient,
//                   SynchronizationStatusOptions.Nunca_ha_sido_sincronizado
//               );

//               //new PopulateGestprojectClientSynchronizationData(gestprojectClient);

//               new AddClientToSynchronizationUITable(
//                   gestprojectClient,
//                   table,
//                   gestprojectClient.synchronization_status,
//                   gestprojectClient.comments
//               );
//            }
//            else
//            {
//               //new PopulateGestprojectClientSynchronizationData(gestprojectClient);

//               if(!new EndPointsData(gestprojectClient).Matches)
//               {
//                  new UpdateClientSynchronizationStatus(
//                      gestprojectClient,
//                      SynchronizationStatusOptions.Desincronizado
//                  );
//               };

//               //new PopulateGestprojectClientSynchronizationData(gestprojectClient);

//               new AddClientToSynchronizationUITable(
//                   gestprojectClient,
//                   table,
//                   gestprojectClient.synchronization_status,
//                   gestprojectClient.comments
//               );
//            };
//         };

//         return table;
//      }
//   }
//}










