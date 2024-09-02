using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace SincronizadorGPS50
{
   internal class EntityPainter : ISynchronizableEntityPainter
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

               row[0] = item.ID.GetValueOrDefault();
               row[1] = item.SYNC_STATUS;
               row[2] = item.PAR_ID.GetValueOrDefault();

               row[3] = item.PAR_SUBCTA_CONTABLE_2;
               row[4] = item.NOMBRE_COMPLETO;
               row[5] = item.PAR_NOMBRE_COMERCIAL;
               row[6] = item.PAR_CIF_NIF;
               row[7] = item.PAR_DIRECCION_1;
               row[8] = item.PAR_CP_1;
               row[9] = item.PAR_LOCALIDAD_1;
               row[10] = item.PAR_PROVINCIA_1;
               row[11] = item.PAR_PAIS_1;
               row[12] = item.S50_CODE;
               row[13] = item.S50_GUID_ID;
               row[14] = item.S50_COMPANY_GROUP_NAME;
               row[15] = item.S50_COMPANY_GROUP_CODE;
               row[16] = item.S50_COMPANY_GROUP_MAIN_CODE;
               row[17] = item.S50_COMPANY_GROUP_GUID_ID;

               row[18] = item.LAST_UPDATE.GetValueOrDefault();
               row[19] = item.GP_USU_ID.GetValueOrDefault();

               int commentsLenght = item.COMMENTS.Length;
               row[20] = (commentsLenght > 1000 ? item.COMMENTS.Substring(0, 999) : item.COMMENTS) ?? "";

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
