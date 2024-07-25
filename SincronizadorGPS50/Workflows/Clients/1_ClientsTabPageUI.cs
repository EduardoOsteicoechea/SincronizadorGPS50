using Infragistics.Win.Misc;
using System;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class ClientsTabPageUI
    {
        internal ClientsTabPageUI()
        {
            ClientsUIHolder.MainPanel = new UltraPanel();
            ClientsUIHolder.MainPanel.Dock = DockStyle.Fill; 

            ClientsUIHolder.TableLayoutPanel = new TableLayoutPanel();
            ClientsUIHolder.TableLayoutPanel.ColumnCount = 1;
            ClientsUIHolder.TableLayoutPanel.RowCount = 3;
            ClientsUIHolder.TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            ClientsUIHolder.TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 87.50f));
            ClientsUIHolder.TableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            ClientsUIHolder.TableLayoutPanel.Dock = DockStyle.Fill;

            ClientsUIHolder.MainPanel.ClientArea.Controls.Add(ClientsUIHolder.TableLayoutPanel);

            UIHolder.ClientsTab.TabPage.Controls.Add(ClientsUIHolder.MainPanel);

            // TopRow;
            // TopRow;
            // TopRow;
            // TopRow;
            // TopRow;

            ClientsUIHolder.TopRow = new UltraPanel();
            ClientsUIHolder.TopRow.Dock = System.Windows.Forms.DockStyle.Fill;
            ClientsUIHolder.TopRow.Appearance.BackColor = StyleHolder.c_transparent;

            ClientsUIHolder.TableLayoutPanel.Controls.Add(ClientsUIHolder.TopRow, 0, 0);

            new TopRowUI();

            // CenterRow
            // CenterRow
            // CenterRow
            // CenterRow
            // CenterRow

            ClientsUIHolder.CenterRow = new UltraPanel();
            ClientsUIHolder.CenterRow.Height = StyleHolder.CenterRowHeight;
            ClientsUIHolder.CenterRow.Dock = System.Windows.Forms.DockStyle.Fill;
            ClientsUIHolder.CenterRow.Appearance.BackColor = StyleHolder.c_white;

            ClientsUIHolder.TableLayoutPanel.Controls.Add(ClientsUIHolder.CenterRow, 0, 1);

            new CenterRowUI();

            // BottomRow
            // BottomRow
            // BottomRow
            // BottomRow
            // BottomRow

            ClientsUIHolder.BottomRow = new UltraPanel();
            ClientsUIHolder.BottomRow.Dock = System.Windows.Forms.DockStyle.Fill;
            ClientsUIHolder.BottomRow.Appearance.BackColor = StyleHolder.c_transparent;

            ClientsUIHolder.TableLayoutPanel.Controls.Add(ClientsUIHolder.BottomRow, 0, 2);

            new BottomRowUI();

            //Customer customer = new Customer();
            //clsEntityCustomer clsEntityCustomerInstance = new clsEntityCustomer();
            //clsEntityCustomerInstance.codigo = "43000002";
            //customer._Create(clsEntityCustomerInstance);
        }
    }
}
