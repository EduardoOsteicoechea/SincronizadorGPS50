using sage.ew.usuario;
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
    internal class ConnectToSage50
    {
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

            //GrupoEmpresaSel CompanyGroups = new GrupoEmpresaSel();
            //CompanyGroups._ObtenerGruposEmp();

            //if(CompanyGroups._CambiarGrupo("0001", "01", true))
            //{
            //    MessageBox.Show("Grupo de empresa cambiado y reconexión exitosa");
            //    //Sage50ConnectionObjectInstance._Disconnect();
            //}
            //else
            //{
            //    MessageBox.Show("Error al intentar cambiar de grupo de empresa cambiado");
            //};
        }
    }
}
