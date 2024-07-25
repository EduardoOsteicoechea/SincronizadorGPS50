using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class GetSelectedClientsInUITable
    {
        internal List<GestprojectClient> Clients {  get; set; } = new List<GestprojectClient>();
        internal GetSelectedClientsInUITable(List<int> gestProjectIdList) 
        {
            for (int i = 0; i < gestProjectIdList.Count; i++)
            {
                string sqlString = @"
                SELECT 
                    PAR_ID,
                    PAR_SUBCTA_CONTABLE,
                    PAR_NOMBRE,
                    PAR_NOMBRE_COMERCIAL,
                    PAR_CIF_NIF,
                    PAR_DIRECCION_1,
                    PAR_CP_1,
                    PAR_LOCALIDAD_1,
                    PAR_PROVINCIA_1,
                    PAR_PAIS_1
                FROM 
                    PARTICIPANTE 
                WHERE "
                    +$"PAR_ID={gestProjectIdList[i]};";

                SqlCommand sqlCommand = new SqlCommand(sqlString, DataHolder.GestprojectSQLConnection);

                using(SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        GestprojectClient client = new GestprojectClient();

                        client.PAR_ID = (int)reader.GetValue(0);
                        client.PAR_SUBCTA_CONTABLE = (string)reader.GetValue(1);
                        client.PAR_NOMBRE = (string)reader.GetValue(2);
                        client.PAR_NOMBRE_COMERCIAL = (string)reader.GetValue(3);
                        client.PAR_CIF_NIF = (string)reader.GetValue(4);
                        client.PAR_DIRECCION_1 = (string)reader.GetValue(5);
                        client.PAR_CP_1 = (string)reader.GetValue(6);
                        client.PAR_LOCALIDAD_1 = (string)reader.GetValue(7);
                        client.PAR_PROVINCIA_1 = (string)reader.GetValue(8);
                        client.PAR_PAIS_1 = (string)reader.GetValue(9);

                        Clients.Add(client);
                    };
                };
            }
        }
    }
}
