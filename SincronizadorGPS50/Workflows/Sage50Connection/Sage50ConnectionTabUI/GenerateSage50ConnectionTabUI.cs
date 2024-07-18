using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using SincronizadorGPS50.Workflows.Sage50Connection.Sage50ConnectionTabUI;
using SincronizadorGPS50.Workflows.Sage50Connection.Sage50ConnectionTabUI.CenterRow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
    internal class GenerateSage50ConnectionTabPageUI
    {
        internal GenerateSage50ConnectionTabPageUI()
        {

            UIHolder.MainTabControlMainPanel = new UltraPanel();
            UIHolder.MainTabControlMainPanel.Dock = DockStyle.Fill;

            UIHolder.Sage50ConnectionTableLayoutPanel = new TableLayoutPanel();
            UIHolder.Sage50ConnectionTableLayoutPanel.ColumnCount = 1;
            UIHolder.Sage50ConnectionTableLayoutPanel.RowCount = 3;
            UIHolder.Sage50ConnectionTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            UIHolder.Sage50ConnectionTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 87.50f));
            UIHolder.Sage50ConnectionTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            UIHolder.Sage50ConnectionTableLayoutPanel.Dock = DockStyle.Fill;

            UIHolder.MainTabControlMainPanel.ClientArea.Controls.Add(UIHolder.Sage50ConnectionTableLayoutPanel);
            UIHolder.Sage50ConnectionTab.TabPage.Controls.Add(UIHolder.MainTabControlMainPanel);

            // TopRow;
            // TopRow;
            // TopRow;
            // TopRow;
            // TopRow;

            UIHolder.Sage50ConnectionTopRow = new UltraPanel();
            UIHolder.Sage50ConnectionTopRow.Dock = System.Windows.Forms.DockStyle.Fill;
            UIHolder.Sage50ConnectionTopRow.Appearance.BackColor = StyleHolder.c_transparent;

            UIHolder.Sage50ConnectionTableLayoutPanel.Controls.Add(UIHolder.Sage50ConnectionTopRow, 0, 0);

            // CenterRow
            // CenterRow
            // CenterRow
            // CenterRow
            // CenterRow

            UIHolder.Sage50ConnectionCenterRowTableLayoutPanel = new TableLayoutPanel();
            UIHolder.Sage50ConnectionCenterRowTableLayoutPanel.ColumnCount = 3;
            UIHolder.Sage50ConnectionCenterRowTableLayoutPanel.RowCount = 1;
            UIHolder.Sage50ConnectionCenterRowTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42f));
            UIHolder.Sage50ConnectionCenterRowTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26f));
            UIHolder.Sage50ConnectionCenterRowTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42f));
            UIHolder.Sage50ConnectionCenterRowTableLayoutPanel.Dock = DockStyle.Fill;

            UIHolder.Sage50ConnectionCenterRow = new UltraPanel();
            UIHolder.Sage50ConnectionCenterRow.Height = StyleHolder.CenterRowHeight;
            UIHolder.Sage50ConnectionCenterRow.Dock = System.Windows.Forms.DockStyle.Fill;
            UIHolder.Sage50ConnectionCenterRow.Appearance.BackColor = StyleHolder.c_white;

            UIHolder.Sage50ConnectionCenterRow.ClientArea.Controls.Add(UIHolder.Sage50ConnectionCenterRowTableLayoutPanel);
            UIHolder.Sage50ConnectionTableLayoutPanel.Controls.Add(UIHolder.Sage50ConnectionCenterRow, 0, 1);

            new CenterRowUI();

            // BottomRow
            // BottomRow
            // BottomRow
            // BottomRow
            // BottomRow

            UIHolder.Sage50ConnectionBottomRow = new UltraPanel();
            UIHolder.Sage50ConnectionBottomRow.Dock = System.Windows.Forms.DockStyle.Fill;
            UIHolder.Sage50ConnectionBottomRow.Appearance.BackColor = StyleHolder.c_transparent;

            UIHolder.Sage50ConnectionTableLayoutPanel.Controls.Add(UIHolder.Sage50ConnectionBottomRow, 0, 2);
        }
    }
}
