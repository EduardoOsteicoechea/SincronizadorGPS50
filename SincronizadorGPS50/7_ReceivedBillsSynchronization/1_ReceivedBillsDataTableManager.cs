using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   public class ReceivedBillsDataTableManager : IGridDataSourceGenerator<GestprojectReceivedBillModel, Sage50ReceivedBillModel>
   {
      public List<GestprojectReceivedBillModel> GestprojectEntities { get; set; }
      public List<Sage50ReceivedBillModel> Sage50Entities { get; set; }
      public List<GestprojectReceivedBillModel> ProcessedGestprojectEntities { get; set; }
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

      public void ManageSynchronizationTableStatus ( IGestprojectConnectionManager gestprojectConnectionManager, ISynchronizationTableSchemaProvider tableSchemaProvider )
      {
         ISynchronizationDatabaseTableManager entitySyncronizationTableStatusManager = new EntitySyncronizationTableStatusManager();

         bool tableExists = entitySyncronizationTableStatusManager.TableExists(
            gestprojectConnectionManager.GestprojectSqlConnection,
            tableSchemaProvider.TableName
         );

         if(tableExists == false)
         {
            entitySyncronizationTableStatusManager.CreateTable
            (
               gestprojectConnectionManager.GestprojectSqlConnection,
               tableSchemaProvider
            );
         };
      }

      public void GetAndStoreGestprojectEntities ( IGestprojectConnectionManager gestprojectConnectionManager, ISynchronizationTableSchemaProvider tableSchemaProvider )
      {
         GestprojectEntities = new GestprojectReceivedBillsManager().GetEntities(
            gestprojectConnectionManager.GestprojectSqlConnection,
            "FACTURA_PROVEEDOR",
            tableSchemaProvider.GestprojectFieldsTupleList
         );

         //foreach(var item in GestprojectEntities)
         //{
         //   StringBuilder stringBuilder = new StringBuilder();
         //   foreach(var prpoertyInfo in item.GetType().GetProperties())
         //   {
         //      stringBuilder.Append($"{prpoertyInfo.Name}: {prpoertyInfo.GetValue(item)}\n");
         //   }  
         //   MessageBox.Show( stringBuilder.ToString() );
         //}
      }

      public void GetAndStoreSage50Entities ( ISynchronizationTableSchemaProvider tableSchemaProvider )
      {
         Sage50Entities = new GetSage50ReceivedBills().Entities;
      }

      public void ProccessAndStoreGestprojectEntities
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider tableSchemaProvider,
         List<GestprojectReceivedBillModel> GestprojectEntities,
         List<Sage50ReceivedBillModel> Sage50Entities
      )
      {

         ISynchronizableEntityProcessor<GestprojectReceivedBillModel, Sage50ReceivedBillModel> gestprojectProvidersProcessor = new GestprojectReceivedBillsProcessor();
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
         IDataTableGenerator entityDataTableGenerator = new SyncrhonizationDataTableGenerator();
         DataTable = entityDataTableGenerator.CreateDataTable(tableSchemaProvider.ColumnsTuplesList);
      }

      public void PaintEntitiesOnDataSource
      (
         ISynchronizationTableSchemaProvider tableSchemaProvider,
         List<GestprojectReceivedBillModel> ProcessedGestprojectEntities,
         DataTable dataTable
      )
      {
         //foreach(var entity in ProcessedGestprojectEntities)
         //{
         //   StringBuilder stringBuilder = new StringBuilder();
         //   foreach(var item in entity.GetType().GetProperties())
         //   {
         //      stringBuilder.Append($"{item.Name}: {item.GetValue(entity)}\n");
         //   };
         //   MessageBox.Show(stringBuilder.ToString());
         //};

         ISynchronizableEntityPainter<GestprojectReceivedBillModel> entityPainter = new EntityPainter<GestprojectReceivedBillModel>();
         entityPainter.PaintEntityListOnDataTable(
            ProcessedGestprojectEntities,
            dataTable,
            tableSchemaProvider.ColumnsTuplesList
         );
      }
   }
}
