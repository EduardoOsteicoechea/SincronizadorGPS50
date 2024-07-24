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
    internal class CreateGestprojectSage50SynchronizationTable
    {
        internal CreateGestprojectSage50SynchronizationTable() 
        {
            string sqlString = @"
                CREATE TABLE 
                    INT_SAGE_SINC_CLIENTE 
                    (
                        id INT PRIMARY KEY IDENTITY(1,1), 
                        synchronization_status VARCHAR(MAX), 
                        gestproject_id INT, 
                        sage50_code VARCHAR(MAX), 
                        sage50_guid_id VARCHAR(MAX), 
                        sage50_instance VARCHAR(MAX),
                        last_record DATETIME DEFAULT GETDATE() NOT NULL
                    )
                ;";

            using(SqlCommand SQLCommand = new SqlCommand(sqlString, DataHolder.GestprojectSQLConnection))
            {
                if(SQLCommand.ExecuteNonQuery() > 0)
                {
                    //MessageBox.Show("Se creó la tabla INT_SAGE_SINC_CLIENTE exitosamente.");
                }
                else
                {
                    //MessageBox.Show("No se logró crear la tabla INT_SAGE_SINC_CLIENTE.");
                };
            };
        }
    }
}
