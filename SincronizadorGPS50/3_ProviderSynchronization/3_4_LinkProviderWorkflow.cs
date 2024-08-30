using SincronizadorGPS50.GestprojectDataManager;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal class LinkProviderWorkflow
   {
      public LinkProviderWorkflow(System.Data.SqlClient.SqlConnection connection, GestprojectCustomer gestprojectClient,
         CustomerSyncronizationTableSchema tableSchema)
      {
         try
         {
            //new SincronizadorGPS50.Sage50Connector.UpdateSage50Customer(
            //   gestprojectClient.sage50_guid_id,
            //   gestprojectClient.PAR_PAIS_1,
            //   gestprojectClient.PAR_NOMBRE,
            //   gestprojectClient.PAR_CIF_NIF,
            //   gestprojectClient.PAR_CP_1,
            //   gestprojectClient.PAR_DIRECCION_1,
            //   gestprojectClient.PAR_PROVINCIA_1
            //);

            //gestprojectClient.sage50_company_group_name = sage50CompanyGroup.CompanyName;
            //gestprojectClient.sage50_company_group_code = sage50CompanyGroup.CompanyCode;
            //gestprojectClient.sage50_company_group_main_code = sage50CompanyGroup.CompanyMainCode;
            //gestprojectClient.sage50_company_group_guid_id = sage50CompanyGroup.CompanyGuidId;
            //gestprojectClient.parent_gesproject_user_id = userRememberableData.GP_USU_ID;

            //new GestprojectDataManager.RegisterNewSage50ClientData(
            //   connection,
            //   gestprojectClient.PAR_ID,
            //   gestprojectClient.sage50_client_code,
            //   gestprojectClient.sage50_guid_id,
            //   gestprojectClient.sage50_company_group_name,
            //   gestprojectClient.sage50_company_group_code,
            //   gestprojectClient.sage50_company_group_main_code,
            //   gestprojectClient.sage50_company_group_guid_id,
            //   gestprojectClient.parent_gesproject_user_id,
            //   "Desincronizado",
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
