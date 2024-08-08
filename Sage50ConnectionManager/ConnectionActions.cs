
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sage50ConnectionManager
{
    public static class ConnectionActions
    {
        public static LinkSage50 Sage50ConnectionManager { get; set; } = null;
        public static bool Connect(string Sage50LocalTerminalPath, string Sage50Username, string Sage50Password)
        {
            Sage50ConnectionManager = new LinkSage50(Sage50LocalTerminalPath);

            return Sage50ConnectionManager._Connect(Sage50Username, Sage50Password);
        }

        public static void Disconnect() 
        {
            Sage50ConnectionManager._Disconnect();
        }
    }
}
