//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Reflection;

//namespace SincronizadorGPS50
//{
//   public class SubaccountableAccountsDataTableManager : IGridDataSourceGenerator<GestprojectSubaccountableAccountModel, Sage50SubaccountableAccountModel>
//   {
//      public List<GestprojectSubaccountableAccountModel> GestprojectEntities { get; set; }
//      public List<Sage50SubaccountableAccountModel> Sage50Entities { get; set; }
//      public List<GestprojectSubaccountableAccountModel> ProcessedGestprojectEntities { get; set; }
//      public DataTable DataTable { get; set; }

//      public System.Data.DataTable GenerateDataTable
//      (
//         IGestprojectConnectionManager gestprojectConnectionManager,
//         ISage50ConnectionManager sage50ConnectionManager,
//         ISynchronizationTableSchemaProvider tableSchema
//      )
//      {
//         try
//         {
//            ManageSynchronizationTableStatus(gestprojectConnectionManager, tableSchema);
//            GetAndStoreGestprojectEntities(gestprojectConnectionManager, tableSchema);
//            GetAndStoreSage50Entities(tableSchema);
//            ProccessAndStoreGestprojectEntities(
//               gestprojectConnectionManager,
//               sage50ConnectionManager,
//               tableSchema,
//               GestprojectEntities,
//               Sage50Entities
//            );
//            CreateAndDefineDataSource(tableSchema);
//            PaintEntitiesOnDataSource(tableSchema, ProcessedGestprojectEntities, DataTable);
//            return DataTable;
//         }
//         catch(System.Exception exception)
//         {
//            throw ApplicationLogger.ReportError(
//               MethodBase.GetCurrentMethod().DeclaringType.Namespace,
//               MethodBase.GetCurrentMethod().DeclaringType.Name,
//               MethodBase.GetCurrentMethod().Name,
//               exception
//            );
//         };
//      }

//      public void ManageSynchronizationTableStatus
//      (
//         IGestprojectConnectionManager gestprojectConnectionManager,
//         ISynchronizationTableSchemaProvider tableSchema
//      )
//      {
//         ISynchronizationDatabaseTableManager entitySyncronizationTableStatusManager = new EntitySyncronizationTableStatusManager();

//         bool tableExists = entitySyncronizationTableStatusManager.TableExists(
//               gestprojectConnectionManager.GestprojectSqlConnection,
//               tableSchema.TableName
//            );

//         if(tableExists == false)
//         {
//            entitySyncronizationTableStatusManager.CreateTable
//            (
//               gestprojectConnectionManager.GestprojectSqlConnection,
//               tableSchema
//            );
//         };
//      }

//      public void GetAndStoreGestprojectEntities
//      (
//         IGestprojectConnectionManager gestprojectConnectionManager,
//         ISynchronizationTableSchemaProvider tableSchema
//      )
//      {
//         GestprojectEntities = new List<GestprojectSubaccountableAccountModel> ();

//         GestprojectEntities = new GestprojectSubaccountableAccountsManager().GetEntities(
//            gestprojectConnectionManager.GestprojectSqlConnection,
//            tableSchema.GestprojectEntityTableName,
//            tableSchema.GestprojectFieldsTupleList
//         );

//         var subaccountableAccountList = GestprojectEntities.Select(x=>x.COS_CODIGO);
//         var subaccountableAccountList2 = GestprojectEntities.Select(x=>x.COS_NOMBRE);

//         List<Sage50SubaccountableAccountModel> sage50Entities = new GetSage50SubaccountableAccounts(tableSchema).Entities;

//         bool itemExists = true;
//         foreach(var item in sage50Entities)
//         {
//            itemExists =
//                subaccountableAccountList.Contains(item.CODIGO)
//                &&
//                subaccountableAccountList2.Contains(item.NOMBRE);

//            if( itemExists )
//            {
//               GestprojectSubaccountableAccountModel existingEntity = GestprojectEntities.FirstOrDefault(
//                  x => (x.COS_CODIGO == item.CODIGO && x.COS_NOMBRE == item.NOMBRE) 
//               );

//               itemExists = existingEntity != null;
//            }; 
               
//            if(!itemExists)
//            {
//               GestprojectSubaccountableAccountModel gestprojectSubaccountableAccountModel = new GestprojectSubaccountableAccountModel();

//               gestprojectSubaccountableAccountModel.COS_ID = -1;
//               gestprojectSubaccountableAccountModel.COS_CODIGO = item.CODIGO.Trim();
//               gestprojectSubaccountableAccountModel.COS_NOMBRE = item.NOMBRE.Trim();
//               //gestprojectSubaccountableAccountModel.COS_GRUPO = item.CODIGO.Trim();

//               GestprojectEntities.Add(gestprojectSubaccountableAccountModel);
//            };
//         };

//         new VisualizePropertiesAndValues<GestprojectSubaccountableAccountModel>(
//             "At: " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name,
//             "GestprojectEntities",
//             GestprojectEntities
//         );
//      }

//      public void GetAndStoreSage50Entities
//      (
//         ISynchronizationTableSchemaProvider tableSchema
//      )
//      {
//         Sage50Entities = new List<Sage50SubaccountableAccountModel>();
//         Sage50Entities = new GetSage50SubaccountableAccounts(tableSchema).Entities;

//         //new VisualizePropertiesAndValues<Sage50SubaccountableAccountModel>(
//         //    "At: " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name,
//         //    "Sage50Entities",
//         //    Sage50Entities
//         //);
//      }

//      public void ProccessAndStoreGestprojectEntities
//      (
//         IGestprojectConnectionManager gestprojectConnectionManager,
//         ISage50ConnectionManager sage50ConnectionManager,
//         ISynchronizationTableSchemaProvider tableSchema,
//         List<GestprojectSubaccountableAccountModel> GestprojectEntities,
//         List<Sage50SubaccountableAccountModel> Sage50Entities
//      )
//      {
//         ISynchronizableEntityProcessor<GestprojectSubaccountableAccountModel, Sage50SubaccountableAccountModel> gestprojectProvidersProcessor = new GestprojectSubaccountableAccountsProcessor();

//         ProcessedGestprojectEntities = gestprojectProvidersProcessor.ProcessEntityList(
//            gestprojectConnectionManager.GestprojectSqlConnection,
//            sage50ConnectionManager,
//            tableSchema,
//            GestprojectEntities,
//            Sage50Entities
//         );

//         //new VisualizePropertiesAndValues<GestprojectSubaccountableAccountModel>(
//         //    "At: " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name,
//         //    "ProcessedGestprojectEntities",
//         //    ProcessedGestprojectEntities
//         //);
//      }

//      public void CreateAndDefineDataSource
//      (
//         ISynchronizationTableSchemaProvider tableSchema
//      )
//      {
//         IDataTableGenerator entityDataTableGenerator = new SyncrhonizationDataTableGenerator();
//         DataTable = entityDataTableGenerator.CreateDataTable(tableSchema.ColumnsTuplesList);
//      }

//      public void PaintEntitiesOnDataSource
//      (
//         ISynchronizationTableSchemaProvider tableSchema,
//         List<GestprojectSubaccountableAccountModel> ProcessedGestprojectEntities,
//         DataTable dataTable
//      )
//      {
//         ISynchronizableEntityPainter<GestprojectSubaccountableAccountModel> entityPainter = new EntityPainter<GestprojectSubaccountableAccountModel>();
//         entityPainter.PaintEntityListOnDataTable(
//            ProcessedGestprojectEntities,
//            dataTable,
//            tableSchema.ColumnsTuplesList
//         );
//      }
//   }
//}