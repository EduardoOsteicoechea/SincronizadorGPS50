using SincronizadorGPS50.GestprojectDataManager;
using SincronizadorGPS50.Sage50Connector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   public class ValidateClientSyncronizationStatus
   {
      public bool MustBeDeleted { get; set; } = false;

      public string CreateErrorMesage(string nombreDeCampo, string valorEnSage50)
      {
         return $"\"{nombreDeCampo}\" no coincide. Su valor en Sage50 es: \"{valorEnSage50}\". ";
      }
      public ValidateClientSyncronizationStatus
      (
         GestprojectCustomer gestprojectCustomer,
         List<Sage50Customer> sage50ClientList
      )
      {
         try
         {
            if(gestprojectCustomer.sage50_guid_id != null && gestprojectCustomer.sage50_guid_id != "")
            {
               for(int i = 0; i < sage50ClientList.Count; i++)
               {
                  if
                  (
                     sage50ClientList[i].GUID_ID.Trim() == gestprojectCustomer.sage50_guid_id.Trim()
                  )
                  {
                     bool isSynchronized = true;
                     if(sage50ClientList[i].NOMBRE.Trim() != gestprojectCustomer.fullName.Trim())
                     {
                        isSynchronized = false;
                        gestprojectCustomer.comments += this.CreateErrorMesage(ClientSynchronizationTableSchema.GestprojectClientNameColumn.ColumnUserFriendlyNane, sage50ClientList[i].NOMBRE);
                     };

                     if(sage50ClientList[i].CIF.Trim() != gestprojectCustomer.PAR_CIF_NIF.Trim())
                     {
                        isSynchronized = false;
                        gestprojectCustomer.comments += this.CreateErrorMesage(ClientSynchronizationTableSchema.GestprojectClientCIFNIFColumn.ColumnUserFriendlyNane, sage50ClientList[i].CIF);
                     };

                     if(sage50ClientList[i].CODPOST.Trim() != gestprojectCustomer.PAR_CP_1.Trim())
                     {
                        isSynchronized = false;
                        gestprojectCustomer.comments += this.CreateErrorMesage(ClientSynchronizationTableSchema.GestprojectClientPostalCodeColumn.ColumnUserFriendlyNane, sage50ClientList[i].CODPOST);
                     };

                     if(sage50ClientList[i].DIRECCION.Trim() != gestprojectCustomer.PAR_DIRECCION_1.Trim())
                     {
                        isSynchronized = false;
                        gestprojectCustomer.comments += this.CreateErrorMesage(ClientSynchronizationTableSchema.GestprojectClientAddressColumn.ColumnUserFriendlyNane, sage50ClientList[i].DIRECCION);
                     };

                     if(sage50ClientList[i].PROVINCIA.Trim() != gestprojectCustomer.PAR_PROVINCIA_1.Trim())
                     {
                        isSynchronized = false;
                        gestprojectCustomer.comments += this.CreateErrorMesage(ClientSynchronizationTableSchema.GestprojectClientProvinceColumn.ColumnUserFriendlyNane, sage50ClientList[i].PROVINCIA);
                     };

                     gestprojectCustomer.synchronization_status = isSynchronized ? "Sincronizado" : "Desincronizado";
                     MustBeDeleted = false;
                     break;
                  }
                  else
                  {
                     gestprojectCustomer.synchronization_status = "Fue eliminado en Sage50";
                     MustBeDeleted = true;
                  };
               };
            }
            else
            {
               gestprojectCustomer.synchronization_status = "Nunca ha sido sincronizado";
            };
         }
         catch(System.Exception exception)
         {
            throw new System.Exception(
               $"At:\n\nSincronizadorGPS50\n.ValidateClientSyncronizationStatus:\n\n{exception.Message}"
            );
         };
      }
   }
}
