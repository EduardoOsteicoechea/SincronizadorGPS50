﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class ClearCustomerSynchronizationData
   {
      public List<GestprojectCustomer> GestprojectClientList { get; set; } = new List<GestprojectCustomer>();
      public ClearCustomerSynchronizationData
      (
         GestprojectCustomer customer
      )
      {
         try
         {
            customer.synchronization_table_id = -1;
            customer.synchronization_status = "";
            customer.sage50_client_code = "";
            customer.sage50_guid_id = "";
            customer.sage50_company_group_name = "";
            customer.sage50_company_group_code = "";
            customer.sage50_company_group_main_code = "";
            customer.comments = "";
            customer.parent_gesproject_user_id = -1;
            customer.last_record = DateTime.Now;
         }
         catch(System.Exception exception)
         {
            throw new System.Exception(
               $"At:\n\nSincronizadorGPS50.GestprojectDataManager\n.ClearCustomerSynchronizationData:\n\n{exception.Message}"
            );
         };
      }
   }
}
