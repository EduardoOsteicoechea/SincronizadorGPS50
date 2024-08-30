using SincronizadorGPS50.GestprojectDataManager;
using SincronizadorGPS50.Sage50Connector;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal class UnexsistingProviderListWorkflow
   {
      public UnexsistingProviderListWorkflow
      (
         SqlConnection connection, 
         List<GestprojectProviderModel> entityList, 
         CustomerSyncronizationTableSchema tableSchema
      )
      {
         try
         {
            List<GestprojectProviderModel> existingEntityList = new List<GestprojectProviderModel> ();
            List<GestprojectProviderModel> unexistingEntityList = new List<GestprojectProviderModel> ();

            for(global::System.Int32 i = 0; i < entityList.Count; i++)
            {
               GestprojectProviderModel entity = entityList[i];

               ProviderComparer customerComparer = new ProviderComparer(
                  entity.fullName,
                  entity.PAR_CIF_NIF
               );

               if(customerComparer.Exists)
               {
                  entity.sage50_code = customerComparer.Sage50Code;
                  entity.PAR_SUBCTA_CONTABLE_2 = customerComparer.Sage50Code;
                  entity.sage50_guid_id = customerComparer.Sage50Guid;
                  existingEntityList.Add(entity);
               }
               else
               {
                  unexistingEntityList.Add(entity);
               };
            };

            string dialogMessage = "";
            if(existingEntityList.Count > 0 && unexistingEntityList.Count > 0)
            {
               dialogMessage = $"Partiendo de la selección encontramos {unexistingEntityList.Count} cliente(s) desactualizados y {unexistingEntityList.Count} inexistentes en Sage50.\n\n¿Desea vincular los clientes existentes y crear los faltantes en Sage50?";
            }
            else if(existingEntityList.Count > 0 && unexistingEntityList.Count == 0)
            {
               dialogMessage = $"Partiendo de la selección encontramos {unexistingEntityList.Count} cliente(s) que ya existen en Sage50.\n\n¿Desea vincularlo(s)?";
            }
            else if(existingEntityList.Count == 0 && unexistingEntityList.Count > 0)
            {
               dialogMessage = $"Partiendo de la selección encontramos {unexistingEntityList.Count} cliente(s) inexistentes en Sage50.\n\n¿Desea crearlos y sincronizar sus datos?";
            };

            DialogResult result = MessageBox.Show(dialogMessage, "Confirmación de actualización y creación", MessageBoxButtons.OKCancel);

            if(result == DialogResult.OK)
            {
               for(global::System.Int32 i = 0; i < existingEntityList.Count; i++)
               {
                  GestprojectProviderModel existingEntity = existingEntityList[i];
                  //new LinkProviderWorkflow(connection, existingEntity, tableSchema);
               };

               for(global::System.Int32 i = 0; i < unexistingEntityList.Count; i++)
               {
                  GestprojectProviderModel unexistingEntity = unexistingEntityList[i];
                  //new CreateProviderWorkflow(connection, unexistingEntity, tableSchema);
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
