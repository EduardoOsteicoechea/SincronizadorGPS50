﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectAPI
{
    internal class WasGestprojectClientRegistered
    {
        internal bool ItIs {  get; set; } = false;
        internal WasGestprojectClientRegistered(GestprojectClient client)
        {
            using(System.Data.SqlClient.SqlConnection connection = GestprojectDatabase.Connect())
            {
                try
                {
                    connection.Open();

                    string sqlString = $"SELECT id FROM INT_SAGE_SINC_CLIENTE WHERE gestproject_id={client.PAR_ID};";

                    using(SqlCommand sqlCommand = new SqlCommand(sqlString, connection))
                    {
                        using(SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                if((int)reader.GetValue(0) != -1)
                                {
                                    client.synchronization_table_id = (int)reader.GetValue(0);
                                    ItIs = true;
                                    break;
                                }
                            };
                        };
                    };
                }
                catch(SqlException ex)
                {
                    MessageBox.Show($"Error during data retrieval: \n\n{ex.Message}");
                }
                finally
                {
                    connection.Close();
                };
            };
        }
    }
}
