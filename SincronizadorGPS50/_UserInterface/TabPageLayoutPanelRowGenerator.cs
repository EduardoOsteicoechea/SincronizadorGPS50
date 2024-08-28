using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
   internal class TabPageLayoutPanelRowGenerator : ITabPageLayoutPanelRowGenerator
   {
      public UltraPanel RowPanel { get; set; }

      public UltraPanel GenerateRowPanel()
      {
         RowPanel = new UltraPanel();
         RowPanel.Height = StyleHolder.CenterRowHeight;
         RowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
         return RowPanel;
      }
   }
}
