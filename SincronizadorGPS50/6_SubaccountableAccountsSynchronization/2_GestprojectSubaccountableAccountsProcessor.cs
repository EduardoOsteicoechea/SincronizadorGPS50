using SincronizadorGPS50.Workflows.Sage50Connection;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   public class GestprojectSubaccountableAccountsProcessor : ISynchronizableEntityProcessor<GestprojectSubaccountableAccountModel, Sage50SubaccountableAccountModel>
   {
      public List<GestprojectSubaccountableAccountModel> ProcessedEntities { get; set; }
      public bool MustBeRegistered { get; set; } = false;
      public bool MustBeSkipped { get; set; } = false;
      public bool MustBeUpdated { get; set; } = false;
      public bool MustBeDeleted { get; set; } = false;
      public bool NevesWasSynchronized { get; set; } = false;

      public List<GestprojectSubaccountableAccountModel> ProcessEntityList
      (
         System.Data.SqlClient.SqlConnection connection,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider tableSchema,
         List<GestprojectSubaccountableAccountModel> gestprojectEntites,
         List<Sage50SubaccountableAccountModel> sage50Entities
      )
      {
         try
         {
            ProcessedEntities = new List<GestprojectSubaccountableAccountModel>();

            for(int i = 0; i < gestprojectEntites.Count; i++)
            {
               GestprojectSubaccountableAccountModel entity = gestprojectEntites[i];

               AppendSynchronizationTableDataToEntity(connection, tableSchema, entity);

               DetermineEntityWorkflow(connection, sage50ConnectionManager, tableSchema, entity);

               if(MustBeSkipped)
               {
                  //MessageBox.Show(entity.COS_NOMBRE + " MustBeSkipped");
                  continue;
               }
               else if(MustBeRegistered)
               {
                  //MessageBox.Show(entity.COS_NOMBRE + " MustBeRegistered");
                  RegisterEntity(connection, tableSchema, entity);
               }
               else if(MustBeUpdated)
               {
                  //MessageBox.Show(entity.COS_NOMBRE + " MustBeUpdated");
                  UpdateEntity(connection, tableSchema, entity);
               };

               ValidateEntitySynchronizationStatus(connection, tableSchema, sage50Entities, entity);

               if(MustBeDeleted)
               {
                  //DeleteEntity(connection, tableSchema, gestprojectEntites, entity);
                  //RegisterEntity(connection, tableSchema, entity);
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


      public void AppendSynchronizationTableDataToEntity(SqlConnection connection, ISynchronizationTableSchemaProvider tableSchema, GestprojectSubaccountableAccountModel entity)
      {
         try
         {
            bool isASage50Entity = entity.COS_ID == -1;
            bool isAGestprojectEntity = !isASage50Entity;

            if (isAGestprojectEntity) 
            {
               new EntitySynchronizationTable<GestprojectSubaccountableAccountModel>().AppendTableDataToEntity
               (
                  connection,
                  tableSchema.TableName,
                  tableSchema.SynchronizationFieldsTupleList,
                  (tableSchema.Sage50GuidId.ColumnDatabaseName, entity.S50_GUID_ID),
                  entity,
                  (tableSchema.GestprojectId.ColumnDatabaseName, entity.COS_ID)
               );
            }
            else if(isASage50Entity)
            {
               new EntitySynchronizationTable<GestprojectSubaccountableAccountModel>().AppendTableDataToEntity
               (
                  connection,
                  tableSchema.TableName,
                  tableSchema.SynchronizationFieldsTupleList,
                  (tableSchema.GestprojectGroup.ColumnDatabaseName, entity.COS_GRUPO),
                  entity
                  //(tableSchema.Sage50GuidId.ColumnDatabaseName, entity.S50_GUID_ID)
               );
            };
         }
         catch(System.Exception exception)
         {
            //new VisualizePropertiesAndValues<GestprojectSubaccountableAccountModel>("Error with: " + entity.COS_NOMBRE,entity);
            throw ApplicationLogger.ReportError(
               MethodBase.GetCurrentMethod().DeclaringType.Namespace,
               MethodBase.GetCurrentMethod().DeclaringType.Name,
               MethodBase.GetCurrentMethod().Name,
               exception
            );
         };
      }

      public void DetermineEntityWorkflow(SqlConnection connection, ISage50ConnectionManager sage50ConnectionManager, ISynchronizationTableSchemaProvider tableSchema, GestprojectSubaccountableAccountModel entity)
      {
         try
         {
            //new VisualizePropertiesAndValues<GestprojectSubaccountableAccountModel>(entity.COS_NOMBRE, entity);

            MustBeRegistered = !new WasSubaccountableAccountRegistered(
               connection,
               tableSchema.TableName,
               tableSchema.Id.ColumnDatabaseName,
               (tableSchema.Id.ColumnDatabaseName, entity.ID),
               (tableSchema.GestprojectId.ColumnDatabaseName, entity.COS_ID)
            ).ItWas;

            bool registeredInDifferentCompanyGroup =
               entity.S50_COMPANY_GROUP_GUID_ID.Trim() != ""
               &&
               sage50ConnectionManager.CompanyGroupData.CompanyGuidId.Trim() != entity.S50_COMPANY_GROUP_GUID_ID.Trim();

            //MessageBox.Show(
            //"entity.S50_COMPANY_GROUP_GUID_ID: " + entity.S50_COMPANY_GROUP_GUID_ID + "\n\n" +
            //"sage50ConnectionManager.CompanyGroupData.CompanyGuidId: " + sage50ConnectionManager.CompanyGroupData.CompanyGuidId + "\n\n" +
            //"registeredInDifferentCompanyGroup: " + registeredInDifferentCompanyGroup
            //);

            MustBeSkipped = registeredInDifferentCompanyGroup;

            bool neverSynchronized = entity.S50_COMPANY_GROUP_GUID_ID == "";
            NevesWasSynchronized = neverSynchronized;

            bool synchronizedInThePast =
            entity.S50_COMPANY_GROUP_GUID_ID != ""
            &&
            sage50ConnectionManager.CompanyGroupData.CompanyGuidId == entity.S50_COMPANY_GROUP_GUID_ID;

            MustBeUpdated = neverSynchronized || synchronizedInThePast;

            //MessageBox.Show(
            //   "entity.COS_NOMBRE: " + entity.COS_NOMBRE + "\n" +
            //   "entity.ID: " + entity.ID + "\n" +
            //   "entity.COS_ID: " + entity.COS_ID + "\n" +
            //   "entity.S50_GUID_ID: " + entity.S50_GUID_ID + "\n\n" +
            //   "MustBeRegistered: " + MustBeRegistered + "\n" +
            //   "MustBeSkipped: " + MustBeSkipped + "\n" +
            //   "MustBeUpdated: " + MustBeUpdated + "\n"
            //);
         }
         catch(System.Exception exception)
         {
            new VisualizePropertiesAndValues<GestprojectSubaccountableAccountModel>(
                "At: " + System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name,
                "Error in entity",
                entity
            );

            throw ApplicationLogger.ReportError(
               MethodBase.GetCurrentMethod().DeclaringType.Namespace,
               MethodBase.GetCurrentMethod().DeclaringType.Name,
               MethodBase.GetCurrentMethod().Name,
               exception
            );
         };
      }
      public void RegisterEntity(SqlConnection connection, ISynchronizationTableSchemaProvider tableSchema, GestprojectSubaccountableAccountModel entity)
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
                  (tableSchema.GestprojectId.ColumnDatabaseName, entity.COS_ID),
                  (tableSchema.GestprojectCode.ColumnDatabaseName, entity.COS_CODIGO),
                  (tableSchema.GestprojectName.ColumnDatabaseName, entity.COS_NOMBRE),
                  (tableSchema.GestprojectGroup.ColumnDatabaseName, entity.COS_GRUPO),
                  (tableSchema.Sage50Code.ColumnDatabaseName, entity.S50_CODE ?? ""),
                  (tableSchema.Sage50GuidId.ColumnDatabaseName, entity.S50_GUID_ID ?? ""),
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
      public void UpdateEntity(SqlConnection connection, ISynchronizationTableSchemaProvider tableSchema, GestprojectSubaccountableAccountModel entity)
      {
         try
         {
            if(entity.COS_ID == -1)
            {
               new UpdateEntity
               (
                  connection,
                  tableSchema.TableName,
                  new List<(string, dynamic)>()
                  {
                     (tableSchema.SynchronizationStatus.ColumnDatabaseName, entity.SYNC_STATUS),
                     (tableSchema.GestprojectId.ColumnDatabaseName, entity.COS_ID),
                     (tableSchema.GestprojectCode.ColumnDatabaseName, entity.COS_CODIGO),
                     (tableSchema.GestprojectName.ColumnDatabaseName, entity.COS_NOMBRE),
                     (tableSchema.GestprojectGroup.ColumnDatabaseName, entity.COS_GRUPO),
                  },
                  (tableSchema.GestprojectGroup.ColumnDatabaseName, entity.COS_GRUPO)
               );
            }
            else
            {
               new UpdateEntity
               (
                  connection,
                  tableSchema.TableName,
                  new List<(string, dynamic)>()
                  {
                     (tableSchema.SynchronizationStatus.ColumnDatabaseName, entity.SYNC_STATUS),
                     (tableSchema.GestprojectId.ColumnDatabaseName, entity.COS_ID),
                     (tableSchema.GestprojectCode.ColumnDatabaseName, entity.COS_CODIGO),
                     (tableSchema.GestprojectName.ColumnDatabaseName, entity.COS_NOMBRE),
                     (tableSchema.GestprojectGroup.ColumnDatabaseName, entity.COS_GRUPO),
                  },
                  (tableSchema.GestprojectId.ColumnDatabaseName, entity.COS_ID),
                  (tableSchema.Sage50GuidId.ColumnDatabaseName, entity.S50_GUID_ID)
               );
            }
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
      public void ValidateEntitySynchronizationStatus(SqlConnection connection, ISynchronizationTableSchemaProvider tableSchema, List<Sage50SubaccountableAccountModel> sage50Entities, GestprojectSubaccountableAccountModel entity)
      {
         try
         {
            MustBeDeleted = new ValidateSubaccountableAccountSyncronizationStatus(
               entity,
               sage50Entities,
               tableSchema.GestprojectCode.ColumnDatabaseName,
               tableSchema.GestprojectName.ColumnDatabaseName,
               tableSchema.GestprojectGroup.ColumnDatabaseName,
               NevesWasSynchronized
            ).MustBeDeleted;
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

      public void DeleteEntity(SqlConnection connection, ISynchronizationTableSchemaProvider tableSchema, List<GestprojectSubaccountableAccountModel> gestprojectEntites, GestprojectSubaccountableAccountModel entity)
      {
         try
         {
            MessageBox.Show(
               "Deleting"
            );

            //new DeleteEntityFromSynchronizationTable(
            //   connection,
            //   tableSchema.TableName,
            //   (tableSchema.GestprojectId.ColumnDatabaseName, entity.COS_ID),
            //   (tableSchema.Sage50GuidId.ColumnDatabaseName, entity.S50_GUID_ID)
            //);

            //new ClearEntityDataInGestproject(
            //      connection,
            //      "IMPUESTO_CONFIG",
            //      new List<string>(){
            //      tableSchema.AccountableSubaccount.ColumnDatabaseName
            //   },
            //   (tableSchema.GestprojectId.ColumnDatabaseName, entity.COS_ID),
            //   (tableSchema.Sage50GuidId.ColumnDatabaseName, entity.S50_GUID_ID)
            //);

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

      public void ClearEntitySynchronizationData(GestprojectSubaccountableAccountModel entity, List<(string propertyName, dynamic defaultValue)> entityPropertiesValuesTupleList)
      {
         try
         {
            for(int i = 0; i < entityPropertiesValuesTupleList.Count; i++)
            {
               typeof(GestprojectSubaccountableAccountModel)
               .GetProperty(entityPropertiesValuesTupleList[i].propertyName)
               .SetValue(entity, entityPropertiesValuesTupleList[i].defaultValue);
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