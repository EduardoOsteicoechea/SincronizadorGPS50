using sage.ew.db;
using sage.ew.usuario;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SincronizadorGPS50.Sage50API
{
    internal class Sage50CompanyGroupActions
    {
        internal void GetCompanyGroups() 
        {
            string sqlCommandString = $"SELECT codigo,nombre,codpripal,guid_id FROM {DB.SQLDatabase("eurowinsys", "gruposemp")}";

            DataTable sage50CompanyGroupsDataTable = new DataTable();

            DB.SQLExec(sqlCommandString, ref sage50CompanyGroupsDataTable);

            //MessageBox.Show(
            //     "sage50CompanyGroupsDataTable.Rows: " + sage50CompanyGroupsDataTable.Rows.Count
            // );

            for(int i = 0; i < sage50CompanyGroupsDataTable.Rows.Count; i++)
            {
                CompanyGroup companyGroup = new CompanyGroup();
                companyGroup.CompanyCode = (string)sage50CompanyGroupsDataTable.Rows[i].ItemArray[0];
                companyGroup.CompanyName = (string)sage50CompanyGroupsDataTable.Rows[i].ItemArray[1];
                companyGroup.CompanyMainCode = (string)sage50CompanyGroupsDataTable.Rows[i].ItemArray[2];
                companyGroup.CompanyGuidId = (string)sage50CompanyGroupsDataTable.Rows[i].ItemArray[3];

                //MessageBox.Show(
                //    companyGroup.CompanyCode + "\n" +
                //    companyGroup.CompanyName + "\n" +
                //    companyGroup.CompanyMainCode + "\n" +
                //    companyGroup.CompanyGuidId
                //);

                DataHolder.Sage50CompanyGroupsList.Add(companyGroup);
            };

            DataHolder.Sage50CompanyGroupsDataTable = sage50CompanyGroupsDataTable;
        }

        internal void ChangeCompanyGroup(object sender, System.EventArgs e) 
        {
            if(UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedText != "") 
            { 
                GrupoEmpresaSel CompanyGroupsOperator = new GrupoEmpresaSel();

                var selectedCompanyName = UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedText;

                var selectedCompanyGroup = DataHolder.Sage50CompanyGroupsList.Where(companyGroup => companyGroup.CompanyName == selectedCompanyName).FirstOrDefault();

                CompanyGroupsOperator._CambiarGrupo(selectedCompanyGroup.CompanyCode, "", true);

                UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelConnectButton, 0, 16);

                UIHolder.CenterRowCenterPanelConnectButton.Focus();
            };
        }

        internal void GetCompanyGroupCompanies(object sender, System.EventArgs e) 
        {
            DataTable dtTabla = new DataTable();
            DB.SQLExec("SELECT codigo FROM " + DB.SQLDatabase("GESTION", "empresa"), ref dtTabla);

            for(int i = 0; i < dtTabla.Rows.Count; i++)
            {
                var codigo = (string)dtTabla.Rows[i].ItemArray[0];
                MessageBox.Show(codigo);
            };
        }
        internal void ShowCompanyGroupSage50UI(object sender, System.EventArgs e) 
        {
            if(UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedText != "")
            {
                GrupoEmpresaSel CompanyGroupsOperator = new GrupoEmpresaSel();
                CompanyGroupsOperator._MostrarFormulario();
            };
        }
    }
}
