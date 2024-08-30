using SincronizadorGPS50.GestprojectDataManager;
using SincronizadorGPS50.Sage50Connector;
using System.Linq;
using System.Reflection;

namespace SincronizadorGPS50
{
   internal class CreateProviderWorkflow
   {
      public CreateProviderWorkflow
      (
         System.Data.SqlClient.SqlConnection connection, 
         GestprojectCustomer gestprojectClient,
         CustomerSyncronizationTableSchema tableSchema
      )
      {
         try
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

            CreateSage50Customer newSage50Client = new SincronizadorGPS50.Sage50Connector.CreateSage50Customer(
               gestprojectClient.PAR_PAIS_1,
               gestprojectClient.PAR_NOMBRE ,
               gestprojectClient.PAR_CIF_NIF,
               gestprojectClient.PAR_CP_1,
               gestprojectClient.PAR_DIRECCION_1,
               gestprojectClient.PAR_PROVINCIA_1
            );

            //new GestprojectDataManager.RegisterNewSage50ClientData(
            //   connection,
            //   gestprojectClient.PAR_ID,
            //   newSage50Client.ClientCode,
            //   newSage50Client.GUID_ID,
            //   sage50CompanyGroup.CompanyName,
            //   sage50CompanyGroup.CompanyMainCode,
            //   sage50CompanyGroup.CompanyCode,
            //   sage50CompanyGroup.CompanyGuidId,
            //   userRememberableData.GP_USU_ID,
            //   "Sincronizado",
            //   tableSchema
            //);
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
