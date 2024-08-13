

using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Sage50Connection {
   internal class Sage50ConnectionUIManager {
      internal bool IsSuccessful { get; set; } = false;
      internal System.Windows.Forms.TableLayoutControlCollection ParentControl { get; set; } = null;
      internal ShowConnectionStatePanel ShowConnectionStateUI { get; set; } = null;
      internal GetLocalTerminalUserDataPanel GetLocalTerminalUserDataUI { get; set; } = null;
      internal ValidateTerminalUserDataPanel ValidateTerminalUserDataUI { get; set; } = null;
      internal SelectCompanyGroupPanel SelectCompanyGroupUI { get; set; } = null;
      internal ConnectPanel ConnectUI { get; set; } = null;
      internal RememberAllDataPanel RememberAllDataUI { get; set; } = null;
      internal ManageConnectionPanel ManageConnectionUI { get; set; } = null;


      internal Sage50ConnectionUIManager(System.Windows.Forms.TableLayoutControlCollection parentControl, string uiModel) {
         ParentControl = parentControl;
         if(uiModel == "stateless")
            CreateStatelessUI();
         else
            CreateStatefulUI();
      }
      internal void CreateStatelessUI() {
         RemoveAllUIElements();

         StateManager.State = UIStates.StatelessStart;
         ShowConnectionStateUI = new ShowConnectionStatePanel(this, ParentControl, 0, 0);
         GetLocalTerminalUserDataUI = new GetLocalTerminalUserDataPanel(this, ParentControl, 0, 2);
         ValidateTerminalUserDataUI = new ValidateTerminalUserDataPanel(this, ParentControl, 0, 3);
         ValidateTerminalUserDataUI.SetUIToAwaitingForData(); 
         IsSuccessful = true;
      }
      internal void CreateStatefulUI() {
         RemoveAllUIElements(); 

         StateManager.State = UIStates.StatefulAwaitForFullDataRevision;

         ShowConnectionStateUI = new ShowConnectionStatePanel(this, ParentControl, 0, 0);

         GetLocalTerminalUserDataUI = new GetLocalTerminalUserDataPanel(this, ParentControl, 0, 2);
         GetLocalTerminalUserDataUI.Remember();

         ValidateTerminalUserDataUI = new ValidateTerminalUserDataPanel(this, ParentControl, 0, 3);

         SelectCompanyGroupUI = new SelectCompanyGroupPanel(this, ParentControl, 0, 5);
         SelectCompanyGroupUI.Remember();
         SelectCompanyGroupUI.DisableControls();

         RememberAllDataUI = new RememberAllDataPanel(this, ParentControl, 0, 6);

         ConnectUI = new ConnectPanel(this, ParentControl, 0, 7);

         ManageConnectionUI = new ManageConnectionPanel(this, ParentControl, 0, 9);
         ManageConnectionUI.SetUIToConnected();

         IsSuccessful = true;
      }

      internal void RestoreRememberedDataUI() {
         ClearUI();
         CreateStatefulUI();
      }
      internal void SetEditingTerminalDataUI() {

         if(SelectCompanyGroupUI != null) {
            SelectCompanyGroupUI.Dispose();
            SelectCompanyGroupUI = null;
         };

         if(RememberAllDataUI != null) {
            RememberAllDataUI.Dispose();
            RememberAllDataUI = null;
         };

         if(ConnectUI != null) {
            ConnectUI.Dispose();
            ConnectUI = null;
         };

         if(ManageConnectionUI != null) {
            ManageConnectionUI.Dispose();
            ManageConnectionUI = null;
         };
      }
      internal void SetValidatedTerminalSelectCompanyGroupUI() {
         if(SelectCompanyGroupUI != null) {
            SelectCompanyGroupUI.Dispose();
            SelectCompanyGroupUI = null;
         };

         if(ConnectUI != null) {
            ConnectUI.Dispose();
            ConnectUI = null;
         };

         if(RememberAllDataUI != null) {
            RememberAllDataUI.Dispose();
            RememberAllDataUI = null;
         };

         ValidateTerminalUserDataUI.DisableControls();

         SelectCompanyGroupUI = new SelectCompanyGroupPanel(this, ParentControl, 0, 5);
         SelectCompanyGroupUI.GetCompanyGroupsFromTerminal();

         RememberAllDataUI = new RememberAllDataPanel(this, ParentControl, 0, 6);

         ConnectUI = new ConnectPanel(this, ParentControl, 0, 7);

         if(ManageConnectionUI != null) {
            ManageConnectionUI.Dispose();
            ManageConnectionUI = null;
         };
      }
      internal void SetDataAcceptedAndConnetedUI() {
         ManageConnectionUI = new ManageConnectionPanel(this, ParentControl, 0, 9);
         SetConnetedUI();
      }
      internal void ClearUI() {
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

         RememberAllDataUI.Dispose();
         RememberAllDataUI = null;

         ConnectUI.Dispose();
         ConnectUI = null;

         ManageConnectionUI.Dispose();
         ManageConnectionUI = null;
      }
      internal void SetConnetedUI() {
         StateManager.State = UIStates.Connected;
         ShowConnectionStateUI.SetUIToConnected();

         GetLocalTerminalUserDataUI.SetUIToConnected();

         ValidateTerminalUserDataUI.SetUIToConnected();

         SelectCompanyGroupUI.SetUIToConnected();

         RememberAllDataUI.SetUIToConnected();

         ConnectUI.SetUIToConnected();

         ManageConnectionUI.SetUIToConnected();
      }
      internal void SetStatefulAwaitForFullDataRevisionUI() {
         StateManager.State = UIStates.StatefulAwaitForFullDataRevision;
         Sage50ConnectionManager.ConnectionActions.Disconnect();
         ShowConnectionStateUI.SetUIToDisconnected();

         ValidateTerminalUserDataUI.DisableControls();

         ManageConnectionUI.Dispose();
         ManageConnectionUI = null;
      }
      internal void SetStatelessStartUI() {
         ShowConnectionStateUI.Dispose();
         ShowConnectionStateUI = null;

         GetLocalTerminalUserDataUI.Dispose();
         GetLocalTerminalUserDataUI = null;

         ValidateTerminalUserDataUI.Dispose();
         ValidateTerminalUserDataUI = null;

         SelectCompanyGroupUI.Dispose();
         SelectCompanyGroupUI = null;

         RememberAllDataUI.Dispose();
         RememberAllDataUI = null;

         ConnectUI.Dispose();
         ConnectUI = null;

         ManageConnectionUI.Dispose();
         ManageConnectionUI = null;

         Sage50ConnectionManager.ConnectionActions.Disconnect();
         CreateStatelessUI();
      }
      internal void SetStatefulStartUI() {
         ShowConnectionStateUI.Dispose();
         ShowConnectionStateUI = null;

         GetLocalTerminalUserDataUI.Dispose();
         GetLocalTerminalUserDataUI = null;

         ValidateTerminalUserDataUI.Dispose();
         ValidateTerminalUserDataUI = null;

         SelectCompanyGroupUI.Dispose();
         SelectCompanyGroupUI = null;

         RememberAllDataUI.Dispose();
         RememberAllDataUI = null;

         ConnectUI.Dispose();
         ConnectUI = null;

         ManageConnectionUI.Dispose();
         ManageConnectionUI = null;

         Sage50ConnectionManager.ConnectionActions.Disconnect();
         CreateStatefulUI();
      }
      internal void RemoveAllUIElements() {

         if(ShowConnectionStateUI != null) {
            ShowConnectionStateUI.Dispose();
            ShowConnectionStateUI = null;
         };

         if(GetLocalTerminalUserDataUI != null) {
            GetLocalTerminalUserDataUI.Dispose();
            GetLocalTerminalUserDataUI = null;
         };

         if(ValidateTerminalUserDataUI != null) {
            ValidateTerminalUserDataUI.Dispose();
            ValidateTerminalUserDataUI = null;
         };

         if(SelectCompanyGroupUI != null) {
            SelectCompanyGroupUI.Dispose();
            SelectCompanyGroupUI = null;
         };

         if(RememberAllDataUI != null) {
            RememberAllDataUI.Dispose();
            RememberAllDataUI = null;
         };

         if(ConnectUI != null) {
            ConnectUI.Dispose();
            ConnectUI = null;
         };

         if(ManageConnectionUI != null) {
            ManageConnectionUI.Dispose();
            ManageConnectionUI = null;
         };
      }
   }
}
