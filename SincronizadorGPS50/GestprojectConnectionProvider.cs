using SincronizadorGPS50.GestprojectDataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
   internal class GestprojectConnectionManager : IGestprojectConnectionManager
   {
      public System.Data.SqlClient.SqlConnection GestprojectSqlConnection { get; set; }
      public SynchronizerUserRememberableDataModel GestprojectUserRememberableData { get; set; }
      public GestprojectConnectionManager() 
      {
         GestprojectSqlConnection = new SincronizadorGPS50.GestprojectConnector.ConnectionManager().Connect();

         GestprojectUserRememberableData = ManageRememberableUserData.GetSynchronizerUserRememberableDataForConnection(
            GestprojectDataHolder.GestprojectDatabaseConnection
         );
      }
   }
}
