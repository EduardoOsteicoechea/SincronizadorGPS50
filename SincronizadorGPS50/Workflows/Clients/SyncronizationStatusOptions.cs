using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
    internal static class SynchronizationStatusOptions
    {
        public static string Nunca_ha_sido_sincronizado { get; } = "Nunca ha sido sincronizado";
        public static string Desincronizado { get; } = "Desincronizado";
        public static string Sincronizado { get; } = "Sincronizado";
    }
}
