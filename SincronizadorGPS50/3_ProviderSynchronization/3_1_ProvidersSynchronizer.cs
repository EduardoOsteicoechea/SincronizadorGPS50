using SincronizadorGPS50.GestprojectDataManager;
using SincronizadorGPS50.Sage50Connector;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   public class ProvidersSynchronizer : IEntitySynchronizer<GestprojectProviderModel, Sage50ProviderModel>
   {
      public List<GestprojectProviderModel> GestprojectEntityList {get;set;}
      public List<Sage50ProviderModel> Sage50EntityList {get;set;}
      public List<GestprojectProviderModel> UnexistingGestprojectEntityList {get;set;}
      public List<GestprojectProviderModel> ExistingGestprojectEntityList {get;set;}
      public List<GestprojectProviderModel> UnsynchronizedGestprojectEntityList {get;set;}
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
               new List<(string, System.Type)>()
               {
                  (tableSchema.GestprojectProviderNameColumn.ColumnDatabaseName, tableSchema.GestprojectProviderNameColumn.ColumnValueType),
                  (tableSchema.GestprojectProviderCIFNIFColumn.ColumnDatabaseName, tableSchema.GestprojectProviderCIFNIFColumn.ColumnValueType),
                  (tableSchema.GestprojectProviderAddressColumn.ColumnDatabaseName, tableSchema.GestprojectProviderAddressColumn.ColumnValueType),
                  (tableSchema.GestprojectProviderPostalCodeColumn.ColumnDatabaseName, tableSchema.GestprojectProviderPostalCodeColumn.ColumnValueType),
                  (tableSchema.GestprojectProviderLocalityColumn.ColumnDatabaseName, tableSchema.GestprojectProviderLocalityColumn.ColumnValueType),
                  (tableSchema.GestprojectProviderProvinceColumn.ColumnDatabaseName, tableSchema.GestprojectProviderProvinceColumn.ColumnValueType),
                  (tableSchema.GestprojectProviderCountryColumn.ColumnDatabaseName, tableSchema.GestprojectProviderCountryColumn.ColumnValueType),
                  (tableSchema.SynchronizationStatusColumn.ColumnDatabaseName, tableSchema.SynchronizationStatusColumn.ColumnValueType),
                  (tableSchema.Sage50ProviderCompanyGroupNameColumn.ColumnDatabaseName, tableSchema.Sage50ProviderCompanyGroupNameColumn.ColumnValueType),
                  (tableSchema.Sage50ProviderCompanyGroupCodeColumn.ColumnDatabaseName, tableSchema.Sage50ProviderCompanyGroupCodeColumn.ColumnValueType),
                  (tableSchema.Sage50ProviderCompanyGroupMainCodeColumn.ColumnDatabaseName, tableSchema.Sage50ProviderCompanyGroupMainCodeColumn.ColumnValueType),
                  (tableSchema.Sage50ProviderCompanyGroupGuidIdColumn.ColumnDatabaseName, tableSchema.Sage50ProviderCompanyGroupGuidIdColumn.ColumnValueType),
                  (tableSchema.GestprojectProviderIdColumn.ColumnDatabaseName, tableSchema.GestprojectProviderIdColumn.ColumnValueType),
                  (tableSchema.Sage50ProviderCodeColumn.ColumnDatabaseName, tableSchema.Sage50ProviderCodeColumn.ColumnValueType),
                  (tableSchema.Sage50ProviderGuidIdColumn.ColumnDatabaseName, tableSchema.Sage50ProviderGuidIdColumn.ColumnValueType),
                  (tableSchema.CommentsColumn.ColumnDatabaseName, tableSchema.CommentsColumn.ColumnValueType)
               }, 
               (
                  tableSchema.GestprojectProviderIdColumn.ColumnDatabaseName,
                  string.Join(",", selectedIdList)
               )
            );

            StoreSage50EntityList
            (
               "proveed", 
               new List<(string, System.Type)>() 
               {
                  ("codigo", typeof(string)),
                  ("cif", typeof(string)),
                  ("nombre", typeof(string)),
                  ("direccion", typeof(string)),
                  ("codpost", typeof(string)),
                  ("poblacion", typeof(string)),
                  ("provincia", typeof(string)),
                  ("pais", typeof(string)),
                  ("guid_id", typeof(string))
               }
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
         GestprojectEntityList = new GestprojectEntities<GestprojectProviderModel>().GetAll(
            gestprojectConnectionManager.GestprojectSqlConnection,
            selectedIdList,
            tableName,
            fieldsToBeRetrieved,
            condition1Data
         );
      }

      public void StoreSage50EntityList
      (
         string tableName, 
         List<(string, System.Type)> fieldsToBeRetrieved
      )
      {
         Sage50EntityList = new Sage50Entities<Sage50ProviderModel>().GetAll(
            tableName,
            fieldsToBeRetrieved
         );
      }
      
      public void StoreBreakDownGestprojectEntityListByStatus
      (
         List<GestprojectProviderModel> GestprojectEntityList, 
         List<Sage50ProviderModel> Sage50EntityList
      )
      {
         for(int i = 0; i < GestprojectEntityList.Count; i++)
         {
            var gestprojectEntity = GestprojectEntityList[i];
            bool found = false;

            for(global::System.Int32 j = 0; j < Sage50EntityList.Count; j++)
            {
               var sage50Entity = Sage50EntityList[j];
               if(
                  gestprojectEntity.sage50_guid_id == sage50Entity.GUID_ID
               )
               {
               ExistingGestprojectEntityList.Add(gestprojectEntity);
                  found = true;
                  break;
               };
            };

            if(!found)
            {
            UnexistingGestprojectEntityList.Add(gestprojectEntity);
            };

            if(gestprojectEntity.synchronization_status != "Sincronizado" && gestprojectEntity.sage50_guid_id != "")
            {
            UnsynchronizedGestprojectEntityList.Add(gestprojectEntity);
            };
         };
      }

      public void DetermineEntitySincronizationWorkflow
      (
         List<GestprojectProviderModel> UnexistingGestprojectEntityList, 
         List<GestprojectProviderModel> ExistingGestprojectEntityList, 
         List<GestprojectProviderModel> UnsynchronizedGestprojectEntityList,
         List<GestprojectProviderModel> GestprojectEntityList
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
         IGestprojectConnectionManager GestprojectConnectionManager, 
         ISage50ConnectionManager Sage50ConnectionManager, 
         ISynchronizationTableSchemaProvider SynchronizationTableSchemaProvider, 
         List<GestprojectProviderModel> UnexistingGestprojectEntityList, 
         List<GestprojectProviderModel> ExistingGestprojectEntityList, 
         List<GestprojectProviderModel> UnsynchronizedGestprojectEntityList,
         List<GestprojectProviderModel> GestprojectEntityList
      )
      {
         var aa = "";
         foreach (var item in GestprojectEntityList)
         {
            aa += $"{item.fullName}\n";
         };
         MessageBox.Show(aa);

         if (NoEntitiesExistsInSage50)
         {
            //new UnexsistingProviderListWorkflow
            //(
            //   GestprojectDataHolder.GestprojectDatabaseConnection,
            //   nonExistingProvidersList,
            //   tableSchema
            //);
         }

         if(AllEntitiesExistsInSage50)
         {
            //new ExsistingProviderListWorkflow
            //(
            //   GestprojectDataHolder.GestprojectDatabaseConnection,
            //   gestProjectProviderList,
            //   unsynchronizedProviderList,
            //   unsynchronizedProvidersExists,
            //   tableSchema
            //);
         }

         if(SomeEntitiesExistsInSage50 && !AllEntitiesExistsInSage50)
         {
            if(UnsynchronizedEntityExists)
            {
               //new ExsistingProviderListWorkflow
               //(
               //   GestprojectDataHolder.GestprojectDatabaseConnection,
               //   existingProvidersList,
               //   unsynchronizedProviderList,
               //   unsynchronizedProvidersExists,
               //   tableSchema
               //);
            };

            //new UnexsistingProviderListWorkflow
            //(
            //   GestprojectDataHolder.GestprojectDatabaseConnection,
            //   nonExistingProvidersList,
            //   tableSchema
            //);
         };
      }
   }
}
