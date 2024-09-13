using System;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace SincronizadorGPS50
{
   public class GestprojectReceivedBillModel : ISynchronizationModel
   {
      // Gestproject fields
      public int? FCP_ID { get; set; } = null;
      public int? PAR_DAO_ID { get; set; } = null;
      public string FCP_NUM_FACTURA { get; set; } = "";
      public DateTime? FCP_FECHA { get; set; } = null;
      public int? PAR_PRO_ID { get; set; } = null;
      public decimal? FCP_BASE_IMPONIBLE { get; set; } = null;
      public decimal? FCP_VALOR_IVA { get; set; } = null;
      public decimal? FCP_IVA { get; set; } = null;
      public decimal? FCP_VALOR_IRPF { get; set; } = null;
      public decimal? FCP_IRPF { get; set; } = null;
      public decimal? FCP_TOTAL_FACTURA { get; set; } = null;
      public string FCP_OBSERVACIONES { get; set; } = "";
      public string PROYECTO { get; set; } = "";
      public string TIPO { get; set; } = "";

      // Syncronization fields
      public int? ID { get; set; } = null;
      public string SYNC_STATUS { get; set; } = "";
      public string S50_CODE { get; set; } = "";
      public string S50_GUID_ID { get; set; } = "";
      public string S50_COMPANY_GROUP_NAME { get; set; } = "";
      public string S50_COMPANY_GROUP_CODE { get; set; } = "";
      public string S50_COMPANY_GROUP_MAIN_CODE { get; set; } = "";
      public string S50_COMPANY_GROUP_GUID_ID { get; set; } = "";
      public DateTime? LAST_UPDATE { get; set; } = null;
      public int? GP_USU_ID { get; set; } = null;
      public string COMMENTS { get; set; } = "";
   }
}
