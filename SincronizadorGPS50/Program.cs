using sage.ew.db;
using SincronizadorGPS50.GestprojectAPI;
using SincronizadorGPS50.Workflows.Clients;
using System;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
    internal static class Program
    {
        [STAThread]
        internal static void Main(string[] args) 
        {
            // InitialWindow wokflow
            new GenerateMainWindow();

            // Sage50Connection workflow
            new GenerateSage50ConnectionTabPageUI();

            // Used After ClientsTabUI workflow is initialized
            new ConnectToGestprojectDatabase();

            // Display initial window
            UIHolder.MainWindow.ShowDialog();
        }
    }
}
