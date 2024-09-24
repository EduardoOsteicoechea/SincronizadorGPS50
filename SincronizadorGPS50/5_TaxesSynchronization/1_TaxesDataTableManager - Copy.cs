//using Infragistics.Designers.SqlEditor;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Reflection;
//using System.Windows.Forms;

//namespace SincronizadorGPS50
//{
//   public class TaxesDataTableManager : IGridDataSourceGenerator<GestprojectTaxModel, Sage50TaxModel>
//   {
//      public List<GestprojectTaxModel> GestprojectEntities { get; set; }
//      public List<Sage50TaxModel> Sage50Entities { get; set; }
//      public List<GestprojectTaxModel> ProcessedGestprojectEntities { get; set; }
//      public DataTable DataTable { get; set; }

//      public System.Data.DataTable GenerateDataTable
//      (
//         IGestprojectConnectionManager gestprojectConnectionManager,
//         ISage50ConnectionManager sage50ConnectionManager,
//         ISynchronizationTableSchemaProvider tableSchemaProvider
//      )
//      {
//         try
//         {
//            //GestprojectEntities = new List<GestprojectTaxModel>();
//            //Sage50Entities = new List<Sage50TaxModel>();
//            //ProcessedGestprojectEntities = new List<GestprojectTaxModel>();

//            ManageSynchronizationTableStatus(gestprojectConnectionManager, tableSchemaProvider);
//            GetAndStoreGestprojectEntities(gestprojectConnectionManager, tableSchemaProvider);
//            GetAndStoreSage50Entities(tableSchemaProvider);
//            ProccessAndStoreGestprojectEntities(
//               gestprojectConnectionManager,
//               sage50ConnectionManager,
//               tableSchemaProvider,
//               GestprojectEntities,
//               Sage50Entities
//            );
//            CreateAndDefineDataSource(tableSchemaProvider);
//            PaintEntitiesOnDataSource(tableSchemaProvider, ProcessedGestprojectEntities, DataTable);
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
//         ISynchronizationTableSchemaProvider tableSchemaProvider
//      )
//      {
//         ISynchronizationDatabaseTableManager entitySyncronizationTableStatusManager = new EntitySyncronizationTableStatusManager();

//         bool tableExists = entitySyncronizationTableStatusManager.TableExists(
//               gestprojectConnectionManager.GestprojectSqlConnection,
//               tableSchemaProvider.TableName
//            );

//         if(tableExists == false)
//         {
//            entitySyncronizationTableStatusManager.CreateTable
//            (
//               gestprojectConnectionManager.GestprojectSqlConnection,
//               tableSchemaProvider
//            );
//         };
//      }

//      public void GetAndStoreGestprojectEntities
//      (
//         IGestprojectConnectionManager gestprojectConnectionManager,
//         ISynchronizationTableSchemaProvider tableSchemaProvider
//      )
//      {
//         //////////////////////////////////////////
//         /// In thes case of Taxes and Subaccountable Accounts, this process is quite different and hacky. Because our desing pattern requires the Gestproject entity to be processed and synchronized by comparing it to all of the Sage entities, the mentioned entities "Painting", "Registering" and "Synchronization" workflows incorporate some workarounds to proceed. This clarification is required because Both "Taxes" and "Subaccountable accounts" possess an inverse synchronization workflow. 
//         /// Ussualy, we only register and paint the Gestproject entities to process them agains all of the Sage related entities. But for Taxes and Subaccountable Accounts we need to extract, insert and update them into Gestproject. Therefore, to comply with the chosed design pattern, we need to obtain and group both the Gestproject and Sage entities, into the Gestproject entity bundle that will be compared one by one against the all of the Sage entities. As ot may be evident to the reader, we could modify our interfaces to accomodate this variations in the Taxes and Subaccountable Accounts, but we´re constrained by the first priority requirement to run with the development and incorporate the minimal required tasks to get the app running.
//         //////////////////////////////////////////

//         //////////////////////////////////////////
//         /// Gest Gestproject entities
//         //////////////////////////////////////////
         
//         GestprojectEntities = new List<GestprojectTaxModel> ();

//         GestprojectEntities = new GestprojectTaxesManager().GetEntities(
//            gestprojectConnectionManager.GestprojectSqlConnection,
//            "IMPUESTO_CONFIG",
//            tableSchemaProvider.GestprojectFieldsTupleList
//         );

//         if(GestprojectEntities.Count < 1)
//         {
//            GestprojectEntities = new GestprojectEntities<GestprojectTaxModel>().GetAll(
//               gestprojectConnectionManager.GestprojectSqlConnection,
//               tableSchemaProvider.TableName,
//               tableSchemaProvider.GestprojectFieldsTupleList
//            );

//            //////////////////////////////////////////
//            /// Be careful here
//            /// This makes sense because if we have no entities in Gestproject, that means that 
//            /// it's existing taxes were deleted, therefore, by definition, everything is 
//            /// Unsynchronized
//            //////////////////////////////////////////

//            foreach(var item in GestprojectEntities)
//            {
//               item.S50_CODE = "";
//               item.S50_GUID_ID = "";
//               item.S50_COMPANY_GROUP_NAME = "";
//               item.S50_COMPANY_GROUP_CODE = "";
//               item.S50_COMPANY_GROUP_MAIN_CODE = "";
//               item.S50_COMPANY_GROUP_GUID_ID = "";
//               item.COMMENTS = "";
//               item.GP_USU_ID = 0;
//            };
//         };

//         //////////////////////////////////////////
//         /// Gest Sage entities
//         //////////////////////////////////////////

//         List<Sage50TaxModel> sage50Entities = new GetSage50Taxes().Entities;

//         //////////////////////////////////////////
//         /// Discard the Sage entities that were already registered in Gestproject (because they were synchronized before)
//         //////////////////////////////////////////

//         var subaccountableAccountList = GestprojectEntities.Select(x=>x.IMP_SUBCTA_CONTABLE);
//         var subaccountableAccount2List = GestprojectEntities.Select(x=>x.IMP_SUBCTA_CONTABLE_2);
//         var taxNameList = GestprojectEntities.Select(x=>x.IMP_DESCRIPCION);

//         bool itemExists = true;
//         foreach(var item in sage50Entities)
//         {
//            //////////////////////////////////////////
//            /// If this values of the Sage entity exist in the Gestprojects registry, that means that it was likely registed in the past, therefore we'll discard it to avoid duplication. We'll avoid comparing tax values because they are variable on the Sage side that must follow current legislations.
//            //////////////////////////////////////////

//            if(item.IMP_TIPO == "IVA")
//            {
//               itemExists =
//               subaccountableAccountList.Contains(item.CTA_IV_REP)
//               &&
//               subaccountableAccount2List.Contains(item.CTA_IV_SOP)
//               &&
//               taxNameList.Contains(item.NOMBRE);
//            }
//            else
//            {
//               itemExists =
//               subaccountableAccountList.Contains(item.CTA_RE_REP)
//               &&
//               subaccountableAccount2List.Contains(item.CTA_RE_SOP)
//               &&
//               taxNameList.Contains(item.NOMBRE);
//            };

//            //////////////////////////////////////////
//            /// If the Sage entity doesn't exist, add it to the to be processed bundle by storing it as a Gestproject entity (this hack was explained at the beginning of this method), else, discard it. In this manner we'll avoid duplication at this point.
//            //////////////////////////////////////////
            
//            if(!itemExists)
//            {
//               MessageBox.Show(
//                  "At: TaxesDataTableManager.GetAndStoreGestprojectEntities\n\n" + 
//                  item.NOMBRE.Trim() + " Doesn't exists"
//               );
//               GestprojectTaxModel gestprojectTaxModel = new GestprojectTaxModel();

//               gestprojectTaxModel.IMP_ID = -1;
//               //gestprojectTaxModel.IMP_ID = null;
//               gestprojectTaxModel.IMP_TIPO = item.IMP_TIPO;
//               gestprojectTaxModel.IMP_DESCRIPCION = item.NOMBRE.Trim();

//               if(item.IMP_TIPO == "IVA")
//               {
//                  gestprojectTaxModel.IMP_NOMBRE = $"{item.IMP_TIPO} {item.IVA.ToString().Split(',')[0]}";
//                  gestprojectTaxModel.IMP_VALOR = Convert.ToDecimal(item.IVA);
//                  gestprojectTaxModel.IMP_SUBCTA_CONTABLE = item.CTA_IV_REP;
//                  gestprojectTaxModel.IMP_SUBCTA_CONTABLE_2 = item.CTA_IV_SOP;
//               }
//               else
//               {
//                  gestprojectTaxModel.IMP_NOMBRE = $"{item.IMP_TIPO} {item.RETENCION.ToString().Split(',')[0]}";
//                  gestprojectTaxModel.IMP_VALOR = item.RETENCION;
//                  gestprojectTaxModel.IMP_SUBCTA_CONTABLE = item.CTA_RE_REP;
//                  gestprojectTaxModel.IMP_SUBCTA_CONTABLE_2 = item.CTA_RE_SOP;
//               };

//               gestprojectTaxModel.S50_CODE = item.CODIGO;
//               gestprojectTaxModel.S50_GUID_ID = item.GUID_ID;

//               GestprojectEntities.Add(gestprojectTaxModel);
//            };
//         };

//         MessageBox.Show("At: TaxesDataTableManager.GetAndStoreGestprojectEntities\n\n"+ "GestprojectEntities.Count: " + GestprojectEntities.Count);

//         //new VisualizePropertiesAndValues<GestprojectTaxModel>("At: TaxesDataTableManager.GetAndStoreGestprojectEntities \n\n GestprojectEntities", GestprojectEntities);
//      }

//      public void GetAndStoreSage50Entities(ISynchronizationTableSchemaProvider tableSchemaProvider)
//      {
//         Sage50Entities = new GetSage50Taxes().Entities;
//         //new VisualizePropertiesAndValues<Sage50TaxModel>("Sage50Entities", Sage50Entities);
//      }

//      public void ProccessAndStoreGestprojectEntities
//      (
//         IGestprojectConnectionManager gestprojectConnectionManager,
//         ISage50ConnectionManager sage50ConnectionManager,
//         ISynchronizationTableSchemaProvider tableSchemaProvider,
//         List<GestprojectTaxModel> GestprojectEntities,
//         List<Sage50TaxModel> Sage50Entities
//      )
//      {

//         ISynchronizableEntityProcessor<GestprojectTaxModel, Sage50TaxModel> gestprojectProvidersProcessor = new GestprojectTaxesProcessor();
//         ProcessedGestprojectEntities = gestprojectProvidersProcessor.ProcessEntityList(
//            gestprojectConnectionManager.GestprojectSqlConnection,
//            sage50ConnectionManager,
//            tableSchemaProvider,
//            GestprojectEntities,
//            Sage50Entities
//         );
//      }

//      public void CreateAndDefineDataSource
//      (
//         ISynchronizationTableSchemaProvider tableSchemaProvider
//      )
//      {
//         IDataTableGenerator entityDataTableGenerator = new SyncrhonizationDataTableGenerator();
//         DataTable = entityDataTableGenerator.CreateDataTable(tableSchemaProvider.ColumnsTuplesList);
//      }

//      public void PaintEntitiesOnDataSource
//      (
//         ISynchronizationTableSchemaProvider tableSchemaProvider,
//         List<GestprojectTaxModel> ProcessedGestprojectEntities,
//         DataTable dataTable
//      )
//      {
//         ISynchronizableEntityPainter<GestprojectTaxModel> entityPainter = new EntityPainter<GestprojectTaxModel>();
//         entityPainter.PaintEntityListOnDataTable(
//            ProcessedGestprojectEntities,
//            dataTable,
//            tableSchemaProvider.ColumnsTuplesList
//         );
//      }
//   }
//}
