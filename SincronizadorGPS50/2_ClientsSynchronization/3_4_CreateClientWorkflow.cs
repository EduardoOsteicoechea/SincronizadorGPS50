using SincronizadorGPS50.GestprojectDataManager;
using SincronizadorGPS50.Sage50Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
   internal class CreateClientWorkflow
   {
      public CreateClientWorkflow(System.Data.SqlClient.SqlConnection connection, GestprojectCustomer gestprojectClient) 
      {
         string d = "";
         CreateSage50Customer newSage50Client = new SincronizadorGPS50.Sage50Connector.CreateSage50Customer(
            gestprojectClient.PAR_PAIS_1,
            gestprojectClient.PAR_NOMBRE ,
            gestprojectClient.PAR_CIF_NIF,
            gestprojectClient.PAR_CP_1,
            gestprojectClient.PAR_DIRECCION_1,
            gestprojectClient.PAR_PROVINCIA_1
         );

         SynchronizerUserRememberableDataModel userRememberableData = ManageRememberableUserData.GetSynchronizerUserRememberableDataForConnection(
                GestprojectDataHolder.GestprojectDatabaseConnection
            );

         new GestprojectDataManager.RegisterNewSage50ClientData(
            connection,
            gestprojectClient.PAR_ID,
            newSage50Client.ClientCode,
            newSage50Client.GUID_ID,
            userRememberableData.SAGE_50_COMPANY_GROUP_NAME,
            userRememberableData.SAGE_50_COMPANY_GROUP_MAIN_CODE,
            userRememberableData.SAGE_50_COMPANY_GROUP_CODE,
            userRememberableData.SAGE_50_COMPANY_GROUP_GUID_ID,
            userRememberableData.GP_USU_ID
         );
      }
   }
}
