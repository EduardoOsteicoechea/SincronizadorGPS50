using Infragistics.Designers.SqlEditor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using static sage.ew.docsven.FirmaElectronica;

namespace SincronizadorGPS50
{
   public class TaxesDataTableManager : IGridDataSourceGenerator<GestprojectTaxModel, Sage50TaxModel>
   {
      public List<GestprojectTaxModel> GestprojectEntities { get; set; }
      public List<Sage50TaxModel> Sage50Entities { get; set; }
      public List<GestprojectTaxModel> ProcessedGestprojectEntities { get; set; }
      public DataTable DataTable { get; set; }

      public System.Data.DataTable GenerateDataTable
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider tableSchemaProvider
      )
      {
         try
         {
            ManageSynchronizationTableStatus(gestprojectConnectionManager, tableSchemaProvider);
            GetAndStoreGestprojectEntities(gestprojectConnectionManager, tableSchemaProvider);
            GetAndStoreSage50Entities(tableSchemaProvider);
            ProccessAndStoreGestprojectEntities(
               gestprojectConnectionManager,
               sage50ConnectionManager,
               tableSchemaProvider,
               GestprojectEntities,
               Sage50Entities
            );
            CreateAndDefineDataSource(tableSchemaProvider);
            PaintEntitiesOnDataSource(tableSchemaProvider, ProcessedGestprojectEntities, DataTable);
            return DataTable;
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

      public void ManageSynchronizationTableStatus
      (
         IGestprojectConnectionManager gestprojectConnectionManager, 
         ISynchronizationTableSchemaProvider tableSchemaProvider
      )
      {
         ISynchronizationDatabaseTableManager entitySyncronizationTableStatusManager = new EntitySyncronizationTableStatusManager();

         bool tableExists = entitySyncronizationTableStatusManager.TableExists(
               gestprojectConnectionManager.GestprojectSqlConnection,
               tableSchemaProvider.TableName
            );

         if(tableExists == false)
         {
            entitySyncronizationTableStatusManager.CreateTable
            (
               gestprojectConnectionManager.GestprojectSqlConnection,
               tableSchemaProvider
            );
         };
      }

      public void GetAndStoreGestprojectEntities
      (
         IGestprojectConnectionManager gestprojectConnectionManager, 
         ISynchronizationTableSchemaProvider tableSchemaProvider
      )
      {
         //GestprojectEntities = new GestprojectTaxesManager().GetEntities(
         //   gestprojectConnectionManager.GestprojectSqlConnection,
         //   "IMPUESTO_CONFIG",
         //   tableSchemaProvider.GestprojectFieldsTupleList
         //);

         // Check if the new Taxes table exists on the Gestproject Database

         NewGestprojectTaxesTableSchemaProvider newTableSchema = new NewGestprojectTaxesTableSchemaProvider();
         ISynchronizationDatabaseTableManager entitySyncronizationTableStatusManager = new EntitySyncronizationTableStatusManager();

         bool tableExists = entitySyncronizationTableStatusManager.TableExists(
               gestprojectConnectionManager.GestprojectSqlConnection,
               newTableSchema.TableName
            );

         if(tableExists == false)
         {
            try
            {
               gestprojectConnectionManager.GestprojectSqlConnection.Open();

               string sqlString = $@"
               CREATE TABLE [INT_SAGE_NEW_GESTPROJECT_TAXES] (
                  [ID] INT PRIMARY KEY IDENTITY(1,1),
                  [NOMBRE] VARCHAR(MAX),
                  [IMP_VALOR] DECIMAL(18, 2),
                  [IVA] DECIMAL(18, 2),
                  [CTA_IV_REP] VARCHAR(MAX),
                  [CTA_IV_SOP] VARCHAR(MAX),
                  [RETENCION] DECIMAL(18, 2),
                  [CTA_RE_REP] VARCHAR(MAX),
                  [CTA_RE_SOP] VARCHAR(MAX),
                  [TAX_TYPE] VARCHAR(MAX),
                  [IMP_SUBCTA_CONTABLE] VARCHAR(MAX),
                  [IMP_SUBCTA_CONTABLE_2] VARCHAR(MAX),
                  [S50_GUID_ID] VARCHAR(MAX),
                  [IMP_ID] INT,
                  [IMP_TIPO] VARCHAR(MAX),
                  [IMP_NOMBRE] VARCHAR(MAX)
               );";

               using(SqlCommand sqlCommand = new SqlCommand(sqlString, gestprojectConnectionManager.GestprojectSqlConnection))
               {
                  sqlCommand.ExecuteNonQuery();
               };
            }
            catch(SqlException exception)
            {
               throw ApplicationLogger.ReportError(
                  MethodBase.GetCurrentMethod().DeclaringType.Namespace,
                  MethodBase.GetCurrentMethod().DeclaringType.Name,
                  MethodBase.GetCurrentMethod().Name,
                  exception
               );
            }
            finally
            {
               gestprojectConnectionManager.GestprojectSqlConnection.Close();
            };


            GestprojectEntities = new List<GestprojectTaxModel>();
            List <Sage50TaxModel> sage50Entities = new GetSage50Taxes().Entities;
            int counter = 1;
            foreach (var item in sage50Entities)
            {
               GestprojectTaxModel tax = new GestprojectTaxModel();

               tax.NOMBRE = item.NOMBRE;
               tax.IVA = item.IVA;
               tax.CTA_IV_REP = item.CTA_IV_REP;
               tax.CTA_IV_SOP = item.CTA_IV_SOP;
               tax.RETENCION = item.RETENCION;
               tax.CTA_RE_REP = item.CTA_RE_REP;
               tax.CTA_RE_SOP = item.CTA_RE_SOP;
               tax.TAX_TYPE = item.TAX_TYPE;
               tax.S50_GUID_ID = item.GUID_ID;

               if(tax.TAX_TYPE == "IVA")
               {
                  tax.IMP_VALOR = tax.IVA;
                  tax.IMP_SUBCTA_CONTABLE = tax.CTA_IV_REP;
                  tax.IMP_SUBCTA_CONTABLE_2 = tax.CTA_IV_SOP;
               }
               else
               {
                  tax.IMP_VALOR = tax.RETENCION;
                  tax.IMP_SUBCTA_CONTABLE = tax.CTA_RE_REP;
                  tax.IMP_SUBCTA_CONTABLE_2 = tax.CTA_RE_REP;
               };

               tax.IMP_TIPO = tax.TAX_TYPE;
               tax.IMP_NOMBRE = tax.NOMBRE;
               tax.ID = counter;
               tax.IMP_ID = counter;

               try
               {
                  gestprojectConnectionManager.GestprojectSqlConnection.Open();

                  string sqlString = $@"
                  INSERT INTO [INT_SAGE_NEW_GESTPROJECT_TAXES] (
                     [NOMBRE],
                     [IMP_VALOR],
                     [IVA],
                     [CTA_IV_REP],
                     [CTA_IV_SOP],
                     [RETENCION],
                     [CTA_RE_REP],
                     [CTA_RE_SOP],
                     [TAX_TYPE],
                     [IMP_SUBCTA_CONTABLE],
                     [IMP_SUBCTA_CONTABLE_2],
                     [S50_GUID_ID],
                     [IMP_ID],
                     [IMP_TIPO],
                     [IMP_NOMBRE]
                  ) VALUES (
                     '{tax.NOMBRE}',
                     CAST(REPLACE('{tax.IMP_VALOR}',',','.') AS NUMERIC),
                     CAST(REPLACE('{tax.IVA}',',','.') AS NUMERIC),
                     '{tax.CTA_IV_REP}',
                     '{tax.CTA_IV_SOP}',
                     CAST(REPLACE('{tax.RETENCION}',',','.') AS NUMERIC),
                     '{tax.CTA_RE_REP}',
                     '{tax.CTA_RE_SOP}',
                     '{tax.TAX_TYPE}',
                     '{tax.IMP_SUBCTA_CONTABLE}',
                     '{tax.IMP_SUBCTA_CONTABLE_2}',
                     '{tax.S50_GUID_ID}',
                     {tax.IMP_ID},
                     '{tax.IMP_TIPO}',
                     '{tax.IMP_NOMBRE}'
                  );";

                  using(SqlCommand sqlCommand = new SqlCommand(sqlString, gestprojectConnectionManager.GestprojectSqlConnection))
                  {
                     sqlCommand.ExecuteNonQuery();
                  };
               }
               catch(SqlException exception)
               {
                  throw ApplicationLogger.ReportError(
                     MethodBase.GetCurrentMethod().DeclaringType.Namespace,
                     MethodBase.GetCurrentMethod().DeclaringType.Name,
                     MethodBase.GetCurrentMethod().Name,
                     exception
                  );
               }
               finally
               {
                  gestprojectConnectionManager.GestprojectSqlConnection.Close();
               };

               counter++;
            };

            GestprojectEntities = new GestprojectTaxesManager().GetEntities(
               gestprojectConnectionManager.GestprojectSqlConnection,
               newTableSchema.TableName,
               newTableSchema.GestprojectFieldsTupleList
            );
         }
         else
         {
            GestprojectEntities = new GestprojectTaxesManager().GetEntities(
               gestprojectConnectionManager.GestprojectSqlConnection,
               newTableSchema.TableName,
               newTableSchema.GestprojectFieldsTupleList
            );
         };

         //MessageBox.Show(GestprojectEntities.Count + "");
         //MessageBox.Show(GestprojectEntities[4].IMP_ID + "");
      }

      public void GetAndStoreSage50Entities
      (
         ISynchronizationTableSchemaProvider tableSchemaProvider
      )
      {
         Sage50Entities = new GetSage50Taxes().Entities;
      }

      public void ProccessAndStoreGestprojectEntities
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider tableSchemaProvider,
         List<GestprojectTaxModel> GestprojectEntities,
         List<Sage50TaxModel> Sage50Entities
      )
      {
         
         ISynchronizableEntityProcessor<GestprojectTaxModel, Sage50TaxModel> gestprojectProvidersProcessor = new GestprojectTaxesProcessor();
         ProcessedGestprojectEntities = gestprojectProvidersProcessor.ProcessEntityList(
            gestprojectConnectionManager.GestprojectSqlConnection,
            sage50ConnectionManager,
            tableSchemaProvider,
            GestprojectEntities,
            Sage50Entities
         );
      }

      public void CreateAndDefineDataSource
      (
         ISynchronizationTableSchemaProvider tableSchemaProvider
      )
      {
         IDataTableGenerator entityDataTableGenerator = new SyncrhonizationDataTableGenerator();
         DataTable = entityDataTableGenerator.CreateDataTable(tableSchemaProvider.ColumnsTuplesList);
      }

      public void PaintEntitiesOnDataSource
      (
         ISynchronizationTableSchemaProvider tableSchemaProvider,
         List<GestprojectTaxModel> ProcessedGestprojectEntities,
         DataTable dataTable
      )
      {
         ISynchronizableEntityPainter<GestprojectTaxModel> entityPainter = new EntityPainter<GestprojectTaxModel>();
         entityPainter.PaintEntityListOnDataTable(
            ProcessedGestprojectEntities,
            dataTable,
            tableSchemaProvider.ColumnsTuplesList
         );
      }
   }
}
