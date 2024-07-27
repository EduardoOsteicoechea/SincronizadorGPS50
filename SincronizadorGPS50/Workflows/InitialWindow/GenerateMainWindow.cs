using Infragistics.Win.UltraWinTabControl;
using Sage.ES.S50.Modelos.Interficies;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
    internal class GenerateMainWindow
    {
        internal GenerateMainWindow() 
        {
            UIHolder.MainWindow = new System.Windows.Forms.Form();
            UIHolder.MainWindow.Text = "SincronizadorGPS50";
            UIHolder.MainWindow.WindowState = FormWindowState.Maximized;
            UIHolder.MainWindow.SizeGripStyle = SizeGripStyle.Hide;

            string mainWindowIconPath = Application.StartupPath + @"\Media\Image\appicon.ico";
            if(File.Exists(mainWindowIconPath))
            {
                UIHolder.MainWindow.Icon = new Icon(mainWindowIconPath);
            };

            // MainUltraTabControl
            // MainUltraTabControl
            // MainUltraTabControl
            // MainUltraTabControl
            // MainUltraTabControl

            UIHolder.MainWindow.Width = StyleHolder.ScreenWorkableWidth;
            UIHolder.MainWindow.Height = StyleHolder.ScreenWorkableHeight;

            UIHolder.MainTabControl = new UltraTabControl();
            UIHolder.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            UIHolder.MainTabControl.TabStop = false;

            UIHolder.MainWindow.Controls.Add(UIHolder.MainTabControl);

            // MainUltraTabControlTabs
            // MainUltraTabControlTabs
            // MainUltraTabControlTabs
            // MainUltraTabControlTabs
            // MainUltraTabControlTabs

            UIHolder.MainTabControl.SelectedTab = UIHolder.Sage50ConnectionTab;

            UIHolder.Sage50ConnectionTab = UIHolder.MainTabControl.Tabs.Add("Sage50ConnectionTab", "Conexión con Sage50");
            UIHolder.ClientsTab = UIHolder.MainTabControl.Tabs.Add("ClientsTab", "Clientes");
            UIHolder.ProvidersTab = UIHolder.MainTabControl.Tabs.Add("ProvidersTab", "Proveedores");
            UIHolder.IssuedBillsTab = UIHolder.MainTabControl.Tabs.Add("IssuedBillsTab", "Facturas Emitidas");
            UIHolder.ReceivedBillsTab = UIHolder.MainTabControl.Tabs.Add("ReceivedBillsTab", "Facturas Recibidas");

            foreach(UltraTab tab in UIHolder.MainTabControl.Tabs)
            {
                tab.Enabled = false;
            };

            UIHolder.MainTabControl.SelectedTab.Enabled = true;

            UIHolder.MainWindow.Controls.Add(UIHolder.MainTabControl);
        }
    }
}
