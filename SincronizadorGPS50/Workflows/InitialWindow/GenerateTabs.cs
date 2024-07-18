using Infragistics.Win.UltraWinTabControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
    internal class GenerateTabs
    {
        public GenerateTabs() 
        {
            UIHolder.MainTabControl.SelectedTab = UIHolder.Sage50ConnectionTab;

            UIHolder.Sage50ConnectionTab = UIHolder.MainTabControl.Tabs.Add("Sage50ConnectionTab", "Conexión con Sage50");
            UIHolder.GlobalActionsTab = UIHolder.MainTabControl.Tabs.Add("GeneralTab", "Controles Globales");
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
