using Infragistics.Win.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal interface ITabPageLayoutPanelBottomRowControlsGenerator
   {
      TableLayoutPanel RowTableLayout { get; set; }
      UltraButton ExitButton { get; set; }
      IGestprojectConnectionManager GestprojectConnectionManager { get; set; }
      ISage50ConnectionManager Sage50ConnectionManager { get; set; }
      ISynchronizationTableSchemaProvider SynchronizationTableSchemaProvider { get; set; }
      void GenerateControls
      (
         UltraPanel rowPanel,
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider synchronizationTableSchemaProvider
      );

      void GenerateRowTableLayoutPanel();
      void GenerateExitButtonControl();
      void SetExitButtonClickEventHandler();
      void ExitButtonClickEventHandler(object sender, EventArgs e);
      void AddButtonsToRowTableLayoutPanel();
      void AddRowTableLayoutPanelToRowPanel(UltraPanel rowPanel);
   }
}
