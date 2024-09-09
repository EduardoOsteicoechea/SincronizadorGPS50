using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   public class GestprojectTaxesProcessor : ISynchronizableEntityProcessor<GestprojectTaxModel, Sage50TaxModel>
   {
      public List<GestprojectTaxModel> ProcessedEntities { get; set; }
      public bool MustBeRegistered { get; set; } = false;
      public bool MustBeSkipped { get; set; } = false;
      public bool MustBeUpdated { get; set; } = false;
      public bool MustBeDeleted { get; set; } = false;

      public List<GestprojectTaxModel> ProcessEntityList
      (
         System.Data.SqlClient.SqlConnection connection,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider tableSchema,
         List<GestprojectTaxModel> gestprojectEntites,
         List<Sage50TaxModel> sage50Entities
      )
      {
         try
         {
            ProcessedEntities = new List<GestprojectTaxModel>();

            for(int i = 0; i < gestprojectEntites.Count; i++)
            {
               GestprojectTaxModel entity = gestprojectEntites[i];

               AppendSynchronizationTableDataToEntity(connection, tableSchema, entity);
               DetermineEntityWorkflow(connection, sage50ConnectionManager, tableSchema, entity);

               if(MustBeSkipped)
               {
                  continue;
               }
               else if(MustBeRegistered)
               {
                  RegisterEntity(connection, tableSchema, entity);
               }
               else if(MustBeUpdated)
               {
                  UpdateEntity(connection, tableSchema, entity);
               };

               ValidateEntitySynchronizationStatus(connection, tableSchema, sage50Entities, entity);

               if(MustBeDeleted)
               {
                  DeleteEntity(connection, tableSchema, gestprojectEntites, entity);
                  RegisterEntity(connection, tableSchema, entity);
               };


               UpdateEntity(connection, tableSchema, entity);

               ProcessedEntities.Add(entity);
            };

            return ProcessedEntities;
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


      public void AppendSynchronizationTableDataToEntity(SqlConnection connection, ISynchronizationTableSchemaProvider tableSchema, GestprojectTaxModel entity)
      {
         new EntitySynchronizationTable<GestprojectTaxModel>().AppendTableDataToEntity
         (
            connection,
            tableSchema.TableName,
            new List<(string, System.Type)>()
            {
               (tableSchema.Id.ColumnDatabaseName, tableSchema.Id.ColumnValueType),
               (tableSchema.SynchronizationStatus.ColumnDatabaseName, tableSchema.SynchronizationStatus.ColumnValueType),
               (tableSchema.Sage50Code.ColumnDatabaseName, tableSchema.Sage50Code.ColumnValueType),
               (tableSchema.Sage50GuidId.ColumnDatabaseName, tableSchema.Sage50GuidId.ColumnValueType),
               (tableSchema.CompanyGroupName.ColumnDatabaseName, tableSchema.CompanyGroupName.ColumnValueType),
               (tableSchema.CompanyGroupCode.ColumnDatabaseName, tableSchema.CompanyGroupCode.ColumnValueType),
               (tableSchema.CompanyGroupMainCode.ColumnDatabaseName, tableSchema.CompanyGroupMainCode.ColumnValueType),
               (tableSchema.CompanyGroupGuidId.ColumnDatabaseName, tableSchema.CompanyGroupGuidId.ColumnValueType),
               (tableSchema.LastUpdate.ColumnDatabaseName, tableSchema.LastUpdate.ColumnValueType),
               (tableSchema.ParentUserId.ColumnDatabaseName, tableSchema.ParentUserId.ColumnValueType),
               (tableSchema.Comments.ColumnDatabaseName, tableSchema.Comments.ColumnValueType),
            },
            (tableSchema.GestprojectId.ColumnDatabaseName,entity.IMP_ID),
            entity
         );
      }
      public void DetermineEntityWorkflow(SqlConnection connection, ISage50ConnectionManager sage50ConnectionManager, ISynchronizationTableSchemaProvider tableSchema, GestprojectTaxModel entity)
      {
         MustBeRegistered = !new WasEntityRegistered(
            connection,
            tableSchema.TableName,
            tableSchema.GestprojectId.ColumnDatabaseName,
            (tableSchema.GestprojectId.ColumnDatabaseName, entity.IMP_ID)
         ).ItIs;

         bool registeredInDifferentCompanyGroup =
         entity.S50_COMPANY_GROUP_GUID_ID != ""
         &&
         sage50ConnectionManager.CompanyGroupData.CompanyGuidId != entity.S50_COMPANY_GROUP_GUID_ID;

         MustBeSkipped = registeredInDifferentCompanyGroup;

         bool neverSynchronized = entity.S50_COMPANY_GROUP_GUID_ID == "";

         bool synchronizedInThePast =
         entity.S50_COMPANY_GROUP_GUID_ID != ""
         &&
         sage50ConnectionManager.CompanyGroupData.CompanyGuidId == entity.S50_COMPANY_GROUP_GUID_ID;

         MustBeUpdated = neverSynchronized || synchronizedInThePast;
      }
      public void RegisterEntity(SqlConnection connection, ISynchronizationTableSchemaProvider tableSchema, GestprojectTaxModel entity)
      {
         new RegisterEntity
         (
            connection,
            tableSchema.TableName,
            new List<(string, dynamic)>(){
               (tableSchema.SynchronizationStatus.ColumnDatabaseName, SynchronizationStatusOptions.Desincronizado),
               (tableSchema.GestprojectId.ColumnDatabaseName, entity.IMP_ID),
               (tableSchema.Name.ColumnDatabaseName, entity.IMP_TIPO),
               (tableSchema.Address.ColumnDatabaseName, entity.IMP_NOMBRE),
               (tableSchema.PostalCode.ColumnDatabaseName, entity.IMP_VALOR),
               (tableSchema.Locality.ColumnDatabaseName, entity.IMP_SUBCTA_CONTABLE),
               (tableSchema.Province.ColumnDatabaseName, entity.IMP_SUBCTA_CONTABLE_2)
            }
         );

         AppendSynchronizationTableDataToEntity(connection, tableSchema, entity);
      }
      public void UpdateEntity(SqlConnection connection, ISynchronizationTableSchemaProvider tableSchema, GestprojectTaxModel entity)
      {
         new UpdateEntity
         (
            connection,
            tableSchema.TableName,
            new List<(string, dynamic)>(){
               (tableSchema.SynchronizationStatus.ColumnDatabaseName, entity.SYNC_STATUS),
               (tableSchema.GestprojectId.ColumnDatabaseName, entity.IMP_ID),
               (tableSchema.Name.ColumnDatabaseName, entity.IMP_TIPO),
               (tableSchema.Address.ColumnDatabaseName, entity.IMP_NOMBRE),
               (tableSchema.PostalCode.ColumnDatabaseName, entity.IMP_VALOR),
               (tableSchema.Locality.ColumnDatabaseName, entity.IMP_SUBCTA_CONTABLE),
               (tableSchema.Province.ColumnDatabaseName, entity.IMP_SUBCTA_CONTABLE_2)
            },
            (tableSchema.GestprojectId.ColumnDatabaseName, entity.IMP_ID)
         );
      }
      public void ValidateEntitySynchronizationStatus(SqlConnection connection, ISynchronizationTableSchemaProvider tableSchema, List<Sage50TaxModel> sage50Entities, GestprojectTaxModel entity)
      {

         ValidateTaxSyncronizationStatus ProviderSyncronizationStatusValidator = new ValidateTaxSyncronizationStatus(
            entity,
            sage50Entities,
            tableSchema.Name.ColumnDatabaseName,
            tableSchema.PostalCode.ColumnDatabaseName,
            tableSchema.Address.ColumnDatabaseName,
            tableSchema.Locality.ColumnDatabaseName,
            tableSchema.Province.ColumnDatabaseName
         );

         MustBeDeleted = ProviderSyncronizationStatusValidator.MustBeDeleted;
      }
      public void DeleteEntity(SqlConnection connection, ISynchronizationTableSchemaProvider tableSchema, List<GestprojectTaxModel> gestprojectEntites, GestprojectTaxModel entity)
      {
         new DeleteEntityFromSynchronizationTable(
            connection,
            tableSchema.TableName,
            (tableSchema.GestprojectId.ColumnDatabaseName, entity.IMP_ID),
            (tableSchema.Sage50GuidId.ColumnDatabaseName, entity.S50_GUID_ID)
         );

         new ClearEntityDataInGestproject(
            connection,
            "PROYECTO",
            new List<string>(){
               tableSchema.AccountableSubaccount.ColumnDatabaseName
            },
            (tableSchema.GestprojectId.ColumnDatabaseName, entity.IMP_ID)
         );

         ClearEntitySynchronizationData(entity, tableSchema.SynchronizationFieldsDefaultValuesTupleList);
      }

      public void ClearEntitySynchronizationData(GestprojectTaxModel entity, List<(string propertyName, dynamic defaultValue)> entityPropertiesValuesTupleList)
      {
         for(global::System.Int32 i = 0; i < entityPropertiesValuesTupleList.Count; i++)
         {
            typeof(GestprojectTaxModel).GetProperty(entityPropertiesValuesTupleList[i].propertyName).SetValue(entity, entityPropertiesValuesTupleList[i].defaultValue);
         };
      }
   }
}