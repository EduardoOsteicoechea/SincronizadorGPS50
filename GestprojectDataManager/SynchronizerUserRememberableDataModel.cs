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
        public List<string> Sage50AvailableCompanyGroupsNameList { get; set; } = new List<string>();
        public List<string> Sage50AvailableCompanyGroupsMainCodeList { get; set; } = new List<string>();
        public List<string> Sage50AvailableCompanyGroupsCodeList { get; set; } = new List<string>();
        public List<string> Sage50AvailableCompanyGroupsGuidIdList { get; set; } = new List<string>();
        public byte Remember { get; set; } = 0;
    }
}
