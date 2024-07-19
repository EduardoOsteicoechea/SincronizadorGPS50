using System.Data;
using System.Reflection;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class AddRowToClientsSincronizationTable
    {
        internal AddRowToClientsSincronizationTable
        (
            DataTable sincronizationTable,
            bool sage50ClientIsAlreadySincronized,
            GestprojectClient gestprojectClient,
            string Sage50CurrentClientCode,
            string Sage50ClientGUID_ID
        ) 
        {
            DataRow row = sincronizationTable.NewRow();
            PropertyInfo[] sincronizationTableProperties = typeof(ClientSyncronizationStateTable).GetProperties();
            foreach(PropertyInfo prop in sincronizationTableProperties)
            {
                if(sage50ClientIsAlreadySincronized)
                {
                    row[0] = "Sincronizado";
                }
                else
                {
                    row[0] = "No Sincronizado";
                };
                row[1] = sincronizationTable.Rows.Count;
                row[2] = gestprojectClient.PAR_ID;
                row[3] = Sage50CurrentClientCode;
                row[4] = Sage50ClientGUID_ID;
                row[5] = Sage50CurrentClientCode;
                row[6] = gestprojectClient.PAR_NOMBRE;
                row[7] = gestprojectClient.PAR_NOMBRE_COMERCIAL;
                row[8] = gestprojectClient.PAR_CIF_NIF;
                row[9] = gestprojectClient.PAR_DIRECCION_1;
                row[10] = gestprojectClient.PAR_CP_1;
                row[11] = gestprojectClient.PAR_LOCALIDAD_1;
                row[12] = gestprojectClient.PAR_PROVINCIA_1;
                row[13] = gestprojectClient.PAR_PAIS_1;
                row[14] = DataHolder.Sage50LocalTerminalPath;
            };
            sincronizationTable.Rows.Add(row);
        }
    }
}
