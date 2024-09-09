using sage.ew.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   public class GetSage50Taxes
   {
      public List<Sage50TaxModel> Entities { get; set; } = new List<Sage50TaxModel>();
      public List<string> Codes { get; set; } = new List<string>();
      public List<string> Guids { get; set; } = new List<string>();
      public int LastCodeValue { get; set; }
      public int NextCodeAvailable { get; set; }
      public string Code { get; set; }
      public string Guid { get; set; }
      public bool Exists { get; set; } = false;
      public GetSage50Taxes()
      {
         try
         {
            string sqlString1 = $@"
                SELECT 
                    guid_id,
                    iva,
                    nombre,
                    cta_iv_rep,
                    cta_iv_sop
                FROM {DB.SQLDatabase("gestion","tipo_iva")}";

            DataTable table1 = new DataTable();

            DB.SQLExec(sqlString1, ref table1);

            if(table1.Rows.Count > 0)
            {
               for(int i = 0; i < table1.Rows.Count; i++)
               {
                  Sage50TaxModel sage50Entity = new Sage50TaxModel();

                  sage50Entity.GUID_ID = table1.Rows[i].ItemArray[0].ToString().Trim();
                  sage50Entity.IVA = table1.Rows[i].ItemArray[2].ToString().Trim();
                  sage50Entity.NOMBRE = table1.Rows[i].ItemArray[3].ToString().Trim();
                  sage50Entity.CTA_IV_REP = table1.Rows[i].ItemArray[5].ToString().Trim();
                  sage50Entity.CTA_IV_SOP = table1.Rows[i].ItemArray[6].ToString().Trim();

                  Entities.Add(sage50Entity);
               };
            };

            string sqlString2 = $@"
                SELECT 
                    guid_id,
                    irpf,
                    nombre,
                    retencion,
                    cta_re_rep,
                    cta_re_sop
                FROM {DB.SQLDatabase("gestion","tipo_ret")}";

            DataTable table2 = new DataTable();

            DB.SQLExec(sqlString2, ref table2);

            if(table2.Rows.Count > 0)
            {
               for(int i = 0; i < table2.Rows.Count; i++)
               {
                  Sage50TaxModel sage50Entity = new Sage50TaxModel();

                  sage50Entity.GUID_ID = table2.Rows[i].ItemArray[0].ToString().Trim();
                  sage50Entity.IRPF = table2.Rows[i].ItemArray[2].ToString().Trim();
                  sage50Entity.NOMBRE = table2.Rows[i].ItemArray[3].ToString().Trim();
                  sage50Entity.RETENCION = table2.Rows[i].ItemArray[4].ToString().Trim();
                  sage50Entity.CTA_RE_REP = table2.Rows[i].ItemArray[5].ToString().Trim();
                  sage50Entity.CTA_RE_SOP = table2.Rows[i].ItemArray[6].ToString().Trim();

                  Entities.Add(sage50Entity);
               };
            };
         }
         catch(System.Exception exception)
         {
            throw ApplicationLogger.ReportError(
               MethodBase.GetCurrentMethod().DeclaringType.Namespace,
               MethodBase.GetCurrentMethod().DeclaringType.Name,
               MethodBase.GetCurrentMethod().Name,
               exception
            );
         };
      }
   }
}
