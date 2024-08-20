using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class GetSingleCustomerFromSynchronizationTable
   {
      public GestprojectCustomer GestprojectCustomer { get; set; } = new GestprojectCustomer();
      public GetSingleCustomerFromSynchronizationTable
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
                     GestprojectCustomer.PAR_NOMBRE = Convert.ToString(reader.GetValue(0));
                     GestprojectCustomer.PAR_CIF_NIF = Convert.ToString(reader.GetValue(1));
                     GestprojectCustomer.PAR_DIRECCION_1 = Convert.ToString(reader.GetValue(2));
                     GestprojectCustomer.PAR_CP_1 = Convert.ToString(reader.GetValue(3));
                     GestprojectCustomer.PAR_LOCALIDAD_1 = Convert.ToString(reader.GetValue(4));
                     GestprojectCustomer.PAR_PROVINCIA_1 = Convert.ToString(reader.GetValue(5));
                     GestprojectCustomer.PAR_PAIS_1 = Convert.ToString(reader.GetValue(6));
                     GestprojectCustomer.synchronization_status = Convert.ToString(reader.GetValue(7));
                     GestprojectCustomer.sage50_company_group_name = Convert.ToString(reader.GetValue(8));
                     GestprojectCustomer.sage50_company_group_code = Convert.ToString(reader.GetValue(9));
                     GestprojectCustomer.sage50_company_group_main_code = Convert.ToString(reader.GetValue(10));
                     GestprojectCustomer.sage50_company_group_guid_id = Convert.ToString(reader.GetValue(11));
                     GestprojectCustomer.PAR_ID = Convert.ToInt32(reader.GetValue(12));

                     GestprojectCustomer.sage50_client_code = System.Convert.ToString(reader.GetValue(13)) == "" || Convert.ToString(reader.GetValue(13)) == null || Convert.ToString(reader.GetValue(13)) == null ? "" : System.Convert.ToString(reader.GetValue(13));

                     GestprojectCustomer.sage50_guid_id = System.Convert.ToString(reader.GetValue(14)) == "" || Convert.ToString(reader.GetValue(14)) == null || Convert.ToString(reader.GetValue(14)) == null ? "" : System.Convert.ToString(reader.GetValue(14));

                     GestprojectCustomer.comments = System.Convert.ToString(reader.GetValue(15)) == "" || Convert.ToString(reader.GetValue(15)) == null || Convert.ToString(reader.GetValue(15)) == null ? "" : System.Convert.ToString(reader.GetValue(15));
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
