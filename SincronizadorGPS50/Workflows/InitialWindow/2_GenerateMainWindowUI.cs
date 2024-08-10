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

            MainWindowUIHolder.MainTabControl.SelectedTab = MainWindowUIHolder.Sage50ConnectionTab;

            MainWindowUIHolder.Sage50ConnectionTab = MainWindowUIHolder.MainTabControl.Tabs.Add("Sage50ConnectionTab", "Conexión con Sage50");
            MainWindowUIHolder.ClientsTab = MainWindowUIHolder.MainTabControl.Tabs.Add("ClientsTab", "Clientes");
            MainWindowUIHolder.ProvidersTab = MainWindowUIHolder.MainTabControl.Tabs.Add("ProvidersTab", "Proveedores");
            MainWindowUIHolder.TaxesTab = MainWindowUIHolder.MainTabControl.Tabs.Add("TaxesTab", "Impuestos");
            MainWindowUIHolder.IssuedBillsTab = MainWindowUIHolder.MainTabControl.Tabs.Add("IssuedBillsTab", "Facturas Emitidas");
            MainWindowUIHolder.ReceivedBillsTab = MainWindowUIHolder.MainTabControl.Tabs.Add("ReceivedBillsTab", "Facturas Recibidas");

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
