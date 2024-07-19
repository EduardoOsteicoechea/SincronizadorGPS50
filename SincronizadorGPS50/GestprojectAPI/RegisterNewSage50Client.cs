using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50.GestprojectAPI
{
    internal class RegisterNewSage50Client
    {
        public RegisterNewSage50Client
        (
            int gestProjectClientId,
            string Sage50CurrentClientCode,
            string Sage50ClientId,
            string sage50InstanceTernimalFolderPath
        ) 
        {
            string sqlString = $"INSERT INTO INT_SAGE_SINC_CLIENTE (gestproject_id, sage50_code, sage50_guid_id, sage50_instance_terminal) VALUES ({gestProjectClientId}, '{Sage50CurrentClientCode}', '{Sage50ClientId}', '{sage50InstanceTernimalFolderPath}');";

            using(SqlCommand SQLCommand = new SqlCommand(sqlString, DataHolder.GestprojectSQLConnection))
            {
                if(SQLCommand.ExecuteNonQuery() > 0)
                {
                    //MessageBox.Show($"Se insertó exsitosamente el usuario {Sage50ClientId} en la tabla INT_SAGE_SINC_CLIENTE exitosamente.");
                }
                else
                {
                    //MessageBox.Show($"No se logró inserta el usuario {Sage50ClientId} en la tabla INT_SAGE_SINC_CLIENTE.");
                };
            };
        }
    }
}
