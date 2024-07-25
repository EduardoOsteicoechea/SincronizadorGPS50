using SincronizadorGPS50.Workflows.Clients;
using System;

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

            // EndWorkflow
            UIHolder.MainWindow.ShowDialog();
        }
    }
}
