using Sage.ES.S50.Addons;
using SincronizadorGPS50.Sage50Connector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal class TaxSynchronizationWorkflow
   {
      private string _EntityTypeNameRoot {get;set;} = "";
      private string _EntityTypeNameGender {get;set;} = "";
      private string _EntityTypePlural {get;set;} = "";
      private string _GestprojectDatabaseEntityTableName {get;set;} = "";
      private IGestprojectConnectionManager _GestprojectConnectionManager {get;set;} = null;
      private CompanyGroup _Sage50CompanyGroupData {get;set;} = null;
      private ISynchronizationTableSchemaProvider _TableSchema {get;set;} = null;
      private List<GestprojectTaxModel> _SynchronizationTableEntities {get;set;} = null;
      private List<GestprojectTaxModel> _DesynchronizedEntities {get;set;} = null;
      private List<Sage50TaxModel> _Sage50Entities {get;set;} = null;
      private DialogResult _DialogResult {get;set;} = DialogResult.Cancel;
      private List<Sage50TaxModel> _EntitiesToBeCreated {get;set;} = null;
      private List<Sage50TaxModel> _SageEntitiesToBeSynchronized {get;set;} = null;

      private List<GestprojectTaxModel> _GestprojectTaxTableTaxes = null;
      private List<int?> _GestprojectTaxTableTaxesIds = null;
      private List<string> _GestprojectTaxTableTaxesDescriptions = null;

      public TaxSynchronizationWorkflow
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         CompanyGroup sage50CompanyGroupData,
         ISynchronizationTableSchemaProvider tableSchema,
         List<GestprojectTaxModel> synchronizationTableEntities,
         List<GestprojectTaxModel> unsynchronizedEntityList,
         List<Sage50TaxModel> sage50Entities
      )
      {
         try
         {
            this._GestprojectConnectionManager = gestprojectConnectionManager;
            this._Sage50CompanyGroupData = sage50CompanyGroupData;
            this._TableSchema = tableSchema;
            this._SynchronizationTableEntities = synchronizationTableEntities;
            this._DesynchronizedEntities = unsynchronizedEntityList;
            this._Sage50Entities = sage50Entities;
            this._EntitiesToBeCreated= new List<Sage50TaxModel>();
            this._SageEntitiesToBeSynchronized= new List<Sage50TaxModel>();
            this._GestprojectTaxTableTaxes = new List<GestprojectTaxModel>();
            this._GestprojectDatabaseEntityTableName = "IMPUESTO_CONFIG";

            this._DetermineEntityGenderAndPluralFormat();
            this._DisplaySynchronizationAutorizationDialog();
            if(this._DialogResult == DialogResult.OK)
            {
               this._GetGestprojectTaxTableTaxesData();
               this._PopulateEntitiesToBeCreatedAndUpdatedLists();
               this._CreateEntities();
               this._UpdateEntities();
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
      public void _DetermineEntityGenderAndPluralFormat()
      {
         try
         {
            this._EntityTypeNameRoot = "impuest";
            this._EntityTypeNameGender = "o";
            this._EntityTypePlural = "(s)";
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
      public void _DisplaySynchronizationAutorizationDialog()
      {
         try
         {
            if(this._DesynchronizedEntities.Count > 0)
            {
               this._DialogResult = MessageBox.Show($"Partiendo de la selección encontramos {this._DesynchronizedEntities.Count} {this._EntityTypeNameRoot + this._EntityTypeNameGender + this. _EntityTypePlural} desactualizad{this._EntityTypeNameGender + this._EntityTypePlural}.\n\n¿Desea sincronizarl{this._EntityTypeNameGender + this._EntityTypePlural}?", "Confirmación de actualización", MessageBoxButtons.OKCancel);
            }
            else
            {
               MessageBox.Show($"Los {this._SynchronizationTableEntities.Count - (this._SynchronizationTableEntities.Count - _Sage50Entities.Count)} {this._EntityTypeNameRoot + this._EntityTypeNameGender + this. _EntityTypePlural} están sincronizad{this._EntityTypeNameGender + this._EntityTypePlural}.");
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

      public void _GetGestprojectTaxTableTaxesData()
      {
         SqlConnection connection = this._GestprojectConnectionManager.GestprojectSqlConnection;
         try
         {                     
            this._GestprojectTaxTableTaxes = new List<GestprojectTaxModel>();
            
            connection.Open();

            string sqlString = $@"
            SELECT 
               IMP_ID,
               IMP_SUBCTA_CONTABLE
            FROM
               {this._GestprojectDatabaseEntityTableName}
            ;";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     GestprojectTaxModel entity = new GestprojectTaxModel();
                     entity.IMP_ID = Convert.ToInt32(reader.GetValue(0));
                     entity.IMP_SUBCTA_CONTABLE = Convert.ToString(reader.GetValue(1));
                     this._GestprojectTaxTableTaxes.Add(entity);
                  };
               };
            };

            this._GestprojectTaxTableTaxesIds = this._GestprojectTaxTableTaxes.Select(entity => entity.IMP_ID).ToList();
            this._GestprojectTaxTableTaxesDescriptions = this._GestprojectTaxTableTaxes.Select(entity => entity.IMP_DESCRIPCION).ToList();
         }
         catch(System.Exception exception)
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
            connection.Close();
         };      
      }
      public void _PopulateEntitiesToBeCreatedAndUpdatedLists()
      { 
         try
         {
            foreach (Sage50TaxModel sageEntity in this._Sage50Entities)
            {
               GestprojectTaxModel entity = null;
               bool sageEntityExistInGestproject = true;
               if(sageEntity.NOMBRE.Contains("IVA"))
               {
                  entity = this._DesynchronizedEntities.FirstOrDefault(
                     desynchronizedEntity => desynchronizedEntity.IMP_SUBCTA_CONTABLE == sageEntity.CTA_IV_REP 
                     &&
                     this._GestprojectTaxTableTaxesIds.Contains(desynchronizedEntity.IMP_ID)
                     && 
                     desynchronizedEntity.S50_COMPANY_GROUP_GUID_ID != ""
                  );

                  sageEntityExistInGestproject = this._SynchronizationTableEntities.FirstOrDefault(
                     syncronizationTableEntity => syncronizationTableEntity.IMP_SUBCTA_CONTABLE == sageEntity.CTA_IV_REP 
                     &&
                     this._GestprojectTaxTableTaxesIds.Contains(syncronizationTableEntity.IMP_ID)
                     && 
                     syncronizationTableEntity.S50_COMPANY_GROUP_GUID_ID != ""
                  ) != null;
               }
               else
               {
                  entity = this._DesynchronizedEntities.FirstOrDefault(
                     desynchronizedEntity => desynchronizedEntity.IMP_SUBCTA_CONTABLE == sageEntity.CTA_RE_REP 
                     && 
                     this._GestprojectTaxTableTaxesIds.Contains(desynchronizedEntity.IMP_ID)
                     && 
                     desynchronizedEntity.S50_COMPANY_GROUP_GUID_ID != ""
                  );

                  sageEntityExistInGestproject = this._SynchronizationTableEntities.FirstOrDefault(
                     syncronizationTableEntity => syncronizationTableEntity.IMP_SUBCTA_CONTABLE == sageEntity.CTA_RE_REP 
                     &&
                     this._GestprojectTaxTableTaxesIds.Contains(syncronizationTableEntity.IMP_ID)
                     && 
                     syncronizationTableEntity.S50_COMPANY_GROUP_GUID_ID != ""
                  ) != null;
               };
                  
               //if(entity == null && sageEntityExistInGestproject == false)
               if(sageEntityExistInGestproject == false)
                  this._EntitiesToBeCreated.Add( sageEntity );
               //else if(entity != null && sageEntityExistInGestproject)
               else if(entity != null)
                  this._SageEntitiesToBeSynchronized.Add( sageEntity );
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

      public void _CreateEntities()
      {
         try
         {
            if(this._EntitiesToBeCreated.Count > 0)
            {
               foreach (Sage50TaxModel sageEntity in this._EntitiesToBeCreated)
               {
                  GestprojectTaxModel entity = new GestprojectTaxModel();

                  entity.IMP_TIPO = sageEntity.IMP_TIPO;                           
                  if(sageEntity.IMP_TIPO == "IVA")
                  {
                     entity.IMP_NOMBRE = $"{sageEntity.IMP_TIPO} {sageEntity.IVA.ToString().Split(',')[0]}";
                     entity.IMP_VALOR = Convert.ToDecimal(sageEntity.IVA);
                     entity.IMP_SUBCTA_CONTABLE = sageEntity.CTA_IV_REP;
                     entity.IMP_SUBCTA_CONTABLE_2 = sageEntity.CTA_IV_SOP;
                  }
                  else
                  {
                     entity.IMP_NOMBRE = $"{sageEntity.IMP_TIPO} {sageEntity.RETENCION.ToString().Split(',')[0]}";
                     entity.IMP_VALOR = sageEntity.RETENCION;
                     entity.IMP_SUBCTA_CONTABLE = sageEntity.CTA_RE_REP;
                     entity.IMP_SUBCTA_CONTABLE_2 = sageEntity.CTA_RE_SOP;
                  };
                  entity.IMP_DESCRIPCION = sageEntity.NOMBRE;
                  
                  entity.S50_CODE = sageEntity.CODIGO;
                  entity.S50_GUID_ID = sageEntity.GUID_ID;
                  entity.S50_COMPANY_GROUP_NAME = this._Sage50CompanyGroupData.CompanyName;
                  entity.S50_COMPANY_GROUP_CODE = this._Sage50CompanyGroupData.CompanyCode;
                  entity.S50_COMPANY_GROUP_MAIN_CODE = this._Sage50CompanyGroupData.CompanyMainCode;
                  entity.S50_COMPANY_GROUP_GUID_ID = this._Sage50CompanyGroupData.CompanyGuidId;
                  entity.GP_USU_ID = this._GestprojectConnectionManager.GestprojectUserRememberableData.GP_CNX_ID;

                  this._InsertSageEntityIntoGestprojectTaxTable(ref entity);
                  this._AppendGestprojectTaxTableIdToSynchronizationTable(ref entity);
                  this._AppendSynchronizationDataToEntityRegistry(ref entity);
               };
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

      public void _UpdateEntities()
      {
         try
         {
            if(this._SageEntitiesToBeSynchronized.Count > 0)
            {
               foreach (Sage50TaxModel sageEntity in this._SageEntitiesToBeSynchronized)
               {
                  GestprojectTaxModel entity = this._DesynchronizedEntities.FirstOrDefault(
                     desynchronizedEntity => desynchronizedEntity.IMP_SUBCTA_CONTABLE == sageEntity.CTA_RE_REP 
                     && 
                     this._GestprojectTaxTableTaxesIds.Contains(desynchronizedEntity.IMP_ID)
                     && 
                     desynchronizedEntity.S50_COMPANY_GROUP_GUID_ID != ""
                  );
                  
                  entity.S50_CODE = sageEntity.CODIGO;
                  entity.S50_GUID_ID = sageEntity.GUID_ID;
                  entity.S50_COMPANY_GROUP_NAME = this._Sage50CompanyGroupData.CompanyName;
                  entity.S50_COMPANY_GROUP_CODE = this._Sage50CompanyGroupData.CompanyCode;
                  entity.S50_COMPANY_GROUP_MAIN_CODE = this._Sage50CompanyGroupData.CompanyMainCode;
                  entity.S50_COMPANY_GROUP_GUID_ID = this._Sage50CompanyGroupData.CompanyGuidId;
                  entity.GP_USU_ID = this._GestprojectConnectionManager.GestprojectUserRememberableData.GP_CNX_ID;

                  this._UpdateGestprojectTaxDataWithSageTaxData(ref entity);
                  this._AppendSynchronizationDataToEntityRegistry(ref entity);
               };
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

      private void _InsertSageEntityIntoGestprojectTaxTable(ref GestprojectTaxModel entity)
      {
         SqlConnection connection = this._GestprojectConnectionManager.GestprojectSqlConnection;
         try
         {
            connection.Open();
            string sqlString = $@"
            SELECT 
               MAX(IMP_ID)
            FROM
               {this._GestprojectDatabaseEntityTableName}
            ;";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     int maxIdValue = Convert.ToInt32(reader.GetValue(0).GetType().Name == "DBNull" ? 0 : reader.GetValue(0));
                     entity.IMP_ID = ++maxIdValue;
                  };
               };
            };

            string sqlString2 = $@"
            INSERT INTO 
               {this._GestprojectDatabaseEntityTableName} 
               (
                  IMP_ID
                  ,IMP_TIPO
                  ,IMP_NOMBRE
                  ,IMP_DESCRIPCION
                  ,IMP_VALOR
                  ,IMP_SUBCTA_CONTABLE
                  ,IMP_SUBCTA_CONTABLE_2
               )
            VALUES
               (
                  @IMP_ID
                  ,@IMP_TIPO
                  ,@IMP_NOMBRE
                  ,@IMP_DESCRIPCION
                  ,@IMP_VALOR
                  ,@IMP_SUBCTA_CONTABLE
                  ,@IMP_SUBCTA_CONTABLE_2
               )
            ;";

            using(SqlCommand command = new SqlCommand(sqlString2, connection))
            {
               command.Parameters.AddWithValue("@IMP_ID",entity.IMP_ID);
               command.Parameters.AddWithValue("@IMP_TIPO",entity.IMP_TIPO);
               command.Parameters.AddWithValue("@IMP_NOMBRE",entity.IMP_NOMBRE);
               command.Parameters.AddWithValue("@IMP_DESCRIPCION",entity.IMP_DESCRIPCION);
               command.Parameters.AddWithValue("@IMP_VALOR",entity.IMP_VALOR);
               command.Parameters.AddWithValue("@IMP_SUBCTA_CONTABLE",entity.IMP_SUBCTA_CONTABLE);
               command.Parameters.AddWithValue("@IMP_SUBCTA_CONTABLE_2",entity.IMP_SUBCTA_CONTABLE_2);

               command.ExecuteNonQuery();
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
         }
         finally
         {
            connection.Close();
         };
      
      }
      private void _AppendGestprojectTaxTableIdToSynchronizationTable(ref GestprojectTaxModel entity)
      {      
         SqlConnection connection = this._GestprojectConnectionManager.GestprojectSqlConnection;
         try
         {
            connection.Open();

            string sqlString = $@"
            SELECT 
               IMP_ID
            FROM
               {this._GestprojectDatabaseEntityTableName}
            WHERE
               IMP_SUBCTA_CONTABLE=@IMP_SUBCTA_CONTABLE
            ;";

            using(SqlCommand command = new SqlCommand(sqlString, connection))
            {
               command.Parameters.AddWithValue("@IMP_SUBCTA_CONTABLE",entity.IMP_SUBCTA_CONTABLE);

               using(SqlDataReader reader = command.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     entity.IMP_ID = Convert.ToInt32(reader.GetValue(0));
                     break;
                  };
               };
            };

            string sqlString2 = $@"
            UPDATE 
               {this._TableSchema.TableName} 
            SET
               IMP_ID=@IMP_ID
            WHERE
               S50_GUID_ID=@S50_GUID_ID
            ;";

            using(SqlCommand command = new SqlCommand(sqlString2, connection))
            {
               command.Parameters.AddWithValue("@IMP_ID",entity.IMP_ID);
               command.Parameters.AddWithValue("@S50_GUID_ID",entity.S50_GUID_ID);

               command.ExecuteNonQuery();
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
         }
         finally
         {
            connection.Close();
         };
      }
      private void _AppendSynchronizationDataToEntityRegistry(ref GestprojectTaxModel entity)
      {
         SqlConnection connection = this._GestprojectConnectionManager.GestprojectSqlConnection;
         try
         {
            connection.Open();

            string sqlString = $@"
            UPDATE 
               {this._TableSchema.TableName} 
            SET
               S50_CODE=@S50_CODE
               ,S50_COMPANY_GROUP_NAME=@S50_COMPANY_GROUP_NAME
               ,S50_COMPANY_GROUP_CODE=@S50_COMPANY_GROUP_CODE
               ,S50_COMPANY_GROUP_MAIN_CODE=@S50_COMPANY_GROUP_MAIN_CODE
               ,S50_COMPANY_GROUP_GUID_ID=@S50_COMPANY_GROUP_GUID_ID
               ,GP_USU_ID=@GP_USU_ID
            WHERE
               S50_GUID_ID=@S50_GUID_ID
            ;";

            using(SqlCommand command = new SqlCommand(sqlString, connection))
            {
               command.Parameters.AddWithValue("@S50_GUID_ID",entity.S50_GUID_ID);
               command.Parameters.AddWithValue("@S50_CODE",entity.S50_CODE);
               command.Parameters.AddWithValue("@S50_COMPANY_GROUP_NAME",entity.S50_COMPANY_GROUP_NAME);
               command.Parameters.AddWithValue("@S50_COMPANY_GROUP_CODE",entity.S50_COMPANY_GROUP_CODE);
               command.Parameters.AddWithValue("@S50_COMPANY_GROUP_MAIN_CODE",entity.S50_COMPANY_GROUP_MAIN_CODE);
               command.Parameters.AddWithValue("@S50_COMPANY_GROUP_GUID_ID",entity.S50_COMPANY_GROUP_GUID_ID);
               command.Parameters.AddWithValue("@GP_USU_ID",entity.GP_USU_ID);

               command.ExecuteNonQuery();
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
         }
         finally
         {
            connection.Close();
         };
      }

      private void _UpdateGestprojectTaxDataWithSageTaxData(ref GestprojectTaxModel entity)
      {
         SqlConnection connection = this._GestprojectConnectionManager.GestprojectSqlConnection;
         try
         {
            connection.Open();

            string sqlString = $@"
            UPDATE 
               {this._GestprojectDatabaseEntityTableName} 
            SET
               ,IMP_TIPO=@IMP_TIPO
               ,IMP_NOMBRE=@IMP_NOMBRE
               ,IMP_DESCRIPCION=@IMP_DESCRIPCION
               ,IMP_VALOR=@IMP_VALOR
               ,IMP_SUBCTA_CONTABLE=@IMP_SUBCTA_CONTABLE
               ,IMP_SUBCTA_CONTABLE_2=@IMP_SUBCTA_CONTABLE_2
            WHERE
               IMP_ID=@IMP_ID
            ;";

            using(SqlCommand command = new SqlCommand(sqlString, connection))
            {
               command.Parameters.AddWithValue("@IMP_ID",entity.IMP_ID);
               command.Parameters.AddWithValue("@IMP_TIPO",entity.IMP_TIPO);
               command.Parameters.AddWithValue("@IMP_NOMBRE",entity.IMP_NOMBRE);
               command.Parameters.AddWithValue("@IMP_DESCRIPCION",entity.IMP_DESCRIPCION);
               command.Parameters.AddWithValue("@IMP_VALOR",entity.IMP_VALOR);
               command.Parameters.AddWithValue("@IMP_SUBCTA_CONTABLE",entity.IMP_SUBCTA_CONTABLE);
               command.Parameters.AddWithValue("@IMP_SUBCTA_CONTABLE_2",entity.IMP_SUBCTA_CONTABLE_2);

               command.ExecuteNonQuery();
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
         }
         finally
         {
            connection.Close();
         };
      }
   }
}
