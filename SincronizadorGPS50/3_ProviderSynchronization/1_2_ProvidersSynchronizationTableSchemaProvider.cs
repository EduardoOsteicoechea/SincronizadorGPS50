using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
   public class ProvidersSynchronizationTableSchemaProvider : ISynchronizationTableSchemaProvider
   {
      public string TableName { get; set; } = "INT_SAGE_PROVIDERS_SYNCRONIZATION_DATA";
      public List<(string columnName, string friendlyName,Type columnType)> ColumnsTuplesList { get; set; } = new List<(string, string, Type)>()
      {
          ("ID", "Id de Sincronización", typeof(int)),
          ("SYNC_STATUS", "Estado", typeof(string)),
          ("PAR_ID", "Id de Proveedor en Gestproject", typeof(int)),
          ("PAR_SUBCTA_CONTABLE_2", "Subcuenta contable", typeof(string)),
          ("NOMBRE_COMPLETO", "Nombre", typeof(string)),
          ("PAR_NOMBRE_COMERCIAL", "Nombre comercial", typeof(string)),
          ("PAR_CIF_NIF", "CIF - NIF", typeof(string)),
          ("PAR_DIRECCION_1", "Dirección", typeof(string)),
          ("PAR_CP_1", "Código postal", typeof(string)),
          ("PAR_LOCALIDAD_1", "Localidad", typeof(string)),
          ("PAR_PROVINCIA_1", "Provincia", typeof(string)),
          ("PAR_PAIS_1", "País", typeof(string)),
          ("S50_Provider_CODE", "Código de Proveedor", typeof(string)),
          ("S50_Provider_GUID_ID", "Guid de Proveedor en Sage50", typeof(string)),
          ("S50_Provider_COMPANY_GROUP_NAME", "Nombre de Grupo de Empresas en Sage50", typeof(string)),
          ("S50_Provider_COMPANY_GROUP_CODE", "Código de Grupo de Empresas en Sage50", typeof(string)),
          ("S50_Provider_COMPANY_GROUP_MAIN_CODE", "Código principal de Grupo de Empresas en Sage50", typeof(string)),
          ("S50_Provider_COMPANY_GROUP_GUID_ID", "Guid de Grupo de Empresas en Sage50", typeof(string)),
          ("COMMENTS", "Comentarios", typeof(string)),
          ("LAST_UPDATE", "Última actualización", typeof(DateTime)),
          ("GP_USU_ID", "Id de Gestor en Gestproject", typeof(int))
      };      
      
      // I need this becauseI can't call this in the other properties. This must always be an exact copy of the above
      public static List<(string columnName, string friendlyName,Type columnType)> ColumnsTuplesItemsProvider { get; set; } = new List<(string, string, Type)>()
      {
          ("ID", "Id de Sincronización", typeof(int)),
          ("SYNC_STATUS", "Estado", typeof(string)),
          ("PAR_ID", "Id de Proveedor en Gestproject", typeof(int)),
          ("PAR_SUBCTA_CONTABLE_2", "Subcuenta contable", typeof(string)),
          ("NOMBRE_COMPLETO", "Nombre", typeof(string)),
          ("PAR_NOMBRE_COMERCIAL", "Nombre comercial", typeof(string)),
          ("PAR_CIF_NIF", "CIF - NIF", typeof(string)),
          ("PAR_DIRECCION_1", "Dirección", typeof(string)),
          ("PAR_CP_1", "Código postal", typeof(string)),
          ("PAR_LOCALIDAD_1", "Localidad", typeof(string)),
          ("PAR_PROVINCIA_1", "Provincia", typeof(string)),
          ("PAR_PAIS_1", "País", typeof(string)),
          ("S50_Provider_CODE", "Código de Proveedor", typeof(string)),
          ("S50_Provider_GUID_ID", "Guid de Proveedor en Sage50", typeof(string)),
          ("S50_Provider_COMPANY_GROUP_NAME", "Nombre de Grupo de Empresas en Sage50", typeof(string)),
          ("S50_Provider_COMPANY_GROUP_CODE", "Código de Grupo de Empresas en Sage50", typeof(string)),
          ("S50_Provider_COMPANY_GROUP_MAIN_CODE", "Código principal de Grupo de Empresas en Sage50", typeof(string)),
          ("S50_Provider_COMPANY_GROUP_GUID_ID", "Guid de Grupo de Empresas en Sage50", typeof(string)),
          ("COMMENTS", "Comentarios", typeof(string)),
          ("LAST_UPDATE", "Última actualización", typeof(DateTime)),
          ("GP_USU_ID", "Id de Gestor en Gestproject", typeof(int))
      };




      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) SynchronizationTableProviderIdColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(0).columnName, ColumnsTuplesItemsProvider.ElementAt(0).friendlyName, ColumnsTuplesItemsProvider.ElementAt(0).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) SynchronizationStatusColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(1).columnName, ColumnsTuplesItemsProvider.ElementAt(1).friendlyName, ColumnsTuplesItemsProvider.ElementAt(1).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderIdColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(2).columnName, ColumnsTuplesItemsProvider.ElementAt(2).friendlyName, ColumnsTuplesItemsProvider.ElementAt(2).columnType);





      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderAccountableSubaccountColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(3).columnName, ColumnsTuplesItemsProvider.ElementAt(3).friendlyName, ColumnsTuplesItemsProvider.ElementAt(3).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderNameColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(4).columnName, ColumnsTuplesItemsProvider.ElementAt(4).friendlyName, ColumnsTuplesItemsProvider.ElementAt(4).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderCommercialNameColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(5).columnName, ColumnsTuplesItemsProvider.ElementAt(5).friendlyName, ColumnsTuplesItemsProvider.ElementAt(5).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderCIFNIFColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(6).columnName, ColumnsTuplesItemsProvider.ElementAt(6).friendlyName, ColumnsTuplesItemsProvider.ElementAt(6).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderAddressColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(7).columnName, ColumnsTuplesItemsProvider.ElementAt(7).friendlyName, ColumnsTuplesItemsProvider.ElementAt(7).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderPostalCodeColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(8).columnName, ColumnsTuplesItemsProvider.ElementAt(8).friendlyName, ColumnsTuplesItemsProvider.ElementAt(8).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderLocalityColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(9).columnName, ColumnsTuplesItemsProvider.ElementAt(9).friendlyName, ColumnsTuplesItemsProvider.ElementAt(9).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderProvinceColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(10).columnName, ColumnsTuplesItemsProvider.ElementAt(10).friendlyName, ColumnsTuplesItemsProvider.ElementAt(10).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderCountryColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(0).columnName, ColumnsTuplesItemsProvider.ElementAt(0).friendlyName, ColumnsTuplesItemsProvider.ElementAt(0).columnType);


      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) Sage50ProviderCodeColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(12).columnName, ColumnsTuplesItemsProvider.ElementAt(12).friendlyName, ColumnsTuplesItemsProvider.ElementAt(12).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) Sage50ProviderGuidIdColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(13).columnName, ColumnsTuplesItemsProvider.ElementAt(13).friendlyName, ColumnsTuplesItemsProvider.ElementAt(13).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) Sage50ProviderCompanyGroupNameColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(14).columnName, ColumnsTuplesItemsProvider.ElementAt(14).friendlyName, ColumnsTuplesItemsProvider.ElementAt(14).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) Sage50ProviderCompanyGroupCodeColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(15).columnName, ColumnsTuplesItemsProvider.ElementAt(15).friendlyName, ColumnsTuplesItemsProvider.ElementAt(15).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) Sage50ProviderCompanyGroupMainCodeColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(16).columnName, ColumnsTuplesItemsProvider.ElementAt(16).friendlyName, ColumnsTuplesItemsProvider.ElementAt(16).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) Sage50ProviderCompanyGroupGuidIdColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(17).columnName, ColumnsTuplesItemsProvider.ElementAt(17).friendlyName, ColumnsTuplesItemsProvider.ElementAt(17).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) CommentsColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(18).columnName, ColumnsTuplesItemsProvider.ElementAt(18).friendlyName, ColumnsTuplesItemsProvider.ElementAt(18).columnType);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) ProviderLastUpdateTerminalColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(19).columnName, ColumnsTuplesItemsProvider.ElementAt(19).friendlyName, ColumnsTuplesItemsProvider.ElementAt(19).columnType);



      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderParentUserIdColumn { get; set; } = (ColumnsTuplesItemsProvider.ElementAt(20).columnName, ColumnsTuplesItemsProvider.ElementAt(20).friendlyName, ColumnsTuplesItemsProvider.ElementAt(20).columnType);
   }
}
