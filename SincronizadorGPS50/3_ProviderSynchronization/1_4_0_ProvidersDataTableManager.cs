using SincronizadorGPS50.GestprojectDataManager;
using SincronizadorGPS50.Sage50Connector;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   public class ProvidersDataTableManager : IGridDataSourceGenerator
   {
      public DataTable DataSource { get; set; }
      public System.Data.DataTable GenerateDataTable
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider synchronizationTableSchemaProvider
      )
      {
         try
         {
            //////////////////////////////
            /// 1. manage syncronization table status
            //////////////////////////////

            ISynchronizationDatabaseTableManager providersSyncronizationTableStatusManager = new ProvidersSyncronizationTableStatusManager();

            bool tableExists = providersSyncronizationTableStatusManager.TableExists(
               gestprojectConnectionManager.GestprojectSqlConnection,
               synchronizationTableSchemaProvider
            );

            if(tableExists == false)
            {
               providersSyncronizationTableStatusManager.CreateTable
               (
                  gestprojectConnectionManager.GestprojectSqlConnection,
                  synchronizationTableSchemaProvider
               );
            };

            //////////////////////////////
            /// 2. get entities to be processed
            //////////////////////////////

            IGestprojectEntitiesProvider gestprojectEntitiesProvider = new GestprojectEntitiesProvider();
            List<GestprojectProviderModel> gestprojectProviders = gestprojectEntitiesProvider.GetProviders(gestprojectConnectionManager.GestprojectSqlConnection);

            ISage50EntitiesProvider sage50EntitiesProvider = new Sage50EntitiesProvider();
            List<Sage50ProviderModel> sage50Providers = sage50EntitiesProvider.GetProviders();

            //////////////////////////////
            /// 3. process entities
            //////////////////////////////

            ISynchronizableEntityProcessor gestprojectProvidersProcessor = new GestprojectProvidersProcessor();
            List<GestprojectProviderModel> proccessedGestprojectProviders = gestprojectProvidersProcessor.ProcessEntityList(
               gestprojectConnectionManager.GestprojectSqlConnection, 
               sage50ConnectionManager,
               synchronizationTableSchemaProvider,
               gestprojectProviders, 
               sage50Providers
            );

            //////////////////////////////
            /// 4. create and define data source
            //////////////////////////////

            IDataTableGenerator providersDataTableGenerator = new ProvidersDataTableGenerator();
            DataTable dataTable = providersDataTableGenerator.CreateDataTable(synchronizationTableSchemaProvider.ColumnsTuplesList);

            //////////////////////////////
            /// 5. paint entities on data source
            //////////////////////////////

            ISynchronizableEntityPainter entityPainter = new GestprojectProvidersPainter();
            entityPainter.PaintEntityListOnDataTable(
               proccessedGestprojectProviders,
               dataTable,
               synchronizationTableSchemaProvider.ColumnsTuplesList
            );

            //////////////////////////////
            /// 6. return populated data source for UI consumption
            //////////////////////////////

            return dataTable;
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
   }
}
