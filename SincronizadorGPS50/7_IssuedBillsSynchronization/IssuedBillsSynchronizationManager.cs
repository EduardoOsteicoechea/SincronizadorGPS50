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

            UIFactory<SynchronizableIssuedInvoiceModel, Sage50IssuedBillModel>.GenerateTabPage
            (
               // Application Constructor
               new SynchronizationTabGenerator<SynchronizableIssuedInvoiceModel, Sage50IssuedBillModel>(),

               // UI comon
               hostTab.TabPage.Controls,
               new TabPageMainPanelTableLayoutPanelGenerator(),
               new TabPageLayoutPanelRowGenerator(),
               new MiddleRowControlsGenerator<SynchronizableIssuedInvoiceModel, Sage50IssuedBillModel>(),
               new TopRowControlsGenerator<SynchronizableIssuedInvoiceModel, Sage50IssuedBillModel>(true),
               new BottomRowControlsGenerator<SynchronizableIssuedInvoiceModel, Sage50IssuedBillModel>(),

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
