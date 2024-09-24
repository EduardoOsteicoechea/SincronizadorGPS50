//using SincronizadorGPS50.Sage50Connector;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Windows.Forms;

//namespace SincronizadorGPS50
//{
//   internal class SubaccountableAccountsSynchronizationWorkflow
//   {
//      public SubaccountableAccountsSynchronizationWorkflow
//      (
//         IGestprojectConnectionManager gestprojectConnectionManager,
//         CompanyGroup Sage50CompanyGroupData,
//         ISynchronizationTableSchemaProvider tableSchema,
//         List<GestprojectSubaccountableAccountModel> SynchronizationTableEntities,
//         List<GestprojectSubaccountableAccountModel> unsynchronizedEntityList,
//         List<Sage50SubaccountableAccountModel> Sage50Entities
//      )
//      {
//         try
//         {
//            string entityTypeName = "impuesto(s)";

//            //MessageBox.Show("unsynchronizedEntityList.Count: " + unsynchronizedEntityList.Count);
//            //MessageBox.Show("Sage50Entities.Count: " + Sage50Entities.Count);

//            if(unsynchronizedEntityList.Count > 0)
//            {
//               DialogResult result = MessageBox.Show($"Partiendo de la selección encontramos {unsynchronizedEntityList.Count} {entityTypeName} desactualizados.\n\n¿Desea sincronizarlo(s)?", "Confirmación de actualización", MessageBoxButtons.OKCancel);

//               if(result == DialogResult.OK)
//               {
//                  ////////////////////////////////////////
//                  // For this entity, we'll always override the Gestproject data
//                  // This will keep the Gestproject tax data updated in relation to Sage SubaccountableAccount data
//                  ////////////////////////////////////////
                  
//                  List<int?> unsynchronizedEntityListIds = unsynchronizedEntityList.Select(entity => entity.IMP_ID).ToList();

//                  //new DataVisualizer<int?>("gestprojectSubaccountableAccountTableSubaccountableAccountesIds", unsynchronizedEntityListIds);

//                  foreach (Sage50SubaccountableAccountModel Sage50Entity in Sage50Entities)
//                  {
//                     ////////////////////////////////////////
//                     // Validate if Sage50 entity has been synchronized
//                     ////////////////////////////////////////
                     
//                     List<GestprojectSubaccountableAccountModel> gestprojectSubaccountableAccountTableSubaccountableAccountes = new GetGestprojectSubaccountableAccountTableSubaccountableAccountes(
//                           gestprojectConnectionManager.GestprojectSqlConnection,
//                           "IMPUESTO_CONFIG"
//                        ).Entities;
//                     List<int?> gestprojectSubaccountableAccountTableSubaccountableAccountesIds = gestprojectSubaccountableAccountTableSubaccountableAccountes.Select(x => x.IMP_ID).ToList();

//                     //MessageBox.Show("gestprojectSubaccountableAccountTableSubaccountableAccountes: " + gestprojectSubaccountableAccountTableSubaccountableAccountes.Count);

//                     GestprojectSubaccountableAccountModel correspondingUnsynchronizedSynchronizationEntity = null;
//                     if(Sage50Entity.NOMBRE.Contains("IVA"))
//                     {
//                        correspondingUnsynchronizedSynchronizationEntity = 
//                           unsynchronizedEntityList
//                           .FirstOrDefault(
//                              entity => entity.IMP_SUBCTA_CONTABLE == Sage50Entity.CTA_IV_REP 
//                              && 
//                              gestprojectSubaccountableAccountTableSubaccountableAccountesIds.Contains(entity.IMP_ID)
//                              && 
//                              entity.S50_COMPANY_GROUP_GUID_ID != ""
//                           );
                           
//                        //MessageBox.Show("Analizing IVA");
//                     }
//                     else
//                     {
//                        //MessageBox.Show("Sincronizing");

//                        correspondingUnsynchronizedSynchronizationEntity = 
//                           unsynchronizedEntityList
//                           .FirstOrDefault(
//                              entity => entity.IMP_SUBCTA_CONTABLE == Sage50Entity.CTA_RE_REP 
//                              && 
//                              gestprojectSubaccountableAccountTableSubaccountableAccountesIds.Contains(entity.IMP_ID)
//                              && 
//                              entity.S50_COMPANY_GROUP_GUID_ID != ""
//                           );
                           
//                        //MessageBox.Show("Analizing IRPF");
//                     };

//                     //new VisualizePropertiesAndValues<GestprojectSubaccountableAccountModel>(correspondingUnsynchronizedSynchronizationEntity.IMP_DESCRIPCION, correspondingUnsynchronizedSynchronizationEntity);
                     
//                     ////////////////////////////////////////
//                     /// If never synchronized                     
//                     ////////////////////////////////////////
                     
//                     if( correspondingUnsynchronizedSynchronizationEntity == null )
//                     //if(correspondingUnsynchronizedSynchronizationEntity != null)
//                     {
//                        //MessageBox.Show("Sincronizing");

//                        GestprojectSubaccountableAccountModel entity = new GestprojectSubaccountableAccountModel();
//                        entity.IMP_TIPO = Sage50Entity.IMP_TIPO;                           
//                        if(Sage50Entity.IMP_TIPO == "IVA")
//                        {
//                           entity.IMP_NOMBRE = $"{Sage50Entity.IMP_TIPO} {Sage50Entity.IVA.ToString().Split(',')[0]}";
//                           entity.IMP_VALOR = Convert.ToDecimal(Sage50Entity.IVA);
//                           entity.IMP_SUBCTA_CONTABLE = Sage50Entity.CTA_IV_REP;
//                           entity.IMP_SUBCTA_CONTABLE_2 = Sage50Entity.CTA_IV_SOP;
//                        }
//                        else
//                        {
//                           entity.IMP_NOMBRE = $"{Sage50Entity.IMP_TIPO} {Sage50Entity.RETENCION.ToString().Split(',')[0]}";
//                           entity.IMP_VALOR = Sage50Entity.RETENCION;
//                           entity.IMP_SUBCTA_CONTABLE = Sage50Entity.CTA_RE_REP;
//                           entity.IMP_SUBCTA_CONTABLE_2 = Sage50Entity.CTA_RE_SOP;
//                        };
//                        entity.S50_CODE = Sage50Entity.CODIGO;
//                        entity.S50_GUID_ID = Sage50Entity.GUID_ID;
//                        entity.IMP_DESCRIPCION = Sage50Entity.NOMBRE;
                     
//                        StringBuilder GestprojectSubaccountableAccountesColumnsStringBuilder = new StringBuilder();
//                        StringBuilder GestprojectSubaccountableAccountesValuesStringBuilder = new StringBuilder();
//                        for(global::System.Int32 j = 0; j < tableSchema.GestprojectFieldsTupleList.Count; j++)
//                        {
//                           string currentColumnName = tableSchema.GestprojectFieldsTupleList[j].columnName;
//                           Type currentColumnType = tableSchema.GestprojectFieldsTupleList[j].columnType;
//                           dynamic value = 
//                              entity
//                              .GetType()
//                              .GetProperty(currentColumnName)
//                              .GetValue(entity, null);

//                           if(currentColumnName != "IMP_ID")
//                           {
//                              GestprojectSubaccountableAccountesColumnsStringBuilder.Append($"{currentColumnName},");
//                              GestprojectSubaccountableAccountesValuesStringBuilder.Append($"{DynamicValuesFormatters.Formatters[currentColumnType](value)},");
//                           };
//                        };

//                        new InsertSageEntityIntoGestprojectSubaccountableAccountTable(
//                           gestprojectConnectionManager.GestprojectSqlConnection,
//                           "IMPUESTO_CONFIG",
//                           GestprojectSubaccountableAccountesColumnsStringBuilder.ToString().TrimEnd(','),
//                           GestprojectSubaccountableAccountesValuesStringBuilder.ToString().TrimEnd(','),
//                           entity
//                        );

//                        //new AppendSubaccountableAccountSynchronizationData(
//                        //   gestprojectConnectionManager.GestprojectSqlConnection,
//                        //   "IMPUESTO_CONFIG",
//                        //   entity,
//                        //   tableSchema
//                        //);

//                        correspondingUnsynchronizedSynchronizationEntity = entity;
//                     }                     
//                     ////////////////////////////////////////
//                     /// If previously Synchronized
//                     ////////////////////////////////////////
//                     else
//                     {
//                        //MessageBox.Show("Updating");

//                        StringBuilder GestprojectSubaccountableAccountesColumnsAndValuesStringBuilder = new StringBuilder();
//                        for(global::System.Int32 j = 0; j < tableSchema.GestprojectFieldsTupleList.Count; j++)
//                        {
//                           string currentColumnName = tableSchema.GestprojectFieldsTupleList[j].columnName;
//                           dynamic value = 
//                              correspondingUnsynchronizedSynchronizationEntity
//                              .GetType()
//                              .GetProperty(currentColumnName)
//                              .GetValue(correspondingUnsynchronizedSynchronizationEntity, null);

//                           if(currentColumnName != "IMP_ID")
//                           {
//                              GestprojectSubaccountableAccountesColumnsAndValuesStringBuilder.Append($"{currentColumnName}={DynamicValuesFormatters.Formatters[value.GetType()](value)},");
//                           };
//                        };

//                        new UpdateGestprojectSubaccountableAccountDataWithSageSubaccountableAccountData(
//                           gestprojectConnectionManager.GestprojectSqlConnection,
//                           "IMPUESTO_CONFIG",
//                           GestprojectSubaccountableAccountesColumnsAndValuesStringBuilder.ToString().TrimEnd(','),
//                           correspondingUnsynchronizedSynchronizationEntity
//                        );
//                     }

//                     ////////////////////////////////////////
//                     /// Complete Entity Synchonization Data appending process
//                     ////////////////////////////////////////
                     
//                     StringBuilder SubaccountableAccountSynchronizationDataAppendingString = new StringBuilder();

//                     int? currentSessionUserId = gestprojectConnectionManager.GestprojectUserRememberableData.GP_CNX_ID;
//                     SubaccountableAccountSynchronizationDataAppendingString.Append($"{tableSchema.ParentUserId.ColumnDatabaseName}={DynamicValuesFormatters.Formatters[currentSessionUserId.GetType()](currentSessionUserId)},");
                        
//                     string taxCode = Sage50Entity.CODIGO;
//                     SubaccountableAccountSynchronizationDataAppendingString.Append($"{tableSchema.Sage50Code.ColumnDatabaseName}={DynamicValuesFormatters.Formatters[taxCode.GetType()](taxCode)},");

//                     string taxGuidId = Sage50Entity.GUID_ID;
//                     SubaccountableAccountSynchronizationDataAppendingString.Append($"{tableSchema.Sage50GuidId.ColumnDatabaseName}={DynamicValuesFormatters.Formatters[taxGuidId.GetType()](taxGuidId)},");
                        
//                     string companyGroupName = Sage50CompanyGroupData.CompanyName;
//                     SubaccountableAccountSynchronizationDataAppendingString.Append($"{tableSchema.CompanyGroupName.ColumnDatabaseName}={DynamicValuesFormatters.Formatters[companyGroupName.GetType()](companyGroupName)},");
                        
//                     string companyGroupCode = Sage50CompanyGroupData.CompanyCode;
//                     SubaccountableAccountSynchronizationDataAppendingString.Append($"{tableSchema.CompanyGroupCode.ColumnDatabaseName}={DynamicValuesFormatters.Formatters[companyGroupName.GetType()](companyGroupCode)},");
                        
//                     string companyGroupMainCode = Sage50CompanyGroupData.CompanyMainCode;
//                     SubaccountableAccountSynchronizationDataAppendingString.Append($"{tableSchema.CompanyGroupMainCode.ColumnDatabaseName}={DynamicValuesFormatters.Formatters[companyGroupName.GetType()](companyGroupMainCode)},");
                        
//                     string companyGroupGuidId = Sage50CompanyGroupData.CompanyGuidId;
//                     SubaccountableAccountSynchronizationDataAppendingString.Append($"{tableSchema.CompanyGroupGuidId.ColumnDatabaseName}={DynamicValuesFormatters.Formatters[companyGroupName.GetType()](companyGroupGuidId)},");

//                     ////////////////////////////////////////
//                     /// This last step is required to relate the current worked entity to it's synchronization table registry
//                     ////////////////////////////////////////

//                     correspondingUnsynchronizedSynchronizationEntity.S50_GUID_ID = Sage50Entity.GUID_ID;

//                     new AppendSubaccountableAccountSynchronizationData(
//                        gestprojectConnectionManager.GestprojectSqlConnection,
//                        tableSchema,
//                        correspondingUnsynchronizedSynchronizationEntity,
//                        SubaccountableAccountSynchronizationDataAppendingString.ToString().TrimEnd(',')
//                     );
//                  };
//               };
//            }
//            else
//            {
//               MessageBox.Show($"Los {SynchronizationTableEntities.Count - (SynchronizationTableEntities.Count - Sage50Entities.Count)} {entityTypeName} están sincronizados.");
//            };
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
//   }
//}
