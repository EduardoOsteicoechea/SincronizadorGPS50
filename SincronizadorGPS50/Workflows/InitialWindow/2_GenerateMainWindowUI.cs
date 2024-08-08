using Infragistics.Win.UltraWinTabControl;
using Sage.ES.S50.Modelos.Interficies;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
    internal class GenerateMainWindowUI
    {
        internal bool IsSuccessful { get; set; } = false;
        internal GenerateMainWindowUI() 
        {
            // MainUltraTabControl
            // MainUltraTabControl
            // MainUltraTabControl
            // MainUltraTabControl
            // MainUltraTabControl

            MainWindowUIHolder.MainWindow.Width = StyleHolder.ScreenWorkableWidth;
            MainWindowUIHolder.MainWindow.Height = StyleHolder.ScreenWorkableHeight;

            MainWindowUIHolder.MainTabControl = new UltraTabControl();
            MainWindowUIHolder.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            MainWindowUIHolder.MainTabControl.TabStop = false;

            MainWindowUIHolder.MainWindow.Controls.Add(MainWindowUIHolder.MainTabControl);

            // MainUltraTabControlTabs
            // MainUltraTabControlTabs
            // MainUltraTabControlTabs
            // MainUltraTabControlTabs
            // MainUltraTabControlTabs

            MainWindowUIHolder.MainTabControl.SelectedTab = UIHolder.Sage50ConnectionTab;

            UIHolder.Sage50ConnectionTab = MainWindowUIHolder.MainTabControl.Tabs.Add("Sage50ConnectionTab", "Conexión con Sage50");
            UIHolder.ClientsTab = MainWindowUIHolder.MainTabControl.Tabs.Add("ClientsTab", "Clientes");
            UIHolder.ProvidersTab = MainWindowUIHolder.MainTabControl.Tabs.Add("ProvidersTab", "Proveedores");
            UIHolder.TaxesTab = MainWindowUIHolder.MainTabControl.Tabs.Add("TaxesTab", "Impuestos");
            UIHolder.IssuedBillsTab = MainWindowUIHolder.MainTabControl.Tabs.Add("IssuedBillsTab", "Facturas Emitidas");
            UIHolder.ReceivedBillsTab = MainWindowUIHolder.MainTabControl.Tabs.Add("ReceivedBillsTab", "Facturas Recibidas");

            foreach(UltraTab tab in MainWindowUIHolder.MainTabControl.Tabs)
            {
                tab.Enabled = false;
            };

            MainWindowUIHolder.MainTabControl.SelectedTab.Enabled = true;

            MainWindowUIHolder.MainWindow.Controls.Add(MainWindowUIHolder.MainTabControl);

            IsSuccessful = true;
        }
    }
}
