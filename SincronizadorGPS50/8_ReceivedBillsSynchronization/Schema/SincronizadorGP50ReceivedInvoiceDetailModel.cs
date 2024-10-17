using System;

namespace SincronizadorGPS50
{
   public class SincronizadorGPS50ReceivedInvoiceDetailModel
   {   
      public string DFP_CONCEPTO { get; set; } = ""; // Definition
      public int? DFP_ID { get; set; } = 0; // Lo asigna GP automático
      public string DFP_PRECIO_UNIDAD { get; set; } = ""; // Price
      public string DFP_UNIDADES { get; set; } = ""; // Units
      public string DFP_SUBTOTAL { get; set; } = ""; // Import
      public int? PRY_ID { get; set; } = 0; // Lo asigna GP automático
      public string FCP_ID { get; set; } = ""; // Obtener el id de la factura en GP
      public string DFP_ESTRUCTURAL { get; set; } = ""; // Import

      // Syncronization fields
      public int? ID { get; set; } = null;
      public string SYNC_STATUS { get; set; } = "";
      public string S50_CODE { get; set; } = "";
      public string S50_GUID_ID { get; set; } = ""; // c_factucom.GUID_ID
      public string S50_COMPANY_GROUP_NAME { get; set; } = "";
      public string S50_COMPANY_GROUP_CODE { get; set; } = "";
      public string S50_COMPANY_GROUP_MAIN_CODE { get; set; } = "";
      public string S50_COMPANY_GROUP_GUID_ID { get; set; } = "";
      public DateTime? LAST_UPDATE { get; set; } = null;
      public int? GP_USU_ID { get; set; } = null;
      public string COMMENTS { get; set; } = "";
   }
}                     
