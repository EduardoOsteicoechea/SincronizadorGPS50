using sage._50;
using sage.ew.db;
using SincronizadorGPS50.Sage50API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Sage50Connection.Sage50ConnectionTabUI.CenterRow
{
    internal class Sage50ConnectionActions
    {
        internal void VeryfyUserData(object sender, System.EventArgs e)
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

        internal void Connect(object sender, System.EventArgs e) 
        {
            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelConnectingSpinner);

            string CenterRowCenterPanelStateIcon1ImagePath = Application.StartupPath + @"\Media\Image\Semaforo verde.png";
            Image CenterRowCenterPanelStateIcon1Image = null;
            if(File.Exists(CenterRowCenterPanelStateIcon1ImagePath))
            {
                CenterRowCenterPanelStateIcon1Image = Image.FromFile(CenterRowCenterPanelStateIcon1ImagePath);
            };

            UIHolder.CenterRowCenterPanelStateIcon1.Image = CenterRowCenterPanelStateIcon1Image;

            UIHolder.CenterRowCenterPanelStateStateMessageLabel.Text = "Conectado";

            UIHolder.CenterRowCenterPanelConnectButton.Enabled = false;

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelDisconnectButton, 0, 17);
        }

        internal void Disconnect(object sender, System.EventArgs e) 
        {
            LinkSage50 linkSage50 = new LinkSage50();
            linkSage50._connected = true;
            linkSage50._Disconnect();

             UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelDisconnectButton);
             
             UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelConnectButton);

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelSesionDataValidationLabel);

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelEnterpryseGroupLabel);

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Remove(UIHolder.CenterRowCenterPanelEnterpryseGroupMenu);
            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedIndex = 0;
            UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.Items.Clear();
            DataHolder.Sage50CompanyGroupsList.Clear();

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelConnectingSpinner, 0, 17);

            UIHolder.CenterRowCenterPanelLocalInstanceTextBox.Enabled = true;
            UIHolder.CenterRowCenterPanelUsernameTextBox.Enabled = true;
            UIHolder.CenterRowCenterPanelPasswordTextBox.Enabled = true;

            string CenterRowCenterPanelStateIcon1ImagePath = Application.StartupPath + @"\Media\Image\Semaforo rojo.png";
            Image CenterRowCenterPanelStateIcon1Image = null;
            if(File.Exists(CenterRowCenterPanelStateIcon1ImagePath))
            {
                CenterRowCenterPanelStateIcon1Image = Image.FromFile(CenterRowCenterPanelStateIcon1ImagePath);
            };

            UIHolder.CenterRowCenterPanelStateIcon1.Image = CenterRowCenterPanelStateIcon1Image;
            UIHolder.CenterRowCenterPanelStateStateMessageLabel.Text = "Desconectado";
            UIHolder.CenterRowCenterPanelConnectButton.Enabled = true;
            UIHolder.CenterRowCenterPanelValidateUserDataButton.Enabled = true;
            UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Text = "Datos Incorrectos";
            UIHolder.CenterRowCenterPanelSesionDataValidationLabel.Appearance.ForeColor = StyleHolder.c_red_1;

        }
    }
}
