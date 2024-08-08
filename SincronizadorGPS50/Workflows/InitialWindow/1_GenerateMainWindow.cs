using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
    internal class GenerateMainWindow
    {
        internal bool IsSuccessful { get; set; } = false;
        internal GenerateMainWindow() 
        {
            try
            {
                MainWindowUIHolder.MainWindow = new System.Windows.Forms.Form();
                MainWindowUIHolder.MainWindow.Text = "SincronizadorGPS50";
                MainWindowUIHolder.MainWindow.WindowState = FormWindowState.Maximized;
                MainWindowUIHolder.MainWindow.SizeGripStyle = SizeGripStyle.Hide;
                MainWindowUIHolder.MainWindow.Icon = Resources.appicon;

                IsSuccessful = true;
            }
            catch(System.Exception e)
            {
                MessageBox.Show($"Error: \n\n{e.ToString()}. \n\nProcederemos a detener la aplicación. Contacte a nuestro servicio de atención al cliente para reportar el error y recibir servicio técnico al respecto.");
            };
        }
    }
}
