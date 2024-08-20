using SincronizadorGPS50.Sage50Connector;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal static class ManageCustomerSynchronizationTable
   {
      public static DataTable Create()
      {
         ///////////////////////////////////
         /// get both gestproject and sage50 clients
         ///////////////////////////////////
         
         List<GestprojectDataManager.GestprojectCustomer> gestprojectCustomerList = new GestprojectDataManager.GestprojectClientsManager()
         .GetClients(
            GestprojectDataHolder.GestprojectDatabaseConnection
         );

         List<Sage50Customer> sage50CustomerList = new GetSage50Customer().CustomerList;

         ///////////////////////////////////
         /// create data table
         ///////////////////////////////////

         DataTable table = new CreateTableControl().Table;

         ///////////////////////////////////
         /// manage synchronization table state
         ///////////////////////////////////

         bool Sage50SincronizationTableExists = new GestprojectDataManager.CheckIfTableExistsOnGestproject(
            GestprojectDataHolder.GestprojectDatabaseConnection, 
            "INT_SAGE_SINC_CLIENTE"
         ).Exists;

         if(!Sage50SincronizationTableExists)
         {
            new GestprojectDataManager.CreateClientSynchronizationTable(GestprojectDataHolder.GestprojectDatabaseConnection);
         };

         bool Sage50SynchronizationTableWasJustCreated = !Sage50SincronizationTableExists;

         ///////////////////////////////////
         /// process each gestproject database client
         ///////////////////////////////////

         for(int i = 0; i < gestprojectCustomerList.Count; i++)
         {
            GestprojectDataManager.GestprojectCustomer gestprojectCustomer = gestprojectCustomerList[i];

            if(Sage50SynchronizationTableWasJustCreated)
            {
               new GestprojectDataManager.RegisterClient(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  gestprojectCustomer,
                  SynchronizationStatusOptions.Nunca_ha_sido_sincronizado
               );
            }
            else
            {
               if(
                  !new GestprojectDataManager.WasGestprojectClientRegistered(
                     GestprojectDataHolder.GestprojectDatabaseConnection,
                     gestprojectCustomer
                  ).ItIs
               )
               {
                  new GestprojectDataManager.RegisterClient(
                     GestprojectDataHolder.GestprojectDatabaseConnection,
                     gestprojectCustomer,
                     SynchronizationStatusOptions.Nunca_ha_sido_sincronizado
                  );
               }
               else
               {
                  GestprojectDataManager.GestprojectCustomer synchronizationTableProjectClient =
                  new SincronizadorGPS50
                  .GestprojectDataManager
                  .GetSingleCustomerFromSynchronizationTable(
                     GestprojectDataHolder.GestprojectDatabaseConnection,
                     gestprojectCustomer.PAR_ID
                  ).GestprojectCustomer;

                  GestprojectDataManager.GestprojectCustomer validatedGestprojectClient = 
                  new ValidateClientSyncronizationStatus(
                     synchronizationTableProjectClient.sage50_guid_id,
                     gestprojectCustomer.fullName,
                     gestprojectCustomer.PAR_APELLIDO_1,
                     gestprojectCustomer.PAR_APELLIDO_2,
                     gestprojectCustomer.PAR_CIF_NIF,
                     gestprojectCustomer.PAR_CP_1,
                     gestprojectCustomer.PAR_DIRECCION_1,
                     gestprojectCustomer.PAR_PROVINCIA_1,
                     gestprojectCustomer.PAR_PAIS_1,
                     sage50CustomerList
                  ).GestprojectClient;

                  gestprojectCustomer.synchronization_status = validatedGestprojectClient.synchronization_status;
                  gestprojectCustomer.comments = validatedGestprojectClient.comments;
               };
            };

            new GestprojectDataManager.PopulateUnsynchronizedClientRegistrationData(
               GestprojectDataHolder.GestprojectDatabaseConnection,
               gestprojectCustomer
            );

            new AddClientToUITable(gestprojectCustomer, table);
         };

         return table;
      }
   }
}










