using Infragistics.Win.UltraWinGrid;
using System.Data;

namespace SincronizadorGPS50.Workflows.Clients
{
    internal class CenterRowUI
    {
        internal CenterRowUI() 
        {
            ClientsUIHolder.ClientDataTable = new UltraGrid();

            ClientsUIHolder.ClientDataTable.Dock = System.Windows.Forms.DockStyle.Fill;

            DataTable synchronizationTable = new CreateSynchronizationTable().Table;

            ClientsUIHolder.ClientDataTable.DataSource = synchronizationTable;

            ClientsUIHolder.CenterRow.ClientArea.Controls.Add(ClientsUIHolder.ClientDataTable);
        }
    }
}





//ClientsUIHolder.ClientDataTable.Rows.ColumnFilters = new ColumnFiltersCollection();

//UltraGridBand band = ClientsUIHolder.ClientDataTable.DisplayLayout.Bands[0];
//band.Override.AllowRowFiltering = DefaultableBoolean.True;
//band.Columns[1].AllowRowFiltering = DefaultableBoolean.False;
//band.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
//band.ColumnFilters["id"].FilterConditions.Clear();
//band.ColumnFilters["id"].FilterConditions.Add(FilterComparisionOperator.GreaterThan, 5);
//band.ColumnFilters["id"].FilterConditions.Add(FilterComparisionOperator.LessThan, 10);
//band.ColumnFilters["id"].LogicalOperator = FilterLogicalOperator.And;


//string TopRowRefreshTableButtonImagePath = Application.StartupPath + @"\Media\Image\Refrescar 02.png";
//Image TopRowRefreshTableButtonImage = null;
//if(File.Exists(TopRowRefreshTableButtonImagePath))
//{
//    TopRowRefreshTableButtonImage = Image.FromFile(TopRowRefreshTableButtonImagePath);
//};

//UltraPictureBox aa = new UltraPictureBox();
//aa.Image = ;
//ClientsUIHolder.ClientDataTable.Rows[0].Cells[0].EditorComponent = ;
//ClientsUIHolder.ClientDataTable.Rows[0].Cells[0].IsInEditMode = false;
//ClientsUIHolder.ClientDataTable.Rows[0].Cells[0].Appearance.Image = TopRowRefreshTableButtonImage;