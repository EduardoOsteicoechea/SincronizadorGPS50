using Infragistics.Designers.SqlEditor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
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
         GestprojectEntities = new GestprojectTaxesManager().GetEntities(
            gestprojectConnectionManager.GestprojectSqlConnection,
            "IMPUESTO_CONFIG",
            tableSchemaProvider.GestprojectFieldsTupleList
         );

         List<Sage50TaxModel> sage50Entities = new GetSage50Taxes().Entities;

         foreach(var item in sage50Entities)
         {
            GestprojectTaxModel gestprojectTaxModel = new GestprojectTaxModel();
            
            gestprojectTaxModel.IMP_ID = 0;
            gestprojectTaxModel.IMP_TIPO = item.IMP_TIPO;
            gestprojectTaxModel.IMP_DESCRIPCION = item.NOMBRE;

            if(item.IMP_TIPO == "IVA")
            {
               gestprojectTaxModel.IMP_NOMBRE = item.IMP_TIPO + item.IVA;
               gestprojectTaxModel.IMP_VALOR = item.IVA;
               gestprojectTaxModel.IMP_SUBCTA_CONTABLE = item.CTA_IV_REP;
               gestprojectTaxModel.IMP_SUBCTA_CONTABLE_2 = item.CTA_IV_SOP;
            }
            else
            {
               gestprojectTaxModel.IMP_NOMBRE = item.IMP_TIPO + item.RETENCION;
               gestprojectTaxModel.IMP_VALOR = item.RETENCION;
               gestprojectTaxModel.IMP_SUBCTA_CONTABLE = item.CTA_RE_REP;
               gestprojectTaxModel.IMP_SUBCTA_CONTABLE_2 = item.CTA_RE_SOP;
            };

            gestprojectTaxModel.S50_CODE = item.CODIGO;
            gestprojectTaxModel.S50_GUID_ID = item.GUID_ID;

            GestprojectEntities.Add(gestprojectTaxModel);
         };

         MessageBox.Show(GestprojectEntities.Count + "");

         //foreach(var item in GestprojectEntities)
         //{
         //   string message = "Gestproject Tax:\n\n";
         //   foreach(var propertyInfo in item.GetType().GetProperties())
         //   {
         //      message += $"{propertyInfo.Name}: {propertyInfo.GetValue(item)}\n";
         //   };
         //   MessageBox.Show(message, "Object Properties");
         //};
      }

      public void GetAndStoreSage50Entities ( ISynchronizationTableSchemaProvider tableSchemaProvider )
      {
         Sage50Entities = new GetSage50Taxes().Entities;

         //foreach(var item in Sage50Entities)
         //{
         //   string message = "Sage50 Tax:\n\n";
         //   foreach(var propertyInfo in item.GetType().GetRuntimeProperties())
         //   {
         //      message += $"{propertyInfo.Name}: {propertyInfo.GetValue(item)}\n";
         //   };
         //   MessageBox.Show(message, "Object Properties");
         //};
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
         foreach(var entity in ProcessedGestprojectEntities)
         {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var item in entity.GetType().GetProperties())
            {
               stringBuilder.Append($"{item.Name}: {item.GetValue(entity)}\n");
            };
            MessageBox.Show(stringBuilder.ToString());
         };

         ISynchronizableEntityPainter<GestprojectTaxModel> entityPainter = new EntityPainter<GestprojectTaxModel>();
         entityPainter.PaintEntityListOnDataTable(
            ProcessedGestprojectEntities,
            dataTable,
            tableSchemaProvider.ColumnsTuplesList
         );
      }
   }
}
