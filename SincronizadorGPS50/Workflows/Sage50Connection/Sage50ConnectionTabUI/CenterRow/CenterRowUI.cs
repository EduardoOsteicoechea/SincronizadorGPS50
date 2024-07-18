using Infragistics.Win.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Sage50Connection.Sage50ConnectionTabUI.CenterRow
{
    internal class CenterRowUI
    {
        internal CenterRowUI() 
        {
            //LeftPanel
            //LeftPanel
            //LeftPanel
            //LeftPanel
            //LeftPanel

            UIHolder.Sage50ConnectionCenterRowLeftPanel = new UltraPanel();
            UIHolder.Sage50ConnectionCenterRowLeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            UIHolder.Sage50ConnectionCenterRowTableLayoutPanel.Controls.Add(UIHolder.Sage50ConnectionCenterRowLeftPanel, 0, 0);

            // CenterPanel
            // CenterPanel
            // CenterPanel
            // CenterPanel
            // CenterPanel

            UIHolder.Sage50ConnectionCenterRowCenterPanel = new UltraPanel();
            UIHolder.Sage50ConnectionCenterRowCenterPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel = new TableLayoutPanel();
            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.ColumnCount = 1;
            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowCount = 20;
            UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Dock = DockStyle.Fill;

            UIHolder.Sage50ConnectionCenterRowCenterPanel.ClientArea.Controls.Add(UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel);
            UIHolder.Sage50ConnectionCenterRowTableLayoutPanel.Controls.Add(UIHolder.Sage50ConnectionCenterRowCenterPanel, 1, 0);

            new CenterRowCenterPanelControls();
            new VerifyUserLocalSessionDataAction();
            //new VerifyUserLocalSessionDataAction();

            // RightPanel
            // RightPanel
            // RightPanel
            // RightPanel
            // RightPanel

            UIHolder.Sage50ConnectionCenterRowRightPanel = new UltraPanel();
            UIHolder.Sage50ConnectionCenterRowRightPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            UIHolder.Sage50ConnectionCenterRowTableLayoutPanel.Controls.Add(UIHolder.Sage50ConnectionCenterRowRightPanel, 2, 0);
        }
    }
}
