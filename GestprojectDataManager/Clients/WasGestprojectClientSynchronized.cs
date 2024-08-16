using System.Data.SqlClient;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   public class WasGestprojectClientSynchronized
   {
      public bool ItIs { get; set; } = false;
      public WasGestprojectClientSynchronized
      (
       System.Data.SqlClient.SqlConnection connection,
       GestprojectDataManager.GestprojectClient client
      )
      {
            try
            {
               connection.Open();

               string sqlString = $"SELECT sage50_guid_id FROM INT_SAGE_SINC_CLIENTE WHERE gestproject_id={client.PAR_ID};";

               using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
               {
                  using(SqlDataReader reader = sqlCommand.ExecuteReader())
                  {
                     while(reader.Read())
                     {
                        if((string)reader.GetValue(0) != "" && (string)reader.GetValue(0) != null)
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
