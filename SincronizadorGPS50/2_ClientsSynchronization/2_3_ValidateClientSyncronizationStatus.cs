using SincronizadorGPS50.GestprojectDataManager;
using SincronizadorGPS50.Sage50Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal class ValidateClientSyncronizationStatus
   {
      public GestprojectDataManager.GestprojectCustomer GestprojectClient { get; set; } = new GestprojectDataManager.GestprojectCustomer();

      public string CreateErrorMesage(string nombreDeCampo, string valorEnSage50) 
      {
         return $"\"{nombreDeCampo}\" no coincide. Su valor en Sage50 es: \"{valorEnSage50}\". ";
      }
      public ValidateClientSyncronizationStatus
      (
         string guid,
         string name,
         string apellido1,
         string apellido2,
         string cif,
         string postalCode,
         string address,
         string province,
         string country,
         List<Sage50Customer> sage50ClientList
      )
      {
         GestprojectClient.sage50_guid_id = guid;
         GestprojectClient.PAR_NOMBRE = name;
         GestprojectClient.PAR_APELLIDO_1 = apellido1;
         GestprojectClient.PAR_APELLIDO_2 = apellido2;
         GestprojectClient.PAR_CIF_NIF = cif;
         GestprojectClient.PAR_CP_1 = postalCode;
         GestprojectClient.PAR_DIRECCION_1 = address;
         GestprojectClient.PAR_PROVINCIA_1 = province;
         GestprojectClient.PAR_PAIS_1 = country;

         if(GestprojectClient.sage50_guid_id != null && GestprojectClient.sage50_guid_id != "") 
         {
            for(int i = 0; i < sage50ClientList.Count; i++)
            {
               if(sage50ClientList[i].GUID_ID == GestprojectClient.sage50_guid_id)
               {
                  bool isSynchronized = true;
                  if(sage50ClientList[i].NOMBRE != GestprojectClient.PAR_NOMBRE)
                  //if(sage50ClientList[i].NOMBRE != GestprojectClient.fullName)
                  {
                     isSynchronized = false;
                     GestprojectClient.comments += this.CreateErrorMesage(ClientSynchronizationTableSchema.GestprojectClientNameColumn.ColumnUserFriendlyNane, sage50ClientList[i].NOMBRE);
                  };

                  if(sage50ClientList[i].CIF != GestprojectClient.PAR_CIF_NIF)
                  {
                     isSynchronized = false;
                     GestprojectClient.comments += this.CreateErrorMesage(ClientSynchronizationTableSchema.GestprojectClientCIFNIFColumn.ColumnUserFriendlyNane, sage50ClientList[i].CIF);
                  };

                  if(sage50ClientList[i].CODPOST != GestprojectClient.PAR_CP_1)
                  {
                     isSynchronized = false;
                     GestprojectClient.comments += this.CreateErrorMesage(ClientSynchronizationTableSchema.GestprojectClientPostalCodeColumn.ColumnUserFriendlyNane, sage50ClientList[i].CODPOST);
                  };

                  if(sage50ClientList[i].DIRECCION != GestprojectClient.PAR_DIRECCION_1)
                  {
                     isSynchronized = false;
                     GestprojectClient.comments += this.CreateErrorMesage(ClientSynchronizationTableSchema.GestprojectClientAddressColumn.ColumnUserFriendlyNane, sage50ClientList[i].DIRECCION);
                  };

                  if(sage50ClientList[i].PROVINCIA != GestprojectClient.PAR_PROVINCIA_1)
                  {
                     isSynchronized = false;
                     GestprojectClient.comments += this.CreateErrorMesage(ClientSynchronizationTableSchema.GestprojectClientProvinceColumn.ColumnUserFriendlyNane, sage50ClientList[i].PROVINCIA);
                  };

                  GestprojectClient.synchronization_status = isSynchronized ? "Sincronizado" : "Desincronizado";
                  break;
               }
               else
               {
                  GestprojectClient.synchronization_status = "Nunca ha sido sincronizado";
               };
            };
         }
         else
         {
            GestprojectClient.synchronization_status = "Nunca ha sido sincronizado";
         };
      }
   }
}
