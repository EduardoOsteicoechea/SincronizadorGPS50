using SincronizadorGPS50.GestprojectAPI;
using System.Data;
using System.Reflection;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class PaintSynchronizedClientOnTable
    {
        internal PaintSynchronizedClientOnTable
        (
            DataTable sincronizationTable,
            GestprojectClient gestprojectClient
        )
        {
            int synchronizationId = new GetGestprojectClientSynchronizationId(gestprojectClient).Value;
            DataRow row = sincronizationTable.NewRow();
            PropertyInfo[] sincronizationTableProperties = typeof(ClientSyncronizationStateTable).GetProperties();
            foreach(PropertyInfo prop in sincronizationTableProperties)
            {
                row[0] = "Sincronizado";
                row[1] = synchronizationId;
                row[2] = gestprojectClient.PAR_ID;
                row[3] = gestprojectClient.sage50_client_code;
                row[4] = gestprojectClient.sage50_guid_id;
                row[5] = gestprojectClient.PAR_NOMBRE;
                row[6] = gestprojectClient.PAR_NOMBRE_COMERCIAL;
                row[7] = gestprojectClient.PAR_CIF_NIF;
                row[8] = gestprojectClient.PAR_DIRECCION_1;
                row[9] = gestprojectClient.PAR_CP_1;
                row[10] = gestprojectClient.PAR_LOCALIDAD_1;
                row[11] = gestprojectClient.PAR_PROVINCIA_1;
                row[12] = gestprojectClient.PAR_PAIS_1;
                row[13] = gestprojectClient.sage50_instance_terminal;
            };
            sincronizationTable.Rows.Add(row);
        }
    }
}
