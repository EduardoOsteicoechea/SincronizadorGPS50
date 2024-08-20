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
            DialogResult result = MessageBox.Show($"¿Desea sincronizar los datos de los {unsynchronizedClientList.Count} cliente(s) desincronizados?", "Confirmación para actualización", MessageBoxButtons.OKCancel);

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
