﻿using System;
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

		static List<(string columnName, string friendlyName, Type columnType, string columnDefinition, dynamic defaultValue)> ColumnsTuplesItemsProvider = new List<(string, string, Type, string, dynamic)>()
		{
			("ID", "Id de Sincronización", typeof(int), "INT PRIMARY KEY IDENTITY(1,1)", null),
			("SYNC_STATUS", "Estado", typeof(string), "VARCHAR(MAX)", string.Empty),

			("PRY_ID", $"Id en Gestproject", typeof(int), "INT", null),
			("PRY_CODIGO", "Código", typeof(string), "VARCHAR(MAX)", string.Empty),
			("PRY_NOMBRE", "Nombre", typeof(string), "VARCHAR(MAX)", string.Empty),
			("PRY_DIRECCION", "Nombre comercial", typeof(string), "VARCHAR(MAX)", string.Empty),
			("PRY_LOCALIDAD", "Localidad", typeof(string), "VARCHAR(MAX)", string.Empty),
			("PRY_PROVINCIA", "Provincia", typeof(string), "VARCHAR(MAX)", string.Empty),
			("PRY_CP", "Código postal", typeof(string), "VARCHAR(MAX)", string.Empty),

			("S50_CODE", $"Código en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
			("S50_GUID_ID", $"Guid en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),

			("S50_COMPANY_GROUP_NAME", "Nombre de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
			("S50_COMPANY_GROUP_CODE", "Código de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
			("S50_COMPANY_GROUP_MAIN_CODE", "Código principal de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
			("S50_COMPANY_GROUP_GUID_ID", "Guid de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),

			("LAST_UPDATE", "Última actualización", typeof(DateTime),"DATETIME DEFAULT GETDATE() NOT NULL", DateTime.Now),
			("GP_USU_ID", "Id de Gestor en Gestproject", typeof(int), "INT", null),
			("COMMENTS", "Comentarios", typeof(string), "VARCHAR(MAX)", string.Empty),
		};

		public string TableName { get; set; } = "INT_SAGE_SYNCHRONIZATION_ENTITY_DATA_PROJECTS";
		public List<(string columnName, string friendlyName, Type columnType, string columnDefinition, dynamic defaultValue)> ColumnsTuplesList { get; set; } = new List<(string, string, Type, string, dynamic)>()
		{
			("ID", "Id de Sincronización", typeof(int), "INT PRIMARY KEY IDENTITY(1,1)", null),
			("SYNC_STATUS", "Estado", typeof(string), "VARCHAR(MAX)", string.Empty),

			("PRY_ID", $"Id en Gestproject", typeof(int), "INT", null),
			("PRY_CODIGO", "Código", typeof(string), "VARCHAR(MAX)", string.Empty),
			("PRY_NOMBRE", "Nombre", typeof(string), "VARCHAR(MAX)", string.Empty),
			("PRY_DIRECCION", "Nombre comercial", typeof(string), "VARCHAR(MAX)", string.Empty),
			("PRY_LOCALIDAD", "Localidad", typeof(string), "VARCHAR(MAX)", string.Empty),
			("PRY_PROVINCIA", "Provincia", typeof(string), "VARCHAR(MAX)", string.Empty),
			("PRY_CP", "Código postal", typeof(string), "VARCHAR(MAX)", string.Empty),

			("S50_CODE", $"Código en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
			("S50_GUID_ID", $"Guid en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),

			("S50_COMPANY_GROUP_NAME", "Nombre de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
			("S50_COMPANY_GROUP_CODE", "Código de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
			("S50_COMPANY_GROUP_MAIN_CODE", "Código principal de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
			("S50_COMPANY_GROUP_GUID_ID", "Guid de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),

			("LAST_UPDATE", "Última actualización", typeof(DateTime),"DATETIME DEFAULT GETDATE() NOT NULL", DateTime.Now),
			("GP_USU_ID", "Id de Gestor en Gestproject", typeof(int), "INT", null),
			("COMMENTS", "Comentarios", typeof(string), "VARCHAR(MAX)", string.Empty),
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




		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Id { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(0).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(0).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(0).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(0).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(0).defaultValue
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) SynchronizationStatus { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(1).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(1).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(1).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(1).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(1).defaultValue
		);


		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectId { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(2).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(2).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(2).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(2).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(2).defaultValue
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) ProjectCode { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(3).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(3).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(3).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(3).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(3).defaultValue
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Name { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(4).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(4).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(4).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(4).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(4).defaultValue
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Address { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(5).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(5).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(5).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(5).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(5).defaultValue
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Locality { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(6).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(6).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(6).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(6).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(6).defaultValue
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Province { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(7).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(7).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(7).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(7).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(7).defaultValue
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) PostalCode { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(8).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(8).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(8).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(8).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(8).defaultValue
		);



		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Sage50Code { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(9).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(9).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(9).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(9).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(9).defaultValue
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Sage50GuidId { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(10).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(10).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(10).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(10).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(10).defaultValue
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CompanyGroupName { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(11).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(11).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(11).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(11).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(11).defaultValue
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CompanyGroupCode { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(12).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(12).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(12).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(12).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(12).defaultValue
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CompanyGroupMainCode { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(13).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(13).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(13).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(13).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(13).defaultValue
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CompanyGroupGuidId { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(14).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(14).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(14).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(14).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(14).defaultValue
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) LastUpdate { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(15).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(15).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(15).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(15).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(15).defaultValue
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) ParentUserId { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(16).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(16).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(16).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(16).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(16).defaultValue
		);
		public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Comments { get; set; } = (
		   ColumnsTuplesItemsProvider.ElementAt(17).columnName,
		   ColumnsTuplesItemsProvider.ElementAt(17).friendlyName,
		   ColumnsTuplesItemsProvider.ElementAt(17).columnType,
		   ColumnsTuplesItemsProvider.ElementAt(17).columnDefinition,
		   ColumnsTuplesItemsProvider.ElementAt(17).defaultValue
		);
      public ((string SageDispactcherMechanism, string tableName) dispatcherAndName, List<(string name, Type type)> tableFieldsAlongTypes) SageTableData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectDaoId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectReference { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectBillNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectCliId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectTaxableBase { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectIvaValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectIvaValueInEuros { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectIrpfValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectIrpfValueInEuros { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectTotalInvoiced { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectBillTotal { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectBillObservations { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectDescription { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) AccountableSubaccount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) AccountableSubaccount2 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CommercialName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Cif { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Country { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Iva { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CtaIvRep { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CtaIvSop { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Withholding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CtaReRep { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CtaReSop { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) TaxType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectProId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
   }
}
