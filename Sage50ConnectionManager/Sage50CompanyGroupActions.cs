using sage.ew.db;
using sage.ew.usuario;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Sage50ConnectionManager
{
    public static class Sage50CompanyGroupActions
    {
        public static List<CompanyGroup> CompanyGroupList { get; set; } = new List<CompanyGroup>();
        public static DataTable Sage50CompanyGroupsDataTable { get; set; } = null;
        public static List<CompanyGroup> GetCompanyGroups() 
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

                CompanyGroupList.Add(companyGroup);
            };

            Sage50CompanyGroupsDataTable = sage50CompanyGroupsDataTable;

            return CompanyGroupList;
        }

        public static bool ChangeCompanyGroup(string selectedCompanyName) 
        {
            List<CompanyGroup> companyGroupList = GetCompanyGroups();
            List<string> companyGroupNamesList = companyGroupList.Select(companyGroup => companyGroup.CompanyName).ToList();

            if(companyGroupNamesList.Contains(selectedCompanyName)) 
            { 
                GrupoEmpresaSel companyGroupsOperator = new GrupoEmpresaSel();

                var selectedCompanyGroup = companyGroupList.Where(companyGroup => companyGroup.CompanyName == selectedCompanyName).FirstOrDefault();

                return companyGroupsOperator._CambiarGrupo(selectedCompanyGroup.CompanyCode, "", true);
            }
            else
            {
                return false;
            };
        }

        public static void GetCompanyGroupCompanies(object sender, System.EventArgs e) 
        {
            DataTable dtTabla = new DataTable();
            DB.SQLExec("SELECT codigo FROM " + DB.SQLDatabase("GESTION", "empresa"), ref dtTabla);

            for(int i = 0; i < dtTabla.Rows.Count; i++)
            {
                var codigo = (string)dtTabla.Rows[i].ItemArray[0];
                MessageBox.Show(codigo);
            };
        }

        public static void ShowCompanyGroupSage50UI(object sender, System.EventArgs e) 
        {
            GrupoEmpresaSel CompanyGroupsOperator = new GrupoEmpresaSel();
            CompanyGroupsOperator._MostrarFormulario();
        }
    }
}
