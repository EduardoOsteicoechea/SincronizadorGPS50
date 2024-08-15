

namespace SincronizadorGPS50
{
    internal static class Program
    {
        [System.STAThreadAttribute]
        internal static void Main()
        {
            System.Windows.Forms.Application.Run(new SetupAndConnections());
        }
    }
}