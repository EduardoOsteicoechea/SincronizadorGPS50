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
            MainWindowUIHolder.MainTabControl = new UltraTabControl();
            MainWindowUIHolder.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            MainWindowUIHolder.MainTabControl.TabStop = false;

            MainWindowUIHolder.MainWindow.Controls.Add(MainWindowUIHolder.MainTabControl);
        }
    }
}
