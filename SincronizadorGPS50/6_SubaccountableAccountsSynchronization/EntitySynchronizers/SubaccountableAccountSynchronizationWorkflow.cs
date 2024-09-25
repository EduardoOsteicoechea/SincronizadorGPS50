using SincronizadorGPS50.Sage50Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal class SubaccountableAccountsSynchronizationWorkflow
   {
      public SubaccountableAccountsSynchronizationWorkflow
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         CompanyGroup Sage50CompanyGroupData,
         ISynchronizationTableSchemaProvider tableSchema,
         List<GestprojectSubaccountableAccountModel> SynchronizationTableEntities,
         List<GestprojectSubaccountableAccountModel> unsynchronizedEntityList,
         List<Sage50SubaccountableAccountModel> Sage50Entities
      )
      {
         try
         {
            string entityTypeName = "Cuentas Contable(s)";

            //MessageBox.Show("unsynchronizedEntityList.Count: " + unsynchronizedEntityList.Count);
            //MessageBox.Show("Sage50Entities.Count: " + Sage50Entities.Count);

            if(unsynchronizedEntityList.Count > 0)
            {
               DialogResult result = MessageBox.Show($"Partiendo de la selección encontramos {unsynchronizedEntityList.Count} {entityTypeName} desactualizados.\n\n¿Desea sincronizarlo(s)?", "Confirmación de actualización", MessageBoxButtons.OKCancel);

               if(result == DialogResult.OK)
               {
                  ////////////////////////////////////////
                  // For this entity, we'll always override the Gestproject data
                  // This will keep the Gestproject entity data updated in relation to Sage SubaccountableAccount data
                  ////////////////////////////////////////

                  List<int?> unsynchronizedEntityListIds = unsynchronizedEntityList.Select(entity => entity.COS_ID).ToList();

                  //new VisualizeData<Sage50SubaccountableAccountModel>( 
                  //   MethodBase.GetCurrentMethod().DeclaringType.Name + "." + MethodBase.GetCurrentMethod().Name , 
                  //   "Sage50Entities", 
                  //   Sage50Entities
                  //);

                  foreach(Sage50SubaccountableAccountModel Sage50Entity in Sage50Entities)
                  {
                     ////////////////////////////////////////
                     // Validate if Sage50 entity has been synchronized
                     ////////////////////////////////////////

                     List<GestprojectSubaccountableAccountModel> gestprojectSubaccountableAccountTableSubaccountableAccounts = new GetGestprojectSubaccountableAccountTableSubaccountableAccounts(
                           gestprojectConnectionManager.GestprojectSqlConnection,
                           tableSchema.GestprojectEntityTableName
                        ).Entities;

                     //new VisualizeData<GestprojectSubaccountableAccountModel>(
                     //   MethodBase.GetCurrentMethod().DeclaringType.Name + "." + MethodBase.GetCurrentMethod().Name,
                     //   "gestprojectSubaccountableAccountTableSubaccountableAccounts",
                     //   gestprojectSubaccountableAccountTableSubaccountableAccounts
                     //);

                     List<int?> gestprojectSubaccountableAccountTableSubaccountableAccountsIds = gestprojectSubaccountableAccountTableSubaccountableAccounts.Select(x => x.COS_ID).ToList();                     
                     
                     //new VisualizeData<int?>(
                     //   MethodBase.GetCurrentMethod().DeclaringType.Name + "." + MethodBase.GetCurrentMethod().Name,
                     //   "gestprojectSubaccountableAccountTableSubaccountableAccountsIds",
                     //   gestprojectSubaccountableAccountTableSubaccountableAccountsIds
                     //);

                     GestprojectSubaccountableAccountModel correspondingUnsynchronizedSynchronizationEntity = null;

                     correspondingUnsynchronizedSynchronizationEntity =
                        unsynchronizedEntityList
                        .FirstOrDefault(
                           entity => entity.COS_GRUPO == Sage50Entity.CODIGO
                           &&
                           gestprojectSubaccountableAccountTableSubaccountableAccountsIds.Contains(entity.COS_ID)
                           &&
                           entity.S50_COMPANY_GROUP_GUID_ID != ""
                        );

                     ////////////////////////////////////////
                     /// If never synchronized                     
                     ////////////////////////////////////////

                     if(correspondingUnsynchronizedSynchronizationEntity == null)
                     {
                        GestprojectSubaccountableAccountModel entity = new GestprojectSubaccountableAccountModel();
                        entity.COS_NOMBRE = Sage50Entity.NOMBRE;
                        entity.COS_CODIGO = Sage50Entity.CODIGO;
                        entity.S50_CODE = Sage50Entity.CODIGO;
                        entity.COS_GRUPO = Sage50Entity.CODIGO;
                        entity.S50_GUID_ID = Sage50Entity.GUID_ID;

                        StringBuilder GestprojectSubaccountableAccountsColumnsStringBuilder = new StringBuilder();
                        StringBuilder GestprojectSubaccountableAccountsValuesStringBuilder = new StringBuilder();
                        for(global::System.Int32 j = 0; j < tableSchema.GestprojectFieldsTupleList.Count; j++)
                        {
                           string currentColumnName = tableSchema.GestprojectFieldsTupleList[j].columnName;
                           Type currentColumnType = tableSchema.GestprojectFieldsTupleList[j].columnType;
                           dynamic value =
                              entity
                              .GetType()
                              .GetProperty(currentColumnName)
                              .GetValue(entity, null);

                           //if(currentColumnName != "COS_ID" && currentColumnName != "COS_GRUPO")
                           if(currentColumnName != "COS_ID")
                           {
                              GestprojectSubaccountableAccountsColumnsStringBuilder.Append($"{currentColumnName},");
                              GestprojectSubaccountableAccountsValuesStringBuilder.Append($"{DynamicValuesFormatters.Formatters[currentColumnType](value)},");
                           };
                        };

                        new InsertSageEntityIntoGestprojectSubaccountableAccountTable(
                           gestprojectConnectionManager.GestprojectSqlConnection,
                           tableSchema.GestprojectEntityTableName,
                           GestprojectSubaccountableAccountsColumnsStringBuilder.ToString().TrimEnd(','),
                           GestprojectSubaccountableAccountsValuesStringBuilder.ToString().TrimEnd(','),
                           entity
                        );

                        new AppendGestprojectSubaccountableAccountTableId(
                           gestprojectConnectionManager.GestprojectSqlConnection,
                           tableSchema,
                           tableSchema.GestprojectEntityTableName,
                           entity
                        );

                        new DeleteSubaccountableAccountTemporalRegistry(
                           gestprojectConnectionManager.GestprojectSqlConnection,
                           tableSchema.TableName,
                           entity
                        );

                        // Append It's synchronization Id
                        correspondingUnsynchronizedSynchronizationEntity = entity;
                     }
                     ////////////////////////////////////////
                     /// If previously Synchronized
                     ////////////////////////////////////////
                     else
                     {
                        //new VisualizePropertiesAndValues<GestprojectSubaccountableAccountModel>(
                        //   MethodBase.GetCurrentMethod().DeclaringType.Name + "." + MethodBase.GetCurrentMethod().Name,
                        //   correspondingUnsynchronizedSynchronizationEntity.COS_NOMBRE, 
                        //   correspondingUnsynchronizedSynchronizationEntity
                        //);

                        StringBuilder GestprojectSubaccountableAccountsColumnsAndValuesStringBuilder = new StringBuilder();
                        for(global::System.Int32 j = 0; j < tableSchema.GestprojectFieldsTupleList.Count; j++)
                        {
                           string currentColumnName = tableSchema.GestprojectFieldsTupleList[j].columnName;
                           dynamic value =
                              correspondingUnsynchronizedSynchronizationEntity
                              .GetType()
                              .GetProperty(currentColumnName)
                              .GetValue(correspondingUnsynchronizedSynchronizationEntity, null);

                           if(currentColumnName != "COS_ID")
                           {
                              GestprojectSubaccountableAccountsColumnsAndValuesStringBuilder.Append($"{currentColumnName}={DynamicValuesFormatters.Formatters[value.GetType()](value)},");
                           };
                        };

                        new UpdateGestprojectSubaccountableAccountDataWithSageSubaccountableAccountData(
                           gestprojectConnectionManager.GestprojectSqlConnection,
                           tableSchema.GestprojectEntityTableName,
                           GestprojectSubaccountableAccountsColumnsAndValuesStringBuilder.ToString().TrimEnd(','),
                           correspondingUnsynchronizedSynchronizationEntity
                        );
                     }

                     ////////////////////////////////////////
                     /// Complete Entity Synchonization Data appending process
                     ////////////////////////////////////////

                     StringBuilder SubaccountableAccountSynchronizationDataAppendingString = new StringBuilder();

                     //int? entitySynchronizationId = correspondingUnsynchronizedSynchronizationEntity.ID;
                     //SubaccountableAccountSynchronizationDataAppendingString.Append($"{tableSchema.Id.ColumnDatabaseName}={DynamicValuesFormatters.Formatters[entitySynchronizationId.GetType()](entitySynchronizationId)},");

                     int? currentSessionUserId = gestprojectConnectionManager.GestprojectUserRememberableData.GP_CNX_ID;
                     SubaccountableAccountSynchronizationDataAppendingString.Append($"{tableSchema.ParentUserId.ColumnDatabaseName}={DynamicValuesFormatters.Formatters[currentSessionUserId.GetType()](currentSessionUserId)},");

                     string entityValueGestprojectCode = Sage50Entity.CODIGO;
                     SubaccountableAccountSynchronizationDataAppendingString.Append($"{tableSchema.GestprojectCode.ColumnDatabaseName}={DynamicValuesFormatters.Formatters[entityValueGestprojectCode.GetType()](entityValueGestprojectCode)},");

                     string entityValueSage50Code = Sage50Entity.CODIGO;
                     SubaccountableAccountSynchronizationDataAppendingString.Append($"{tableSchema.Sage50Code.ColumnDatabaseName}={DynamicValuesFormatters.Formatters[entityValueSage50Code.GetType()](entityValueSage50Code)},");

                     string entityValueGuidId = Sage50Entity.GUID_ID;
                     SubaccountableAccountSynchronizationDataAppendingString.Append($"{tableSchema.Sage50GuidId.ColumnDatabaseName}={DynamicValuesFormatters.Formatters[entityValueGuidId.GetType()](entityValueGuidId)},");

                     string companyGroupName = Sage50CompanyGroupData.CompanyName;
                     SubaccountableAccountSynchronizationDataAppendingString.Append($"{tableSchema.CompanyGroupName.ColumnDatabaseName}={DynamicValuesFormatters.Formatters[companyGroupName.GetType()](companyGroupName)},");

                     string companyGroupCode = Sage50CompanyGroupData.CompanyCode;
                     SubaccountableAccountSynchronizationDataAppendingString.Append($"{tableSchema.CompanyGroupCode.ColumnDatabaseName}={DynamicValuesFormatters.Formatters[companyGroupName.GetType()](companyGroupCode)},");

                     string companyGroupMainCode = Sage50CompanyGroupData.CompanyMainCode;
                     SubaccountableAccountSynchronizationDataAppendingString.Append($"{tableSchema.CompanyGroupMainCode.ColumnDatabaseName}={DynamicValuesFormatters.Formatters[companyGroupName.GetType()](companyGroupMainCode)},");

                     string companyGroupGuidId = Sage50CompanyGroupData.CompanyGuidId;
                     SubaccountableAccountSynchronizationDataAppendingString.Append($"{tableSchema.CompanyGroupGuidId.ColumnDatabaseName}={DynamicValuesFormatters.Formatters[companyGroupName.GetType()](companyGroupGuidId)},");

                     //MessageBox.Show($@"At: SubaccountableAccountsSynchronizationWorkflow
                     //   {SubaccountableAccountSynchronizationDataAppendingString.ToString()}
                     //");

                     ////////////////////////////////////////
                     /// This last step is required to relate the current worked entity to it's synchronization table registry
                     ////////////////////////////////////////

                     correspondingUnsynchronizedSynchronizationEntity.S50_GUID_ID = Sage50Entity.GUID_ID;

                     new AppendSubaccountableAccountSynchronizationData(
                        gestprojectConnectionManager.GestprojectSqlConnection,
                        tableSchema,
                        correspondingUnsynchronizedSynchronizationEntity,
                        SubaccountableAccountSynchronizationDataAppendingString.ToString().TrimEnd(',')
                     );
                  };
               };
            }
            else
            {
               MessageBox.Show($"Los {SynchronizationTableEntities.Count - (SynchronizationTableEntities.Count - Sage50Entities.Count)} {entityTypeName} están sincronizados.");
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
