//using System;
//using System.Data;
//using System.Data.SqlClient;

//namespace SincronizadorGPS50.Workflows.Clients
//{
//    internal class AddClientToSyncronizationUITable
//    {
//        internal AddClientToSyncronizationUITable
//        (
//            DataTable sincronizationTable,
//            GestprojectClient gestprojectClient,
//            string comments = ""
//        )
//        {
//            string sqlString = @"
//            SELECT 
//                synchronization_status,
//                id, 
//                PAR_ID,
//                sage50_client_code,
//                sage50_guid_id,
//                PAR_NOMBRE,
//                PAR_NOMBRE_COMERCIAL,
//                PAR_CIF_NIF,
//                PAR_DIRECCION_1,
//                PAR_CP_1,
//                PAR_LOCALIDAD_1,
//                PAR_PROVINCIA_1,
//                PAR_PAIS_1,
//                sage50_instance,
//                comments,
//                last_record
//            FROM 
//                INT_SAGE_SINC_CLIENTE_IMAGEN 
//            WHERE 
//                PAR_ID=" + gestprojectClient.PAR_ID
//            + ";";

//            SqlCommand sqlCommand = new SqlCommand(sqlString, DataHolder.GestprojectSQLConnection);

//            using(SqlDataReader reader = sqlCommand.ExecuteReader())
//            {
//                while(reader.Read())
//                {
//                    DataRow row = sincronizationTable.NewRow();

//                    row[0] = (string)reader.GetValue(0);
//                    row[1] = (int)reader.GetValue(1);
//                    row[2] = (int)reader.GetValue(2);
//                    row[3] = (string)reader.GetValue(3);
//                    row[4] = (string)reader.GetValue(4);
//                    row[5] = (string)reader.GetValue(5);
//                    row[6] = (string)reader.GetValue(6);
//                    row[7] = (string)reader.GetValue(7);
//                    row[8] = (string)reader.GetValue(8);
//                    row[9] = (string)reader.GetValue(9);
//                    row[10] = (string)reader.GetValue(10);
//                    row[11] = (string)reader.GetValue(11);
//                    row[12] = (string)reader.GetValue(12);
//                    row[13] = (string)reader.GetValue(13);
//                    if(comments != "") 
//                    {
//                        row[14] = comments;
//                    } 
//                    else
//                    {
//                        row[14] = (string)reader.GetValue(14);
//                    };
//                    DateTime dateTime = (System.DateTime)reader.GetValue(15);
//                    row[15] = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

//                    sincronizationTable.Rows.Add(row);
//                };
//            };           
//        }
//    }
//}
