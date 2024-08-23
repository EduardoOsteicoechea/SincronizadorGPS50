using SincronizadorGPS50.GestprojectDataManager;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal class UnexsistingClientListWorkflow
   {
      public UnexsistingClientListWorkflow(System.Data.SqlClient.SqlConnection connection, List<GestprojectDataManager.GestprojectCustomer> clientsList, CustomerSyncronizationTableSchema tableSchema)
      {
         List<GestprojectDataManager.GestprojectCustomer> existingClientsList = new List<GestprojectDataManager.GestprojectCustomer> ();
         List<GestprojectDataManager.GestprojectCustomer> unexistingClientsList = new List<GestprojectDataManager.GestprojectCustomer> ();

         for(global::System.Int32 i = 0; i < clientsList.Count; i++)
         {
            GestprojectDataManager.GestprojectCustomer currentClient = clientsList[i];

            Sage50ConnectionManager.CustomerManager customerManager = new Sage50ConnectionManager.CustomerManager(
               currentClient.fullName,
               currentClient.PAR_CIF_NIF
            );

            if(customerManager.ClientExists)
            {
               existingClientsList.Add(currentClient);
            }
            else
            {
               unexistingClientsList.Add(currentClient);
            };
         };

         string dialogMessage = "";
         if(existingClientsList.Count > 0 && unexistingClientsList.Count > 0)
         {
            dialogMessage = $"Partiendo de la selección encontramos {existingClientsList.Count} cliente(s) desactualizados y {unexistingClientsList.Count} inexistentes en Sage50.\n\n¿Desea sincronizar los clientes existentes y crear los faltantes en Sage50?";
         }
         else if(existingClientsList.Count > 0 && unexistingClientsList.Count == 0)
         {
            dialogMessage = $"Partiendo de la selección encontramos {existingClientsList.Count} cliente(s) desactualizados.\n\n¿Desea sincronizarlo(s)?";
         }
         else if(existingClientsList.Count == 0 && unexistingClientsList.Count > 0)
         {
            dialogMessage = $"Partiendo de la selección encontramos {unexistingClientsList.Count} cliente(s) inexistentes en Sage50.\n\n¿Desea crearlos y sincronizar sus datos?";
         };

         DialogResult result = MessageBox.Show(dialogMessage, "Confirmación de actualización y creación", MessageBoxButtons.OKCancel);

         if(result == DialogResult.OK)
         {
            for(global::System.Int32 i = 0; i < existingClientsList.Count; i++)
            {
               GestprojectDataManager.GestprojectCustomer currentClient = existingClientsList[i];
               new UpdateClientWorkflow(connection, currentClient, tableSchema);
            };
            for(global::System.Int32 i = 0; i < unexistingClientsList.Count; i++)
            {
               GestprojectDataManager.GestprojectCustomer currentClient = unexistingClientsList[i];
               new CreateClientWorkflow(connection, currentClient, tableSchema);
            };
         };
      }
   }
}
