using System;
using System.Collections.Generic;
using System.Linq;

namespace SincronizadorGPS50
{
   public class ReceivedBillsSynchronizationTableSchemaProvider : ISynchronizationTableSchemaProvider
   {
      // The following static fields are necessary because properties aren't callable in other properties.     
      
      public string TableName { get; set; } = "INT_SAGE_SYNCHRONIZATION_ENTITY_DATA_RECEIVED_BILLS";

      static List<(string columnName, string friendlyName, Type columnType, string columnDefinition, dynamic defaultValue)> ColumnsTuplesItemsProvider = new List<(string, string, Type, string, dynamic)>()
      {
         /*0*/("ID", "Id de Sincronización", typeof(int), "INT PRIMARY KEY IDENTITY(1,1)", null),
         /*1*/("SYNC_STATUS", "Estado", typeof(string), "VARCHAR(MAX)", SynchronizationStatusOptions.Desincronizado),

         /*2*/("FCP_ID", $"Id en Gestproject", typeof(int), "INT", null),
         /*3*/("PAR_DAO_ID", "PAR_DAO_ID", typeof(int), "INT", null),

         /*4*/("FCP_NUM_FACTURA", "FCP_NUM_FACTURA", typeof(string), "VARCHAR(MAX)", string.Empty), // Corresponde a NUMERO en "c_factucom" y FCE_REFERENCIA en facturas emitidas
         /*5*/("FCP_FECHA", "FCP_FECHA", typeof(DateTime), "DATETIME", null),
         /*6*/("PAR_PRO_ID", "PAR_PRO_ID", typeof(int), "INT", null),
         /*7*/("FCP_BASE_IMPONIBLE", "FCP_BASE_IMPONIBLE", typeof(decimal), "DECIMAL(18,2)", null),
         /*8*/("FCP_VALOR_IVA", "FCP_VALOR_IVA", typeof(decimal), "DECIMAL(18,2)", null),
         /*9*/("FCP_IVA", "FCP_IVA", typeof(decimal), "DECIMAL(18,2)", null),
         /*10*/("FCP_VALOR_IRPF", "FCP_VALOR_IRPF", typeof(decimal), "DECIMAL(18,2)", null),
         /*11*/("FCP_IRPF", "FCP_IRPF", typeof(decimal), "DECIMAL(18,2)", null),

         /*12*/("FCP_TOTAL_FACTURA", "FCP_TOTAL_FACTURA", typeof(decimal), "DECIMAL(18,2)", null),
         /*13*/("FCP_OBSERVACIONES", "FCP_OBSERVACIONES", typeof(string), "VARCHAR(MAX)", string.Empty),

         /*14*/("S50_GUID_ID", $"Guid en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
         /*15*/("S50_COMPANY_GROUP_NAME", "Nombre de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
         /*16*/("S50_COMPANY_GROUP_CODE", "Código de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
         /*17*/("S50_COMPANY_GROUP_MAIN_CODE", "Código principal de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
         /*18*/("S50_COMPANY_GROUP_GUID_ID", "Guid de Grupo de Empresas en Sage50", typeof(string), "VARCHAR(MAX)", string.Empty),
         /*19*/("LAST_UPDATE", "Última actualización", typeof(DateTime),"DATETIME DEFAULT GETDATE() NOT NULL", DateTime.Now),
         /*20*/("GP_USU_ID", "Id de Gestor en Gestproject", typeof(int), "INT", null),
         /*21*/("COMMENTS", "Comentarios", typeof(string), "VARCHAR(MAX)", string.Empty),
      };
      static ((string SageDispactcherMechanism, string tableName) dispatcherAndName, List<(string name, Type type)> tableFieldsAlongTypes) SageTableDataProvider { get; set;} = (
         ("gestion","c_factucom"),
         new List<(string name, Type type)>() 
         {
            /*0*/("GUID_ID",typeof(string)),
            /*1*/("EMPRESA",typeof(string)),
            /*2*/("NUMERO",typeof(string)),
            /*3*/("CREATED",typeof(DateTime)),
            /*4*/("PROVEEDOR",typeof(string)),
            /*5*/("IMPORTE",typeof(decimal)),
            /*6*/("TOTALDOC",typeof(decimal))
         }
      );      
      public ((string SageDispactcherMechanism, string tableName) dispatcherAndName, List<(string name, Type type)> tableFieldsAlongTypes) SageTableData { get; set;} =  
      (
         SageTableDataProvider.dispatcherAndName,
         SageTableDataProvider.tableFieldsAlongTypes
      );
      //"d_albcom" // para detalles de facturas recibidas
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
      };
      public List<(string columnName, Type columnType)> SynchronizationFieldsTupleList { get; set; } = new List<(string, Type)>()
      {
         (ColumnsTuplesItemsProvider.ElementAt(0).columnName,ColumnsTuplesItemsProvider.ElementAt(0).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(1).columnName,ColumnsTuplesItemsProvider.ElementAt(1).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(14).columnName,ColumnsTuplesItemsProvider.ElementAt(14).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(15).columnName,ColumnsTuplesItemsProvider.ElementAt(15).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(16).columnName,ColumnsTuplesItemsProvider.ElementAt(16).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(17).columnName,ColumnsTuplesItemsProvider.ElementAt(17).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(18).columnName,ColumnsTuplesItemsProvider.ElementAt(18).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(19).columnName,ColumnsTuplesItemsProvider.ElementAt(19).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(20).columnName,ColumnsTuplesItemsProvider.ElementAt(20).columnType),
         (ColumnsTuplesItemsProvider.ElementAt(21).columnName,ColumnsTuplesItemsProvider.ElementAt(21).columnType),
      };
      public List<(string columnName, dynamic value)> SynchronizationFieldsDefaultValuesTupleList { get; set; } = new List<(string, dynamic)>()
      {
         (ColumnsTuplesItemsProvider.ElementAt(0).columnName,ColumnsTuplesItemsProvider.ElementAt(0).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(1).columnName,ColumnsTuplesItemsProvider.ElementAt(1).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(14).columnName,ColumnsTuplesItemsProvider.ElementAt(14).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(15).columnName,ColumnsTuplesItemsProvider.ElementAt(15).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(16).columnName,ColumnsTuplesItemsProvider.ElementAt(16).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(17).columnName,ColumnsTuplesItemsProvider.ElementAt(17).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(18).columnName,ColumnsTuplesItemsProvider.ElementAt(18).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(19).columnName,ColumnsTuplesItemsProvider.ElementAt(19).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(20).columnName,ColumnsTuplesItemsProvider.ElementAt(20).defaultValue),
         (ColumnsTuplesItemsProvider.ElementAt(21).columnName,ColumnsTuplesItemsProvider.ElementAt(21).defaultValue),
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
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectBillNumber { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(4);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectDate { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(5);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectProId { get; set; } = ColumnsTuplesItemsProvider
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

      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Sage50GuidId { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(14);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CompanyGroupName { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(15);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CompanyGroupCode { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(16);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CompanyGroupMainCode { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(17);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CompanyGroupGuidId { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(18);

      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) LastUpdate { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(19);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) ParentUserId { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(20);
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Comments { get; set; } = ColumnsTuplesItemsProvider
      .ElementAt(21);




      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectCliId { get => throw new NotImplementedException(); set => throw new NotImplementedException();  }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectReference { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectProject { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) BillType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectBillObservations { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Sage50Code { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) ProjectCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CommercialName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Cif { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Address { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Locality { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Province { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) PostalCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Country { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Iva { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CtaIvRep { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CtaIvSop { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) Withholding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CtaReRep { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) CtaReSop { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) TaxType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectDescription { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) GestprojectValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) AccountableSubaccount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
      public (string ColumnDatabaseName, string ColumnUserFriendlyNane, Type ColumnValueType, string columnDefinition, dynamic defaultValue) AccountableSubaccount2 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
   }
}
