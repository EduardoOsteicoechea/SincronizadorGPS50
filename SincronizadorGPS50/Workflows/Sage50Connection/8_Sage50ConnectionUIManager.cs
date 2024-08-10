
namespace SincronizadorGPS50.Workflows.Sage50Connection
{
    internal class Sage50ConnectionUIManager
    {
        internal System.Windows.Forms.TableLayoutControlCollection ParentControl { get; set; } = null;
        internal string UIState { get; set; } = UIStates.StatefulStart;
        internal struct UIStates
        {
            public const string StatefulStart = "StatefulStart";
            public const string StatelessStart = "StatelessStart";
            public const string StatefulAwaitForFullDataRevision = "StatefulAwaitForFullDataRevision";
            public const string AwaitingLocalTerminalUserData = "AwaitingLocalTerminalUserData";
            public const string AwaitingConnectionWithUserDataAction = "AwaitingConnectionWithUserDataAction";
            public const string AwaitingCompanyGroupSelection = "AwaitingCompanyGroupSelection";
            public const string AwaitingConnectToCompanyGroupAndRememberAllInstruction = "AwaitingConnectToCompanyGroupAndRememberAllInstruction";
            public const string Connected = "Connected";
            public const string EditingConnection = "EditingConnection";
        };
        internal ConnectionStatePanel ConnectionStateUI { get; set; } = null;
        internal LocalTerminalUserDataPanel LocalTerminalUserDataUI { get; set; } = null;
        internal ConnectWithLocalTerminalUserDataPanel ConnectWithLocalTerminalUserDataUI { get; set; } = null;
        internal DisplayCompanyGroupsPanel DisplayCompanyGroupsUI { get; set; } = null;
        internal ConnectToCompanyGroupPanel ConnectToCompanyGroupUI { get; set; } = null;
        internal RememberAllDataPanel RememberAllDataUI { get; set; } = null;
        internal ManageFullConnectionPanel ManageFullConnectionUI { get; set; } = null;


        internal Sage50ConnectionUIManager(System.Windows.Forms.TableLayoutControlCollection parentControl, string uiModel) 
        {
            ParentControl = parentControl;

            if(uiModel == "stateless")
            { 
                CreateStatelessUI(); 
            }
            else
            { 
                CreateStatefulUI(); 
            }
        }



        internal void CreateStatelessUI()
        {
            UIState = UIStates.StatelessStart;
            ConnectionStateUI = new ConnectionStatePanel(this, ParentControl, 0, 0);
            LocalTerminalUserDataUI = new LocalTerminalUserDataPanel(this, ParentControl, 0, 1);
        }
        internal void CreateStatefulUI()
        {
            UIState = UIStates.StatefulAwaitForFullDataRevision;

            ConnectionStateUI = new ConnectionStatePanel(this, ParentControl, 0, 0);

            LocalTerminalUserDataUI = new LocalTerminalUserDataPanel(this, ParentControl, 0, 2);
            LocalTerminalUserDataUI.Remember();
            
            ConnectWithLocalTerminalUserDataUI = new ConnectWithLocalTerminalUserDataPanel(this, ParentControl, 0, 3);
            
            DisplayCompanyGroupsUI = new DisplayCompanyGroupsPanel(this, ParentControl, 0, 5);
            DisplayCompanyGroupsUI.Remember();
            
            ConnectToCompanyGroupUI = new ConnectToCompanyGroupPanel(this, ParentControl, 0, 7);
            ConnectToCompanyGroupUI.Remember();

            RememberAllDataUI = new RememberAllDataPanel(this, ParentControl, 0, 8);
            RememberAllDataUI.Remember();

            ManageFullConnectionUI = new ManageFullConnectionPanel(this, ParentControl, 0, 10);
            RememberAllDataUI.Remember();
        }
        internal void RestoreRememberedDataUI()
        {
            ClearUI();
            CreateStatefulUI();
        }
        internal void ClearUI()
        {
            UIState = UIStates.StatelessStart;
            Sage50ConnectionManager.ConnectionActions.Disconnect();

            ConnectionStateUI.Dispose();
            ConnectionStateUI = null;

            LocalTerminalUserDataUI.Dispose();
            LocalTerminalUserDataUI = null;

            ConnectWithLocalTerminalUserDataUI.Dispose();
            ConnectWithLocalTerminalUserDataUI = null;

            DisplayCompanyGroupsUI.Dispose();
            DisplayCompanyGroupsUI = null;

            ConnectToCompanyGroupUI.Dispose();
            ConnectToCompanyGroupUI = null;

            RememberAllDataUI.Dispose();
            RememberAllDataUI = null;

            ManageFullConnectionUI.Dispose();
            ManageFullConnectionUI = null;
        }
        internal void SetConnetedUI()
        {
            UIState = UIStates.Connected;
            ConnectionStateUI.SetUIToConnected();

            LocalTerminalUserDataUI.KeepData();
            LocalTerminalUserDataUI.KeepData();

            ConnectWithLocalTerminalUserDataUI.DisableControls();

            DisplayCompanyGroupsUI.KeepData();
            DisplayCompanyGroupsUI.DisableControls();

            ConnectToCompanyGroupUI.KeepData();
            ConnectToCompanyGroupUI.DisableControls();

            RememberAllDataUI.Dispose();
            RememberAllDataUI = null;

            ManageFullConnectionUI.EnableControls();
        }
        internal void SetStatefulAwaitForFullDataRevisionUI()
        {
            UIState = UIStates.StatefulAwaitForFullDataRevision;
            Sage50ConnectionManager.ConnectionActions.Disconnect();
            ConnectionStateUI.SetUIToDisconnected();

            LocalTerminalUserDataUI.KeepData();
            ConnectWithLocalTerminalUserDataUI.DisableControls();
            DisplayCompanyGroupsUI.KeepData();
            ConnectToCompanyGroupUI.KeepData();

            ManageFullConnectionUI.Dispose();
            ManageFullConnectionUI = null;
        }
        internal void SetStatelessStartUI()
        {
            UIState = UIStates.StatelessStart;
            Sage50ConnectionManager.ConnectionActions.Disconnect();
            ConnectionStateUI.SetUIToDisconnected();

            LocalTerminalUserDataUI.ClearData();

            ConnectWithLocalTerminalUserDataUI.Dispose();
            ConnectWithLocalTerminalUserDataUI = null;

            DisplayCompanyGroupsUI.Dispose();
            DisplayCompanyGroupsUI = null;

            ConnectToCompanyGroupUI.Dispose();
            ConnectToCompanyGroupUI = null;

            RememberAllDataUI.Dispose();
            RememberAllDataUI = null;

            ManageFullConnectionUI.Dispose();
            ManageFullConnectionUI = null;
        }
        internal void SetStatefulStartUI()
        {
            UIState = UIStates.StatefulStart;
            Sage50ConnectionManager.ConnectionActions.Disconnect();
            ConnectionStateUI.SetUIToDisconnected();

            LocalTerminalUserDataUI.KeepData();

            ConnectWithLocalTerminalUserDataUI.Dispose();
            ConnectWithLocalTerminalUserDataUI = null;

            DisplayCompanyGroupsUI.Dispose();
            DisplayCompanyGroupsUI = null;

            ConnectToCompanyGroupUI.Dispose();
            ConnectToCompanyGroupUI = null;

            RememberAllDataUI.Dispose();
            RememberAllDataUI = null;

            ManageFullConnectionUI.Dispose();
            ManageFullConnectionUI = null;
        }
    }
}
