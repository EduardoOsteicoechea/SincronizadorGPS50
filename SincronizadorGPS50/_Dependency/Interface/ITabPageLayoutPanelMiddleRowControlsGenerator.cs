using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;

namespace SincronizadorGPS50
{
   internal interface ITabPageLayoutPanelMiddleRowControlsGenerator
   {
      UltraGrid Grid { get; set; }
      void CreateGrid();
      void SetGridFilters();
      void PreventGridUpdates();
      void StyleGrid();
      void SetGridDataSource
      (
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider synchronizationTableSchemaProvider,
         IGridDataSourceGenerator gridDataSourceGenerator
      );
      void AddGridToRow(UltraPanel row);
      void SetClickCellEventHandler();
      void SetAfterRowFilterChangedEventHandler();
      void AfterRowFilterChangedEventHandler(object sender, AfterRowFilterChangedEventArgs e);

      void GenerateControls
      (
         UltraPanel rowPanel,
         IGestprojectConnectionManager gestprojectConnectionManager,
         ISage50ConnectionManager sage50ConnectionManager,
         ISynchronizationTableSchemaProvider synchronizationTableSchemaProvider,
         IGridDataSourceGenerator gridDataSourceGenerator
      );
   }
}
