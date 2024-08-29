﻿

using Infragistics.Win.Misc;
using System.Data;

namespace SincronizadorGPS50
{
   internal interface ITabPageGenerator
   {
      IGestprojectConnectionManager GestprojectConnectionManager { get; set; }
      ISage50ConnectionManager Sage50ConnectionManager { get; set; }
      ISynchronizationTableSchemaProvider SynchronizationTableSchemaProvider { get; set; }
      Infragistics.Win.Misc.UltraPanel MainPanel { get; set; }
      System.Windows.Forms.TableLayoutPanel TabPageTableLayoutPanel { get; set; }
      Infragistics.Win.Misc.UltraPanel TopRow { get; set; }
      Infragistics.Win.Misc.UltraPanel MiddleRow { get; set; }
      Infragistics.Win.Misc.UltraPanel BottomRow { get; set; }
      IEntitySynchronizer EntitySynchronizer { get; set; }
      // Must follow this order
      void _01_Build(
         Infragistics.Win.UltraWinTabControl.UltraTabPageControl.ControlCollection MainWindowUITabControlCollection,
         ITabPageMainPanelTableLayoutPanelGenerator tabPageMainPanelTableLayoutGenerator,
         ITabPageLayoutPanelRowGenerator tabPageUIRowGenerator,

         ITabPageLayoutPanelMiddleRowControlsGenerator tabPageUImiddleRowControlsGenerator,
         ITabPageLayoutPanelTopRowControlsGenerator tabPageUItopRowControlsGenerator,
         ITabPageLayoutPanelBottomRowControlsGenerator tabPageUIbottomRowControlsGenerator,

         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider synchronizationTableSchemaProvider,
         IGridDataSourceGenerator gridDataSourceGenerator,
         IEntitySynchronizer entitySynchronizer
      );

      Infragistics.Win.Misc.UltraPanel _02_CreateMainPanel();
      System.Windows.Forms.TableLayoutPanel _03_GenerateMainPanelTableLayoutPanel(ITabPageMainPanelTableLayoutPanelGenerator tabPageMainPanelTableLayoutGenerator);
      void _04_AddTabPageTableLayoutToMainPanel(System.Windows.Forms.TableLayoutPanel tabPageTableLayoutPanel, Infragistics.Win.Misc.UltraPanel mainPanel);
      void _05_AddMainPanelToTab
      (
         Infragistics.Win.Misc.UltraPanel mainPanel, 
         Infragistics.Win.UltraWinTabControl.UltraTabPageControl.ControlCollection tabControlCollection
      );

      Infragistics.Win.Misc.UltraPanel _06_CreateTopRow(ITabPageLayoutPanelRowGenerator rowGenerator,
         IGridDataSourceGenerator gridDataSourceGenerator);
      void _07_CreateAndAddTopRowControls
      (
         Infragistics.Win.Misc.UltraPanel topRow,
         Infragistics.Win.UltraWinGrid.UltraGrid middleRowGrid,
         ITabPageLayoutPanelTopRowControlsGenerator topRowControlsGenerator,
         IGridDataSourceGenerator gridDataSourceGenerator
      );


      Infragistics.Win.Misc.UltraPanel _08_CreateMiddleRow(ITabPageLayoutPanelRowGenerator rowGenerator);
      void _09_CreateAndAddMiddleRowControls
      (
         Infragistics.Win.Misc.UltraPanel middleRow, 
         ITabPageLayoutPanelMiddleRowControlsGenerator middleRowControlsGenerator,
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider synchronizationTableSchemaProvider,
         IGridDataSourceGenerator gridDataSourceGenerator
      );


      Infragistics.Win.Misc.UltraPanel _10_CreateBottomRow(ITabPageLayoutPanelRowGenerator rowGenerator);
      void _11_CreateAndAddBottomRowControls
      (
         Infragistics.Win.Misc.UltraPanel bottomRow, 
         ITabPageLayoutPanelBottomRowControlsGenerator bottomRowControlsGenerator
      );


      void _12_AddRowsPanelsToTabPageTableLayout
      (
         Infragistics.Win.Misc.UltraPanel topRow,
         Infragistics.Win.Misc.UltraPanel middleRow,
         Infragistics.Win.Misc.UltraPanel bottomRow,
         System.Windows.Forms.TableLayoutPanel tabPageTableLayoutPanel
      );
   }
}
