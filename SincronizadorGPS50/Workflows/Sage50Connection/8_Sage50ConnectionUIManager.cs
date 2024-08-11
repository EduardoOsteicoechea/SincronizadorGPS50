
using System;
using System.Windows.Forms;

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
        internal ShowConnectionStatePanel ShowConnectionStateUI { get; set; } = null;
        internal GetLocalTerminalUserDataPanel GetLocalTerminalUserDataUI { get; set; } = null;
        internal ValidateTerminalUserDataPanel ValidateTerminalUserDataUI { get; set; } = null;
        internal SelectCompanyGroupPanel SelectCompanyGroupUI { get; set; } = null;
        internal ValidateCompanyGroupPanel ValidateCompanyGroupUI { get; set; } = null;
        internal ConnectPanel ConnectUI { get; set; } = null;
        internal RememberAllDataPanel RememberAllDataUI { get; set; } = null;
        internal ManageConnectionPanel ManageConnectionUI { get; set; } = null;

        internal Sage50ConnectionUIManager(System.Windows.Forms.TableLayoutControlCollection parentControl, string uiModel) 
        {
            ParentControl = parentControl;
            if(uiModel == "stateless")
                CreateStatelessUI(); 
            else
                CreateStatefulUI();            
        }
        internal void CreateStatelessUI()
        {
            UIState = UIStates.StatelessStart;
            ShowConnectionStateUI = new ShowConnectionStatePanel(this, ParentControl, 0, 0);
            GetLocalTerminalUserDataUI = new GetLocalTerminalUserDataPanel(this, ParentControl, 0, 1);
        }
        internal void CreateStatefulUI()
        {
            UIState = UIStates.StatefulAwaitForFullDataRevision;

            ShowConnectionStateUI = new ShowConnectionStatePanel(this, ParentControl, 0, 0);

            GetLocalTerminalUserDataUI = new GetLocalTerminalUserDataPanel(this, ParentControl, 0, 2);
            GetLocalTerminalUserDataUI.Remember();
            
            ValidateTerminalUserDataUI = new ValidateTerminalUserDataPanel(this, ParentControl, 0, 3);
            
            SelectCompanyGroupUI = new SelectCompanyGroupPanel(this, ParentControl, 0, 5);
            SelectCompanyGroupUI.Remember();

            ValidateCompanyGroupUI = new ValidateCompanyGroupPanel(this, ParentControl, 0, 6);

            ConnectUI = new ConnectPanel(this, ParentControl, 0, 8);

            RememberAllDataUI = new RememberAllDataPanel(this, ParentControl, 0, 9);
            RememberAllDataUI.Remember();

            ManageConnectionUI = new ManageConnectionPanel(this, ParentControl, 0, 11);
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

            ShowConnectionStateUI.Dispose();
            ShowConnectionStateUI = null;

            GetLocalTerminalUserDataUI.Dispose();
            GetLocalTerminalUserDataUI = null;

            ValidateTerminalUserDataUI.Dispose();
            ValidateTerminalUserDataUI = null;

            SelectCompanyGroupUI.Dispose();
            SelectCompanyGroupUI = null;

            ValidateCompanyGroupUI.Dispose();
            ValidateCompanyGroupUI = null;

            ConnectUI.Dispose();
            ConnectUI = null;

            RememberAllDataUI.Dispose();
            RememberAllDataUI = null;

            ManageConnectionUI.Dispose();
            ManageConnectionUI = null;
        }
        internal void SetConnetedUI()
        {
            UIState = UIStates.Connected;
            ShowConnectionStateUI.SetUIToConnected();

            GetLocalTerminalUserDataUI.KeepData();
            GetLocalTerminalUserDataUI.KeepData();

            ValidateTerminalUserDataUI.DisableControls();

            SelectCompanyGroupUI.KeepData();
            SelectCompanyGroupUI.DisableControls();

            ValidateCompanyGroupUI.DisableControls();

            ConnectUI.DisableControls();

            RememberAllDataUI.KeepData();

            ManageConnectionUI.EnableControls();
        }
        internal void SetStatefulAwaitForFullDataRevisionUI()
        {
            UIState = UIStates.StatefulAwaitForFullDataRevision;
            Sage50ConnectionManager.ConnectionActions.Disconnect();
            ShowConnectionStateUI.SetUIToDisconnected();

            GetLocalTerminalUserDataUI.KeepData();
            ValidateTerminalUserDataUI.DisableControls();
            SelectCompanyGroupUI.KeepData();
            ValidateCompanyGroupUI.KeepData();
            ConnectUI.KeepData();

            ManageConnectionUI.Dispose();
            ManageConnectionUI = null;
        }
        internal void SetStatelessStartUI()
        {
            UIState = UIStates.StatelessStart;
            Sage50ConnectionManager.ConnectionActions.Disconnect();
            ShowConnectionStateUI.SetUIToDisconnected();

            GetLocalTerminalUserDataUI.ClearData();

            ValidateTerminalUserDataUI.Dispose();
            ValidateTerminalUserDataUI = null;

            SelectCompanyGroupUI.Dispose();
            SelectCompanyGroupUI = null;

            ValidateCompanyGroupUI.Dispose();
            ValidateCompanyGroupUI = null;

            ConnectUI.Dispose();
            ConnectUI = null;

            RememberAllDataUI.Dispose();
            RememberAllDataUI = null;

            ManageConnectionUI.Dispose();
            ManageConnectionUI = null;
        }
        internal void SetStatefulStartUI()
        {
            UIState = UIStates.StatefulStart;
            Sage50ConnectionManager.ConnectionActions.Disconnect();
            ShowConnectionStateUI.SetUIToDisconnected();

            GetLocalTerminalUserDataUI.KeepData();

            ValidateTerminalUserDataUI.Dispose();
            ValidateTerminalUserDataUI = null;

            SelectCompanyGroupUI.Dispose();
            SelectCompanyGroupUI = null;

            ValidateCompanyGroupUI.Dispose();
            ValidateCompanyGroupUI = null;

            ConnectUI.Dispose();
            ConnectUI = null;

            RememberAllDataUI.Dispose();
            RememberAllDataUI = null;

            ManageConnectionUI.Dispose();
            ManageConnectionUI = null;
        }
    }
}
