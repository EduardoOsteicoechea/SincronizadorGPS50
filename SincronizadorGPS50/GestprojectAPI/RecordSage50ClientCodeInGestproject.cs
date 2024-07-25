using System.Data.SqlClient;
namespace SincronizadorGPS50.GestprojectAPI
{
    internal class RecordSage50ClientCodeInGestproject
    {
        internal RecordSage50ClientCodeInGestproject(int gestprojectClientid, string sage50ClientCode)
        {
            string sqlString = $"UPDATE PARTICIPANTE SET PAR_SUBCTA_CONTABLE = {sage50ClientCode} WHERE PAR_ID = {gestprojectClientid};";

            using(SqlCommand SQLCommand = new SqlCommand(sqlString, DataHolder.GestprojectSQLConnection))
            {
                SQLCommand.ExecuteNonQuery();
            };
        }
    }
}
