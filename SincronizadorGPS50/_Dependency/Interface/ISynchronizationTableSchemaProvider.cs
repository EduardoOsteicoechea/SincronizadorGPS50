using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
   public interface ISynchronizationTableSchemaProvider
   {
      string TableName { get; set; }
      List<(string columnName, string friendlyName, Type columnType, string columnDefinition)> ColumnsTuplesList { get; set; }
      List<(string columnName, Type columnType)> SynchronizationFieldsTupleList { get; set; }
      List<(string columnName, Type columnType)> GestprojectFieldsTupleList { get; set; }
      List<(string columnName, dynamic value)> SynchronizationFieldsDefaultValuesTupleList { get; set; }











      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Id { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) SynchronizationStatus { get; set; }


      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) GestprojectId { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) GestprojectType { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) GestprojectName { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) GestprojectValue { get; set; }
		(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) ProjectCode { get; set; }
		(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) AccountableSubaccount { get; set; }
		(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) AccountableSubaccount2 { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Name { get; set; }
		(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CommercialName { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Cif { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Address { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) PostalCode { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Locality { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Province { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Country { get; set; }


      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Sage50Code { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Sage50GuidId { get; set; }


      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CompanyGroupName { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CompanyGroupCode { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CompanyGroupMainCode { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CompanyGroupGuidId { get; set; }


      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) LastUpdate { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) ParentUserId { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Comments { get; set; }





      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Sage50Type { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Iva { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CtaIvRep { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CtaIvSop { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Irpf { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Withholding { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CtaReRep { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CtaReSop{ get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) TaxType { get; set; }




      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) SynchronizationTableProviderIdColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) SynchronizationStatusColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderIdColumn { get; set; }

      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderAccountableSubaccountColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderNameColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderCommercialNameColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderCIFNIFColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderAddressColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderPostalCodeColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderLocalityColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderProvinceColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderCountryColumn { get; set; }

      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) Sage50ProviderCodeColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) Sage50ProviderGuidIdColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) Sage50ProviderCompanyGroupNameColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) Sage50ProviderCompanyGroupCodeColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) Sage50ProviderCompanyGroupMainCodeColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) Sage50ProviderCompanyGroupGuidIdColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) CommentsColumn { get; set; }
      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) ProviderLastUpdateTerminalColumn { get; set; }

      //(string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType) GestprojectProviderParentUserIdColumn { get; set; }
   }
}
