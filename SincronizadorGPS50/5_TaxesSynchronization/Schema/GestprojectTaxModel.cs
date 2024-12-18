﻿using System;

namespace SincronizadorGPS50
{
	public class GestprojectTaxModel : ISynchronizationModel
	{
		// Gestproject fields
		public int? IMP_ID { get; set; }
		public string IMP_TIPO { get; set; }
		public string IMP_NOMBRE { get; set; }
		public string IMP_DESCRIPCION { get; set; }
		public System.Decimal IMP_VALOR { get; set; }
		public string IMP_SUBCTA_CONTABLE { get; set; }
		public string IMP_SUBCTA_CONTABLE_2 { get; set; }


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
