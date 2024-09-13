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
      List<(string columnName, string friendlyName, Type columnType, string columnDefinition, dynamic defaultValue)> ColumnsTuplesList { get; set; }
      List<(string columnName, Type columnType)> SynchronizationFieldsTupleList { get; set; }
      List<(string columnName, Type columnType)> GestprojectFieldsTupleList { get; set; }
      //List<(string columnName, Type columnType)> Sage50FieldsTupleList { get; set; }
      List<(string columnName, dynamic value)> SynchronizationFieldsDefaultValuesTupleList { get; set; }      
      ((string SageDispactcherMechanism, string tableName) dispatcherAndName, List<(string name, Type type)> tableFieldsAlongTypes) SageTableData {get;set;}
      




      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectProId { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectDaoId { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectReference { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectBillNumber { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectDate { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectCliId { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectTaxableBase { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectIvaValue { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectIvaValueInEuros { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectIrpfValue { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectIrpfValueInEuros { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectTotalInvoiced { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectBillTotal { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectBillObservations { get; set; }


      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Id { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) SynchronizationStatus { get; set; }


      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectId { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectType { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectName { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectDescription { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectValue { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) ProjectCode { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) AccountableSubaccount { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) AccountableSubaccount2 { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Name { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CommercialName { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Cif { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Address { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) PostalCode { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Locality { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Province { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Country { get; set; }


      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Sage50Code { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Sage50GuidId { get; set; }


      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CompanyGroupName { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CompanyGroupCode { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CompanyGroupMainCode { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CompanyGroupGuidId { get; set; }


      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) LastUpdate { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) ParentUserId { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Comments { get; set; }





      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Iva { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CtaIvRep { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CtaIvSop { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Withholding { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CtaReRep { get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CtaReSop{ get; set; }
      (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) TaxType { get; set; }
   }
}
