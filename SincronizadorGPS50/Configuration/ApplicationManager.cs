using System;

namespace SincronizadorGPS50
{
    internal static class ApplicationManager
    {
        internal static System.Windows.Forms.ApplicationContext ApplicationGlobalContext { get; set; } = null;
        internal static string GestprojectXMLConfigurationFilePath { get; set; } = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Micad\Gestproject\12.0.0.0\Gestproject.config.xml");
        internal static string GestprojectDATConfigurationFilePath { get; set; } = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Micad\Gestproject\12.0.0.0\_APPSETTINGS.DAT");
        internal static string GestprojectDATUserSettingsFilePath { get; set; } = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Micad\Gestproject\12.0.0.0\_USERSETTINGS.DAT");
        internal static string GestprojectStylesFolderPath { get; set; } = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"Micad\Gestproject 2020\Styles");
    }
}
