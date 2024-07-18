using sage.ew.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Sage.ES.S50.NuevoEjercicio.GrupoEmpConfig.TraspasoDatos;

namespace SincronizadorGPS50.GestprojectAPI
{
    internal static class GestprojectClientsActions
    {
        public static void GetGestProjectClientsAndProviders() 
        {
            DataHolder.GestprojectSQLConnection.Open();

            string sqlString = "SELECT * FROM PAR_TPA";

            SqlCommand sqlCommand = new SqlCommand(sqlString, DataHolder.GestprojectSQLConnection);
            using(SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                int fieldCount = reader.FieldCount;

                List<int> gestProjectClientIdList = new List<int>();
                List<int> gestProjectProviderIdList = new List<int>();

                while(reader.Read())
                {
                    if(reader.GetValue(0).ToString() == "1")
                    {
                        gestProjectClientIdList.Add((int)reader.GetValue(1));
                    }
                    else if(reader.GetValue(0).ToString() == "12")
                    {
                        gestProjectProviderIdList.Add((int)reader.GetValue(1));
                    };
                };
                gestProjectClientIdList.Distinct().ToList();
                gestProjectProviderIdList.Distinct().ToList();

                DataHolder.GestprojectClientIdList = gestProjectClientIdList;
                DataHolder.GestprojectProviderIdList = gestProjectProviderIdList;

                //MessageBox.Show("DataHolder.GestprojectClientIdList: " + DataHolder.GestprojectClientIdList.Count);
                //MessageBox.Show("DataHolder.GestprojectProviderIdList: " + DataHolder.GestprojectProviderIdList.Count);
            };

            DataHolder.GestprojectSQLConnection.Close();
        }

        public static void GetGestprojectClients()
        {
            DataHolder.GestprojectSQLConnection.Open();

            string sqlString = "SELECT PAR_ID, PAR_SUBCTA_CONTABLE, PAR_NOMBRE, PAR_NOMBRE_COMERCIAL, PAR_CIF_NIF, PAR_DIRECCION_1, PAR_CP_1, PAR_LOCALIDAD_1, PAR_PROVINCIA_1, PAR_PAIS_1 FROM PARTICIPANTE;";

            SqlCommand sqlCommand = new SqlCommand(sqlString, DataHolder.GestprojectSQLConnection);

            using(SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while(reader.Read())
                {
                    if(DataHolder.GestprojectClientIdList.Contains((int)reader.GetValue(0)))
                    {
                        GestprojectClient gestprojectClient = new GestprojectClient();
                        gestprojectClient.PAR_ID = (int)reader.GetValue(0);
                        gestprojectClient.PAR_SUBCTA_CONTABLE = (string)reader.GetValue(1);
                        gestprojectClient.PAR_NOMBRE = (string)reader.GetValue(2);
                        gestprojectClient.PAR_NOMBRE_COMERCIAL = (string)reader.GetValue(3);
                        gestprojectClient.PAR_CIF_NIF = (string)reader.GetValue(4);
                        gestprojectClient.PAR_DIRECCION_1 = (string)reader.GetValue(5);
                        gestprojectClient.PAR_CP_1 = (string)reader.GetValue(6);
                        gestprojectClient.PAR_LOCALIDAD_1 = (string)reader.GetValue(7);
                        gestprojectClient.PAR_PROVINCIA_1 = (string)reader.GetValue(8);
                        gestprojectClient.PAR_PAIS_1 = (string)reader.GetValue(9);

                        DataHolder.GestprojectClientClassList.Add(gestprojectClient);
                    }
                };
            };

            DataHolder.GestprojectSQLConnection.Close();

            new GetSage50Clients();

            // GetSage50Client CIF_NIF
            // GetSage50Client CIF_NIF
            // GetSage50Client CIF_NIF
            // GetSage50Client CIF_NIF
            // GetSage50Client CIF_NIF
            //for(int i = 0; i < DataHolder.Sage50ClientClassList.Count; i++)
            //{
            //    if(DataHolder.Sage50ClientClassList[i].CIF != "" && DataHolder.Sage50ClientClassList[i].CIF != null)
            //    {
            //        DataHolder.Sage50CIFList.Add(DataHolder.Sage50ClientClassList[i].CIF);
            //    }
            //    else
            //    {
            //        DataHolder.Sage50CIFList.Add("Sin valor provisto");
            //    };
            //}

            // GetSage50Client Last CODE
            // GetSage50Client Last CODE
            // GetSage50Client Last CODE
            // GetSage50Client Last CODE
            // GetSage50Client Last CODE
            int Sage50HigestCodeNumber = DataHolder.Sage50ClientClassList.First().CODIGO_NUMERO;
            for(int i = 0; i < DataHolder.Sage50ClientClassList.Count; i++)
            {
                if(DataHolder.Sage50ClientClassList[i].CODIGO_NUMERO > Sage50HigestCodeNumber)
                {
                    Sage50HigestCodeNumber = DataHolder.Sage50ClientClassList[i].CODIGO_NUMERO;
                }
            };

            int counter = Sage50HigestCodeNumber + 1;



            DataTable table = new DataTable();
            PropertyInfo[] properties = typeof(ClientSyncronizationStateTable).GetProperties();

            for(int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];
                table.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            };

            // GetSage50Client
            // GetSage50Client
            // GetSage50Client
            // GetSage50Client
            // GetSage50Client
            for(int i = 0; i < DataHolder.GestprojectClientClassList.Count; i++)
            {
                GestprojectClient gestProjectClient = DataHolder.GestprojectClientClassList[i];

                //if(!DataHolder.Sage50CIFList.Contains(gestProjectClient.PAR_CIF_NIF))
                //{
                Customer customer = new Customer();
                clsEntityCustomer clsEntityCustomerInstance = new clsEntityCustomer();
                string currentClientCode = "";

                if(counter < 10)
                {
                    clsEntityCustomerInstance.codigo = "4300000" + counter;
                    currentClientCode = clsEntityCustomerInstance.codigo;
                }
                else if(counter < 100)
                {
                    clsEntityCustomerInstance.codigo = "430000" + counter;
                    currentClientCode = clsEntityCustomerInstance.codigo;
                }
                else
                {
                    clsEntityCustomerInstance.codigo = "43000" + counter;
                    currentClientCode = clsEntityCustomerInstance.codigo;
                };

                clsEntityCustomerInstance.pais = gestProjectClient.PAR_CP_1;
                clsEntityCustomerInstance.nombre = gestProjectClient.PAR_NOMBRE;
                clsEntityCustomerInstance.cif = gestProjectClient.PAR_CIF_NIF;
                clsEntityCustomerInstance.direccion = gestProjectClient.PAR_DIRECCION_1;
                clsEntityCustomerInstance.provincia = gestProjectClient.PAR_PROVINCIA_1;
                clsEntityCustomerInstance.tipo_iva = "03";

                customer._Create(clsEntityCustomerInstance);

                counter++;
                //};

                // Get new Sage50Client guid_id
                // Get new Sage50Client guid_id
                // Get new Sage50Client guid_id
                // Get new Sage50Client guid_id
                // Get new Sage50Client guid_id

                string sqlCommandString = $"SELECT guid_id FROM {DB.SQLDatabase("gestion","clientes")} WHERE \"codigo\"={currentClientCode}";
                DataTable sage50ClientsDataTable = new DataTable();
                DB.SQLExec(sqlCommandString, ref sage50ClientsDataTable);
                string Sage50ClientId = sage50ClientsDataTable.Rows[0].ItemArray[0].ToString().Trim();

                // Generate SynchronizationTableData
                // Generate SynchronizationTableData
                // Generate SynchronizationTableData
                // Generate SynchronizationTableData
                // Generate SynchronizationTableData

                GestprojectClient client = DataHolder.GestprojectClientClassList[i];
                DataRow row = table.NewRow();
                foreach(PropertyInfo prop in properties)
                {
                    row[0] = "No Sincronizado";
                    row[1] = i + 1;
                    row[2] = client.PAR_ID;
                    row[3] = currentClientCode;
                    row[4] = Sage50ClientId;
                    row[5] = client.PAR_SUBCTA_CONTABLE;
                    row[6] = client.PAR_NOMBRE;
                    row[7] = client.PAR_NOMBRE_COMERCIAL;
                    row[8] = client.PAR_CIF_NIF;
                    row[9] = client.PAR_DIRECCION_1;
                    row[10] = client.PAR_CP_1;
                    row[11] = client.PAR_LOCALIDAD_1;
                    row[12] = client.PAR_PROVINCIA_1;
                    row[13] = client.PAR_PAIS_1;
                };
                table.Rows.Add(row);
            }






            //for (int i = 0; i < DataHolder.GestprojectClientClassList.Count; i++)
            //{
            //    GestprojectClient client = DataHolder.GestprojectClientClassList[i];
            //    DataRow row = table.NewRow();
            //    foreach(PropertyInfo prop in properties)
            //    {
            //        row[0] = "No Sincronizado";
            //        row[1] = i+1;
            //        row[2] = client.PAR_ID;
            //        row[3] = "";
            //        row[4] = "";
            //        row[5] = client.PAR_SUBCTA_CONTABLE;
            //        row[6] = client.PAR_NOMBRE;
            //        row[7] = client.PAR_NOMBRE_COMERCIAL;
            //        row[8] = client.PAR_CIF_NIF;
            //        row[9] = client.PAR_DIRECCION_1;
            //        row[10] = client.PAR_CP_1;
            //        row[11] = client.PAR_LOCALIDAD_1;
            //        row[12] = client.PAR_PROVINCIA_1;
            //        row[13] = client.PAR_PAIS_1;
            //    };
            //    table.Rows.Add(row);
            //};

            DataHolder.GestprojectClientsTable = table;

            //new GetSage50Clients();
            //new CreateSage50MissingClients();

            //new GetSage50Clients();

            //for(int i = 0; i < DataHolder.GestprojectClientClassList.Count; i++)
            //{
            //    GestprojectClient client = DataHolder.GestprojectClientClassList[i];
            //    DataRow row = table.NewRow();
            //    foreach(PropertyInfo prop in properties)
            //    {
            //        row[0] = "No Sincronizado";
            //        row[1] = i + 1;
            //        row[2] = client.PAR_ID;
            //        row[3] = "";
            //        row[4] = "";
            //        row[5] = client.PAR_SUBCTA_CONTABLE;
            //        row[6] = client.PAR_NOMBRE;
            //        row[7] = client.PAR_NOMBRE_COMERCIAL;
            //        row[8] = client.PAR_CIF_NIF;
            //        row[9] = client.PAR_DIRECCION_1;
            //        row[10] = client.PAR_CP_1;
            //        row[11] = client.PAR_LOCALIDAD_1;
            //        row[12] = client.PAR_PROVINCIA_1;
            //        row[13] = client.PAR_PAIS_1;
            //    };
            //    table.Rows.Add(row);
            //};

            ////DataHolder.Sage50ClientClassList;
        }
    }
}
