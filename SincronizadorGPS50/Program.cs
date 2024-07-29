using Infragistics.Win.AppStyling;
using Infragistics.Win.AppStyling.Runtime;
using sage.ew.db;
using SincronizadorGPS50.GestprojectAPI;
using SincronizadorGPS50.Workflows.Clients;
using System;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SincronizadorGPS50
{
    internal static class Program
    {
        [STAThread]
        internal static void Main(string[] args)
        {
            // Add Global Styles
            Infragistics.Win.AppStyling.StyleManager.Load(System.Windows.Forms.Application.StartupPath + "\\Resources\\Styles\\Excel2013 - White.isl");

            // InitialWindow wokflow
            new GenerateMainWindow();

            // Sage50Connection workflow
            new GenerateSage50ConnectionTabPageUI();

            // Used After ClientsTabUI workflow is initialized
            new ConnectToGestprojectDatabase();

            // Display initial window
            UIHolder.MainWindow.ShowDialog();



            //System.Windows.Forms.Application.Run(UIHolder.MainWindow);
        }
    }
}
