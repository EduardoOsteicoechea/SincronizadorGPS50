using SincronizadorGPS50.Sage50Connector;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace SincronizadorGPS50
{
   public class ProvidersDataTableManager : IGridDataSourceGenerator<GestprojectProviderModel, Sage50ProviderModel>
   {
      public List<GestprojectProviderModel> GestprojectEntities { get; set; }
      public List<Sage50ProviderModel> Sage50Entities { get; set; }
      public List<GestprojectProviderModel> ProcessedGestprojectEntities { get; set; }
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
         ISynchronizationDatabaseTableManager providersSyncronizationTableStatusManager = new ProvidersSyncronizationTableStatusManager();

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
         IGestprojectEntitiesProvider gestprojectEntitiesProvider = new GestprojectEntitiesProvider();
         GestprojectEntities = gestprojectEntitiesProvider.GetProviders(gestprojectConnectionManager.GestprojectSqlConnection);
      }

      public void GetAndStoreSage50Entities
      (
         ISynchronizationTableSchemaProvider tableSchemaProvider
      )
      {
         ISage50EntitiesProvider sage50EntitiesProvider = new Sage50EntitiesProvider();
         Sage50Entities = sage50EntitiesProvider.GetProviders();
      }

      public void ProccessAndStoreGestprojectEntities
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider tableSchemaProvider,
         List<GestprojectProviderModel> GestprojectEntities,
         List<Sage50ProviderModel> Sage50Entities
      )
      {
         ISynchronizableEntityProcessor<GestprojectProviderModel, Sage50ProviderModel> gestprojectProvidersProcessor = new GestprojectProvidersProcessor();
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
         IDataTableGenerator providersDataTableGenerator = new ProvidersDataTableGenerator();
         DataTable = providersDataTableGenerator.CreateDataTable(tableSchemaProvider.ColumnsTuplesList);
      }

      public void PaintEntitiesOnDataSource
      (
         ISynchronizationTableSchemaProvider tableSchemaProvider,
         List<GestprojectProviderModel> ProcessedGestprojectEntities,
         DataTable dataTable
      )
      {
         ISynchronizableEntityPainter entityPainter = new EntityPainter();
         entityPainter.PaintEntityListOnDataTable(
            ProcessedGestprojectEntities,
            dataTable,
            tableSchemaProvider.ColumnsTuplesList
         );
      }
   }
}
