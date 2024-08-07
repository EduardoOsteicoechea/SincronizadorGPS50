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
            MainWindowUIHolder.MainTabControl.SelectedTab = UIHolder.Sage50ConnectionTab;

            UIHolder.Sage50ConnectionTab = MainWindowUIHolder.MainTabControl.Tabs.Add("Sage50ConnectionTab", "Conexión con Sage50");
            UIHolder.GlobalActionsTab = MainWindowUIHolder.MainTabControl.Tabs.Add("GeneralTab", "Controles Globales");
            UIHolder.ClientsTab = MainWindowUIHolder.MainTabControl.Tabs.Add("ClientsTab", "Clientes");
            UIHolder.ProvidersTab = MainWindowUIHolder.MainTabControl.Tabs.Add("ProvidersTab", "Proveedores");
            UIHolder.IssuedBillsTab = MainWindowUIHolder.MainTabControl.Tabs.Add("IssuedBillsTab", "Facturas Emitidas");
            UIHolder.ReceivedBillsTab = MainWindowUIHolder.MainTabControl.Tabs.Add("ReceivedBillsTab", "Facturas Recibidas");

            foreach(UltraTab tab in MainWindowUIHolder.MainTabControl.Tabs)
            {
                tab.Enabled = false;
            };

            MainWindowUIHolder.MainTabControl.SelectedTab.Enabled = true;

            MainWindowUIHolder.MainWindow.Controls.Add(MainWindowUIHolder.MainTabControl);
        }
    }
}
