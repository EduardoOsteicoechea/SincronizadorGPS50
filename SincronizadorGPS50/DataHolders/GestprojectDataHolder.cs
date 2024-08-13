using GestprojectConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
    internal static class GestprojectDataHolder
    {

        internal static System.Data.SqlClient.SqlConnection GestprojectDatabaseConnection { get; set; } = null;
        internal static List<GestprojectDataManager.GestprojectClientModel> GestprojectClientList { get; set; } = null;
        internal static List<GestprojectDataManager.GestprojectProviderModel> GestprojectProviderList { get; set; } = null; 
        internal static UserSessionData LocalDeviceUserSessionData { get; set; } = null;

   }
}
