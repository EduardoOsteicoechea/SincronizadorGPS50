using SincronizadorGPS50.GestprojectAPI;
using GestprojectDatabaseConnector;
using System;

namespace SincronizadorGPS50
{
    internal static class Program
    {
        [STAThread]
        internal static void Main(string[] args)
        {
            new GenerateApplicationContext();

            new ConnectToGestprojectDatabase();

            // Display initial window
            System.Windows.Forms.Application.Run(ApplicationManager.ApplicationGlobalContext);
        }
    }
}
