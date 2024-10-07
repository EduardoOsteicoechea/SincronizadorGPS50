using SincronizadorGPS50.GestprojectDataManager;
using SincronizadorGPS50.Sage50Connector;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   public class TaxesSynchronizer : IEntitySynchronizer<GestprojectTaxModel, Sage50TaxModel>
   {
      public List<GestprojectTaxModel> GestprojectEntityList {get;set;} = new List<GestprojectTaxModel>();
      public List<Sage50TaxModel> Sage50EntityList {get;set;} = new List<Sage50TaxModel> { };
      public List<GestprojectTaxModel> UnexistingGestprojectEntityList {get;set;} = new List<GestprojectTaxModel>();
      public List<GestprojectTaxModel> ExistingGestprojectEntityList {get;set; } = new List<GestprojectTaxModel>();
      public List<GestprojectTaxModel> UnsynchronizedGestprojectEntityList {get;set; } = new List<GestprojectTaxModel>();
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

            GestprojectEntityList = new List<GestprojectTaxModel>();
            Sage50EntityList = new List<Sage50TaxModel> { };
            UnexistingGestprojectEntityList = new List<GestprojectTaxModel>();
            ExistingGestprojectEntityList = new List<GestprojectTaxModel>();
            UnsynchronizedGestprojectEntityList = new List<GestprojectTaxModel>();

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
         // Rewrite this command. It was modified, and now is loading the whole list
         GestprojectEntityList = new GestprojectEntities<GestprojectTaxModel>().GetAll(
            gestprojectConnectionManager.GestprojectSqlConnection,
            selectedIdList,
            tableName,
            fieldsToBeRetrieved,
            condition1Data
         );

         //new VisualizePropertiesAndValues<GestprojectTaxModel>(
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
         //Sage50EntityList = new Sage50Entities<Sage50TaxModel>().GetAll(
         //   sageDispactcherMechanismRoute,
         //   tableName,
         //   tableFieldsAlongTypes
         //);
         
         Sage50EntityList = new GetSage50Taxes().Entities;
         
         //MessageBox.Show("Sage50EntityList.Count: " + Sage50EntityList.Count);
      }
      
      public void StoreBreakDownGestprojectEntityListByStatus
      (
         List<GestprojectTaxModel> GestprojectEntityList, 
         List<Sage50TaxModel> Sage50EntityList
      )
      {
         foreach(var gestprojectEntity in GestprojectEntityList)
         //for(int i = 0; i < GestprojectEntityList.Count; i++)
         {
            bool existInSage50 = false;
            bool GestprojectEntityHasSageCodeValue = gestprojectEntity.S50_CODE != "";
            bool GestprojectEntityHasUnsynchronizedStatus = gestprojectEntity.SYNC_STATUS != "Sincronizado";

            foreach(var sageEntity in Sage50EntityList)
            {
               bool entitiesNamesMatch = gestprojectEntity.IMP_DESCRIPCION == sageEntity.NOMBRE;
               bool entitiesCodesMatch = gestprojectEntity.S50_CODE == sageEntity.CODIGO;
               bool entityHasCompanyGroupGuidIdValue = gestprojectEntity.S50_COMPANY_GROUP_GUID_ID != "";
               bool entitiesGuidIdsMatch = gestprojectEntity.S50_GUID_ID == sageEntity.GUID_ID;

               if( 
                  entitiesNamesMatch 
                  && 
                  entitiesCodesMatch 
                  && 
                  entitiesGuidIdsMatch 
                  && 
                  entityHasCompanyGroupGuidIdValue 
                  && 
                  GestprojectEntityHasSageCodeValue
               )
               {
                  ExistingGestprojectEntityList.Add(gestprojectEntity);
                  existInSage50 = true;
                  break;
               };               
            };

            if(existInSage50 == false)
            {
               UnexistingGestprojectEntityList.Add(gestprojectEntity);
            };

            if(GestprojectEntityHasUnsynchronizedStatus && GestprojectEntityHasSageCodeValue)
            {
               UnsynchronizedGestprojectEntityList.Add(gestprojectEntity);
            };
         };
      }

      public void DetermineEntitySincronizationWorkflow
      (
         List<GestprojectTaxModel> UnexistingGestprojectEntityList, 
         List<GestprojectTaxModel> ExistingGestprojectEntityList, 
         List<GestprojectTaxModel> UnsynchronizedGestprojectEntityList,
         List<GestprojectTaxModel> GestprojectEntityList
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
         List<GestprojectTaxModel> unexistingGestprojectEntityList, 
         List<GestprojectTaxModel> existingGestprojectEntityList, 
         List<GestprojectTaxModel> unsynchronizedGestprojectEntityList,
         List<GestprojectTaxModel> gestprojectEntityList
      )
      {
         //new VisualizePropertiesAndValues<GestprojectTaxModel>(
         //   MethodBase.GetCurrentMethod().DeclaringType.Name + "." + MethodBase.GetCurrentMethod().Name,
         //   "unexistingGestprojectEntityList",
         //   unexistingGestprojectEntityList
         //);
         //new VisualizePropertiesAndValues<GestprojectTaxModel>(
         //   MethodBase.GetCurrentMethod().DeclaringType.Name + "." + MethodBase.GetCurrentMethod().Name,
         //   "existingGestprojectEntityList",
         //   existingGestprojectEntityList
         //);
         //new VisualizePropertiesAndValues<GestprojectTaxModel>(
         //   MethodBase.GetCurrentMethod().DeclaringType.Name + "." + MethodBase.GetCurrentMethod().Name,
         //   "unsynchronizedGestprojectEntityList",
         //   unsynchronizedGestprojectEntityList
         //);
         //new VisualizePropertiesAndValues<GestprojectTaxModel>(
         //   MethodBase.GetCurrentMethod().DeclaringType.Name + "." + MethodBase.GetCurrentMethod().Name,
         //   "gestprojectEntityList",
         //   gestprojectEntityList
         //);

         new TaxSynchronizationWorkflow(
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
