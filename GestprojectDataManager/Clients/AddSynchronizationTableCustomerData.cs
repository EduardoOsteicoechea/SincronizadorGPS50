using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class AddSynchronizationTableCustomerData
   {
      public AddSynchronizationTableCustomerData
      (
         System.Data.SqlClient.SqlConnection connection,
         GestprojectCustomer gestprojectCustomer,
         string companyGroupGuid
      )
      {
         try
         {
            connection.Open();

            string sqlString = $@"
            SELECT 
               {ClientSynchronizationTableSchema.SynchronizationTableClientIdColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.SynchronizationStatusColumn.ColumnDatabaseName},

               {ClientSynchronizationTableSchema.Sage50ClientCodeColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientGuidIdColumn.ColumnDatabaseName},

               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupNameColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupCodeColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupMainCodeColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupGuidIdColumn.ColumnDatabaseName},

               {ClientSynchronizationTableSchema.CommentsColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.GestprojectClientParentUserIdColumn.ColumnDatabaseName},
               {ClientSynchronizationTableSchema.ClientLastUpdateTerminalColumn.ColumnDatabaseName}
            FROM 
               {ClientSynchronizationTableSchema.TableName} 
            WHERE 
               {ClientSynchronizationTableSchema.GestprojectClientIdColumn.ColumnDatabaseName}={gestprojectCustomer.PAR_ID}
            AND
               {ClientSynchronizationTableSchema.Sage50ClientCompanyGroupGuidIdColumn.ColumnDatabaseName}='{companyGroupGuid}'               
            ;";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     gestprojectCustomer.synchronization_table_id = Convert.ToInt32(reader.GetValue(0).GetType().Name == "DBNull" ? -1 : reader.GetValue(0));
                     gestprojectCustomer.synchronization_status = Convert.ToString(reader.GetValue(1).GetType().Name == "DBNull" ? "" : reader.GetValue(1));

                     gestprojectCustomer.sage50_client_code = Convert.ToString(reader.GetValue(2).GetType().Name == "DBNull" ? "" : reader.GetValue(2));
                     gestprojectCustomer.sage50_guid_id = Convert.ToString(reader.GetValue(3).GetType().Name == "DBNull" ? "" : reader.GetValue(3));

                     gestprojectCustomer.sage50_company_group_name = Convert.ToString(reader.GetValue(4).GetType().Name == "DBNull" ? "" : reader.GetValue(4));
                     gestprojectCustomer.sage50_company_group_code = Convert.ToString(reader.GetValue(5).GetType().Name == "DBNull" ? "" : reader.GetValue(5));
                     gestprojectCustomer.sage50_company_group_main_code = Convert.ToString(reader.GetValue(6).GetType().Name == "DBNull" ? "" : reader.GetValue(6));
                     gestprojectCustomer.sage50_company_group_guid_id = Convert.ToString(reader.GetValue(7).GetType().Name == "DBNull" ? "" : reader.GetValue(7));

                     gestprojectCustomer.comments = Convert.ToString(reader.GetValue(8).GetType().Name == "DBNull" ? "" : reader.GetValue(8));
                     gestprojectCustomer.comments = gestprojectCustomer.comments.Length > 2000 ? gestprojectCustomer.comments.Substring(0,2000) : gestprojectCustomer.comments;

                     gestprojectCustomer.parent_gesproject_user_id = Convert.ToInt32(reader.GetValue(9).GetType().Name == "DBNull" ? -1 : reader.GetValue(9));
                     gestprojectCustomer.last_record = Convert.ToDateTime(reader.GetValue(10).GetType().Name == "DBNull" ? DateTime.Now : reader.GetValue(10));
                  };
               };
            };
         }
         catch(System.Exception exception)
         {
            throw new System.Exception(
               $"At:\n\nSincronizadorGPS50.GestprojectDataManager\n.AddSynchronizationTableCustomerData:\n\n{exception.Message}"
            );
         }
         finally
         {
            connection.Close();
         };
      }
   }
}
