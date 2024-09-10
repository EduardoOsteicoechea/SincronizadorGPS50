using SincronizadorGPS50.Workflows.Sage50Connection;
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
         try
         {
            new EntitySynchronizationTable<GestprojectTaxModel>().AppendTableDataToEntity
            (
               connection,
               tableSchema.TableName,
               tableSchema.SynchronizationFieldsTupleList,
               (tableSchema.Sage50GuidId.ColumnDatabaseName, entity.S50_GUID_ID),
               entity
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
      public void DetermineEntityWorkflow(SqlConnection connection, ISage50ConnectionManager sage50ConnectionManager, ISynchronizationTableSchemaProvider tableSchema, GestprojectTaxModel entity)
      {
         try
         {
            MustBeRegistered = !new WasEntityRegistered(
               connection,
               tableSchema.TableName,
               tableSchema.GestprojectId.ColumnDatabaseName,
               (tableSchema.Sage50GuidId.ColumnDatabaseName, entity.S50_GUID_ID)
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
      public void RegisterEntity(SqlConnection connection, ISynchronizationTableSchemaProvider tableSchema, GestprojectTaxModel entity)
      {
         try
         {
            new RegisterEntity
            (
               connection,
               tableSchema.TableName,
               new List<(string, dynamic)>()
               {
                  (tableSchema.SynchronizationStatus.ColumnDatabaseName, SynchronizationStatusOptions.Desincronizado),
                  (tableSchema.GestprojectId.ColumnDatabaseName, entity.IMP_ID),
                  (tableSchema.GestprojectType.ColumnDatabaseName, entity.IMP_TIPO),
                  (tableSchema.GestprojectName.ColumnDatabaseName, entity.IMP_NOMBRE),
                  (tableSchema.GestprojectValue.ColumnDatabaseName, entity.IMP_VALOR),
                  (tableSchema.AccountableSubaccount.ColumnDatabaseName, entity.IMP_SUBCTA_CONTABLE),
                  (tableSchema.AccountableSubaccount2.ColumnDatabaseName, entity.IMP_SUBCTA_CONTABLE_2)
               }
            );

            AppendSynchronizationTableDataToEntity(connection, tableSchema, entity);
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
      public void UpdateEntity(SqlConnection connection, ISynchronizationTableSchemaProvider tableSchema, GestprojectTaxModel entity)
      {
         try
         {
            new UpdateEntity
            (
               connection,
               tableSchema.TableName,
               new List<(string, dynamic)>()
               {
                  (tableSchema.SynchronizationStatus.ColumnDatabaseName, entity.SYNC_STATUS),
                  (tableSchema.GestprojectId.ColumnDatabaseName, entity.IMP_ID),
                  (tableSchema.GestprojectType.ColumnDatabaseName, entity.IMP_TIPO),
                  (tableSchema.GestprojectName.ColumnDatabaseName, entity.IMP_NOMBRE),
                  (tableSchema.GestprojectValue.ColumnDatabaseName, entity.IMP_VALOR),
                  (tableSchema.AccountableSubaccount.ColumnDatabaseName, entity.IMP_SUBCTA_CONTABLE),
                  (tableSchema.AccountableSubaccount2.ColumnDatabaseName, entity.IMP_SUBCTA_CONTABLE_2)
               },
               (tableSchema.GestprojectId.ColumnDatabaseName, entity.IMP_ID)
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
      public void ValidateEntitySynchronizationStatus(SqlConnection connection, ISynchronizationTableSchemaProvider tableSchema, List<Sage50TaxModel> sage50Entities, GestprojectTaxModel entity)
      {
         try
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
      public void DeleteEntity(SqlConnection connection, ISynchronizationTableSchemaProvider tableSchema, List<GestprojectTaxModel> gestprojectEntites, GestprojectTaxModel entity)
      {
         try
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

      public void ClearEntitySynchronizationData(GestprojectTaxModel entity, List<(string propertyName, dynamic defaultValue)> entityPropertiesValuesTupleList)
      {
         try
         {
            for(global::System.Int32 i = 0; i < entityPropertiesValuesTupleList.Count; i++)
            {
               typeof(GestprojectTaxModel).GetProperty(entityPropertiesValuesTupleList[i].propertyName).SetValue(entity, entityPropertiesValuesTupleList[i].defaultValue);
            };
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