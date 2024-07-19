using sage.ew.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50.Sage50API
{
    internal class CreateSage50Customer
    {
        internal string ClientCode { get; set; } = "";
        internal string GUID_ID { get; set; } = "";
        internal CreateSage50Customer(
            GestprojectClient gestprojectClient,
            bool isSynchronized,
            ref int existingSage50ClientCounter
        )
        {
            Customer customer = new Customer();
            clsEntityCustomer clsEntityCustomerInstance = new clsEntityCustomer();

            if(existingSage50ClientCounter < 10)
            {
                clsEntityCustomerInstance.codigo = "4300000" + existingSage50ClientCounter;
                ClientCode = clsEntityCustomerInstance.codigo;
            }
            else if(existingSage50ClientCounter < 100)
            {
                clsEntityCustomerInstance.codigo = "430000" + existingSage50ClientCounter;
                ClientCode = clsEntityCustomerInstance.codigo;
            }
            else if(existingSage50ClientCounter < 1000)
            {
                clsEntityCustomerInstance.codigo = "43000" + existingSage50ClientCounter;
                ClientCode = clsEntityCustomerInstance.codigo;
            }
            else if(existingSage50ClientCounter < 10000)
            {
                clsEntityCustomerInstance.codigo = "4300" + existingSage50ClientCounter;
                ClientCode = clsEntityCustomerInstance.codigo;
            }
            else
            {
                MessageBox.Show("Sage50 admite un máximo de 9999 clientes por grupo de empresas y su base de clientes de Gestproject supera éste límite.");
            };

            clsEntityCustomerInstance.pais = gestprojectClient.PAR_CP_1;
            clsEntityCustomerInstance.nombre = gestprojectClient.PAR_NOMBRE;
            clsEntityCustomerInstance.cif = gestprojectClient.PAR_CIF_NIF;
            clsEntityCustomerInstance.direccion = gestprojectClient.PAR_DIRECCION_1;
            clsEntityCustomerInstance.provincia = gestprojectClient.PAR_PROVINCIA_1;
            clsEntityCustomerInstance.tipo_iva = "03";

            customer._Create(clsEntityCustomerInstance);

            // Get new Sage50Client guid_id
            // Get new Sage50Client guid_id
            // Get new Sage50Client guid_id
            // Get new Sage50Client guid_id
            // Get new Sage50Client guid_id

            string Sage50NewClientSQLQuery = $"SELECT guid_id FROM {DB.SQLDatabase("gestion","clientes")} WHERE \"codigo\"={ClientCode}";

            DataTable Sage50NewClientDataTable = new DataTable();

            DB.SQLExec(Sage50NewClientSQLQuery, ref Sage50NewClientDataTable);

            GUID_ID = Sage50NewClientDataTable.Rows[0].ItemArray[0].ToString().Trim();

            existingSage50ClientCounter++;
        }           
    }
}
