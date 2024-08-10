using Infragistics.Win.Misc;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
    internal class CenterRowUI
    {
        internal bool IsSuccessful { get; set; } = false;
        internal CenterRowUI() 
        {
            //LeftPanel
            //LeftPanel
            //LeftPanel
            //LeftPanel
            //LeftPanel

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowLeftPanel = new UltraPanel();
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowLeftPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.Sage50ConnectionCenterRowLeftPanel, 0, 0);

            // RightPanel
            // RightPanel
            // RightPanel
            // RightPanel
            // RightPanel

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowRightPanel = new UltraPanel();
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowRightPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.Sage50ConnectionCenterRowRightPanel, 2, 0);

            // CenterPanel
            // CenterPanel
            // CenterPanel
            // CenterPanel
            // CenterPanel

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanel = new UltraPanel();
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel = new TableLayoutPanel();
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.ColumnCount = 1;
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowCount = 7;
            //Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowCount = 20;
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Dock = DockStyle.Fill;
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 3f));
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 7f));
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 3f));
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 17f));
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 3f));
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 7f));
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 5f));
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 3f));
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));

            //Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanel.ClientArea.Controls.Add(Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel);
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanel, 1, 0);

            

            IsSuccessful = true;
        }
    }
}
