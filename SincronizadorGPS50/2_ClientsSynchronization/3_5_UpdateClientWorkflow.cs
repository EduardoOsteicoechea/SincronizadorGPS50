using SincronizadorGPS50.GestprojectDataManager;

namespace SincronizadorGPS50
{
   internal class UpdateClientWorkflow
   {
      public UpdateClientWorkflow(System.Data.SqlClient.SqlConnection connection, GestprojectCustomer gestprojectClient)
      {
         new SincronizadorGPS50.Sage50Connector.UpdateSage50Customer(
            gestprojectClient.sage50_guid_id,
            gestprojectClient.PAR_PAIS_1,
            gestprojectClient.PAR_NOMBRE,
            gestprojectClient.PAR_CIF_NIF,
            gestprojectClient.PAR_CP_1,
            gestprojectClient.PAR_DIRECCION_1,
            gestprojectClient.PAR_PROVINCIA_1
         );

         new GestprojectDataManager.UpdateClientSyncronizationStatus(
            connection,
            gestprojectClient.PAR_ID,
            true
         );

         SynchronizerUserRememberableDataModel userRememberableData = ManageRememberableUserData.GetSynchronizerUserRememberableDataForConnection(
                GestprojectDataHolder.GestprojectDatabaseConnection
            );

         new GestprojectDataManager.RegisterNewSage50ClientData(
            connection,
            gestprojectClient.PAR_ID,
            gestprojectClient.sage50_client_code,
            gestprojectClient.sage50_guid_id,
            userRememberableData.SAGE_50_COMPANY_GROUP_NAME,
            userRememberableData.SAGE_50_COMPANY_GROUP_MAIN_CODE,
            userRememberableData.SAGE_50_COMPANY_GROUP_CODE,
            userRememberableData.SAGE_50_COMPANY_GROUP_GUID_ID,
            userRememberableData.GP_USU_ID
         );

      }
   }
}
