using System.Data.SqlClient;

namespace SincronizadorGPS50.GestprojectDataManager
{
   public class WasGestprojectClientRegistered
   {
      public bool ItIs { get; set; } = false;
      public WasGestprojectClientRegistered
      (
         System.Data.SqlClient.SqlConnection connection, 
         GestprojectDataManager.GestprojectClient client
      ){
         try
         {
            connection.Open();

            string sqlString = $"SELECT id FROM INT_SAGE_SINC_CLIENTE WHERE PAR_ID={client.PAR_ID};";

            using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
            {
               using(SqlDataReader reader = sqlCommand.ExecuteReader())
               {
                  while(reader.Read())
                  {
                     if((int)reader.GetValue(0) != -1)
                     {
                        ItIs = true;
                        break;
                     }
                  };
               };
            };
         }
         catch(SqlException exception)
         {
            throw exception;
         }
         finally
         {
            connection.Close();
         };
      }
   }
}
