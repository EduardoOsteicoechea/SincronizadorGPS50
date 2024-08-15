using System;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public static class ClientSynchronizationTableSchema
   {
      public static string TableName { get; set; } = "INT_SAGE_SINC_CLIENTE";

      public static (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) SynchronizationTableClientIdColumn { get; set; } = ("ID", "Id de Sincronización", typeof(int));
      public static (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) SynchronizationStatusColumn { get; set; } = ("SYNC_STATUS", "Estado", typeof(string));
      public static (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectClientIdColumn { get; set; } = ("GP_ID", "Id de Cliente en Gestproject", typeof(int));
      public static (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) Sage50ClientCodeColumn { get; set; } = ("S50_CLIENT_CODE", "Subcuenta Contable", typeof(string));
      public static (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) Sage50ClientGuidIdColumn { get; set; } = ("S50_CLIENT_GUID_ID", "Guid de Cliente en Sage50", typeof(string));
      public static (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) Sage50ClientCompanyGroupNameColumn { get; set; } = ("S50_CLIENT_COMPANY_GROUP", "Nombre de Grupo de Empresas en Sage50", typeof(string));
      public static (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) Sage50ClientCompanyGroupGuidIdColumn { get; set; } = ("S50_CLIENT_COMPANY_GROUP_GUID_ID", "Guid de Grupo de Empresas en Sage50", typeof(string));
      public static (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) ClientLastUpdateTerminalColumn { get; set; } = ("LAST_UPDATE", "Última actualización", typeof(DateTime));
   }
}
