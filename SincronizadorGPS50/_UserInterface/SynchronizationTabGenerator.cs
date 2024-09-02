﻿using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using SincronizadorGPS50.Workflows.Sage50Connection;
using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   //internal class SynchronizationTabGenerator<T12, T22> : ITabPageGenerator<T12, T22>
   internal class SynchronizationTabGenerator<T1, T2> : ITabPageGenerator<T1, T2>
   {
      public UltraPanel MainPanel { get; set;}
      public TableLayoutPanel TabPageTableLayoutPanel { get; set; }
      public UltraPanel TopRow { get; set; }
      public UltraPanel MiddleRow { get; set; }
      public UltraPanel BottomRow { get; set; }
      public IGestprojectConnectionManager GestprojectConnectionManager { get; set; }
      public ISage50ConnectionManager Sage50ConnectionManager { get; set; }
      public ISynchronizationTableSchemaProvider SynchronizationTableSchemaProvider { get; set; }
      public DataTableGeneratorDelegate DataTableGeneratorDelegate { get; set; }
      public IEntitySynchronizer<T1, T2> EntitySynchronizer { get; set; }

      public void _01_Build
      (
         Control.ControlCollection MainWindowUITabControlCollection, 
         ITabPageMainPanelTableLayoutPanelGenerator tabPageMainPanelTableLayoutGenerator, 
         ITabPageLayoutPanelRowGenerator tabPageUIRowGenerator,

         ITabPageLayoutPanelMiddleRowControlsGenerator<T1, T2> tabPageUImiddleRowControlsGenerator,
         ITabPageLayoutPanelTopRowControlsGenerator<T1, T2> tabPageUItopRowControlsGenerator, 
         ITabPageLayoutPanelBottomRowControlsGenerator<T1, T2> tabPageUIbottomRowControlsGenerator, 

         IGestprojectConnectionManager gestprojectConnectionManager, 
         ISage50ConnectionManager sage50ConnectionManager, 
         ISynchronizationTableSchemaProvider synchronizationTableSchemaProvider,
         IGridDataSourceGenerator<T1, T2> gridDataSourceGenerator,

         IEntitySynchronizer<T1, T2> entitySynchronizer
      )
      {
         try
         {
            GestprojectConnectionManager = gestprojectConnectionManager;
            Sage50ConnectionManager = sage50ConnectionManager;
            SynchronizationTableSchemaProvider = synchronizationTableSchemaProvider;
            DataTableGeneratorDelegate = gridDataSourceGenerator.GenerateDataTable;
            EntitySynchronizer = entitySynchronizer;

            MainPanel = _02_CreateMainPanel();
            TabPageTableLayoutPanel = _03_GenerateMainPanelTableLayoutPanel(tabPageMainPanelTableLayoutGenerator);
            _04_AddTabPageTableLayoutToMainPanel(TabPageTableLayoutPanel, MainPanel);
            _05_AddMainPanelToTab(MainPanel, MainWindowUITabControlCollection);

            MiddleRow = _08_CreateMiddleRow(tabPageUIRowGenerator);
            _09_CreateAndAddMiddleRowControls(MiddleRow, tabPageUImiddleRowControlsGenerator, gestprojectConnectionManager, sage50ConnectionManager, synchronizationTableSchemaProvider, gridDataSourceGenerator);

            TopRow = _06_CreateTopRow(tabPageUIRowGenerator);
            _07_CreateAndAddTopRowControls(TopRow, tabPageUImiddleRowControlsGenerator.Grid, tabPageUItopRowControlsGenerator, gridDataSourceGenerator);

            BottomRow = _10_CreateBottomRow(tabPageUIRowGenerator);
            _11_CreateAndAddBottomRowControls(BottomRow, tabPageUIbottomRowControlsGenerator);

            _12_AddRowsPanelsToTabPageTableLayout(TopRow, MiddleRow, BottomRow, TabPageTableLayoutPanel);
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

      public UltraPanel _02_CreateMainPanel() 
      {
         UltraPanel panel = new UltraPanel();
         panel.Dock = DockStyle.Fill;
         return panel;
      }
      public TableLayoutPanel _03_GenerateMainPanelTableLayoutPanel(ITabPageMainPanelTableLayoutPanelGenerator tabPageMainPanelTableLayoutGenerator)
      {
         return tabPageMainPanelTableLayoutGenerator.GenerateGlobalTableLayoutPanel();
      }
      public void _04_AddTabPageTableLayoutToMainPanel(TableLayoutPanel tabPageTableLayoutPanel, UltraPanel mainPanel)
      {
         mainPanel.ClientArea.Controls.Add(tabPageTableLayoutPanel);
      }
      public void _05_AddMainPanelToTab(UltraPanel mainPanel, Control.ControlCollection tabControlCollection)
      {
         tabControlCollection.Add(mainPanel);
      }


      public UltraPanel _06_CreateTopRow(ITabPageLayoutPanelRowGenerator rowGenerator)
      {
         return rowGenerator.GenerateRowPanel();
      }

      public UltraPanel _06_CreateTopRow(ITabPageLayoutPanelRowGenerator rowGenerator, IGridDataSourceGenerator<T1, T2> gridDataSourceGenerator) => throw new NotImplementedException();

      public void _07_CreateAndAddTopRowControls
      (
         UltraPanel topRow,
         Infragistics.Win.UltraWinGrid.UltraGrid middleRowGrid,
         ITabPageLayoutPanelTopRowControlsGenerator<T1, T2> tabPageUItopRowControlsGenerator,
         IGridDataSourceGenerator<T1, T2> gridDataSourceGenerator
      )
      {
         tabPageUItopRowControlsGenerator.GenerateControls
         (
            topRow,
            middleRowGrid,
            GestprojectConnectionManager,
            Sage50ConnectionManager,
            SynchronizationTableSchemaProvider,
            gridDataSourceGenerator,
            EntitySynchronizer
         );
      }

      public UltraPanel _08_CreateMiddleRow(ITabPageLayoutPanelRowGenerator rowGenerator)
      {
         return rowGenerator.GenerateRowPanel();
      }
      public void _09_CreateAndAddMiddleRowControls
      (
         UltraPanel middleRow, 
         ITabPageLayoutPanelMiddleRowControlsGenerator<T1, T2> middleRowControlsGenerator, 
         IGestprojectConnectionManager gestprojectConnectionManager, 
         ISage50ConnectionManager sage50ConnectionManager, 
         ISynchronizationTableSchemaProvider synchronizationTableSchemaProvider, 
         IGridDataSourceGenerator<T1, T2> gridDataSourceGenerator
      )
      {
         middleRowControlsGenerator.GenerateControls
         (
            middleRow,
            gestprojectConnectionManager,
            sage50ConnectionManager,
            synchronizationTableSchemaProvider,
            gridDataSourceGenerator
         );
      }

      public UltraPanel _10_CreateBottomRow(ITabPageLayoutPanelRowGenerator rowGenerator)
      {
         return rowGenerator.GenerateRowPanel();
      }
      public void _11_CreateAndAddBottomRowControls
      (
         UltraPanel bottomRow, 
         ITabPageLayoutPanelBottomRowControlsGenerator<T1, T2> tabPageUIbottomRowControlsGenerator
      )
      {
         tabPageUIbottomRowControlsGenerator.GenerateControls
         (
            bottomRow,
            GestprojectConnectionManager,
            Sage50ConnectionManager,
            SynchronizationTableSchemaProvider
         );
      }
      public void _12_AddRowsPanelsToTabPageTableLayout(UltraPanel topRow, UltraPanel middleRow, UltraPanel bottomRow, TableLayoutPanel tabPageTableLayoutPanel)
      {
         tabPageTableLayoutPanel.Controls.Add(topRow, 0, 0);
         tabPageTableLayoutPanel.Controls.Add(middleRow, 0, 1);
         tabPageTableLayoutPanel.Controls.Add(bottomRow, 0, 2);
      }
   }
}
