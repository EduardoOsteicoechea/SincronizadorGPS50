using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class GetSingleClientFromSynchronizationTable
   {
      public GestprojectClient GestprojectClient { get; set; } = new GestprojectClient();
      public GetSingleClientFromSynchronizationTable
      (
         System.Data.SqlClient.SqlConnection connection,
         int gestprojectClientId
      )
      {
         try
         {
            connection.Open();

            string sqlString = $@"
            SELECT 
               {ClientSynchronizationTableSchema.GestprojectClientNameColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientCIFNIFColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientAddressColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientPostalCodeColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientLocalityColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientProvinceColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientCountryColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.SynchronizationStatusColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupNameColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupCodeColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupMainCodeColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupGuidIdColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCodeColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientGuidIdColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.CommentsColumn.ColumnDatabaseName}
            FROM 
               {ClientSynchronizationTableSchema.TableName} 
            WHERE 
               {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName}={gestprojectClientId};";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     GestprojectClient.PAR_NOMBRE = Convert.ToString(reader.GetValue(0));
                     GestprojectClient.PAR_CIF_NIF = Convert.ToString(reader.GetValue(1));
                     GestprojectClient.PAR_DIRECCION_1 = Convert.ToString(reader.GetValue(2));
                     GestprojectClient.PAR_CP_1 = Convert.ToString(reader.GetValue(3));
                     GestprojectClient.PAR_LOCALIDAD_1 = Convert.ToString(reader.GetValue(4));
                     GestprojectClient.PAR_PROVINCIA_1 = Convert.ToString(reader.GetValue(5));
                     GestprojectClient.PAR_PAIS_1 = Convert.ToString(reader.GetValue(6));
                     GestprojectClient.synchronization_status = Convert.ToString(reader.GetValue(7));
                     GestprojectClient.sage50_company_group_name = Convert.ToString(reader.GetValue(8));
                     GestprojectClient.sage50_company_group_code = Convert.ToString(reader.GetValue(9));
                     GestprojectClient.sage50_company_group_main_code = Convert.ToString(reader.GetValue(10));
                     GestprojectClient.sage50_company_group_guid_id = Convert.ToString(reader.GetValue(11));
                     GestprojectClient.PAR_ID = Convert.ToInt32(reader.GetValue(12));

                     GestprojectClient.sage50_client_code = System.Convert.ToString(reader.GetValue(13)) == "" || Convert.ToString(reader.GetValue(13)) == null || Convert.ToString(reader.GetValue(13)) == null ? "" : System.Convert.ToString(reader.GetValue(13));

                     GestprojectClient.sage50_guid_id = System.Convert.ToString(reader.GetValue(14)) == "" || Convert.ToString(reader.GetValue(14)) == null || Convert.ToString(reader.GetValue(14)) == null ? "" : System.Convert.ToString(reader.GetValue(14));

                     GestprojectClient.comments = System.Convert.ToString(reader.GetValue(15)) == "" || Convert.ToString(reader.GetValue(15)) == null || Convert.ToString(reader.GetValue(15)) == null ? "" : System.Convert.ToString(reader.GetValue(15));
                  };
               };
            };
         }
         catch(System.Exception exception)
         {
            throw new System.Exception(
               $"At:\n\nSincronizadorGPS50.GestprojectDataManager\n.GetClientsFromSynchronizationTable:\n\n{exception.Message}"
            );
         }
         finally
         {
            connection.Close();
         };
      }
   }
}
