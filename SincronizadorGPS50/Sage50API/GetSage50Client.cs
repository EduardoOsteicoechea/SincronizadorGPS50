using sage.ew.db;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SincronizadorGPS50
{
    internal class GetSage50Client
    {
        internal string Codigo {  get; set; }
        internal string Cif {  get; set; }
        internal string Nombre {  get; set; }
        internal string Direccion {  get; set; }
        internal string Codpost {  get; set; }
        internal string Poblacion {  get; set; }
        internal string Provincia {  get; set; }
        internal string Pais {  get; set; }
        internal bool Exists { get; set; } = false;
        public GetSage50Client(GestprojectClient client)
        {
            string getSage50ClientSQLQuery = @"
                SELECT 
                    codigo,
                    cif,
                    nombre,
                    direccion,
                    codpost,
                    poblacion,
                    provincia,
                    pais
                FROM " + $"{DB.SQLDatabase("gestion","clientes")} WHERE guid_id='{client.sage50_guid_id}';";

            DataTable sage50ClientsDataTable = new DataTable();

            DB.SQLExec(getSage50ClientSQLQuery, ref sage50ClientsDataTable);

            DataHolder.Sage50ClientClassList.Clear();

            if(sage50ClientsDataTable.Rows.Count > 0)
            {
                Exists = true;
                Codigo = sage50ClientsDataTable.Rows[0].ItemArray[0].ToString().Trim();
                Cif = sage50ClientsDataTable.Rows[0].ItemArray[1].ToString().Trim();
                Nombre = sage50ClientsDataTable.Rows[0].ItemArray[2].ToString().Trim();
                Direccion = sage50ClientsDataTable.Rows[0].ItemArray[3].ToString().Trim();
                Codpost = sage50ClientsDataTable.Rows[0].ItemArray[4].ToString().Trim();
                Poblacion = sage50ClientsDataTable.Rows[0].ItemArray[5].ToString().Trim();
                Provincia = sage50ClientsDataTable.Rows[0].ItemArray[6].ToString().Trim();
                Pais = sage50ClientsDataTable.Rows[0].ItemArray[7].ToString().Trim();
            }
        }
    }
}
