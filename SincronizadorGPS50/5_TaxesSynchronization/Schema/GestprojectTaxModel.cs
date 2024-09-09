using System;

namespace SincronizadorGPS50
{
	public class GestprojectTaxModel : ISynchronizationModel
	{
		// Gestproject fields
		public int? IMP_ID { get; set; }
		public string IMP_TIPO { get; set; }
		public string IMP_NOMBRE { get; set; }
		public string IMP_VALOR { get; set; }
		public string IMP_SUBCTA_CONTABLE { get; set; }
		public string IMP_SUBCTA_CONTABLE_2 { get; set; }


      // Sage50 fields
      public string NOMBRE { get; set; }

      public string IVA { get; set; }
      public string CTA_IV_REP { get; set; }
      public string CTA_IV_SOP { get; set; }

      public string IRPF { get; set; }
      public string RETENCION { get; set; }
      public string CTA_RE_REP { get; set; }
      public string CTA_RE_SOP { get; set; }


      // Additional reference fields
      public string TAX_TYPE { get; set; }


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
