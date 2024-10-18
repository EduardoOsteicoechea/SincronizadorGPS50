﻿using Infragistics.Win.UltraWinTabControl;
using System.Reflection;

namespace SincronizadorGPS50
{
   public class IssuedBillsSynchronizationManager : IEntitySynchronizationManager
   {
      public void Launch
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         UltraTab hostTab
      )
      {
         try
         {
            hostTab.Enabled = true;
            MainWindowUIHolder.MainTabControl.SelectedTab = hostTab;

            UIFactory<GestprojectIssuedBillModel, Sage50IssuedBillModel>.GenerateTabPage
            (
               // Application Constructor
               new SynchronizationTabGenerator<GestprojectIssuedBillModel, Sage50IssuedBillModel>(),

               // UI comon
               hostTab.TabPage.Controls,
               new TabPageMainPanelTableLayoutPanelGenerator(),
               new TabPageLayoutPanelRowGenerator(),
               new MiddleRowControlsGenerator<GestprojectIssuedBillModel, Sage50IssuedBillModel>(),
               new TopRowControlsGenerator<GestprojectIssuedBillModel, Sage50IssuedBillModel>(false),
               new BottomRowControlsGenerator<GestprojectIssuedBillModel, Sage50IssuedBillModel>(),

               // Connectors
               gestprojectConnectionManager,
               sage50ConnectionManager,

               // Data Managers
               new IssuedBillsSynchronizationTableSchemaProvider(),
               new IssuedBillsDataTableManager(),
               new IssuedBillsSynchronizer()
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