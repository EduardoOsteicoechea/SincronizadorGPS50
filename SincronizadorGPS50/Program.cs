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
























            //// GestprojectClients workflow
            //new GenerateSage50ClientsTab();
            //new GetGestprojectParticipants();
            //new FilterGestprojectClients();
            //new SaveGestprojectClientsDataInCSV();
            //new SaveGestprojectClientsIdInCSV();
            //new ShowSynchronizationTableFromCSV();

            //// SynchronizeClients workflow
            //new CreateGestprojectClientsInSage50();
            //new GetNewSage50ClientData();
            //new SaveNewSage50ClientDataInCSV();
            //new RefreshSynchronizationTableFromCSV();
        }
    }
}
