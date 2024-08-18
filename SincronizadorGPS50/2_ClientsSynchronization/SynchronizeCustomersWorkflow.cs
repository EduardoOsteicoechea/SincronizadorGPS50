using SincronizadorGPS50.Sage50Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal class SynchronizeCustomersWorkflow
   {
      public SynchronizeCustomersWorkflow
      (
         System.Data.SqlClient.SqlConnection connection,
         List<int> selectedIdList
      )
      {
         List<GestprojectDataManager.GestprojectClient> gestProjectClientList =
               new SincronizadorGPS50
               .GestprojectDataManager
               .GetClientsFromSynchronizationTable(
                  connection,
                  selectedIdList
               ).GestprojectClientList;

         List<Sage50Customer> sage50CustomerList = new GetSage50Customer().CustomerList;

         List<GestprojectDataManager.GestprojectClient> existingClientsList = new List<GestprojectDataManager.GestprojectClient>();
         List<GestprojectDataManager.GestprojectClient> nonExistingClientsList = new List<GestprojectDataManager.GestprojectClient>();
         for(int i = 0; i < gestProjectClientList.Count; i++)
         {
            GestprojectDataManager.GestprojectClient gestprojectClient = gestProjectClientList[i];
            bool found = false;

            for(global::System.Int32 j = 0; j < sage50CustomerList.Count; j++)
            {
               Sage50Customer sage50Customer = sage50CustomerList[j];
               if(gestprojectClient.sage50_guid_id == sage50Customer.GUID_ID)
               {
                  existingClientsList.Add(gestprojectClient);
                  found = true;
                  break;
               }
            }

            if(!found)
            {
               nonExistingClientsList.Add(gestprojectClient);
            }
         };

         bool someGestprojectClientsExistsInSage50 = existingClientsList.Count > 0;
         bool allGestprojectClientsExistsInSage50 = existingClientsList.Count == gestProjectClientList.Count;
         bool noGestprojectClientsExistsInSage50 = existingClientsList.Count == 0;


         if(noGestprojectClientsExistsInSage50)
         {
            for(global::System.Int32 i = 0; i < gestProjectClientList.Count; i++)
            {
               if(
                  Sage50ConnectionManager.CustomerManager.ClientExists(
                     gestProjectClientList[i].sage50_guid_id,
                     gestProjectClientList[i].PAR_PAIS_1,
                     gestProjectClientList[i].PAR_NOMBRE,
                     gestProjectClientList[i].PAR_CIF_NIF,
                     gestProjectClientList[i].PAR_CP_1,
                     gestProjectClientList[i].PAR_DIRECCION_1,
                     gestProjectClientList[i].PAR_PROVINCIA_1
                  )
               )
               {
                  DialogResult result = MessageBox.Show("Desea crear este cliente de igual manera?", "Confirmación para creación", MessageBoxButtons.OKCancel);
                  if(result == DialogResult.Cancel)
                  {
                     continue;
                  }
               };

               CreateSage50Customer newSage50Client = new SincronizadorGPS50.Sage50Connector.CreateSage50Customer(
                  gestProjectClientList[i].PAR_PAIS_1,
                  gestProjectClientList[i].PAR_NOMBRE ,
                  gestProjectClientList[i].PAR_CIF_NIF,
                  gestProjectClientList[i].PAR_CP_1,
                  gestProjectClientList[i].PAR_DIRECCION_1,
                  gestProjectClientList[i].PAR_PROVINCIA_1
               );

               new GestprojectDataManager.RegisterNewSage50ClientData(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  gestProjectClientList[i].PAR_ID,
                  newSage50Client.ClientCode,
                  newSage50Client.GUID_ID
               );
            }
         }
         else if(allGestprojectClientsExistsInSage50)
         {
            for(global::System.Int32 i = 0; i < gestProjectClientList.Count; i++)
            {
               DialogResult result = MessageBox.Show("Desea actualizar los datos de Sage50 de los clientes desincronizados?", "Confirmación para actualización", MessageBoxButtons.OKCancel);
               if(result == DialogResult.Cancel)
               {
                  break;
               };

               UpdateSage50Customer newSage50Client = new SincronizadorGPS50.Sage50Connector.UpdateSage50Customer(
                  gestProjectClientList[i].PAR_PAIS_1,
                  gestProjectClientList[i].PAR_NOMBRE ,
                  gestProjectClientList[i].PAR_CIF_NIF,
                  gestProjectClientList[i].PAR_CP_1,
                  gestProjectClientList[i].PAR_DIRECCION_1,
                  gestProjectClientList[i].PAR_PROVINCIA_1
               );

               new GestprojectDataManager.RegisterNewSage50ClientData(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  gestProjectClientList[i].PAR_ID,
                  newSage50Client.ClientCode,
                  newSage50Client.GUID_ID
               );
            };
         }
         
         else if(someGestprojectClientsExistsInSage50)
         {
            for(global::System.Int32 i = 0; i < existingClientsList.Count; i++)
            {
               DialogResult result = MessageBox.Show("Desea actualizar los datos de Sage50 de los clientes desincronizados?", "Confirmación para actualización", MessageBoxButtons.OKCancel);
               if(result == DialogResult.Cancel)
               {
                  break;
               };

               UpdateSage50Customer newSage50Client = new SincronizadorGPS50.Sage50Connector.UpdateSage50Customer(
                  gestProjectClientList[i].PAR_PAIS_1,
                  gestProjectClientList[i].PAR_NOMBRE ,
                  gestProjectClientList[i].PAR_CIF_NIF,
                  gestProjectClientList[i].PAR_CP_1,
                  gestProjectClientList[i].PAR_DIRECCION_1,
                  gestProjectClientList[i].PAR_PROVINCIA_1
               );

               new GestprojectDataManager.RegisterNewSage50ClientData(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  gestProjectClientList[i].PAR_ID,
                  newSage50Client.ClientCode,
                  newSage50Client.GUID_ID
               );
            };

            for(global::System.Int32 i = 0; i < nonExistingClientsList.Count; i++)
            {
               if(
                  Sage50ConnectionManager.CustomerManager.ClientExists(
                     gestProjectClientList[i].sage50_guid_id,
                     gestProjectClientList[i].PAR_PAIS_1,
                     gestProjectClientList[i].PAR_NOMBRE,
                     gestProjectClientList[i].PAR_CIF_NIF,
                     gestProjectClientList[i].PAR_CP_1,
                     gestProjectClientList[i].PAR_DIRECCION_1,
                     gestProjectClientList[i].PAR_PROVINCIA_1
                  )
               )
               {
                  DialogResult result = MessageBox.Show("Desea crear este cliente de igual manera?", "Confirmación para creación", MessageBoxButtons.OKCancel);
                  if(result == DialogResult.Cancel)
                  {
                     continue;
                  }
               };

               CreateSage50Customer newSage50Client = new SincronizadorGPS50.Sage50Connector.CreateSage50Customer(
                  gestProjectClientList[i].PAR_PAIS_1,
                  gestProjectClientList[i].PAR_NOMBRE ,
                  gestProjectClientList[i].PAR_CIF_NIF,
                  gestProjectClientList[i].PAR_CP_1,
                  gestProjectClientList[i].PAR_DIRECCION_1,
                  gestProjectClientList[i].PAR_PROVINCIA_1
               );

               new GestprojectDataManager.RegisterNewSage50ClientData(
                  GestprojectDataHolder.GestprojectDatabaseConnection,
                  gestProjectClientList[i].PAR_ID,
                  newSage50Client.ClientCode,
                  newSage50Client.GUID_ID
               );
            };
         };

         // Obtener todos los clientes de Sage
         // Todos existen: ¿Actualizar todo?
         // Algunos existen: ¿Actualizar todo y crear inexistentes?
         // Ninguno existe: 
         // Comparación suelta
         // similares
         // ¿Crear similares
         // no similares
         // Crear
      }
   }
}
