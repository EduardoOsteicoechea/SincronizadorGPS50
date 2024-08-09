using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestprojectDataManager
{
    public class SynchronizerUserRememberableDataModel
    {
        public string Sage50LocalTerminalPath { get; set; } = "";
        public string Sage50Username { get; set; } = "";
        public string Sage50Password { get; set; } = "";
        public string Sage50CompanyGroupName { get; set; } = "";
        public byte Remember { get; set; } = 0;
    }
}
