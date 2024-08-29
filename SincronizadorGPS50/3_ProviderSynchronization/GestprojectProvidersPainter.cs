using SincronizadorGPS50.GestprojectDataManager;
using SincronizadorGPS50.Workflows.Clients;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
   internal class GestprojectProvidersPainter : ISynchronizableEntityPainter
   {
      public void PaintEntityListOnDataTable
      (
         List<GestprojectProviderModel> proccessedGestprojectProviders, 
         DataTable dataTable, 
         List<(string columnName, string friendlyName, Type columnType, string columnDefinition)> tableFieldsTupleList
      )
      {
         try
         {
            foreach (GestprojectProviderModel item in proccessedGestprojectProviders)
            {
               DataRow row = dataTable.NewRow();

               row[0] = item.synchronization_table_id;
               row[1] = item.synchronization_status;
               row[2] = item.PAR_ID;

               row[3] = item.PAR_SUBCTA_CONTABLE_2;
               row[4] = item.fullName;
               row[5] = item.PAR_NOMBRE_COMERCIAL;
               row[6] = item.PAR_CIF_NIF;
               row[7] = item.PAR_DIRECCION_1;
               row[8] = item.PAR_CP_1;
               row[9] = item.PAR_LOCALIDAD_1;
               row[10] = item.PAR_PROVINCIA_1;
               row[11] = item.PAR_PAIS_1;
               row[12] = item.sage50_code;
               row[13] = item.sage50_guid_id;
               row[14] = item.sage50_company_group_name;
               row[15] = item.sage50_company_group_code;
               row[16] = item.sage50_company_group_main_code;
               row[17] = item.sage50_company_group_guid_id;

               row[18] = item.last_record;
               row[19] = item.parent_gesproject_user_id;

               int commentsLenght = item.comments.Length;
               row[20] = (commentsLenght > 1000 ? item.comments.Substring(0, 999) : item.comments) ?? "";

               dataTable.Rows.Add(row);
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
