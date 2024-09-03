using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
	public class ProjectsSynchronizationTableSchemaProvider : ISynchronizationTableSchemaProvider
	{
		// The following static fields are necessary because properties aren't callable in other properties.
		// Keep the "ColumnsTuplesItemsProvider" as an exact copy of it's corresponding property.

		static List<(string columnName, string friendlyName, Type columnType, string columnDefinition)> ColumnsTuplesItemsProvider = new List<(string, string, Type, string)>()
		{
			("ID", "Id de Sincronización", typeof(int), "INT PRIMARY KEY IDENTITY(1,1)"),
			("SYNC_STATUS", "Estado", typeof(string), "VARCHAR(MAX)"),

			("PRY_ID", $"Id en Gestproject", typeof(int), "INT"),
			("PRY_CODIGO", "Código", typeof(string), "VARCHAR(MAX)"),
			("PRY_NOMBRE", "Nombre", typeof(string), "VARCHAR(MAX)"),
			("PRY_DIRECCION", "Nombre comercial", typeof(string), "VARCHAR(MAX)"),
			("PRY_LOCALIDAD", "Localidad", typeof(string), "VARCHAR(MAX)"),
			("PRY_PROVINCIA", "Provincia", typeof(string), "VARCHAR(MAX)"),
			("PRY_CP", "Código postal", typeof(string), "VARCHAR(MAX)"),

			("S50_CODE", $"Código en Sage50", typeof(string), "VARCHAR(MAX)"),
			("S50_GUID_ID", $"Guid en Sage50", typeof(string), "VARCHAR(MAX)"),

			("S50_COMPANY_GROUP_NAME", "Nombre de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)"),
			("S50_COMPANY_GROUP_CODE", "Código de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)"),
			("S50_COMPANY_GROUP_MAIN_CODE", "Código principal de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)"),
			("S50_COMPANY_GROUP_GUID_ID", "Guid de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)"),

			("LAST_UPDATE", "Última actualización", typeof(DateTime),"DATETIME DEFAULT GETDATE() NOT NULL" ),
			("GP_USU_ID", "Id de Gestor en Gestproject", typeof(int), "INT"),
			("COMMENTS", "Comentarios", typeof(string), "VARCHAR(MAX)"),
		};

		public string TableName { get; set; } = "INT_SAGE_SYNCHRONIZATION_ENTITY_DATA_PROJECTS";
		public List<(string columnName, string friendlyName, Type columnType, string columnDefinition)> ColumnsTuplesList { get; set; } = new List<(string, string, Type, string)>()
		{
			("ID", "Id de Sincronización", typeof(int), "INT PRIMARY KEY IDENTITY(1,1)"),
			("SYNC_STATUS", "Estado", typeof(string), "VARCHAR(MAX)"),

			("PRY_ID", $"Id en Gestproject", typeof(int), "INT"),
			("PRY_CODIGO", "Código", typeof(string), "VARCHAR(MAX)"),
			("PRY_NOMBRE", "Nombre", typeof(string), "VARCHAR(MAX)"),
			("PRY_DIRECCION", "Nombre comercial", typeof(string), "VARCHAR(MAX)"),
			("PRY_LOCALIDAD", "Localidad", typeof(string), "VARCHAR(MAX)"),
			("PRY_PROVINCIA", "Provincia", typeof(string), "VARCHAR(MAX)"),
			("PRY_CP", "Código postal", typeof(string), "VARCHAR(MAX)"),

			("S50_CODE", $"Código en Sage50", typeof(string), "VARCHAR(MAX)"),
			("S50_GUID_ID", $"Guid en Sage50", typeof(string), "VARCHAR(MAX)"),

			("S50_COMPANY_GROUP_NAME", "Nombre de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)"),
			("S50_COMPANY_GROUP_CODE", "Código de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)"),
			("S50_COMPANY_GROUP_MAIN_CODE", "Código principal de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)"),
			("S50_COMPANY_GROUP_GUID_ID", "Guid de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)"),

			("LAST_UPDATE", "Última actualización", typeof(DateTime),"DATETIME DEFAULT GETDATE() NOT NULL" ),
			("GP_USU_ID", "Id de Gestor en Gestproject", typeof(int), "INT"),
			("COMMENTS", "Comentarios", typeof(string), "VARCHAR(MAX)"),
		};
		public List<(string columnName, Type columnType)> SynchronizationFieldsTupleList { get; set; } = new List<(string, Type)>()
		{
			("ID", typeof(int)),
			("SYNC_STATUS", typeof(string)),

			("S50_CODE", typeof(string)),
			("S50_GUID_ID", typeof(string)),

			("S50_COMPANY_GROUP_NAME", typeof(string)),
			("S50_COMPANY_GROUP_CODE", typeof(string)),
			("S50_COMPANY_GROUP_MAIN_CODE", typeof(string)),
			("S50_COMPANY_GROUP_GUID_ID", typeof(string)),

			("LAST_UPDATE", typeof(DateTime)),
			("GP_USU_ID", typeof(int)),
			("COMMENTS", typeof(string)),
		};
		public List<(string columnName, dynamic value)> SynchronizationFieldsDefaultValuesTupleList { get; set; } = new List<(string, dynamic)>()
		{
			("ID", null),
			("SYNC_STATUS", SynchronizationStatusOptions.Desincronizado),

			("S50_CODE", ""),
			("S50_GUID_ID", ""),

			("S50_COMPANY_GROUP_NAME", ""),
			("S50_COMPANY_GROUP_CODE", ""),
			("S50_COMPANY_GROUP_MAIN_CODE", ""),
			("S50_COMPANY_GROUP_GUID_ID", ""),

			("LAST_UPDATE", null),
			("GP_USU_ID", null),
			("COMMENTS", ""),
		};
		public List<(string columnName, Type columnType)> GestprojectFieldsTupleList { get; set; } = new List<(string, Type)>()
		{
			("PRY_ID", typeof(int)),
			("PRY_CODIGO", typeof(string)),
			("PRY_NOMBRE", typeof(string)),
			("PRY_DIRECCION", typeof(string)),
			("PRY_LOCALIDAD", typeof(string)),
			("PRY_PROVINCIA", typeof(string)),
			("PRY_CP", typeof(string)),
		};




		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Id { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(0).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(0).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(0).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(0).columnDefinition
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) SynchronizationStatus { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(1).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(1).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(1).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(1).columnDefinition
		);


		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) GestprojectId { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(2).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(2).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(2).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(2).columnDefinition
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) AccountableSubaccount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) ProjectCode { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(3).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(3).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(3).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(3).columnDefinition
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Name { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(4).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(4).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(4).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(4).columnDefinition
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CommercialName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Cif { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Address { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(5).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(5).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(5).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(5).columnDefinition
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Locality { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(6).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(6).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(6).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(6).columnDefinition
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Province { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(7).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(7).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(7).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(7).columnDefinition
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) PostalCode { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(8).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(8).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(8).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(8).columnDefinition
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Country { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }



		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Sage50Code { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(9).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(9).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(9).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(9).columnDefinition
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Sage50GuidId { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(10).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(10).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(10).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(10).columnDefinition
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CompanyGroupName { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(11).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(11).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(11).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(11).columnDefinition
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CompanyGroupCode { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(12).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(12).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(12).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(12).columnDefinition
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CompanyGroupMainCode { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(13).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(13).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(13).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(13).columnDefinition
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CompanyGroupGuidId { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(14).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(14).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(14).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(14).columnDefinition
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) LastUpdate { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(15).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(15).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(15).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(15).columnDefinition
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) ParentUserId { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(16).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(16).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(16).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(16).columnDefinition
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Comments { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(17).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(17).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(17).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(17).columnDefinition
		);
	}
}
