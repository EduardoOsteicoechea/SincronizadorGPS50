namespace SincronizadorGPS50
{
   public class Sage50TaxModel
   {
      // Sage50 fields
      public string GUID_ID { get; set; }
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
   }
}
