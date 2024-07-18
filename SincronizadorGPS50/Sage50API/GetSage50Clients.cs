using sage.ew.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
    internal class GetSage50Clients
    {
        public GetSage50Clients()
        {
            string sqlCommandString = $"SELECT codigo, cif, nombre, nombre2, direccion, codpost, poblacion, provincia, pais, email, http, guid_id FROM {DB.SQLDatabase("gestion","clientes")}";

            DataTable sage50ClientsDataTable = new DataTable();

            DB.SQLExec(sqlCommandString, ref sage50ClientsDataTable);

            DataHolder.Sage50ClientClassList.Clear();

            for(int i = 0; i < sage50ClientsDataTable.Rows.Count; i++)
            {
                Sage50Client sage50Client = new Sage50Client();
                sage50Client.CODIGO = sage50ClientsDataTable.Rows[i].ItemArray[0].ToString().Trim();
                sage50Client.CIF = sage50ClientsDataTable.Rows[i].ItemArray[1].ToString().Trim();
                sage50Client.NOMBRE = sage50ClientsDataTable.Rows[i].ItemArray[2].ToString().Trim();
                sage50Client.NOMBRE2 = sage50ClientsDataTable.Rows[i].ItemArray[3].ToString().Trim();
                sage50Client.DIRECCION = sage50ClientsDataTable.Rows[i].ItemArray[4].ToString().Trim();
                sage50Client.CODPOST = sage50ClientsDataTable.Rows[i].ItemArray[5].ToString().Trim();
                sage50Client.POBLACION = sage50ClientsDataTable.Rows[i].ItemArray[6].ToString().Trim();
                sage50Client.PROVINCIA = sage50ClientsDataTable.Rows[i].ItemArray[7].ToString().Trim();
                sage50Client.PAIS = sage50ClientsDataTable.Rows[i].ItemArray[8].ToString().Trim();
                sage50Client.EMAIL = sage50ClientsDataTable.Rows[i].ItemArray[9].ToString().Trim();
                sage50Client.HTTP = sage50ClientsDataTable.Rows[i].ItemArray[10].ToString().Trim();
                sage50Client.GUID_ID = sage50ClientsDataTable.Rows[i].ItemArray[11].ToString().Trim();

                DataHolder.Sage50ClientClassList.Add(sage50Client);

                //PrintClassProperties.Print(sage50Client);
            };
        }
    }
}
