using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace GestprojectDataManager
{
    public class GestprojectParticipants
    {
        public bool IsSuccessful { get; set; } = false;
        public List<GestprojectParticipantModel> Get(System.Data.SqlClient.SqlConnection connection, List<int>IdList = null) 
        {
            try
            {
                List<GestprojectParticipantModel> gestprojectClientClassList = new List<GestprojectParticipantModel>();
                string sqlString = "";

                if(IdList == null)
                {
                    sqlString = @"
                    SELECT 
                        PAR_ID,
                        PAR_SUBCTA_CONTABLE,
                        PAR_SUBCTA_CONTABLE_2,
                        PAR_NOMBRE,
                        PAR_NOMBRE_COMERCIAL,
                        PAR_CIF_NIF,
                        PAR_DIRECCION_1,
                        PAR_CP_1,
                        PAR_LOCALIDAD_1,
                        PAR_PROVINCIA_1,
                        PAR_PAIS_1
                    FROM 
                        PARTICIPANTE;";
                }
                else
                {
                    sqlString = $@"
                    SELECT 
                        PAR_ID,
                        PAR_SUBCTA_CONTABLE,
                        PAR_SUBCTA_CONTABLE_2,
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
                    WHERE PAR_ID IN ({string.Join(",", IdList)})
                        ;";
                };

                using(System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(sqlString, connection))
                {
                    using(System.Data.SqlClient.SqlDataReader reader = sqlCommand.ExecuteReader())
                    {

                        while(reader.Read())
                        {
                            GestprojectParticipantModel client = new GestprojectParticipantModel();

                            client.PAR_ID = Convert.ToInt32(reader.GetValue(0));
                            client.PAR_SUBCTA_CONTABLE = Convert.ToString(reader.GetValue(1));
                            client.PAR_SUBCTA_CONTABLE_2 = Convert.ToString(reader.GetValue(2));
                            client.PAR_NOMBRE = Convert.ToString(reader.GetValue(3));
                            client.PAR_NOMBRE_COMERCIAL = Convert.ToString(reader.GetValue(4));
                            client.PAR_CIF_NIF = Convert.ToString(reader.GetValue(5));
                            client.PAR_DIRECCION_1 = Convert.ToString(reader.GetValue(6));
                            client.PAR_CP_1 = Convert.ToString(reader.GetValue(7));
                            client.PAR_LOCALIDAD_1 = Convert.ToString(reader.GetValue(8));
                            client.PAR_PROVINCIA_1 = Convert.ToString(reader.GetValue(9));
                            client.PAR_PAIS_1 = Convert.ToString(reader.GetValue(10));

                            gestprojectClientClassList.Add(client);
                        };
                    };
                };

                return gestprojectClientClassList;
            }
            catch(System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show($"Error during data retrieval: \n\n{ex.Message}");
                return null;
            };
        }
    }
}
