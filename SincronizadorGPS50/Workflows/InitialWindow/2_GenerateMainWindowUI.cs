﻿using Infragistics.Win.UltraWinTabControl;

namespace SincronizadorGPS50
{
   internal class GenerateMainWindowUI
   {
      internal GenerateMainWindowUI()
      {
         try
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
         }
         catch (System.Exception exception)
         {
            throw exception;
         };
      }
   }
}
