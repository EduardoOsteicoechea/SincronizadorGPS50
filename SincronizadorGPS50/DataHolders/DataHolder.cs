using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincronizadorGPS50
{
    internal static class DataHolder
    {
        public static List<string> Sage50InstallationsList { get; set; } = new List<String>();
        public static string Sage50LocalTerminalPath { get; set; } = "";
        public static string Sage50Username { get; set; } = "";
        public static string Sage50Password { get; set; } = "";
        public static string Sage50CompanyNumber { get; set; } = "";
        public static bool ConnectedToSage50 { get; set; } = false;
        public static LinkSage50 Sage50ConnectionObjectInstance { get; set; } = null;
        public static DataTable Sage50CompanyGroupsDataTable { get; set; } = null;
        public static List<CompanyGroup> Sage50CompanyGroupsList { get; set; } = new List<CompanyGroup>();
        public static string Sage50SelectedCompanyGroupName { get; set; } = "";
        public static string Sage50SelectedCompanyGroupCode { get; set; } = "";
        public static string Sage50SelectedCompanyGroupMainCode { get; set; } = "";
        
        
        
    }
}
