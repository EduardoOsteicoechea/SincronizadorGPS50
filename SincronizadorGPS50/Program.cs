using SincronizadorGPS50.GestprojectAPI;
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

            System.Windows.Forms.Application.Run(ApplicationManager.ApplicationGlobalContext);
        }
    }
}
