using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinTabControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
    internal class GenerateMainTabControl
    {
        internal GenerateMainTabControl() 
        {
            UIHolder.MainTabControl = new UltraTabControl();
            UIHolder.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            UIHolder.MainTabControl.TabStop = false;

            UIHolder.MainWindow.Controls.Add(UIHolder.MainTabControl);
        }
    }
}
