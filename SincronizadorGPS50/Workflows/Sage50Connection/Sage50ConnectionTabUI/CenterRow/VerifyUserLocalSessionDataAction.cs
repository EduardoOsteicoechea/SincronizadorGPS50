using sage._50;
using SincronizadorGPS50.Sage50API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Sage50Connection.Sage50ConnectionTabUI.CenterRow
{
    internal class VerifyUserLocalSessionDataAction
    {
        public void Verify(object sender, System.EventArgs e) 
        {
            DataHolder.Sage50LocalTerminalPath = UIHolder.CenterRowCenterPanelLocalInstanceTextBox.Text;
            DataHolder.Sage50Username = UIHolder.CenterRowCenterPanelUsernameTextBox.Text;
            DataHolder.Sage50Password = UIHolder.CenterRowCenterPanelPasswordTextBox.Text;

            if(main_s50.Connect(DataHolder.Sage50LocalTerminalPath, DataHolder.Sage50Username, DataHolder.Sage50Password)) 
            {
                UIHolder.CenterRowCenterPanelValidateUserDataButton.Enabled = false;
                UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos correctos";
                UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.ForeColor = StyleHolder.c_green_1;
                UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelSesionDataValidationLabel, 0, 11);
                UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelEnterpryseGroupLabel, 0, 12);
                UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelEnterpryseGroupMenu, 0, 13);

                UIHolder.CenterRowCenterPanelLocalInstanceTextBox.Enabled = false;
                UIHolder.CenterRowCenterPanelUsernameTextBox.Enabled = false;
                UIHolder.CenterRowCenterPanelPasswordTextBox.Enabled = false;

                Sage50CompanyGroupActions.GetCompanyGroups();

                if(UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Count < 1) 
                {
                    UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Add("");
                };
                for(global::System.Int32 i = 0; i < DataHolder.Sage50CompanyGroupsList.Count; i++)
                {
                    UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Add(DataHolder.Sage50CompanyGroupsList[i].CompanyName);
                };                
            }
            else
            {
                UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos inválidos para iniciar sesión";
                UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.ForeColor = StyleHolder.c_red_1;
                UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelSesionDataValidationLabel, 0, 11);
            };
        }
    }
}
