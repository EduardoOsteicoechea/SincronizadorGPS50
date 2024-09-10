using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sage.ew.docsven.FirmaElectronica;

namespace SincronizadorGPS50
{
   public class NewGestprojectTaxesTableSchemaProvider
   {
      // The following static fields are necessary because properties aren't callable in other properties.
      // Keep the "ColumnsTuplesItemsProvider" as an exact copy of it's corresponding property.

      public string TableName { get; set; } = "INT_SAGE_NEW_GESTPROJECT_TAXES";

      static List<(string columnName, string friendlyName, Type columnType, string columnDefinition)> ColumnsTuplesItemsProvider = new List<(string, string, Type, string)>()
      {
         ("ID", "Id de Sincronización", typeof(int), "INT PRIMARY KEY IDENTITY(1,1)"),
         ("NOMBRE", "Nombre de impuesto en Sage50", typeof(string), "VARCHAR(MAX)"),

         ("IMP_VALOR", "Valor en Gestproject", typeof(decimal), "DECIMAL(18,2)"),

         ("IVA", "IVA", typeof(decimal), "DECIMAL(18,2)"),
         ("CTA_IV_REP", "CTA_IV_REP", typeof(string), "VARCHAR(MAX)"),
         ("CTA_IV_SOP", "CTA_IV_SOP", typeof(string), "VARCHAR(MAX)"),

         ("RETENCION", "Retención", typeof(decimal), "DECIMAL(18,2)"),
         ("CTA_RE_REP", "CTA_RE_REP", typeof(string), "VARCHAR(MAX)"),
         ("CTA_RE_SOP", "CTA_RE_SOP", typeof(string), "VARCHAR(MAX)"),

         ("TAX_TYPE", "Tipo de impuesto", typeof(string), "VARCHAR(MAX)"),

         ("IMP_SUBCTA_CONTABLE", "Subcuenta contable", typeof(string), "VARCHAR(MAX)"),
         ("IMP_SUBCTA_CONTABLE_2", "Subcuenta contable 2", typeof(string), "VARCHAR(MAX)"),
         ("S50_GUID_ID", $"Guid en Sage50", typeof(string), "VARCHAR(MAX)"),
         ("IMP_ID", $"Id en Gestproject", typeof(string), "VARCHAR(MAX)"),

         ("IMP_TIPO", "Tipo de impuesto en Gestproject", typeof(string), "VARCHAR(MAX)"),
         ("IMP_NOMBRE", "Nombre de impuesto en Gestproject", typeof(string), "VARCHAR(MAX)"),

      };

      public List<(string columnName, string friendlyName, Type columnType, string columnDefinition)> ColumnsTuplesList { get; set; } = new List<(string, string, Type, string)>()
      {
         ("ID", "Id de Sincronización", typeof(int), "INT PRIMARY KEY IDENTITY(1,1)"),
         ("NOMBRE", "Nombre de impuesto en Sage50", typeof(string), "VARCHAR(MAX)"),

         ("IMP_VALOR", "Valor en Gestproject", typeof(decimal), "DECIMAL(18,2)"),

         ("IVA", "IVA", typeof(decimal), "DECIMAL(18,2)"),
         ("CTA_IV_REP", "CTA_IV_REP", typeof(string), "VARCHAR(MAX)"),
         ("CTA_IV_SOP", "CTA_IV_SOP", typeof(string), "VARCHAR(MAX)"),

         ("RETENCION", "Retención", typeof(decimal), "DECIMAL(18,2)"),
         ("CTA_RE_REP", "CTA_RE_REP", typeof(string), "VARCHAR(MAX)"),
         ("CTA_RE_SOP", "CTA_RE_SOP", typeof(string), "VARCHAR(MAX)"),

         ("TAX_TYPE", "Tipo de impuesto", typeof(string), "VARCHAR(MAX)"),
         ("S50_GUID_ID", $"Guid en Sage50", typeof(string), "VARCHAR(MAX)"),
         ("IMP_ID", $"Id en Gestproject", typeof(string), "VARCHAR(MAX)"),

         ("IMP_TIPO", "Tipo de impuesto en Gestproject", typeof(string), "VARCHAR(MAX)"),
         ("IMP_NOMBRE", "Nombre de impuesto en Gestproject", typeof(string), "VARCHAR(MAX)"),
      };
      public List<(string columnName, dynamic value)> SynchronizationFieldsDefaultValuesTupleList { get; set; } = new List<(string, dynamic)>()
      {
         ("NOMBRE", ""),

         ("IMP_VALOR", 0.00),

         ("IVA", 0.00),
         ("CTA_IV_REP", ""),
         ("CTA_IV_SOP", ""),

         ("RETENCION", 0.00),
         ("CTA_RE_REP", ""),
         ("CTA_RE_SOP", ""),

         ("TAX_TYPE", ""),

         ("IMP_SUBCTA_CONTABLE", ""),
         ("IMP_SUBCTA_CONTABLE_2", ""),
         ("S50_GUID_ID", ""),
         ("IMP_ID", 0),
         ("IMP_TIPO", ""),
         ("IMP_NOMBRE", ""),
      };
      public List<(string columnName, Type columnType)> GestprojectFieldsTupleList { get; set; } = new List<(string, Type)>()
      {
         ("NOMBRE", typeof(string)),

         ("IMP_VALOR", typeof(decimal)),

         ("IVA", typeof(decimal)),
         ("CTA_IV_REP", typeof(string)),
         ("CTA_IV_SOP", typeof(string)),

         ("RETENCION", typeof(decimal)),
         ("CTA_RE_REP", typeof(string)),
         ("CTA_RE_SOP", typeof(string)),

         ("TAX_TYPE", typeof(string)),

         ("IMP_SUBCTA_CONTABLE", typeof(string)),
         ("IMP_SUBCTA_CONTABLE_2", typeof(string)),
         ("S50_GUID_ID", typeof(string)),
         ("IMP_ID", typeof(int)),

         ("IMP_TIPO", typeof(string)),
         ("IMP_NOMBRE", typeof(string)),
      };




      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Id { get; set; } = (
         ColumnsTuplesItemsProvider.ElementAt(0).columnName,
         ColumnsTuplesItemsProvider.ElementAt(0).friendlyName,
         ColumnsTuplesItemsProvider.ElementAt(0).columnType,
         ColumnsTuplesItemsProvider.ElementAt(0).columnDefinition
      );
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Value { get; set; } = (
         ColumnsTuplesItemsProvider.ElementAt(1).columnName,
         ColumnsTuplesItemsProvider.ElementAt(1).friendlyName,
         ColumnsTuplesItemsProvider.ElementAt(1).columnType,
         ColumnsTuplesItemsProvider.ElementAt(1).columnDefinition
      );
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Name { get; set; } = (
         ColumnsTuplesItemsProvider.ElementAt(2).columnName,
         ColumnsTuplesItemsProvider.ElementAt(2).friendlyName,
         ColumnsTuplesItemsProvider.ElementAt(2).columnType,
         ColumnsTuplesItemsProvider.ElementAt(2).columnDefinition
      );
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Iva { get; set; } = (
         ColumnsTuplesItemsProvider.ElementAt(3).columnName,
         ColumnsTuplesItemsProvider.ElementAt(3).friendlyName,
         ColumnsTuplesItemsProvider.ElementAt(3).columnType,
         ColumnsTuplesItemsProvider.ElementAt(3).columnDefinition
      );
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CtaIvRep { get; set; } = (
         ColumnsTuplesItemsProvider.ElementAt(4).columnName,
         ColumnsTuplesItemsProvider.ElementAt(4).friendlyName,
         ColumnsTuplesItemsProvider.ElementAt(4).columnType,
         ColumnsTuplesItemsProvider.ElementAt(4).columnDefinition
      );
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CtaIvSop { get; set; } = (
         ColumnsTuplesItemsProvider.ElementAt(5).columnName,
         ColumnsTuplesItemsProvider.ElementAt(5).friendlyName,
         ColumnsTuplesItemsProvider.ElementAt(5).columnType,
         ColumnsTuplesItemsProvider.ElementAt(5).columnDefinition
      );
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Withholding { get; set; } = (
         ColumnsTuplesItemsProvider.ElementAt(6).columnName,
         ColumnsTuplesItemsProvider.ElementAt(6).friendlyName,
         ColumnsTuplesItemsProvider.ElementAt(6).columnType,
         ColumnsTuplesItemsProvider.ElementAt(6).columnDefinition
      );
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CtaReRep { get; set; } = (
         ColumnsTuplesItemsProvider.ElementAt(7).columnName,
         ColumnsTuplesItemsProvider.ElementAt(7).friendlyName,
         ColumnsTuplesItemsProvider.ElementAt(7).columnType,
         ColumnsTuplesItemsProvider.ElementAt(7).columnDefinition
      );
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) CtaReSop { get; set; } = (
         ColumnsTuplesItemsProvider.ElementAt(8).columnName,
         ColumnsTuplesItemsProvider.ElementAt(8).friendlyName,
         ColumnsTuplesItemsProvider.ElementAt(8).columnType,
         ColumnsTuplesItemsProvider.ElementAt(8).columnDefinition
      );
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) TaxType { get; set; } = (
         ColumnsTuplesItemsProvider.ElementAt(9).columnName,
         ColumnsTuplesItemsProvider.ElementAt(9).friendlyName,
         ColumnsTuplesItemsProvider.ElementAt(9).columnType,
         ColumnsTuplesItemsProvider.ElementAt(9).columnDefinition
      );
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) AccountableSubaccount { get; set; } = (
         ColumnsTuplesItemsProvider.ElementAt(10).columnName,
         ColumnsTuplesItemsProvider.ElementAt(10).friendlyName,
         ColumnsTuplesItemsProvider.ElementAt(10).columnType,
         ColumnsTuplesItemsProvider.ElementAt(10).columnDefinition
      );

      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) AccountableSubaccount2 { get; set; } = (
         ColumnsTuplesItemsProvider.ElementAt(11).columnName,
         ColumnsTuplesItemsProvider.ElementAt(11).friendlyName,
         ColumnsTuplesItemsProvider.ElementAt(11).columnType,
         ColumnsTuplesItemsProvider.ElementAt(11).columnDefinition
      );

      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition) Sage50GuidId { get; set; } = (
         ColumnsTuplesItemsProvider.ElementAt(12).columnName,
         ColumnsTuplesItemsProvider.ElementAt(12).friendlyName,
         ColumnsTuplesItemsProvider.ElementAt(12).columnType,
         ColumnsTuplesItemsProvider.ElementAt(12).columnDefinition
      );
   }
}
