using System;

namespace SincronizadorGPS50
{
   public class SincronizadorGPS50ReceivedInvoiceDetailModel
   {   
      public int? DFP_ID { get; set; } = null; // Lo asigna GP automático
      public string DFP_CONCEPTO { get; set; } = ""; // Definition
      public decimal DFP_PRECIO_UNIDAD { get; set; } = 0; // Price
      public decimal DFP_UNIDADES { get; set; } = 0; // Units
      public decimal DFP_SUBTOTAL { get; set; } = 0; // Import
      public int? PRY_ID { get; set; } = null; // Lo asigna GP automático
      public int? FCP_ID { get; set; } = null; // Obtener el id de la factura en GP
      public string DFP_ESTRUCTURAL { get; set; } = "0"; // Import

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
