using SincronizadorGPS50.GestprojectDataManager;
using System.Linq;

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

         var sage50CompanyGroup = SincronizadorGPS50.Sage50Connector
         .Sage50CompanyGroupActions
         .GetCompanyGroups()
         .FirstOrDefault(companyGroup => companyGroup.CompanyName == Sage50ConnectionUIHolder.Sage50ConnectionUIManagerInstance.SelectCompanyGroupUI.SelectEnterpryseGroupMenu.Text);

         new GestprojectDataManager.UpdateClientSyncronizationStatus(
            connection,
            gestprojectClient.PAR_ID,
            gestprojectClient.sage50_guid_id,
            sage50CompanyGroup.CompanyGuidId,
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
            //userRememberableData.SAGE_50_COMPANY_GROUP_NAME,
            //userRememberableData.SAGE_50_COMPANY_GROUP_MAIN_CODE,
            //userRememberableData.SAGE_50_COMPANY_GROUP_CODE,
            //userRememberableData.SAGE_50_COMPANY_GROUP_GUID_ID,
            sage50CompanyGroup.CompanyName,
            sage50CompanyGroup.CompanyMainCode,
            sage50CompanyGroup.CompanyCode,
            sage50CompanyGroup.CompanyGuidId,
            userRememberableData.GP_USU_ID
         );
      }
   }
}
