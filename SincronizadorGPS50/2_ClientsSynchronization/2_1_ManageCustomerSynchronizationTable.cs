using SincronizadorGPS50.Sage50Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
         /// create ui data source
         ///////////////////////////////////

         DataTable table = new CreateTableControl().Table;

         ///////////////////////////////////
         /// manage synchronization table state
         ///////////////////////////////////

         bool CustomerSincronizationTableExists = new GestprojectDataManager.CheckIfTableExistsOnGestproject(
            GestprojectDataHolder.GestprojectDatabaseConnection,
            "INT_SAGE_SINC_CLIENTE"
         ).Exists;

         if(!CustomerSincronizationTableExists)
            new GestprojectDataManager.CreateClientSynchronizationTable(GestprojectDataHolder.GestprojectDatabaseConnection);

         ///////////////////////////////////
         /// process each Gestproject customer
         ///////////////////////////////////

         for(int i = 0; i < gestprojectCustomerList.Count; i++)
         {
            GestprojectDataManager.GestprojectCustomer gestprojectCustomer = gestprojectCustomerList[i];

            ///////////////////////////////////
            /// get current session UI Company Group data
            ///////////////////////////////////

            var sage50CompanyGroup = SincronizadorGPS50.Sage50Connector.Sage50CompanyGroupActions.GetCompanyGroups().FirstOrDefault(companyGroup => companyGroup.CompanyName == Sage50ConnectionUIHolder.Sage50ConnectionUIManagerInstance.SelectCompanyGroupUI.SelectEnterpryseGroupMenu.Text);

            ///////////////////////////////////
            /// if new, register customer in synchronization table
            ///////////////////////////////////

            if(
               !CustomerSincronizationTableExists 
                  || 
               !new GestprojectDataManager.WasGestprojectClientRegistered(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  gestprojectCustomer,
                  sage50CompanyGroup.CompanyGuidId
               ).ItIs
            )
            {
               new GestprojectDataManager.RegisterClient(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  gestprojectCustomer,
                  SynchronizationStatusOptions.Nunca_ha_sido_sincronizado,
                  sage50CompanyGroup.CompanyName,
                  sage50CompanyGroup.CompanyMainCode,
                  sage50CompanyGroup.CompanyCode,
                  sage50CompanyGroup.CompanyGuidId
               );
            };

            ///////////////////////////////////
            /// add current Gestproject customer synchronization table data if any
            ///////////////////////////////////

            new GestprojectDataManager.AddSynchronizationTableCustomerData(
               GestprojectDataHolder.GestprojectDatabaseConnection,
               gestprojectCustomer,
               sage50CompanyGroup.CompanyGuidId
            );

            ///////////////////////////////////
            /// get Gestproject client current synchronization state
            ///////////////////////////////////

            ValidateClientSyncronizationStatus clientSyncronizationStatusValidator = new ValidateClientSyncronizationStatus(
               gestprojectCustomer,
               sage50CustomerList
            );

            if(clientSyncronizationStatusValidator.MustBeDeleted)
            {
               ///////////////////////////////////
               /// delete sage50 unexisting customer from synchronization table
               /// and register it again as a never synchronized customer
               ///////////////////////////////////
               
               new GestprojectDataManager.DeleteFromSynchronizationTable(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  gestprojectCustomer
               );

               new GestprojectDataManager.ClearCustomerSynchronizationData(
                  gestprojectCustomer
               );

               new GestprojectDataManager.RegisterClient(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  gestprojectCustomer,
                  SynchronizationStatusOptions.Nunca_ha_sido_sincronizado,
                  sage50CompanyGroup.CompanyName,
                  sage50CompanyGroup.CompanyMainCode,
                  sage50CompanyGroup.CompanyCode,
                  sage50CompanyGroup.CompanyGuidId
               );

               new GestprojectDataManager.AddSynchronizationTableCustomerData(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  gestprojectCustomer,
                  sage50CompanyGroup.CompanyGuidId
               );
            };

            ///////////////////////////////////
            /// add stateful Gestproject client to ui data source
            ///////////////////////////////////

            new AddClientToUITable(gestprojectCustomer, table);
         };

         ///////////////////////////////////
         /// return ui data source
         ///////////////////////////////////

         return table;
      }
   }
}










