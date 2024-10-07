using SincronizadorGPS50.GestprojectDataManager;
using SincronizadorGPS50.Sage50Connector;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   public class SubaccountableAccountsSynchronizer : IEntitySynchronizer<GestprojectSubaccountableAccountModel, Sage50SubaccountableAccountModel>
   {
      public List<GestprojectSubaccountableAccountModel> GestprojectEntityList {get;set;} = new List<GestprojectSubaccountableAccountModel>();
      public List<Sage50SubaccountableAccountModel> Sage50EntityList {get;set;} = new List<Sage50SubaccountableAccountModel> { };
      public List<GestprojectSubaccountableAccountModel> UnexistingGestprojectEntityList {get;set;} = new List<GestprojectSubaccountableAccountModel>();
      public List<GestprojectSubaccountableAccountModel> ExistingGestprojectEntityList {get;set; } = new List<GestprojectSubaccountableAccountModel>();
      public List<GestprojectSubaccountableAccountModel> UnsynchronizedGestprojectEntityList {get;set; } = new List<GestprojectSubaccountableAccountModel>();
      public bool SomeEntitiesExistsInSage50 {get;set;}
      public bool AllEntitiesExistsInSage50 {get;set;}
      public bool NoEntitiesExistsInSage50 {get;set;}
      public bool UnsynchronizedEntityExists {get;set;}
      public IGestprojectConnectionManager GestprojectConnectionManager { get; set; }
      public ISage50ConnectionManager Sage50ConnectionManager { get; set; }
      public ISynchronizationTableSchemaProvider SynchronizationTableSchemaProvider { get; set; }

      public void Synchronize
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider tableSchema,
         List<int> selectedIdList
      )
      {
         try
         {
            GestprojectConnectionManager = gestprojectConnectionManager;
            Sage50ConnectionManager = sage50ConnectionManager;
            SynchronizationTableSchemaProvider = tableSchema;

            StoreGestprojectEntityList
            (
               GestprojectConnectionManager,
               selectedIdList,
               SynchronizationTableSchemaProvider.TableName,
               SynchronizationTableSchemaProvider.ColumnsTuplesList.Select(x=>(x.columnName,x.columnType)).ToList(),
               (
                  tableSchema.GestprojectId.ColumnDatabaseName,
                  string.Join(",", selectedIdList)
               )
            );

            StoreSage50EntityList
            (
               tableSchema.SageTableData.dispatcherAndName.sageDispactcherMechanismRoute,
               tableSchema.SageTableData.dispatcherAndName.tableName,
               tableSchema.SageTableData.tableFieldsAlongTypes
            );

            StoreBreakDownGestprojectEntityListByStatus(GestprojectEntityList, Sage50EntityList);

            DetermineEntitySincronizationWorkflow(UnexistingGestprojectEntityList, ExistingGestprojectEntityList, UnsynchronizedGestprojectEntityList, GestprojectEntityList);

            ExecuteSyncronizationWorkflow
            (
               SomeEntitiesExistsInSage50,
               AllEntitiesExistsInSage50,
               NoEntitiesExistsInSage50,
               UnsynchronizedEntityExists,
               GestprojectConnectionManager,
               Sage50ConnectionManager,
               SynchronizationTableSchemaProvider,
               UnexistingGestprojectEntityList,
               ExistingGestprojectEntityList,
               UnsynchronizedGestprojectEntityList,
               GestprojectEntityList
            );
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

      public void StoreGestprojectEntityList
      (
         IGestprojectConnectionManager gestprojectConnectionManager, 
         List<int> selectedIdList, 
         string tableName, 
         List<(string, System.Type)> fieldsToBeRetrieved, 
         (string condition1ColumnName, string condition1Value) condition1Data
      )
      {
         GestprojectEntityList = new GestprojectEntities<GestprojectSubaccountableAccountModel>().GetAll(
            gestprojectConnectionManager.GestprojectSqlConnection,
            selectedIdList,
            tableName,
            fieldsToBeRetrieved,
            condition1Data
         );

         //new VisualizePropertiesAndValues<GestprojectSubaccountableAccountModel>(
         //   MethodBase.GetCurrentMethod().DeclaringType.Name + "." + MethodBase.GetCurrentMethod().Name,
         //   "GestprojectEntityList",
         //   GestprojectEntityList
         //);
      }

      public void StoreSage50EntityList
      (
         string sageDispactcherMechanismRoute,
         string tableName,
         List<(string, System.Type)> tableFieldsAlongTypes
      )
      {
         Sage50EntityList = new GetSage50SubaccountableAccounts(SynchronizationTableSchemaProvider).Entities;
      }
      
      public void StoreBreakDownGestprojectEntityListByStatus
      (
         List<GestprojectSubaccountableAccountModel> GestprojectEntityList, 
         List<Sage50SubaccountableAccountModel> Sage50EntityList
      )
      {
         //new VisualizePropertiesAndValues<GestprojectSubaccountableAccountModel>(
         //    "At: " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name,
         //    "GestprojectEntityList",
         //    GestprojectEntityList
         //);

         for(int i = 0; i < GestprojectEntityList.Count; i++)
         {
            var gestprojectEntity = GestprojectEntityList[i];
            bool found = false;

            for(global::System.Int32 j = 0; j < Sage50EntityList.Count; j++)
            {
               var sage50Entity = Sage50EntityList[j];
               if( gestprojectEntity.S50_GUID_ID == sage50Entity.GUID_ID && gestprojectEntity.S50_CODE != "" && gestprojectEntity.COS_ID != -1)
               {
                  ExistingGestprojectEntityList.Add(gestprojectEntity);
                  found = true;
                  break;
               };
            };

            //if(!found && gestprojectEntity.S50_CODE != "")
            if(!found && gestprojectEntity.COS_ID == -1)
            {
               UnexistingGestprojectEntityList.Add(gestprojectEntity);
            };

            //if(gestprojectEntity.SYNC_STATUS != "Sincronizado" && gestprojectEntity.S50_CODE != "")
            if(gestprojectEntity.SYNC_STATUS != "Sincronizado" && gestprojectEntity.COS_ID == -1)
            {
               UnsynchronizedGestprojectEntityList.Add(gestprojectEntity);
            };
         };
      }

      public void DetermineEntitySincronizationWorkflow
      (
         List<GestprojectSubaccountableAccountModel> UnexistingGestprojectEntityList, 
         List<GestprojectSubaccountableAccountModel> ExistingGestprojectEntityList, 
         List<GestprojectSubaccountableAccountModel> UnsynchronizedGestprojectEntityList,
         List<GestprojectSubaccountableAccountModel> GestprojectEntityList
      )
      {
         SomeEntitiesExistsInSage50 = ExistingGestprojectEntityList.Count > 0;
         AllEntitiesExistsInSage50 = ExistingGestprojectEntityList.Count == GestprojectEntityList.Count;
         NoEntitiesExistsInSage50 = ExistingGestprojectEntityList.Count == 0;
         UnsynchronizedEntityExists = UnsynchronizedGestprojectEntityList.Count > 0;
      }

      public void ExecuteSyncronizationWorkflow
      (
         bool SomeEntitiesExistsInSage50, 
         bool AllEntitiesExistsInSage50, 
         bool NoEntitiesExistsInSage50, 
         bool UnsynchronizedEntityExists, 
         IGestprojectConnectionManager gestprojectConnectionManager, 
         ISage50ConnectionManager sage50ConnectionManager, 
         ISynchronizationTableSchemaProvider tableSchema, 
         List<GestprojectSubaccountableAccountModel> unexistingGestprojectEntityList, 
         List<GestprojectSubaccountableAccountModel> existingGestprojectEntityList, 
         List<GestprojectSubaccountableAccountModel> unsynchronizedGestprojectEntityList,
         List<GestprojectSubaccountableAccountModel> gestprojectEntityList
      )
      {
         new SubaccountableAccountsSynchronizationWorkflow(
            gestprojectConnectionManager,
            sage50ConnectionManager.CompanyGroupData,
            tableSchema,
            gestprojectEntityList,
            unsynchronizedGestprojectEntityList,
            Sage50EntityList
         );

         /////////////////////////////////////////
         // Clear Lists to avoid repetition
         /////////////////////////////////////////

         gestprojectEntityList.Clear();
         unsynchronizedGestprojectEntityList.Clear();
         Sage50EntityList.Clear();
      }
   }
}
