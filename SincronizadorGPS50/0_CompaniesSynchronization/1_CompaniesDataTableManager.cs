﻿using Infragistics.Designers.SqlEditor;
using sage.ew.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   public class CompaniesDataTableManager : IGridDataSourceGenerator<SincronizadorGP50CompanyModel, SageCompanyModel>
   {
      public List<SincronizadorGP50CompanyModel> GestprojectEntities { get; set; }
      public List<SincronizadorGP50CompanyModel> ProcessedGestprojectEntities { get; set; }
      public IGestprojectConnectionManager GestprojectConnectionManager { get; set; }
      public SqlConnection Connection { get; set; }
      public ISage50ConnectionManager SageConnectionManager { get; set; }
      public ISynchronizationTableSchemaProvider TableSchema { get; set; }
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
            GestprojectConnectionManager = gestprojectConnectionManager;
            Connection = GestprojectConnectionManager.GestprojectSqlConnection;
            SageConnectionManager = sage50ConnectionManager;
            TableSchema = tableSchemaProvider;

            ManageSynchronizationTableStatus();
            LinkEndpointsModels();
            CreateAndDefineDataSource();
            PaintEntitiesOnDataSource();
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

      public void ManageSynchronizationTableStatus()
      {
         ISynchronizationDatabaseTableManager entitySyncronizationTableStatusManager = new EntitySyncronizationTableStatusManager();

         bool tableExists = entitySyncronizationTableStatusManager.TableExists(
               GestprojectConnectionManager.GestprojectSqlConnection,
               TableSchema.TableName
            );

         if(tableExists == false)
         {
            entitySyncronizationTableStatusManager.CreateTable
            (
               GestprojectConnectionManager.GestprojectSqlConnection,
               TableSchema
            );
         };
      }
      
      public void LinkEndpointsModels()
      {
         try
         {            
            List<SincronizadorGP50CompanyModel> gestprojectCompaniesData = GetGestprojectCompanies();
            List<SageCompanyModel> sageCompaniesData = GetSageCompanies();
            GestprojectEntities = MatchMatchingCompanies(gestprojectCompaniesData,sageCompaniesData);
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
      public List<SincronizadorGP50CompanyModel> GetGestprojectCompanies()
      {
         try
         {
            Connection.Open();

            List<SincronizadorGP50CompanyModel> sincronizadorGP50CompanyModelList = new List<SincronizadorGP50CompanyModel>();

            string sqlString = $@"SELECT PAR_ID FROM [GESTPROJECT2020].[dbo].[PAR_TPA] WHERE TPA_ID=16;";

            string companiesIds = "";

            using(SqlCommand command = new SqlCommand(sqlString,Connection))
            {
               using(SqlDataReader reader = command.ExecuteReader())
               {
                  while (reader.Read())
                  {
                     companiesIds += "'" + Convert.ToInt32(reader.GetValue(0)) + "',";
                  };
               };
            };

            companiesIds = companiesIds.TrimEnd(',');

            string sqlString2 = $@"SELECT PAR_ID,PAR_NOMBRE,PAR_CIF_NIF FROM [GESTPROJECT2020].[dbo].[PARTICIPANTE] WHERE PAR_ID IN ({companiesIds});";

            new VisualizationForm(sqlString2, sqlString2);

            using(SqlCommand command = new SqlCommand(sqlString2,Connection))
            {
               using(SqlDataReader reader = command.ExecuteReader())
               {
                  while (reader.Read())
                  {
                     SincronizadorGP50CompanyModel sincronizadorGP50CompanyModel = new SincronizadorGP50CompanyModel();

                     sincronizadorGP50CompanyModel.PAR_ID = Convert.ToInt32(reader.GetValue(0));
                     sincronizadorGP50CompanyModel.PAR_NOMBRE = Convert.ToString(reader.GetValue(1));
                     sincronizadorGP50CompanyModel.PAR_CIF_NIF = Convert.ToString(reader.GetValue(2));

                     sincronizadorGP50CompanyModelList.Add(sincronizadorGP50CompanyModel);
                  };
               };
            };

            return sincronizadorGP50CompanyModelList;
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
            Connection.Close();
         };
      }
      public List<SageCompanyModel> GetSageCompanies()
      {
         try
         {
            List<SageCompanyModel> sageCompanyModelList = new List<SageCompanyModel>();

            DataTable companiesTable = new DataTable();
            string sqlString = $@"
            SELECT
               CODIGO
               ,NOMBRE
               ,CIF
               ,GUID_ID
            FROM
               {DB.SQLDatabase("GESTION", "empresa")}
            ";

            DB.SQLExec(sqlString, ref companiesTable);

            for(int i = 0; i < companiesTable.Rows.Count; i++)
            {
               SageCompanyModel sageCompanyModel = new SageCompanyModel();

               sageCompanyModel.SageCompanyNumber = Convert.ToString(companiesTable.Rows[i].ItemArray[0]);
               sageCompanyModel.SageName = Convert.ToString(companiesTable.Rows[i].ItemArray[1]);
               sageCompanyModel.SageCifNif = Convert.ToString(companiesTable.Rows[i].ItemArray[2]);
               sageCompanyModel.SageGuidId = Convert.ToString(companiesTable.Rows[i].ItemArray[3]);

               sageCompanyModelList.Add(sageCompanyModel);
            };

            return sageCompanyModelList;
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
      public List<SincronizadorGP50CompanyModel> MatchMatchingCompanies
      (
         List<SincronizadorGP50CompanyModel> gestprojectCompaniesData,
         List<SageCompanyModel> sageCompaniesData
      )
      {
         try
         {
            List<SincronizadorGP50CompanyModel> matchingCompaniesList = new List<SincronizadorGP50CompanyModel>();

            foreach (SincronizadorGP50CompanyModel gestprojectCompany in gestprojectCompaniesData)
            {
               SageCompanyModel sageMatchingCompany = sageCompaniesData.FirstOrDefault(
                  sageCompany => sageCompany.SageCifNif == gestprojectCompany.PAR_CIF_NIF
               );

               if(sageMatchingCompany != null)
               {
                  gestprojectCompany.SageCompanyNumber = sageMatchingCompany.SageCompanyNumber;
                  gestprojectCompany.S50_GUID_ID = sageMatchingCompany.SageGuidId;
                  matchingCompaniesList.Add(gestprojectCompany);
               };
            };

            return matchingCompaniesList;
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

      public void CreateAndDefineDataSource()
      {
         IDataTableGenerator entityDataTableGenerator = new SyncrhonizationDataTableGenerator();
         DataTable = entityDataTableGenerator.CreateDataTable(TableSchema.ColumnsTuplesList);
      }

      public void PaintEntitiesOnDataSource()
      {
         ISynchronizableEntityPainter<SincronizadorGP50CompanyModel> entityPainter = new EntityPainter<SincronizadorGP50CompanyModel>();
         entityPainter.PaintEntityListOnDataTable(
            GestprojectEntities,
            DataTable,
            TableSchema.ColumnsTuplesList
         );
      }
   }
}