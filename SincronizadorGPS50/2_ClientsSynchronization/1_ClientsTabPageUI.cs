using Infragistics.Win.Misc;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class ClientsTabPageUI
    {
        internal ClientsTabPageUI()
        {
            ////////////////////////////////////////
            // ClientsTab Rows Container
            ////////////////////////////////////////
            
            ClientsUIHolder.MainPanel = new UltraPanel();
            ClientsUIHolder.MainPanel.Dock = DockStyle.Fill; 

            ClientsUIHolder.TableLayoutPanel = new TableLayoutPanel();
            ClientsUIHolder.TableLayoutPanel.ColumnCount = 1;
            ClientsUIHolder.TableLayoutPanel.RowCount = 3;
            ClientsUIHolder.TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            ClientsUIHolder.TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 87.50f));
            ClientsUIHolder.TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            ClientsUIHolder.TableLayoutPanel.Dock = DockStyle.Fill;

            ClientsUIHolder.MainPanel.ClientArea.Controls.Add(ClientsUIHolder.TableLayoutPanel);

            MainWindowUIHolder.ClientsTab.TabPage.Controls.Add(ClientsUIHolder.MainPanel);

            ////////////////////////////////////////
            // Clients TopRow
            ////////////////////////////////////////

            ClientsUIHolder.TopRow = new UltraPanel();
            ClientsUIHolder.TopRow.Dock = System.Windows.Forms.DockStyle.Fill;
            ClientsUIHolder.TopRow.Appearance.BackColor = StyleHolder.c_transparent;

            ClientsUIHolder.TableLayoutPanel.Controls.Add(ClientsUIHolder.TopRow, 0, 0);

            new TopRowUI();

            ////////////////////////////////////////
            // Clients CenterRow
            ////////////////////////////////////////

            ClientsUIHolder.CenterRow = new UltraPanel();
            ClientsUIHolder.CenterRow.Height = StyleHolder.CenterRowHeight;
            ClientsUIHolder.CenterRow.Dock = System.Windows.Forms.DockStyle.Fill;
            ClientsUIHolder.CenterRow.Appearance.BackColor = StyleHolder.c_white;

            ClientsUIHolder.TableLayoutPanel.Controls.Add(ClientsUIHolder.CenterRow, 0, 1);

            new CenterRowUI(ClientSynchronizationTable.Create);

            ////////////////////////////////////////
            // Clients BottomRow
            ////////////////////////////////////////

            ClientsUIHolder.BottomRow = new UltraPanel();
            ClientsUIHolder.BottomRow.Dock = System.Windows.Forms.DockStyle.Fill;
            ClientsUIHolder.BottomRow.Appearance.BackColor = StyleHolder.c_transparent;

            ClientsUIHolder.TableLayoutPanel.Controls.Add(ClientsUIHolder.BottomRow, 0, 2);

            new BottomRowUI();
        }
    }
}
