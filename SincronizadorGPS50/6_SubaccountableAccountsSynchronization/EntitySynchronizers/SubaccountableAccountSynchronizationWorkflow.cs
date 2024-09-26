using SincronizadorGPS50.Sage50Connector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal class SubaccountableAccountsSynchronizationWorkflow
   {
      private string _EntityTypeName { get; set; }
      private string _FemenineOrMasculine { get; set; }
      private bool _Synchronize { get; set; }


        
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
            DetermineEntityNameAndGrammaticalGender();

            SkipOrSynchronize(
               SynchronizationTableEntities,
               unsynchronizedEntityList,
               Sage50Entities,
               _EntityTypeName,
               _FemenineOrMasculine
            );

            if(_Synchronize)            
               SynchronizeEntities(
                  gestprojectConnectionManager,
                  Sage50CompanyGroupData,
                  tableSchema,
                  SynchronizationTableEntities,
                  unsynchronizedEntityList,
                  Sage50Entities
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


      private void DetermineEntityNameAndGrammaticalGender()
      {
         _EntityTypeName = "Cuentas Contable(s)";
         _FemenineOrMasculine = "a";
      }


      private void SkipOrSynchronize(
         List<GestprojectSubaccountableAccountModel> SynchronizationTableEntities,
         List<GestprojectSubaccountableAccountModel> unsynchronizedEntityList,
         List<Sage50SubaccountableAccountModel> Sage50Entities,
         string entityTypeName,
         string femenineOrMasculine
      )
      {
         try
         {
            if(unsynchronizedEntityList.Count > 0)
            {
               DialogResult result = MessageBox.Show($"Partiendo de la selección encontramos {unsynchronizedEntityList.Count} {_EntityTypeName} desactualizad{_FemenineOrMasculine}s.\n\n¿Desea sincronizarl{femenineOrMasculine}(s)?", "Confirmación de actualización", MessageBoxButtons.OKCancel);

               if(result == DialogResult.OK)
               {
                  _Synchronize = true;
               }
               else
               {
                  _Synchronize = false;
               };  
            }
            else
            {
               MessageBox.Show($"L{femenineOrMasculine}s {SynchronizationTableEntities.Count - (SynchronizationTableEntities.Count - Sage50Entities.Count)} {entityTypeName} están sincronizad{femenineOrMasculine}s.");
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
      
      private void SynchronizeEntities
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
            foreach (Sage50SubaccountableAccountModel currentSageEntity in Sage50Entities)
            {
               GestprojectSubaccountableAccountModel gestprojectEntity = DetermineSageEntityExistenceInGestproject(
                  gestprojectConnectionManager,
                  tableSchema,
                  unsynchronizedEntityList,
                  currentSageEntity
               );

               if(gestprojectEntity == null)
               {
                  gestprojectEntity = CreateNonexistentEntity(
                     gestprojectConnectionManager,
                     Sage50CompanyGroupData,
                     tableSchema,
                     currentSageEntity
                  );
               }
               else
               {
                  UpdateOutdatedEntity(
                     gestprojectConnectionManager,
                     Sage50CompanyGroupData,
                     tableSchema,
                     currentSageEntity,
                     gestprojectEntity
                  );
               };
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
      

      private GestprojectSubaccountableAccountModel DetermineSageEntityExistenceInGestproject
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISynchronizationTableSchemaProvider tableSchema,
         List<GestprojectSubaccountableAccountModel> unsynchronizedEntityList,
         Sage50SubaccountableAccountModel Sage50Entity
      )
      {
         try
         {
            List<GestprojectSubaccountableAccountModel> gestprojectEntities = new GetGestprojectSubaccountableAccountTableSubaccountableAccounts(
                  gestprojectConnectionManager.GestprojectSqlConnection,
                  tableSchema.GestprojectEntityTableName
               ).Entities;

            List<int?> gestprojectEntitiesIds = gestprojectEntities.Select(x => x.COS_ID).ToList();      

            GestprojectSubaccountableAccountModel gestprojectEntity = null;

            gestprojectEntity =
            unsynchronizedEntityList
            .FirstOrDefault(
               entity => entity.COS_GRUPO == Sage50Entity.CODIGO
               &&
               gestprojectEntitiesIds.Contains(entity.COS_ID)
               &&
               entity.S50_COMPANY_GROUP_GUID_ID != ""
            );

            return gestprojectEntity;
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
      

      private GestprojectSubaccountableAccountModel CreateNonexistentEntity
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         CompanyGroup Sage50CompanyGroupData,
         ISynchronizationTableSchemaProvider tableSchema,
         Sage50SubaccountableAccountModel sage50Entity
      )
      {
         try
         {
            GestprojectSubaccountableAccountModel entity = new GestprojectSubaccountableAccountModel();

            entity.COS_NOMBRE = sage50Entity.NOMBRE;
            entity.COS_CODIGO = sage50Entity.CODIGO;
            entity.COS_GRUPO = sage50Entity.CODIGO;

            entity.S50_CODE = sage50Entity.CODIGO;
            entity.S50_GUID_ID = sage50Entity.GUID_ID;

            string sqlStatementColumns = "";
            string sqlStatementValues = "";

            for(int j = 0; j < tableSchema.GestprojectFieldsTupleList.Count; j++)
            {
               string currentColumnName = tableSchema.GestprojectFieldsTupleList[j].columnName;
               Type currentColumnType = tableSchema.GestprojectFieldsTupleList[j].columnType;
               dynamic value = entity.GetType().GetProperty(currentColumnName).GetValue(entity, null);

               if(currentColumnName != "COS_ID")
               {
                  sqlStatementColumns += currentColumnName + ",";
                  sqlStatementValues += DynamicValuesFormatters.Formatters[currentColumnType](value) + ",";
               };
            };

            this.DeleteSubaccountableAccountTemporalSynchronizationRegistry(
               gestprojectConnectionManager.GestprojectSqlConnection,
               tableSchema.TableName,
               entity
            ); 

            this.InsertEntityIntoGestprojectSubaccountableAccountTable(
               gestprojectConnectionManager.GestprojectSqlConnection,
               tableSchema.GestprojectEntityTableName,
               sqlStatementColumns.TrimEnd(','),
               sqlStatementValues.TrimEnd(',')
            );

            this.AppendGestprojectSubaccountableAccountTableIdToEntity(
               gestprojectConnectionManager.GestprojectSqlConnection,
               tableSchema,
               entity
            );           

            this.AppendSynchronizationTableIdToEntity(
               gestprojectConnectionManager.GestprojectSqlConnection,
               tableSchema,
               entity
            );

            this.RegisterEntityIntoSynchronizationTable(
                  gestprojectConnectionManager,
                  Sage50CompanyGroupData,
                  tableSchema,
                  sage50Entity,
                  entity
               );

            return entity;
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
      
      private void UpdateOutdatedEntity
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         CompanyGroup Sage50CompanyGroupData,
         ISynchronizationTableSchemaProvider tableSchema,
         Sage50SubaccountableAccountModel sageEntity,
         GestprojectSubaccountableAccountModel entity
      )
      {
         try
         {
            this.UpdateGestprojectDataWithSageData(
               gestprojectConnectionManager,
               tableSchema,
               sageEntity,
               entity
            );

            //this.RegisterEntityIntoSynchronizationTable(
            //   gestprojectConnectionManager,
            //   Sage50CompanyGroupData,
            //   tableSchema,
            //   sageEntity,
            //   entity
            //);
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

      
      ///////////////////////////////////
      /// SQL methods
      ///////////////////////////////////
 

      private void DeleteSubaccountableAccountTemporalSynchronizationRegistry
      (
         SqlConnection connection,
         string tableName,
         GestprojectSubaccountableAccountModel entity
      )
      {
         try
         {
            connection.Open();

            string sqlString = $@"
            DELETE FROM 
               {tableName} 
            WHERE
               COS_GRUPO=@COS_GRUPO
            AND
               COS_ID=@COS_ID
            ;";
            
            //MessageBox.Show("At: " + MethodBase.GetCurrentMethod().Name + "\n\n" + sqlString);

            using(SqlCommand command = new SqlCommand(sqlString, connection))
            {
               command.Parameters.AddWithValue("@COS_GRUPO", entity.COS_GRUPO);
               command.Parameters.AddWithValue("@COS_ID", -1);

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


      private void InsertEntityIntoGestprojectSubaccountableAccountTable(
         SqlConnection connection,
         string tableName,
         string gestprojectSubaccountableAccountsTableColumns, 
         string gestprojectSubaccountableAccountsTableValues
      )
      {
         try
         {
            connection.Open();
            
            ////////////////////////////////////////
            /// Because the table doesn't have an autoincremental index 
            /// we need to get the highest id in it 
            /// and asign that id to the entity to be inserted 
            /// before it's insertion.
            ////////////////////////////////////////

            string sqlString = "SELECT MAX(COS_ID) FROM " + tableName + ";";
            int? entityId = null;

            using(SqlCommand command = new SqlCommand(sqlString, connection))
            {
               using(SqlDataReader reader = command.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     int maxIdValue = Convert.ToInt32(reader.GetValue(0) ?? 1);
                     entityId = ++maxIdValue;
                  };
               };
            };
            
            //MessageBox.Show("At: " + MethodBase.GetCurrentMethod().Name + "\n\n" + sqlString);

            string sqlString2 = $@"            
            INSERT INTO 
               {tableName} 
               (COS_ID,{gestprojectSubaccountableAccountsTableColumns})
            VALUES
               ({entityId},{gestprojectSubaccountableAccountsTableValues})
            ;";
            
            //MessageBox.Show("At: " + MethodBase.GetCurrentMethod().Name + "\n\n" + sqlString2);

            using(SqlCommand command = new SqlCommand(sqlString2, connection))
            {
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

     
      private void AppendGestprojectSubaccountableAccountTableIdToEntity
      (
         System.Data.SqlClient.SqlConnection connection,
         ISynchronizationTableSchemaProvider tableSchema,
         GestprojectSubaccountableAccountModel entity
      )
      {
         try
         {
            connection.Open();

            string sqlString = $@"
               SELECT 
                  COS_ID 
               FROM 
                  {tableSchema.GestprojectEntityTableName}
               WHERE
                  COS_GRUPO=@COS_GRUPO
               ;";

            //MessageBox.Show("At: " + MethodBase.GetCurrentMethod().Name + "\n\n" + sqlString);

            using(SqlCommand command = new SqlCommand(sqlString, connection))
            {
               command.Parameters.AddWithValue("@COS_GRUPO", entity.COS_GRUPO);

               using(SqlDataReader reader = command.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     entity.COS_ID = Convert.ToInt32(reader.GetValue(0));
                  };
               };
            };

            string sqlString2 = $@"
               UPDATE 
                  {tableSchema.TableName} 
               SET
                  {tableSchema.GestprojectId.ColumnDatabaseName}={entity.COS_ID}
               WHERE
                  COS_GRUPO=@COS_GRUPO
               AND
                  COS_NOMBRE=@COS_NOMBRE
               ;";

            //MessageBox.Show("At: AppendGestprojectSubaccountableAccountTableIdToSynchronizationTable" + sqlString2);

            using(SqlCommand command = new SqlCommand(sqlString2, connection))
            {
               command.Parameters.AddWithValue("@COS_GRUPO", entity.COS_GRUPO);
               command.Parameters.AddWithValue("@COS_NOMBRE", entity.COS_NOMBRE);
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

      
      // Verified 2024-09-26-7:00am
      private void AppendSynchronizationTableIdToEntity
      (
         System.Data.SqlClient.SqlConnection connection,
         ISynchronizationTableSchemaProvider tableSchema,
         GestprojectSubaccountableAccountModel entity
      )
      {
         try
         {
            connection.Open();

            string sqlString = $@"
            SELECT 
               ID
            FROM
               {tableSchema.TableName}
            WHERE
               COS_GRUPO=@COS_GRUPO
            ;";
            
            //MessageBox.Show("At: " + MethodBase.GetCurrentMethod().Name + "\n\n" + sqlString);

            using(SqlCommand command = new SqlCommand(sqlString, connection))
            {
               command.Parameters.AddWithValue("@COS_GRUPO", entity.COS_GRUPO);

               using(SqlDataReader reader = command.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     entity.ID = Convert.ToInt32(reader.GetValue(0));
                  };
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
         }
         finally
         {
            connection.Close();
         };
      }


      private void RegisterEntityIntoSynchronizationTable
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         CompanyGroup Sage50CompanyGroupData,
         ISynchronizationTableSchemaProvider tableSchema,
         Sage50SubaccountableAccountModel sageEntity,
         GestprojectSubaccountableAccountModel entity
      )
      {
         SqlConnection connection = gestprojectConnectionManager.GestprojectSqlConnection;
         try
         {
            connection.Open();
            
            entity.COS_CODIGO = sageEntity.CODIGO;
            entity.S50_CODE = sageEntity.CODIGO;
            entity.S50_GUID_ID = sageEntity.GUID_ID;
            entity.S50_COMPANY_GROUP_NAME = Sage50CompanyGroupData.CompanyName;
            entity.S50_COMPANY_GROUP_CODE = Sage50CompanyGroupData.CompanyCode;
            entity.S50_COMPANY_GROUP_MAIN_CODE = Sage50CompanyGroupData.CompanyMainCode;
            entity.S50_COMPANY_GROUP_GUID_ID = Sage50CompanyGroupData.CompanyGuidId;
            entity.GP_USU_ID = gestprojectConnectionManager.GestprojectUserRememberableData.GP_CNX_ID;

            string sqlString = $@"
            INSERT INTO 
            {tableSchema.TableName}
               (      
                  COS_ID
                  ,COS_CODIGO
                  ,COS_NOMBRE
                  ,COS_GRUPO
                  ,S50_CODE
                  ,S50_GUID_ID
                  ,S50_COMPANY_GROUP_NAME
                  ,S50_COMPANY_GROUP_CODE
                  ,S50_COMPANY_GROUP_MAIN_CODE
                  ,S50_COMPANY_GROUP_GUID_ID
                  ,GP_USU_ID
               ) 
            VALUES
               (      
                  @COS_ID
                  ,@COS_CODIGO
                  ,@COS_NOMBRE
                  ,@COS_GRUPO
                  ,@S50_CODE
                  ,@S50_GUID_ID
                  ,@S50_COMPANY_GROUP_NAME
                  ,@S50_COMPANY_GROUP_CODE
                  ,@S50_COMPANY_GROUP_MAIN_CODE
                  ,@S50_COMPANY_GROUP_GUID_ID
                  ,@GP_USU_ID
               )
            ;";

            //MessageBox.Show("At: " + MethodBase.GetCurrentMethod().Name + "\n\n" + sqlString);

            using(SqlCommand command = new SqlCommand(sqlString, connection))
            {
               command.Parameters.AddWithValue("@COS_ID", entity.COS_ID);
               command.Parameters.AddWithValue("@COS_CODIGO", entity.COS_CODIGO);
               command.Parameters.AddWithValue("@COS_NOMBRE", entity.COS_NOMBRE);
               command.Parameters.AddWithValue("@COS_GRUPO", entity.COS_GRUPO);
               command.Parameters.AddWithValue("@S50_CODE", entity.S50_CODE);
               command.Parameters.AddWithValue("@S50_GUID_ID", entity.S50_GUID_ID);
               command.Parameters.AddWithValue("@S50_COMPANY_GROUP_NAME", entity.S50_COMPANY_GROUP_NAME);
               command.Parameters.AddWithValue("@S50_COMPANY_GROUP_CODE", entity.S50_COMPANY_GROUP_CODE);
               command.Parameters.AddWithValue("@S50_COMPANY_GROUP_MAIN_CODE", entity.S50_COMPANY_GROUP_MAIN_CODE);
               command.Parameters.AddWithValue("@S50_COMPANY_GROUP_GUID_ID", entity.S50_COMPANY_GROUP_GUID_ID);
               command.Parameters.AddWithValue("@GP_USU_ID", entity.GP_USU_ID);

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


      private void UpdateGestprojectDataWithSageData
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISynchronizationTableSchemaProvider tableSchema,
         Sage50SubaccountableAccountModel sageEntity,
         GestprojectSubaccountableAccountModel entity
      )
      {
         SqlConnection connection = gestprojectConnectionManager.GestprojectSqlConnection;
         try
         {
            connection.Open();

            string sqlString = $@"
            UPDATE 
               {tableSchema.TableName}
            SET
               COS_CODIGO=@COS_CODIGO
               ,COS_NOMBRE=@COS_NOMBRE
               ,COS_GRUPO=@COS_GRUPO
            WHERE
               COS_ID=@COS_ID
            ;";
            
            //MessageBox.Show("At: " + MethodBase.GetCurrentMethod().Name + "\n\n" + sqlString);

            using(SqlCommand command = new SqlCommand(sqlString, connection))
            {
               command.Parameters.AddWithValue("@COS_CODIGO", sageEntity.CODIGO);
               command.Parameters.AddWithValue("@COS_NOMBRE", sageEntity.NOMBRE);
               command.Parameters.AddWithValue("@COS_GRUPO", sageEntity.CODIGO);
               command.Parameters.AddWithValue("@COS_ID", entity.COS_ID);

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
