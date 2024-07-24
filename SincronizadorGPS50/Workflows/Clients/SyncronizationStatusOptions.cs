using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
    internal static class SynchronizationStatusOptions
    {
    // No sincronizado
    // Desincronizado..... Mostrar el campo de error y el valor en Sage
    // Sincronizado
        public static string Nunca_ha_sido_sincronizado { get; } = "Nunca ha sido sincronizado";
        public static string Sincronizado { get; } = "Sincronizado";
        public static string Desincronizado { get; } = "Desincronizado";

        //public static string Fue_sincronizado_alguna_vez { get; } = "Fue_sincronizado_alguna_vez";

        //public static string Desincronizado_con_Gestproject_por_verificar_Sage50 { get; } = "Desincronizado_con_Gestproject_por_verificar_Sage50";

        //public static string Desincronizado_con_Sage50_por_verificar_Gestproject { get; } = "Desincronizado_con_Sage50_por_verificar_Gestproject";

        //public static string Desincronizado_con_ambos_Gestproject_y_Sage50 { get; } = "Desincronizado_con_ambos_Gestproject_y_Sage50";

        //public static string Sincronizado_con_Gestproject_y_Desincronizado_con_Sage50 { get; } = "Sincronizado_con_Gestproject_y_Desincronizado_con_Sage50";

        //public static string Sincronizado_con_Sage50_y_Desincronizado_con_Gestproject { get; } = "Sincronizado_con_Sage50_y_Desincronizado_con_Gestproject";

        //public static string Sincronizado_con_ambos_Gestproject_y_Sage50 { get; } = "Sincronizado_con_ambos_Gestproject_y_Sage50";
    }
}
