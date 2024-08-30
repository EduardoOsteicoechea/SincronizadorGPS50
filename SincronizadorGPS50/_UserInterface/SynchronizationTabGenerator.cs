﻿using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using SincronizadorGPS50.Workflows.Sage50Connection;
using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal class SynchronizationTabGenerator<GestprojectProviderModel, Sage50ProviderModel> : ITabPageGenerator<GestprojectProviderModel, Sage50ProviderModel>
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
      public IEntitySynchronizer<GestprojectProviderModel, Sage50ProviderModel> EntitySynchronizer { get; set; }

      public void _01_Build
      (
         Control.ControlCollection MainWindowUITabControlCollection, 
         ITabPageMainPanelTableLayoutPanelGenerator tabPageMainPanelTableLayoutGenerator, 
         ITabPageLayoutPanelRowGenerator tabPageUIRowGenerator,

         ITabPageLayoutPanelMiddleRowControlsGenerator<GestprojectProviderModel, Sage50ProviderModel> tabPageUImiddleRowControlsGenerator,
         ITabPageLayoutPanelTopRowControlsGenerator<GestprojectProviderModel, Sage50ProviderModel> tabPageUItopRowControlsGenerator, 
         ITabPageLayoutPanelBottomRowControlsGenerator tabPageUIbottomRowControlsGenerator, 

         IGestprojectConnectionManager gestprojectConnectionManager, 
         ISage50ConnectionManager sage50ConnectionManager, 
         ISynchronizationTableSchemaProvider synchronizationTableSchemaProvider,
         IGridDataSourceGenerator<GestprojectProviderModel, Sage50ProviderModel> gridDataSourceGenerator,

         IEntitySynchronizer<GestprojectProviderModel, Sage50ProviderModel> entitySynchronizer
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

      public UltraPanel _06_CreateTopRow(ITabPageLayoutPanelRowGenerator rowGenerator, IGridDataSourceGenerator<GestprojectProviderModel, Sage50ProviderModel> gridDataSourceGenerator) => throw new NotImplementedException();

      public void _07_CreateAndAddTopRowControls
      (
         UltraPanel topRow,
         Infragistics.Win.UltraWinGrid.UltraGrid middleRowGrid,
         ITabPageLayoutPanelTopRowControlsGenerator<GestprojectProviderModel, Sage50ProviderModel> tabPageUItopRowControlsGenerator,
         IGridDataSourceGenerator<GestprojectProviderModel, Sage50ProviderModel> gridDataSourceGenerator
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
         ITabPageLayoutPanelMiddleRowControlsGenerator<GestprojectProviderModel, Sage50ProviderModel> middleRowControlsGenerator, 
         IGestprojectConnectionManager gestprojectConnectionManager, 
         ISage50ConnectionManager sage50ConnectionManager, 
         ISynchronizationTableSchemaProvider synchronizationTableSchemaProvider, 
         IGridDataSourceGenerator<GestprojectProviderModel, Sage50ProviderModel> gridDataSourceGenerator
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
         ITabPageLayoutPanelBottomRowControlsGenerator tabPageUIbottomRowControlsGenerator
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
