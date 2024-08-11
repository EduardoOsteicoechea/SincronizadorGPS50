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
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowCount = 13;
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Dock = DockStyle.Fill;

            double componentHeight = 580;
            int keyButtonHeight = 35;
            int separatorHeight = 40;

            // 0.ConnectionStatus
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, (int)(componentHeight * .10)));
            //Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, separatorHeight));
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, (int)(componentHeight * .0)));
            // 2.Terminal Connection Data
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, (int)(componentHeight * .18)));
            // 3.Validate terminal
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, keyButtonHeight));
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, separatorHeight));
            // 5.Select enterpryse group
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, (int)(componentHeight * .10)));
            // 6.Validate enterpryse group
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, keyButtonHeight));
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, separatorHeight));
            // 8.Connect Button
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, keyButtonHeight));
            // 9.Remember Data
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, keyButtonHeight));
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, separatorHeight));
            // 11.Manage Full Connection
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize, 50));

            //Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanel.ClientArea.Controls.Add(Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel);
            Sage50ConnectionUIHolder.Sage50ConnectionCenterRowTableLayoutPanel.Controls.Add(Sage50ConnectionUIHolder.Sage50ConnectionCenterRowCenterPanel, 1, 0);

            

            IsSuccessful = true;
        }
    }
}
