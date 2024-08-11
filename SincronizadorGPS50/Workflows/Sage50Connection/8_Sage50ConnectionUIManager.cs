
using System;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Sage50Connection
{
    internal class Sage50ConnectionUIManager
    {
        internal System.Windows.Forms.TableLayoutControlCollection ParentControl { get; set; } = null;
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
            StateManager.State = UIStates.StatelessStart;
            ShowConnectionStateUI = new ShowConnectionStatePanel(this, ParentControl, 0, 0);
            GetLocalTerminalUserDataUI = new GetLocalTerminalUserDataPanel(this, ParentControl, 0, 2);
            ValidateTerminalUserDataUI = new ValidateTerminalUserDataPanel(this, ParentControl, 0, 3);
            ValidateTerminalUserDataUI.SetUIToAwaitingForData();
        }
        internal void CreateStatefulUI()
        {
            StateManager.State = UIStates.StatefulAwaitForFullDataRevision;

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
            ManageConnectionUI.SetUIToConnected();
        }

        internal void RestoreRememberedDataUI()
        {
            ClearUI();
            CreateStatefulUI();
        }
        internal void SetValidatedTerminalSelectCompanyGroupUI()
        {
            if(SelectCompanyGroupUI != null)
            {
                SelectCompanyGroupUI.Dispose();
                SelectCompanyGroupUI = null;
            };

            if(ValidateCompanyGroupUI != null)
            {
                ValidateCompanyGroupUI.Dispose();
                ValidateCompanyGroupUI = null;
            };

            SelectCompanyGroupUI = new SelectCompanyGroupPanel(this, ParentControl, 0, 5);
            SelectCompanyGroupUI.GetCompanyGroupsFromTerminal();

            ValidateCompanyGroupUI = new ValidateCompanyGroupPanel(this, ParentControl, 0, 6);

            ConnectUI.Dispose();
            ConnectUI = null;

            RememberAllDataUI.Dispose();
            RememberAllDataUI = null;

            ManageConnectionUI.Dispose();
            ManageConnectionUI = null;
        }
        internal void SetValidatedCompanyGroupAwaitingConnectionUI()
        {
            ConnectUI = new ConnectPanel(this, ParentControl, 0, 8);

            RememberAllDataUI = new RememberAllDataPanel(this, ParentControl, 0, 9);
        }
        internal void SetDataAcceptedAndConnetedUI()
        {
            ManageConnectionUI = new ManageConnectionPanel(this, ParentControl, 0, 11);
            SetConnetedUI();
        }
        internal void ClearUI()
        {
            StateManager.State = UIStates.StatelessStart;
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
            StateManager.State = UIStates.Connected;
            ShowConnectionStateUI.SetUIToConnected();

            GetLocalTerminalUserDataUI.SetUIToConnected();

            ValidateTerminalUserDataUI.SetUIToConnected();

            SelectCompanyGroupUI.SetUIToConnected();

            ValidateCompanyGroupUI.SetUIToConnected();

            ConnectUI.SetUIToConnected();

            RememberAllDataUI.SetUIToConnected();

            ManageConnectionUI.SetUIToConnected();
        }
        internal void SetStatefulAwaitForFullDataRevisionUI()
        {
            StateManager.State = UIStates.StatefulAwaitForFullDataRevision;
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
            StateManager.State = UIStates.StatelessStart;
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
            StateManager.State = UIStates.StatefulStart;
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
