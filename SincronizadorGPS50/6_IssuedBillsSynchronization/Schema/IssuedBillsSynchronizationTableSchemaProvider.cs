using System;
using System.Collections.Generic;
using System.Linq;

namespace SincronizadorGPS50
{
   public class IssuedBillsSynchronizationTableSchemaProvider : ISynchronizationTableSchemaProvider
   {
      // The following static fields are necessary because properties aren't callable in other properties.
      public string TableName { get; set; } = "INT_SAGE_SYNCHRONIZATION_ENTITY_DATA_ISSUED_BILLS";

      static List<(string columnName, string friendlyName, Type columnType, string columnDefinition, dynamic defaultValue)> ColumnsTuplesItemsProvider = new List<(string, string, Type, string, dynamic)>()
      {
         /*0*/("ID", "Id de Sincronización", typeof(int), "INT PRIMARY KEY IDENTITY(1,1)", null),
         /*1*/("SYNC_STATUS", "Estado", typeof(string), "VARCHAR(MAX)", string.Empty),

         /*2*/("FCE_ID", $"Id en Gestproject", typeof(int), "INT", null),
         /*3*/("PAR_DAO_ID", "PAR_DAO_ID", typeof(int), "INT", null),
         /*4*/("FCE_REFERENCIA", "FCE_REFERENCIA", typeof(string), "VARCHAR(MAX)", string.Empty), // Corresponde a NUMERO en c_albven y FCP_NUM_FACTURA en facturas recibid
         //("FCE_NUM_FACTURA", "FCE_NUM_FACTURA", typeof(int), "INT"),
         /*5*/("FCE_FECHA", "FCE_FECHA", typeof(DateTime), "DATETIME", null),
         /*6*/("PAR_CLI_ID", "PAR_CLI_ID", typeof(int), "INT", null),
         /*7*/("FCE_BASE_IMPONIBLE", "FCE_BASE_IMPONIBLE", typeof(decimal), "DECIMAL(18,2)", null),
         /*8*/("FCE_VALOR_IVA", "FCE_VALOR_IVA", typeof(decimal), "DECIMAL(18,2)", null),
         /*9*/("FCE_IVA", "FCE_IVA", typeof(decimal), "DECIMAL(18,2)", null),
         /*10*/("FCE_VALOR_IRPF", "FCE_VALOR_IRPF", typeof(decimal), "DECIMAL(18,2)", null),
         /*11*/("FCE_IRPF", "FCE_IRPF", typeof(decimal), "DECIMAL(18,2)", null),
         /*12*/("FCE_TOTAL_SUPLIDO", "FCE_TOTAL_SUPLIDO", typeof(decimal), "DECIMAL(18,2)", null),
         /*13*/("FCE_TOTAL_FACTURA", "FCE_TOTAL_FACTURA", typeof(decimal), "DECIMAL(18,2)", null),
         /*14*/("FCE_OBSERVACIONES", "FCE_OBSERVACIONES", typeof(string), "VARCHAR(MAX)", string.Empty),

         /*15*/("S50_GUID_ID", $"Guid en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
         /*16*/("S50_COMPANY_GROUP_NAME", "Nombre de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
         /*17*/("S50_COMPANY_GROUP_CODE", "Código de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
         /*18*/("S50_COMPANY_GROUP_MAIN_CODE", "Código principal de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
         /*19*/("S50_COMPANY_GROUP_GUID_ID", "Guid de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),

         /*20*/("LAST_UPDATE", "Última actualización", typeof(DateTime),"DATETIME DEFAULT GETDATE() NOT NULL", DateTime.Now),
         /*21*/("GP_USU_ID", "Id de Gestor en Gestproject", typeof(int), "INT", null),
         /*22*/("COMMENTS", "Comentarios", typeof(string), "VARCHAR(MAX)", string.Empty),
      };
      static ((string SageDispactcherMechanism, string tableName) dispatcherAndName, List<(string name, Type type)> tableFieldsAlongTypes) SageTableDataProvider { get; set;} = (
         ("gestion","c_factucom"),
         new List<(string name, Type type)>() 
         {
            /*0*/("GUID_ID",typeof(string)),
            /*1*/("EMPRESA",typeof(string)),
            /*2*/("NUMERO",typeof(string)),
            /*3*/("FECHA",typeof(DateTime)),
            /*4*/("OBRA",typeof(string)),
            /*5*/("CLIENTE",typeof(string)),
            /*6*/("IMPORTE",typeof(decimal)),
            /*7*/("TOTALDOC",typeof(decimal)),
            /*8*/("OBSERVACIO",typeof(string)),
         }
      );      
      public ((string SageDispactcherMechanism, string tableName) dispatcherAndName, List<(string name, Type type)> tableFieldsAlongTypes) SageTableData { get; set;} =  
      (
         SageTableDataProvider.dispatcherAndName,
         SageTableDataProvider.tableFieldsAlongTypes
      );
      
      public List<(string columnName, string friendlyName, Type columnType, string columnDefinition, dynamic defaultValue)> ColumnsTuplesList { get; set; } = new List<(string, string, Type, string, dynamic)>()
      {
         ColumnsTuplesItemsProvider.ElementAt(0),
         ColumnsTuplesItemsProvider.ElementAt(1),
         ColumnsTuplesItemsProvider.ElementAt(2),
         ColumnsTuplesItemsProvider.ElementAt(3),
         ColumnsTuplesItemsProvider.ElementAt(4),
         ColumnsTuplesItemsProvider.ElementAt(5),
         ColumnsTuplesItemsProvider.ElementAt(6),
         ColumnsTuplesItemsProvider.ElementAt(7),
         ColumnsTuplesItemsProvider.ElementAt(8),
         ColumnsTuplesItemsProvider.ElementAt(9),
         ColumnsTuplesItemsProvider.ElementAt(10),
         ColumnsTuplesItemsProvider.ElementAt(11),
         ColumnsTuplesItemsProvider.ElementAt(12),
         ColumnsTuplesItemsProvider.ElementAt(13),
         ColumnsTuplesItemsProvider.ElementAt(14),
         ColumnsTuplesItemsProvider.ElementAt(15),
         ColumnsTuplesItemsProvider.ElementAt(16),
         ColumnsTuplesItemsProvider.ElementAt(17),
         ColumnsTuplesItemsProvider.ElementAt(18),
         ColumnsTuplesItemsProvider.ElementAt(19),
         ColumnsTuplesItemsProvider.ElementAt(20),
         ColumnsTuplesItemsProvider.ElementAt(21),
         ColumnsTuplesItemsProvider.ElementAt(22),
      };public List<(string columnName, Type columnType)> SynchronizationFieldsTupleList { get; set; } = new List<(string, Type)>()
      {
         (ColumnsTuplesItemsProvider.ElementAt(0).columnName,ColumnsTuplesItemsProvider.ElementAt(0).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(1).columnName,ColumnsTuplesItemsProvider.ElementAt(1).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(15).columnName,ColumnsTuplesItemsProvider.ElementAt(15).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(16).columnName,ColumnsTuplesItemsProvider.ElementAt(16).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(17).columnName,ColumnsTuplesItemsProvider.ElementAt(17).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(18).columnName,ColumnsTuplesItemsProvider.ElementAt(18).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(19).columnName,ColumnsTuplesItemsProvider.ElementAt(19).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(20).columnName,ColumnsTuplesItemsProvider.ElementAt(20).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(21).columnName,ColumnsTuplesItemsProvider.ElementAt(21).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(22).columnName,ColumnsTuplesItemsProvider.ElementAt(22).columnType),
      };
      public List<(string columnName, dynamic value)> SynchronizationFieldsDefaultValuesTupleList { get; set; } = new List<(string, dynamic)>()
      {
         (ColumnsTuplesItemsProvider.ElementAt(0).columnName,ColumnsTuplesItemsProvider.ElementAt(0).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(1).columnName,ColumnsTuplesItemsProvider.ElementAt(1).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(15).columnName,ColumnsTuplesItemsProvider.ElementAt(15).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(16).columnName,ColumnsTuplesItemsProvider.ElementAt(16).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(17).columnName,ColumnsTuplesItemsProvider.ElementAt(17).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(18).columnName,ColumnsTuplesItemsProvider.ElementAt(18).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(19).columnName,ColumnsTuplesItemsProvider.ElementAt(19).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(20).columnName,ColumnsTuplesItemsProvider.ElementAt(20).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(21).columnName,ColumnsTuplesItemsProvider.ElementAt(21).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(22).columnName,ColumnsTuplesItemsProvider.ElementAt(22).defaultValue),
      };
      public List<(string columnName, Type columnType)> GestprojectFieldsTupleList { get; set; } = new List<(string, Type)>()
      {
         (ColumnsTuplesItemsProvider.ElementAt(2).columnName,ColumnsTuplesItemsProvider.ElementAt(2).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(3).columnName,ColumnsTuplesItemsProvider.ElementAt(3).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(4).columnName,ColumnsTuplesItemsProvider.ElementAt(4).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(5).columnName,ColumnsTuplesItemsProvider.ElementAt(5).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(6).columnName,ColumnsTuplesItemsProvider.ElementAt(6).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(7).columnName,ColumnsTuplesItemsProvider.ElementAt(7).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(8).columnName,ColumnsTuplesItemsProvider.ElementAt(8).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(9).columnName,ColumnsTuplesItemsProvider.ElementAt(9).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(10).columnName,ColumnsTuplesItemsProvider.ElementAt(10).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(11).columnName,ColumnsTuplesItemsProvider.ElementAt(11).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(12).columnName,ColumnsTuplesItemsProvider.ElementAt(12).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(13).columnName,ColumnsTuplesItemsProvider.ElementAt(13).columnType)
      };




      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Id { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(0);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) SynchronizationStatus { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(1);

      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectId { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(2);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectDaoId { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(3);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectReference { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(4);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectDate { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(5);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectCliId { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(6);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectTaxableBase { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(7);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectIvaValue { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(8);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectIvaValueInEuros { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(9);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectIrpfValue { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(10);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectIrpfValueInEuros { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(11);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectTotalInvoiced { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(12);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectBillTotal { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(13);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectBillObservations { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(14);

      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Sage50GuidId { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(15);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CompanyGroupName { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(16);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CompanyGroupCode { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(17);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CompanyGroupMainCode { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(18);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CompanyGroupGuidId { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(19);

      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) LastUpdate { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(20);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) ParentUserId { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(21);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Comments { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(22);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectBillNumber { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectDescription { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) ProjectCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) AccountableSubaccount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) AccountableSubaccount2 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CommercialName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Cif { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Address { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) PostalCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Locality { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Province { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Country { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Sage50Code { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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
