using System.Collections.Generic;
using System.Data;

namespace SincronizadorGPS50
{
   internal static class ClientSynchronizationTable
   {
      public static DataTable Create()
      {
         List<GestprojectDataManager.GestprojectClient> gestprojectClientList =
         new GestprojectDataManager
         .GestprojectClientsManager()
         .GetClients(
            GestprojectDataHolder.GestprojectDatabaseConnection
         );

         DataTable table = new CreateTableControl().Table;

         bool Sage50SincronizationTableExists = 
         new GestprojectDataManager
         .CheckIfTableExistsOnGestproject(
            GestprojectDataHolder.GestprojectDatabaseConnection, 
            "INT_SAGE_SINC_CLIENTE"
         ).Exists;

         if(!Sage50SincronizationTableExists)
         {
            new GestprojectDataManager.CreateClientSynchronizationTable(GestprojectDataHolder.GestprojectDatabaseConnection);
         };

         bool Sage50SynchronizationTableWasJustCreated = !Sage50SincronizationTableExists;

         for(int i = 0; i < gestprojectClientList.Count; i++)
         {
            GestprojectDataManager.GestprojectClient gestprojectClient = gestprojectClientList[i];

            if(Sage50SynchronizationTableWasJustCreated)
            {
               new GestprojectDataManager.RegisterClient(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  gestprojectClient,
                  SynchronizationStatusOptions.Nunca_ha_sido_sincronizado,
                  GestprojectDataHolder.LocalDeviceUserSessionData.USU_ID
               );
            }
            else
            {
               bool gestprojectClientWasRegistered =
               new GestprojectDataManager
               .WasGestprojectClientRegistered(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  gestprojectClient
               ).ItIs;

               if(!gestprojectClientWasRegistered)
               {
                  new GestprojectDataManager.RegisterClient(
                     GestprojectDataHolder.GestprojectDatabaseConnection,
                     gestprojectClient,
                     SynchronizationStatusOptions.Nunca_ha_sido_sincronizado,
                     GestprojectDataHolder.LocalDeviceUserSessionData.USU_ID
                  );
               }
            };

            new GestprojectDataManager.PopulateUnsynchronizedClientRegistrationData(
               GestprojectDataHolder.GestprojectDatabaseConnection,
               gestprojectClient
            );

            new AddClientToUITable(gestprojectClient, table);
         };

         return table;
      }
   }
}










