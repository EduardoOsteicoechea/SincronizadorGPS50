﻿using System.Collections.Generic;
using System.Security.Principal;
using System;
using System.Windows.Forms;

namespace GestprojectDatabaseConnector
{
    internal class GetLocalDeviceData
    {
        internal bool IsSuccessfull { get; set; } = false; 
        public static string WindowsIdentityDomainName { get; set; } = null;
        public static string WindowsIdentityUserName { get; set; } = null;
        public static string MicrosoftSQLServerfolderPath { get; set; } = null;
        public static List<string> DatabaseInstancesNames { get; set; } = new List<string>();
        public static List<string> DatabaseVersionNames { get; set; } = new List<string>();
        public GetLocalDeviceData()
        {
            try
            {
                string MicrosoftSQLServerfolderPath =
                Environment.GetEnvironmentVariable("ProgramW6432") + @"\" + "Microsoft SQL Server";

                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                string userName = identity.Name;
                string[] userNameParts = userName.Split('\\');

                if(userNameParts.Length == 1)
                {
                    string WindowsIdentityUserName = Environment.UserName;
                }
                else
                {
                    WindowsIdentityDomainName = userNameParts[0];
                    WindowsIdentityUserName = userNameParts[1];
                };

                if(WindowsIdentityDomainName != null && WindowsIdentityUserName != null)
                {
                    ConnectionDataHolder.WindowsIdentityDomainName = WindowsIdentityDomainName;
                    ConnectionDataHolder.WindowsIdentityUserName = WindowsIdentityUserName;
                }
                else if(WindowsIdentityUserName != null && WindowsIdentityDomainName == "")
                {
                    ConnectionDataHolder.WindowsIdentityUserName = WindowsIdentityUserName;
                }
                else if(WindowsIdentityUserName == "")
                {
                    MessageBox.Show("No logramos encontrar el nombre de usuario.\n\nContacte al proveedor para más información.");
                };

                IsSuccessfull = true;
            }
            catch(System.Exception e)
            {
                MessageBox.Show($"Error: \n\n{e.ToString()}. \n\nProcederemos a detener la aplicación. Contacte a nuestro servicio de atención al cliente para reportar el error y recibir servicio técnico al respecto.");
            };
        }
    }
}

    