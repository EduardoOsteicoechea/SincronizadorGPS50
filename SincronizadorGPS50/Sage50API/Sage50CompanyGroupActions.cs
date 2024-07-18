using sage.ew.db;
using sage.ew.usuario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50.Sage50API
{
    internal static class Sage50CompanyGroupActions
    {
        internal static void GetCompanyGroups() 
        {
            string sqlCommandString = $"SELECT codigo,nombre,codpripal FROM {DB.SQLDatabase("eurowinsys", "gruposemp")}";

            DataTable sage50CompanyGroupsDataTable = new DataTable();

            DB.SQLExec(sqlCommandString, ref sage50CompanyGroupsDataTable);

            for(int i = 0; i < sage50CompanyGroupsDataTable.Rows.Count; i++)
            {
                CompanyGroup companyGroup = new CompanyGroup();
                companyGroup.CompanyCode = (string)sage50CompanyGroupsDataTable.Rows[0].ItemArray[0];
                companyGroup.CompanyName = (string)sage50CompanyGroupsDataTable.Rows[0].ItemArray[1];
                companyGroup.CompanyMainCode = (string)sage50CompanyGroupsDataTable.Rows[0].ItemArray[2];

                DataHolder.Sage50CompanyGroupsList.Add(companyGroup);
            };

            DataHolder.Sage50CompanyGroupsDataTable = sage50CompanyGroupsDataTable;
        }

        internal static void ChangeCompanyGroup(object sender, System.EventArgs e) 
        {
            if(UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedText != "") 
            { 
                GrupoEmpresaSel CompanyGroupsOperator = new GrupoEmpresaSel();

         
                var selectedCompanyName = UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedText;

                var selectedCompanyGroup = DataHolder.Sage50CompanyGroupsList.Where(companyGroup => companyGroup.CompanyName == selectedCompanyName).FirstOrDefault();

                CompanyGroupsOperator._CambiarGrupo(selectedCompanyGroup.CompanyCode, "", true);

                UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelConnectButton, 0, 16);
            };

            //DataTable dtTabla = new DataTable();
            //DB.SQLExec("SELECT codigo FROM " + DB.SQLDatabase("GESTION", "empresa"), ref dtTabla);

            //for(int i = 0; i < dtTabla.Rows.Count; i++)
            //{
            //    var codigo = (string)dtTabla.Rows[i].ItemArray[0];
            //    MessageBox.Show(codigo);
            //};

            //CompanyGroupsOperator._MostrarFormulario(); // Interesante
        }
    }
}
