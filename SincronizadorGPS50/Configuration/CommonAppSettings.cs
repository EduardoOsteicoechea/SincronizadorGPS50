using Infragistics.Win.AppStyling.Runtime;
using System;
using System.Windows.Forms;

namespace Dinq.Gestproject
{
    /// <summary>
    /// Clase que contiene los elementos de configuración de la aplicación de ambito aplicación (comunes a todos los usuarios)
    /// </summary>
    [Serializable()]
    class CommonAppSettings
    {
        private String mDBServer = "(local)";
        public String DBServer
        {
            get { return mDBServer; }
            set { mDBServer = value; }
        }

        private String mDBInstance = "GESTPROJECT2020";
        public String DBInstance
        {
            get { return mDBInstance; }
            set { mDBInstance = value; }
        }

        private String mDBName = "GESTPROJECT2020";
        public String DBName
        {
            get { return mDBName; }
            set { mDBName = value; }
        }

        private String mDBUserName = "SA";
        public String DBUserName
        {
            get { return mDBUserName; }
            set { mDBUserName = value; }
        }

        private String mDBUserPassword = "W&,Pn)LKG&"; //"N0V0N3T.es" CODIFICADO;
        public String DBUserPassword
        {
            get { return mDBUserPassword; }
            set { mDBUserPassword = value; }
        }

        private String imagesFolder = Application.StartupPath + "\\Proyectos\\Imagenes";
        public String ImagesFolder
        {
            get { return imagesFolder; }
            set { imagesFolder = value; }
        }

        private String projectsFolder = Application.StartupPath + "\\Proyectos";
        public String ProjectsFolder
        {
            get { return projectsFolder; }
            set { projectsFolder = value; }
        }

        //private String plantillasFolder = StaticSettings.AppReportsFolder;
        //public String PlantillasFolder
        //{
        //    get { return plantillasFolder; }
        //    set { plantillasFolder = value; }
        //}

        private String regUserName = "";
        public String RegUserName
        {
            get { return regUserName; }
            set { regUserName = value; }
        }

        private String regCompanyName = "";
        public String RegCompanyName
        {
            get { return regCompanyName; }
            set { regCompanyName = value; }
        }

        private String regSerialNumber = "";
        public String RegSerialNumber
        {
            get { return regSerialNumber; }
            set { regSerialNumber = value; }
        }

        //DE11-03-009 20180809
        private bool askServerAtStartup = false;
        public bool AskServerAtStartup
        {
            get { return askServerAtStartup; }
            set { askServerAtStartup = value; }
        }

        private String lastServer = "";
        public String LastServer
        {
            get { return lastServer; }
            set { lastServer = value; }
        }

    }
}
