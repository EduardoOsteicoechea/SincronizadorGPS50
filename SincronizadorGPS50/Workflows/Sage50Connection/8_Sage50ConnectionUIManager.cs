
using GestprojectDataManager;
using System;
using System.Collections.Generic;
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
            SelectCompanyGroupUI.DisableControls();

            ValidateCompanyGroupUI = new ValidateCompanyGroupPanel(this, ParentControl, 0, 6);
            ValidateCompanyGroupUI.DisableControls();

            ConnectUI = new ConnectPanel(this, ParentControl, 0, 8);
            ConnectUI.DisableControls();

            RememberAllDataUI = new RememberAllDataPanel(this, ParentControl, 0, 9);
            RememberAllDataUI.DisableControls();

            ManageConnectionUI = new ManageConnectionPanel(this, ParentControl, 0, 11);
            ManageConnectionUI.SetUIToConnected();
            ManageConnectionUI.DisableControls();
        }

        internal void RestoreRememberedDataUI()
        {
            ClearUI();
            CreateStatefulUI();
        }
        internal void SetEditingTerminalDataUI()
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

            if(ConnectUI != null)
            {
                ConnectUI.Dispose();
                ConnectUI = null;
            };

            if(RememberAllDataUI != null)
            {
                RememberAllDataUI.Dispose();
                RememberAllDataUI = null;
            };

            if(ManageConnectionUI != null)
            {
                ManageConnectionUI.Dispose();
                ManageConnectionUI = null;
            };
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

            ValidateTerminalUserDataUI.DisableControls();

            SelectCompanyGroupUI = new SelectCompanyGroupPanel(this, ParentControl, 0, 5);
            SelectCompanyGroupUI.GetCompanyGroupsFromTerminal();

            ValidateCompanyGroupUI = new ValidateCompanyGroupPanel(this, ParentControl, 0, 6);

            if(ConnectUI != null)
            {
                ConnectUI.Dispose();
                ConnectUI = null;
            };

            if(RememberAllDataUI != null)
            {
                RememberAllDataUI.Dispose();
                RememberAllDataUI = null;
            };

            if(ManageConnectionUI != null)
            {
                ManageConnectionUI.Dispose();
                ManageConnectionUI = null;
            };
        }
        internal void SetValidatedCompanyGroupAwaitingConnectionUI()
        {
            if(ConnectUI != null)
            {
                ConnectUI.Dispose();
                ConnectUI = null;
            };

            if(RememberAllDataUI != null)
            {
                RememberAllDataUI.Dispose();
                RememberAllDataUI = null;
            };

            if(ManageConnectionUI != null)
            {
                ManageConnectionUI.Dispose();
                ManageConnectionUI = null;
            };

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

            ValidateTerminalUserDataUI.DisableControls();

            ManageConnectionUI.Dispose();
            ManageConnectionUI = null;
        }
        internal void SetStatelessStartUI()
        {
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

            Sage50ConnectionManager.ConnectionActions.Disconnect();
            CreateStatelessUI();
        }
        internal void SetStatefulStartUI()
        {
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

            Sage50ConnectionManager.ConnectionActions.Disconnect();
            CreateStatefulUI();
        }
    }
}
