using System;
using System.Data;
using System.Reflection;

namespace SincronizadorGPS50
{
   internal static class UIFactory
   {
      internal static void GenerateTabPage
      (
         ITabPageGenerator tabPageGenerator,
         Infragistics.Win.UltraWinTabControl.UltraTabPageControl.ControlCollection MainWindowUITabControlCollection, // just call the .Add() method and pass the "MainPanel" UltraPanel that contains the "TabPageTableLayoutPanel" TableLayoutPanel, which itself will contain the UltraPanel corresponding to the UI rows along with their respective TableLayoutPanels, controls and event handlers.

         ITabPageMainPanelTableLayoutPanelGenerator tabPageMainPanelTableLayoutGenerator, // Generates tab page main panel table layout.

         ITabPageLayoutPanelRowGenerator tabPageUIRowGenerator, // Generates the Row UltraPanel.

         ITabPageLayoutPanelMiddleRowControlsGenerator tabPageUImiddleRowControlsGenerator, // Generates the Row's UltraPanel TableLayout controls.
         ITabPageLayoutPanelTopRowControlsGenerator tabPageUItopRowControlsGenerator, // Generates the Row's UltraPanel TableLayout controls.
         ITabPageLayoutPanelBottomRowControlsGenerator tabPageUIbottomRowControlsGenerator, // Generates the Row's UltraPanel TableLayout controls.

         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider synchronizationTableSchemaProvider,
               
         IGridDataSourceGenerator gridDataSourceGenerator,
         IEntitySynchronizer entitySynchronizer
      )
      {
         try
         {
            tabPageGenerator._01_Build(
               MainWindowUITabControlCollection,
               tabPageMainPanelTableLayoutGenerator,
               tabPageUIRowGenerator,
               tabPageUImiddleRowControlsGenerator,
               tabPageUItopRowControlsGenerator,
               tabPageUIbottomRowControlsGenerator,
               gestprojectConnectionManager,
               sage50ConnectionManager,
               synchronizationTableSchemaProvider,
               gridDataSourceGenerator,
               entitySynchronizer
            );
         }
         catch(System.Exception exception)
         {
            throw ApplicationLogger.ReportError(
               MethodBase.GetCurrentMethod().DeclaringType.Namespace,
               MethodBase.GetCurrentMethod().DeclaringType.Name,
               MethodBase.GetCurrentMethod().Name,
               exception
            );
         };
      }
   }
}