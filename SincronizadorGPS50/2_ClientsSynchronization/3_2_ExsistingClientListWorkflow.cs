using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal class ExsistingClientListWorkflow
   {
      public ExsistingClientListWorkflow(System.Data.SqlClient.SqlConnection connection, List<GestprojectDataManager.GestprojectCustomer> clientsList, List<GestprojectDataManager.GestprojectCustomer> unsynchronizedClientList, bool unsynchronizedClientsExists)
      {
         if(unsynchronizedClientsExists)
         {
            DialogResult result = MessageBox.Show($"¿Desea sincronizar {unsynchronizedClientList.Count} cliente(s) con Sage50?", "Confirmación para actualización", MessageBoxButtons.OKCancel);
            //DialogResult result = MessageBox.Show($"Se han encontrado {unsynchronizedClientList.Count} cliente(s) que ya existen en SAGE, pero sus datos están desincronizados.\n\n¿Desea sincronizarlo(s)?", "Confirmación para sincronización", MessageBoxButtons.OKCancel);

            if(result == DialogResult.OK)
            {
               for(global::System.Int32 i = 0; i < clientsList.Count; i++)
               {
                  if(unsynchronizedClientList.Contains(clientsList[i]))
                  {
                     new UpdateClientWorkflow(GestprojectDataHolder.GestprojectDatabaseConnection, clientsList[i]);
                  };
               };
            };
         };
      }
   }
}
