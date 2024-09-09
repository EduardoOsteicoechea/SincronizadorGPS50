using Infragistics.Designers.SqlEditor;
using System.Collections.Generic;
using System.Reflection;

namespace SincronizadorGPS50
{
   public class ValidateTaxSyncronizationStatus
   {
      public bool MustBeDeleted { get; set; } = false;
      public bool NeverWasSynchronized { get; set; } = false;
      public bool IsSynchronized { get; set; } = true;

      public string CreateErrorMesage(string nombreDeCampo, string valorEnSage50)
      {
         return $"\"{nombreDeCampo}\" no coincide. Su valor en Sage50 es: \"{valorEnSage50}\". ";
      }
      public ValidateTaxSyncronizationStatus
      (
         GestprojectTaxModel gestprojectEntity,
         List<Sage50TaxModel> sage50EntityList,
         string entityNameColumnName,
         string entityPostalCodeColumnName,
         string entityAddressColumnName,
         string entityLocalityColumnName,
         string entityProvinceColumnName
      )
      {
         try
         {
            if(gestprojectEntity.S50_CODE != null && gestprojectEntity.S50_CODE != "")
            {
               for(int i = 0; i < sage50EntityList.Count; i++)
               {
                  if(sage50EntityList[i].GUID_ID.Trim() == gestprojectEntity.S50_CODE.Trim())
                  {
                     if(sage50EntityList[i].NOMBRE.Trim() != gestprojectEntity.IMP_NOMBRE.Trim())
                     {
                        NeverWasSynchronized = false;
                        IsSynchronized = false;
                        MustBeDeleted = false;
                        gestprojectEntity.COMMENTS += this.CreateErrorMesage(entityNameColumnName, sage50EntityList[i].NOMBRE);
                     };

                     if(sage50EntityList[i].IVA.Trim() != gestprojectEntity.IMP_VALOR.Trim())
                     {
                        NeverWasSynchronized = false;
                        IsSynchronized = false;
                        MustBeDeleted = false;
                        gestprojectEntity.COMMENTS += this.CreateErrorMesage(entityPostalCodeColumnName, sage50EntityList[i].IVA.Trim());
                     };

                     if(sage50EntityList[i].CTA_IV_REP.Trim() != gestprojectEntity.IMP_SUBCTA_CONTABLE.Trim())
                     {
                        NeverWasSynchronized = false;
                        IsSynchronized = false;
                        MustBeDeleted = false;
                        gestprojectEntity.COMMENTS += this.CreateErrorMesage(entityAddressColumnName, sage50EntityList[i].CTA_IV_REP);
                     };

                     if(sage50EntityList[i].CTA_IV_SOP.Trim() != gestprojectEntity.IMP_SUBCTA_CONTABLE_2.Trim())
                     {
                        NeverWasSynchronized = false;
                        IsSynchronized = false;
                        MustBeDeleted = false;
                        gestprojectEntity.COMMENTS += this.CreateErrorMesage(entityLocalityColumnName, sage50EntityList[i].CTA_IV_SOP);
                     };

                     if
                     (
                        sage50EntityList[i].NOMBRE.Trim() == gestprojectEntity.IMP_NOMBRE.Trim()
                        &&
                        sage50EntityList[i].IVA.Trim() == gestprojectEntity.IMP_VALOR.Trim()
                        &&
                        sage50EntityList[i].CTA_IV_REP.Trim() == gestprojectEntity.IMP_SUBCTA_CONTABLE.Trim()
                        &&
                        sage50EntityList[i].CTA_IV_SOP.Trim() == gestprojectEntity.IMP_SUBCTA_CONTABLE_2.Trim()
                     )
                     {
                        //MessageBox.Show("Sincronizado");
                        NeverWasSynchronized = false;
                        IsSynchronized = true;
                        MustBeDeleted = false;
                        gestprojectEntity.COMMENTS = "";
                        gestprojectEntity.SYNC_STATUS = SynchronizationStatusOptions.Sincronizado;
                     };

                     break;
                  }
                  else
                  {
                     //MessageBox.Show("Eliminado en Sage");
                     NeverWasSynchronized = true;
                     MustBeDeleted = true;
                     gestprojectEntity.SYNC_STATUS = SynchronizationStatusOptions.Desincronizado;
                  };
               };
            }
            else
            {
               //MessageBox.Show("Nunca sincronizado");
               NeverWasSynchronized = true;
               IsSynchronized = false;
               MustBeDeleted = false;
               gestprojectEntity.SYNC_STATUS = SynchronizationStatusOptions.Desincronizado;
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
