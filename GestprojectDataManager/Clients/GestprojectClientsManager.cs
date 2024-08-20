using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class GestprojectClientsManager
   {
      public bool IsSuccessful { get; set; } = false;
      public List<GestprojectCustomer> GetClients(System.Data.SqlClient.SqlConnection connection, List<int> IdList = null)
      {
         try
         {
            connection.Open();

            List<int> gestProjectClientIdList = new List<int>();
            List<GestprojectCustomer> gestprojectClientList = new List<GestprojectCustomer>();

            string sqlString = "";

            if(IdList == null)
            {
               sqlString = "SELECT * FROM PAR_TPA;";
            }
            else
            {
               sqlString = $"SELECT * FROM PAR_TPA WHERE ID IN ({string.Join(",", IdList)});";
            };

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     if(reader.GetValue(0).ToString() == "1")
                     {
                        gestProjectClientIdList.Add(Convert.ToInt32(reader.GetValue(1)));
                     };
                  };
                  gestProjectClientIdList.Distinct().ToList();
               };
            };

            List<GestprojectParticipantModel> gestprojectClientParticipantList = new GestprojectParticipants().Get(connection, gestProjectClientIdList);

            for(global::System.Int32 i = 0; i < gestprojectClientParticipantList.Count; i++)
            {
               GestprojectParticipantModel gestprojectClientParticipant = gestprojectClientParticipantList[i];
               GestprojectCustomer gestprojectClient = new GestprojectCustomer();

               gestprojectClient.PAR_ID = gestprojectClientParticipant.PAR_ID;
               gestprojectClient.PAR_SUBCTA_CONTABLE = gestprojectClientParticipant.PAR_SUBCTA_CONTABLE;
               gestprojectClient.PAR_NOMBRE = gestprojectClientParticipant.PAR_NOMBRE;
               gestprojectClient.PAR_NOMBRE_COMERCIAL = gestprojectClientParticipant.PAR_NOMBRE_COMERCIAL;
               gestprojectClient.PAR_CIF_NIF = gestprojectClientParticipant.PAR_CIF_NIF;
               gestprojectClient.PAR_DIRECCION_1 = gestprojectClientParticipant.PAR_DIRECCION_1;
               gestprojectClient.PAR_CP_1 = gestprojectClientParticipant.PAR_CP_1;
               gestprojectClient.PAR_LOCALIDAD_1 = gestprojectClientParticipant.PAR_LOCALIDAD_1;
               gestprojectClient.PAR_PROVINCIA_1 = gestprojectClientParticipant.PAR_PROVINCIA_1;
               gestprojectClient.PAR_PAIS_1 = gestprojectClientParticipant.PAR_PAIS_1;
               gestprojectClient.PAR_APELLIDO_1 = gestprojectClientParticipant.PAR_APELLIDO_1;
               gestprojectClient.PAR_APELLIDO_2 = gestprojectClientParticipant.PAR_APELLIDO_2;

               gestprojectClientList.Add(gestprojectClient);
            };

            IsSuccessful = true;

            return gestprojectClientList;
         }
         catch(SqlException exception)
         {
            throw new System.Exception(
               $"At:\n\nSincronizadorGPS50.GestprojectDataManager\n.GestprojectClientsManager\n.GetClients\r\n:\n\n{exception.Message}"
            );
         }
         finally
         {
            connection.Close();
         };
      }
   }
}
