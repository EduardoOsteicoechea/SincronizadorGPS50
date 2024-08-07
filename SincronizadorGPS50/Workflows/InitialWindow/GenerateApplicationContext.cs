using Dinq.Gestproject;
using Infragistics.Win.AppStyling.Runtime;
using Microsoft.VisualBasic.Logging;
using sage.ew.functions;
using sage.ew.usuario;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using static sage.ew.docsven.FirmaElectronica;

namespace SincronizadorGPS50
{
    internal class GenerateApplicationContext : ApplicationContext
    {
        private FileStream _applicationData;
        public GenerateApplicationContext() 
        {
            ApplicationManager.ApplicationGlobalContext = this;

            try
            {
                MainWindowUIHolder.MainWindow = new System.Windows.Forms.Form();
                MainWindowUIHolder.MainWindow.Text = "SincronizadorGPS50";
                MainWindowUIHolder.MainWindow.WindowState = FormWindowState.Maximized;
                MainWindowUIHolder.MainWindow.SizeGripStyle = SizeGripStyle.Hide;
                MainWindowUIHolder.MainWindow.Icon = Resources.ApplicationIcon;
            }
            catch(System.Exception e)
            {
                MessageBox.Show($"Error: \n\n{e.ToString()}. \n\nProcederemos a detener la aplicación. Contacte a nuestro servicio de atención al cliente para reportar el error y recibir servicio técnico al respecto.");

                System.Windows.Forms.Application.ExitThread();
                System.Windows.Forms.Application.Exit();
            };

            try
            {
                UserAppSettings userSettings = null;

                if(File.Exists(ApplicationManager.GestprojectDATUserSettingsFilePath))
                {
                    MessageBox.Show("User configuration file exists");
                    MessageBox.Show(ApplicationManager.GestprojectDATUserSettingsFilePath);

                    userSettings = Serializer.DeserializeObject(ApplicationManager.GestprojectDATUserSettingsFilePath) as UserAppSettings;

                    MessageBox.Show(userSettings.StyleFileName.ToString());
                };

                if(userSettings == null)
                {
                    userSettings = new UserAppSettings();
                };

                userSettings = userSettings as UserAppSettings;

                Infragistics.Win.AppStyling.StyleManager.Load(Path.Combine(ApplicationManager.GestprojectStylesFolderPath, userSettings.StyleFileName));
            }
            catch(System.Exception e)
            {
                MessageBox.Show($"Error: \n\n{e.ToString()}. \n\nProcederemos a detener la aplicación. Contacte a nuestro servicio de atención al cliente para reportar el error y recibir servicio técnico al respecto.");

                System.Windows.Forms.Application.ExitThread();
                System.Windows.Forms.Application.Exit();
            };

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(ApplicationManager.GestprojectXMLConfigurationFilePath);

                string xmlContent = xmlDocument.OuterXml;

                GestprojectConnectionModel gestprojectConnectionModel = new GestprojectConnectionModel();

                gestprojectConnectionModel.Server = xmlDocument.SelectSingleNode("/configuration/conexion/Servidor").InnerText;
                gestprojectConnectionModel.DatabaseInstance = xmlDocument.SelectSingleNode("/configuration/conexion/Instancia").InnerText;
                gestprojectConnectionModel.DatabaseName = xmlDocument.SelectSingleNode("/configuration/conexion/NombreBD").InnerText;
                gestprojectConnectionModel.DatabaseUser = xmlDocument.SelectSingleNode("/configuration/conexion/Usuario").InnerText;
                gestprojectConnectionModel.RecordPasswordFromXML(xmlDocument.SelectSingleNode("/configuration/conexion/Password").InnerText);
                gestprojectConnectionModel.AskForServer = xmlDocument.SelectSingleNode("/configuration/conexion/AskServerAtStartup").InnerText;
                gestprojectConnectionModel.LastServer = xmlDocument.SelectSingleNode("/configuration/conexion/LastServer").InnerText;

                MessageBox.Show(
                    "gestprojectConnectionModel.Server: " + gestprojectConnectionModel.Server + "\n" +
                    "gestprojectConnectionModel.DatabaseInstance: " + gestprojectConnectionModel.DatabaseInstance + "\n" +
                    "gestprojectConnectionModel.DatabaseName: " + gestprojectConnectionModel.DatabaseName + "\n" +
                    "gestprojectConnectionModel.DatabaseUser: " + gestprojectConnectionModel.DatabaseUser + "\n" +
                    "gestprojectConnectionModel.DatabasePassword: " + gestprojectConnectionModel.DatabasePassword + "\n" +
                    "gestprojectConnectionModel.AskForServer: " + gestprojectConnectionModel.AskForServer + "\n" +
                    "gestprojectConnectionModel.LastServer: " + gestprojectConnectionModel.LastServer + "\n"
                );
            }
            catch(System.Exception e)
            {
                MessageBox.Show($"Error: \n\n{e.ToString()}. \n\nProcederemos a detener la aplicación. Contacte a nuestro servicio de atención al cliente para reportar el error y recibir servicio técnico al respecto.");

                System.Windows.Forms.Application.ExitThread();
                System.Windows.Forms.Application.Exit();
            };






            MainWindowUIHolder.MainWindow.Show();
        }
    }
}
