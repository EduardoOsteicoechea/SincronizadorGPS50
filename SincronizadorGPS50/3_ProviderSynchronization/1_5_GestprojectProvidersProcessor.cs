﻿using SincronizadorGPS50.GestprojectDataManager;
using SincronizadorGPS50.Sage50Connector;
using System.Collections.Generic;
using System.Reflection;

namespace SincronizadorGPS50
{
   public class GestprojectProvidersProcessor: ISynchronizableEntityProcessor
   {
      public List<GestprojectProviderModel> ProcessEntityList
      (
         System.Data.SqlClient.SqlConnection connection,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider tableSchema,
         List<GestprojectProviderModel> gestprojectEntites,
         List<Sage50ProviderModel> sage50Entities
      ) 
      {
         try
         {
            List<GestprojectProviderModel> processedEntities = new List<GestprojectProviderModel>();
            CompanyGroup sage50CompanyGroupData = sage50ConnectionManager.CompanyGroupData;

            for(int i = 0; i < gestprojectEntites.Count; i++)
            {
               GestprojectProviderModel entity = gestprojectEntites[i];

               ////////////////////////////
               /// append synchronization table data to entity
               ////////////////////////////

               new AppendSynchronizationTableDataToProvider
               (
                  connection, 
                  entity, 
                  tableSchema.TableName,
                  tableSchema.SynchronizationTableProviderIdColumn.ColumnDatabaseName,
                  tableSchema.SynchronizationStatusColumn.ColumnDatabaseName,
                  tableSchema.Sage50ProviderCodeColumn.ColumnDatabaseName,
                  tableSchema.Sage50ProviderGuidIdColumn.ColumnDatabaseName,
                  tableSchema.Sage50ProviderCompanyGroupNameColumn.ColumnDatabaseName,
                  tableSchema.Sage50ProviderCompanyGroupCodeColumn.ColumnDatabaseName,
                  tableSchema.Sage50ProviderCompanyGroupMainCodeColumn.ColumnDatabaseName,
                  tableSchema.Sage50ProviderCompanyGroupGuidIdColumn.ColumnDatabaseName,
                  tableSchema.GestprojectProviderParentUserIdColumn.ColumnDatabaseName,
                  tableSchema.ProviderLastUpdateTerminalColumn.ColumnDatabaseName,
                  tableSchema.CommentsColumn.ColumnDatabaseName,
                  tableSchema.GestprojectProviderIdColumn.ColumnDatabaseName,
                  entity.PAR_ID                  
               );

               ////////////////////////////
               /// append synchronization table data to entity
               ////////////////////////////

               bool mustRegister = !new WasParticipantSynchronized(
                  connection,
                  tableSchema.TableName,
                  tableSchema.Sage50ProviderCodeColumn.ColumnDatabaseName,
                  tableSchema.GestprojectProviderIdColumn.ColumnDatabaseName,
                  entity.PAR_ID
               ).ItIs;

               ////////////////////////////
               /// determine if entity must be skipped, registered, or updated
               ////////////////////////////

               bool registeredInDifferentCompanyGroup = entity.sage50_company_group_guid_id != "" && sage50CompanyGroupData.CompanyGuidId != entity.sage50_company_group_guid_id;

               bool neverSynchronized = entity.sage50_company_group_guid_id == "";

               bool synchronizedInThePast = entity.sage50_company_group_guid_id != "" && sage50CompanyGroupData.CompanyGuidId == entity.sage50_company_group_guid_id;

               ////////////////////////////
               /// according to entity status, skip, register, or update it
               ////////////////////////////

               if(registeredInDifferentCompanyGroup)
               {
                  continue;
               }
               else if(mustRegister)
               {
                  new RegisterParticipant
                  (
                     connection,
                     tableSchema.TableName,
                     tableSchema.SynchronizationStatusColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderIdColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderNameColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderCIFNIFColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderAddressColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderPostalCodeColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderLocalityColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderProvinceColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderCountryColumn.ColumnDatabaseName,
                     entity.PAR_ID,
                     entity.fullName,
                     entity.PAR_CIF_NIF,
                     entity.PAR_DIRECCION_1,
                     entity.PAR_CP_1,
                     entity.PAR_LOCALIDAD_1,
                     entity.PAR_PROVINCIA_1,
                     entity.PAR_PAIS_1
                  );

                  new AppendSynchronizationTableDataToProvider
                  (
                     connection,
                     entity,
                     tableSchema.TableName,
                     tableSchema.SynchronizationTableProviderIdColumn.ColumnDatabaseName,
                     tableSchema.SynchronizationStatusColumn.ColumnDatabaseName,
                     tableSchema.Sage50ProviderCodeColumn.ColumnDatabaseName,
                     tableSchema.Sage50ProviderGuidIdColumn.ColumnDatabaseName,
                     tableSchema.Sage50ProviderCompanyGroupNameColumn.ColumnDatabaseName,
                     tableSchema.Sage50ProviderCompanyGroupCodeColumn.ColumnDatabaseName,
                     tableSchema.Sage50ProviderCompanyGroupMainCodeColumn.ColumnDatabaseName,
                     tableSchema.Sage50ProviderCompanyGroupGuidIdColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderParentUserIdColumn.ColumnDatabaseName,
                     tableSchema.ProviderLastUpdateTerminalColumn.ColumnDatabaseName,
                     tableSchema.CommentsColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderIdColumn.ColumnDatabaseName,
                     entity.PAR_ID
                  );
               }
               else if(neverSynchronized || synchronizedInThePast)
               {
                  new UpdateParticipantState
                  (
                     connection,
                     tableSchema.TableName,
                     tableSchema.SynchronizationStatusColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderIdColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderNameColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderCIFNIFColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderAddressColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderPostalCodeColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderLocalityColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderProvinceColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderCountryColumn.ColumnDatabaseName,
                     entity.synchronization_status,
                     entity.PAR_ID,
                     entity.fullName,
                     entity.PAR_CIF_NIF,
                     entity.PAR_DIRECCION_1,
                     entity.PAR_CP_1,
                     entity.PAR_LOCALIDAD_1,
                     entity.PAR_PROVINCIA_1,
                     entity.PAR_PAIS_1
                  );
               };

               ////////////////////////////
               /// determine entity synchronization status
               ////////////////////////////

               ValidateProviderSyncronizationStatus ProviderSyncronizationStatusValidator = new ValidateProviderSyncronizationStatus(
                  entity,
                  sage50Entities,
                  tableSchema.GestprojectProviderNameColumn.ColumnUserFriendlyNane,
                  tableSchema.GestprojectProviderCIFNIFColumn.ColumnUserFriendlyNane,
                  tableSchema.GestprojectProviderPostalCodeColumn.ColumnUserFriendlyNane,
                  tableSchema.GestprojectProviderAddressColumn.ColumnUserFriendlyNane,
                  tableSchema.GestprojectProviderProvinceColumn.ColumnUserFriendlyNane
               );

               ////////////////////////////
               /// if the entity was deleted in sage, remove it from database and re-register it
               ////////////////////////////

               if(ProviderSyncronizationStatusValidator.MustBeDeleted)
               {
                  new DeleteEntityFromSynchronizationTable(connection, entity.PAR_ID, entity.sage50_guid_id, tableSchema.TableName, tableSchema.GestprojectProviderIdColumn.ColumnDatabaseName, tableSchema.Sage50ProviderGuidIdColumn.ColumnDatabaseName);

                  new DeleteEntityCodeInGestproject(connection, entity.PAR_ID, "PARTICIPANTE", tableSchema.GestprojectProviderAccountableSubaccountColumn.ColumnDatabaseName, tableSchema.GestprojectProviderIdColumn.ColumnDatabaseName);

                  new ClearProviderSynchronizationData(entity);

                  new RegisterParticipant
                  (
                     connection,
                     tableSchema.TableName,
                     tableSchema.SynchronizationStatusColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderIdColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderNameColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderCIFNIFColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderAddressColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderPostalCodeColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderLocalityColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderProvinceColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderCountryColumn.ColumnDatabaseName,
                     entity.PAR_ID,
                     entity.fullName,
                     entity.PAR_CIF_NIF,
                     entity.PAR_DIRECCION_1,
                     entity.PAR_CP_1,
                     entity.PAR_LOCALIDAD_1,
                     entity.PAR_PROVINCIA_1,
                     entity.PAR_PAIS_1
                  );

                  new AppendSynchronizationTableDataToProvider
                  (
                     connection,
                     entity,
                     tableSchema.TableName,
                     tableSchema.SynchronizationTableProviderIdColumn.ColumnDatabaseName,
                     tableSchema.SynchronizationStatusColumn.ColumnDatabaseName,
                     tableSchema.Sage50ProviderCodeColumn.ColumnDatabaseName,
                     tableSchema.Sage50ProviderGuidIdColumn.ColumnDatabaseName,
                     tableSchema.Sage50ProviderCompanyGroupNameColumn.ColumnDatabaseName,
                     tableSchema.Sage50ProviderCompanyGroupCodeColumn.ColumnDatabaseName,
                     tableSchema.Sage50ProviderCompanyGroupMainCodeColumn.ColumnDatabaseName,
                     tableSchema.Sage50ProviderCompanyGroupGuidIdColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderParentUserIdColumn.ColumnDatabaseName,
                     tableSchema.ProviderLastUpdateTerminalColumn.ColumnDatabaseName,
                     tableSchema.CommentsColumn.ColumnDatabaseName,
                     tableSchema.GestprojectProviderIdColumn.ColumnDatabaseName,
                     entity.PAR_ID
                  );
               };

               ////////////////////////////
               /// final entity data update
               ////////////////////////////
               
               new UpdateParticipantState
               (
                  connection,
                  tableSchema.TableName,
                  tableSchema.SynchronizationStatusColumn.ColumnDatabaseName,
                  tableSchema.GestprojectProviderIdColumn.ColumnDatabaseName,
                  tableSchema.GestprojectProviderNameColumn.ColumnDatabaseName,
                  tableSchema.GestprojectProviderCIFNIFColumn.ColumnDatabaseName,
                  tableSchema.GestprojectProviderAddressColumn.ColumnDatabaseName,
                  tableSchema.GestprojectProviderPostalCodeColumn.ColumnDatabaseName,
                  tableSchema.GestprojectProviderLocalityColumn.ColumnDatabaseName,
                  tableSchema.GestprojectProviderProvinceColumn.ColumnDatabaseName,
                  tableSchema.GestprojectProviderCountryColumn.ColumnDatabaseName,
                  entity.synchronization_status,
                  entity.PAR_ID,
                  entity.fullName,
                  entity.PAR_CIF_NIF,
                  entity.PAR_DIRECCION_1,
                  entity.PAR_CP_1,
                  entity.PAR_LOCALIDAD_1,
                  entity.PAR_PROVINCIA_1,
                  entity.PAR_PAIS_1
               );

               ////////////////////////////
               /// dispatch entity
               ////////////////////////////

               processedEntities.Add(entity);
            };

            return processedEntities;
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