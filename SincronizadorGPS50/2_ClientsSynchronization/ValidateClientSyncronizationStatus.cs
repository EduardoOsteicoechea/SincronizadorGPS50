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
      public GestprojectDataManager.GestprojectClient GestprojectClient { get; set; } = new GestprojectDataManager.GestprojectClient();
      public ValidateClientSyncronizationStatus
      (
         string guid,
         string name,
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
         GestprojectClient.PAR_CIF_NIF = cif;
         GestprojectClient.PAR_CP_1 = postalCode;
         GestprojectClient.PAR_DIRECCION_1 = address;
         GestprojectClient.PAR_PROVINCIA_1 = province;
         GestprojectClient.PAR_PAIS_1 = country;

         if(GestprojectClient.sage50_guid_id != null && GestprojectClient.sage50_guid_id != "") 
         {
            //MessageBox.Show("Client was sincronized");
            for(int i = 0; i < sage50ClientList.Count; i++)
            {
               if(sage50ClientList[i].GUID_ID == GestprojectClient.sage50_guid_id)
               {
                  bool isSynchronized = true;
                  //MessageBox.Show("Beginning comparison");
                  if(sage50ClientList[i].NOMBRE != GestprojectClient.PAR_NOMBRE)
                  {
                     isSynchronized = false;
                     GestprojectClient.comments += $"El Campo \"{ClientSynchronizationTableSchema.GestprojectClientNameColumn.ColumnUserFriendlyNane}\" no coincide. Su valor en Sage50 es: \"{sage50ClientList[i].NOMBRE}\". ";
                     //MessageBox.Show(GestprojectClient.comments);
                  };

                  if(sage50ClientList[i].CIF != GestprojectClient.PAR_CIF_NIF)
                  {
                     isSynchronized = false;
                     GestprojectClient.comments += $"El Campo \"{ClientSynchronizationTableSchema.GestprojectClientCIFNIFColumn.ColumnUserFriendlyNane}\" no coincide. Su valor en Sage50 es: \"{sage50ClientList[i].CIF}\". ";
                     //MessageBox.Show(GestprojectClient.comments);
                  };

                  if(sage50ClientList[i].CODPOST != GestprojectClient.PAR_CP_1)
                  {
                     isSynchronized = false;
                     GestprojectClient.comments += $"El Campo \"{ClientSynchronizationTableSchema.GestprojectClientPostalCodeColumn.ColumnUserFriendlyNane}\" no coincide. Su valor en Sage50 es: \"{sage50ClientList[i].CODPOST}\". ";
                     //MessageBox.Show(GestprojectClient.comments);
                  };

                  if(sage50ClientList[i].DIRECCION != GestprojectClient.PAR_DIRECCION_1)
                  {
                     isSynchronized = false;
                     GestprojectClient.comments += $"El Campo \"{ClientSynchronizationTableSchema.GestprojectClientAddressColumn.ColumnUserFriendlyNane}\" no coincide. Su valor en Sage50 es: \"{sage50ClientList[i].DIRECCION}\". ";
                     //MessageBox.Show(GestprojectClient.comments);
                  };

                  if(sage50ClientList[i].PROVINCIA != GestprojectClient.PAR_PROVINCIA_1)
                  {
                     isSynchronized = false;
                     GestprojectClient.comments += $"El Campo \"{ClientSynchronizationTableSchema.GestprojectClientProvinceColumn.ColumnUserFriendlyNane}\" no coincide. Su valor en Sage50 es: \"{sage50ClientList[i].PROVINCIA}\". ";
                     //MessageBox.Show(GestprojectClient.comments);
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
