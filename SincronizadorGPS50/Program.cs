namespace SincronizadorGPS50
{
   internal static class Program
   {
      [System.STAThreadAttribute]
      internal static void Main()
      {
         try
         {
            System.Windows.Forms.Application.Run(new SetupAndConnections());
         }
         catch (System.Exception exception)
         {
            System.Windows.Forms.MessageBox.Show($"Error detectado:\n\n{exception.Message}\n\n{exception.InnerException}");
            System.Windows.Forms.MessageBox.Show($"Procederemos a cerrar la aplicación.");
            MainWindowActions.CloseCompletellyAndAbruptly();
         };
      }
   }
}