using SincronizadorGPS50.GestprojectDataManager;
using System.Collections.Generic;

namespace SincronizadorGPS50
{
   internal interface IGestprojectEntitiesProvider
   {
      List<GestprojectProviderModel> GetProviders(System.Data.SqlClient.SqlConnection connection);
   }
}
