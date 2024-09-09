﻿using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace SincronizadorGPS50
{
   public class TaxesDataTableManager : IGridDataSourceGenerator<GestprojectTaxModel, Sage50TaxModel>
   {
      public List<GestprojectTaxModel> GestprojectEntities { get; set; }
      public List<Sage50TaxModel> Sage50Entities { get; set; }
      public List<GestprojectTaxModel> ProcessedGestprojectEntities { get; set; }
      public DataTable DataTable { get; set; }

      public System.Data.DataTable GenerateDataTable
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider tableSchemaProvider
      )
      {
         try
         {
            ManageSynchronizationTableStatus(gestprojectConnectionManager, tableSchemaProvider);
            GetAndStoreGestprojectEntities(gestprojectConnectionManager, tableSchemaProvider);
            GetAndStoreSage50Entities(tableSchemaProvider);
            ProccessAndStoreGestprojectEntities(
               gestprojectConnectionManager,
               sage50ConnectionManager,
               tableSchemaProvider,
               GestprojectEntities,
               Sage50Entities
            );
            CreateAndDefineDataSource(tableSchemaProvider);
            PaintEntitiesOnDataSource(tableSchemaProvider, ProcessedGestprojectEntities, DataTable);
            return DataTable;
         }
         catch(System.Exception exception)
         {
            throw ApplicationLogger.ReportError(
               MethodBase.GetCurrentMethod().DeclaringType.Namespace,
               MethodBase.GetCurrentMethod().DeclaringType.Name,
               MethodBase.GetCurrentMethod().Name,
               exception
            );
         };
      }

      public void ManageSynchronizationTableStatus
      (
         IGestprojectConnectionManager gestprojectConnectionManager, 
         ISynchronizationTableSchemaProvider tableSchemaProvider
      )
      {
         ISynchronizationDatabaseTableManager providersSyncronizationTableStatusManager = new EntitySyncronizationTableStatusManager();

         bool tableExists = providersSyncronizationTableStatusManager.TableExists(
               gestprojectConnectionManager.GestprojectSqlConnection,
               tableSchemaProvider.TableName
            );

         if(tableExists == false)
         {
            providersSyncronizationTableStatusManager.CreateTable
            (
               gestprojectConnectionManager.GestprojectSqlConnection,
               tableSchemaProvider
            );
         };
      }

      public void GetAndStoreGestprojectEntities
      (
         IGestprojectConnectionManager gestprojectConnectionManager, 
         ISynchronizationTableSchemaProvider tableSchemaProvider
      )
      {
         GestprojectEntities = new GestprojectTaxesManager().GetEntities(
            gestprojectConnectionManager.GestprojectSqlConnection,
            "IMPUESTO_CONFIG",
            tableSchemaProvider.GestprojectFieldsTupleList
         );
      }

      public void GetAndStoreSage50Entities
      (
         ISynchronizationTableSchemaProvider tableSchemaProvider
      )
      {
         Sage50Entities = new GetSage50Taxes().Entities;
      }

      public void ProccessAndStoreGestprojectEntities
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider tableSchemaProvider,
         List<GestprojectTaxModel> GestprojectEntities,
         List<Sage50TaxModel> Sage50Entities
      )
      {
         
         ISynchronizableEntityProcessor<GestprojectTaxModel, Sage50TaxModel> gestprojectProvidersProcessor = new GestprojectTaxesProcessor();
         ProcessedGestprojectEntities = gestprojectProvidersProcessor.ProcessEntityList(
            gestprojectConnectionManager.GestprojectSqlConnection,
            sage50ConnectionManager,
            tableSchemaProvider,
            GestprojectEntities,
            Sage50Entities
         );
      }

      public void CreateAndDefineDataSource
      (
         ISynchronizationTableSchemaProvider tableSchemaProvider
      )
      {
         IDataTableGenerator providersDataTableGenerator = new SyncrhonizationDataTableGenerator();
         DataTable = providersDataTableGenerator.CreateDataTable(tableSchemaProvider.ColumnsTuplesList);
      }

      public void PaintEntitiesOnDataSource
      (
         ISynchronizationTableSchemaProvider tableSchemaProvider,
         List<GestprojectTaxModel> ProcessedGestprojectEntities,
         DataTable dataTable
      )
      {
         ISynchronizableEntityPainter<GestprojectTaxModel> entityPainter = new EntityPainter<GestprojectTaxModel>();
         entityPainter.PaintEntityListOnDataTable(
            ProcessedGestprojectEntities,
            dataTable,
            tableSchemaProvider.ColumnsTuplesList
         );
      }
   }
}
