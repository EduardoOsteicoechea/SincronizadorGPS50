using SincronizadorGPS50.Sage50Connector;
using System.Collections.Generic;

namespace SincronizadorGPS50
{
   internal interface ISage50EntitiesProvider
   {
      List<Sage50ProviderModel> GetProviders();
   }
}
