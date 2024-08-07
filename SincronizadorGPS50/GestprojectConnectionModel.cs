using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SincronizadorGPS50
{
    internal class GestprojectConnectionModel
    {
        internal string Server { get; set; } = "";
        internal string DatabaseInstance { get; set; } = "";
        internal string DatabaseName { get; set; } = "";
        internal string DatabaseUser { get; set; } = "";
        private string _DatabasePassword { get; set; } = "";
        internal string DatabasePassword 
        { 
            get { return _DatabasePassword; } 
        }
        internal string AskForServer { get; set; } = "";
        internal string LastServer { get; set; } = "";

        internal void RecordPasswordFromXML(string encryptedPassword) 
        {
            _DatabasePassword = Encryptor.UnEncrypt(encryptedPassword);
        }
    }
}
