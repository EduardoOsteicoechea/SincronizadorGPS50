using sage.ew.db;
using System.Data;
using System.Windows.Forms;

namespace SincronizadorGPS50.Sage50API
{
    internal class CreateSage50Customer
    {
        internal string ClientCode { get; set; } = "";
        internal string GUID_ID { get; set; } = "";
        internal CreateSage50Customer( GestprojectClient gestprojectClient, int nextAvailableClientCode )
        {
            Customer customer = new Customer();
            clsEntityCustomer clsEntityCustomerInstance = new clsEntityCustomer();

            if(nextAvailableClientCode < 10)
            {
                clsEntityCustomerInstance.codigo = "4300000" + nextAvailableClientCode;
                ClientCode = clsEntityCustomerInstance.codigo;
            }
            else if(nextAvailableClientCode < 100)
            {
                clsEntityCustomerInstance.codigo = "430000" + nextAvailableClientCode;
                ClientCode = clsEntityCustomerInstance.codigo;
            }
            else if(nextAvailableClientCode < 1000)
            {
                clsEntityCustomerInstance.codigo = "43000" + nextAvailableClientCode;
                ClientCode = clsEntityCustomerInstance.codigo;
            }
            else if(nextAvailableClientCode < 10000)
            {
                clsEntityCustomerInstance.codigo = "4300" + nextAvailableClientCode;
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

            string getSage50ClientSQLQuery = @"
                SELECT guid_id FROM " + DB.SQLDatabase("gestion","clientes") + " WHERE codigo = '" + ClientCode + "';";

            DataTable sage50ClientsDataTable = new DataTable();

            DB.SQLExec(getSage50ClientSQLQuery, ref sage50ClientsDataTable);

            GUID_ID = sage50ClientsDataTable.Rows[0].ItemArray[0].ToString().Trim();
        }           
    }
}
