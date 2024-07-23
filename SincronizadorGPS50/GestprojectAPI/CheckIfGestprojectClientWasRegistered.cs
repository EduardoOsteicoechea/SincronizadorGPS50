using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectAPI
{
    internal class CheckIfGestprojectClientWasRegistered
    {
        internal bool ItIs {  get; set; } = false;
        internal CheckIfGestprojectClientWasRegistered(GestprojectClient client) 
        {
            string sqlString = $"SELECT * FROM INT_SAGE_SINC_CLIENTE_IMAGEN WHERE PAR_ID={client.PAR_ID};";

            SqlCommand sqlCommand = new SqlCommand(sqlString, DataHolder.GestprojectSQLConnection);

            using(SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while(reader.Read())
                {
                    //MessageBox.Show("(int)reader.GetValue(0): " + (int)reader.GetValue(0) + "\n\n" + "client.PAR_ID: " + client.PAR_ID);
                    if((int)reader.GetValue(0) == client.PAR_ID || (int)reader.GetValue(0) != null)
                    {
                        ItIs = true;
                        break;
                    }
                };
            };
        }
    }
}
