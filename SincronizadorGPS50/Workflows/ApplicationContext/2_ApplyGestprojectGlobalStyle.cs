using System.Windows.Forms;
using System.IO;
using GestprojectStyleManager;

namespace SincronizadorGPS50
{
    internal class ApplyGestprojectGlobalStyle
    {
        internal bool IsSuccessful { get; set; } = false;
        internal ApplyGestprojectGlobalStyle()
        {
            Infragistics.Win.AppStyling.StyleManager.Load(new GestGestprojectStyleFilePath().FilePath());
            IsSuccessful = true;
        }
    }
}
