using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;
using System.Windows.Forms;

namespace SincronizadorGPS50
{
    internal class UIHolder
    {
        //internal static System.Windows.Forms.Form MainWindow { get; set; } = null;
        //internal static UltraTabControl MainTabControl { get; set; } = null;
        ////internal static TableLayoutPanel MainTabControlTableLayoutPanel { get; set; } = null;
        //internal static UltraPanel MainTabControlMainPanel { get; set; } = null;
        internal static TableLayoutPanel Sage50ConnectionTableLayoutPanel { get; set; } = null;


        internal static UltraTab Sage50ConnectionTab { get; set; } = null;
        internal static UltraLabel Sage50ConnectionDataPanelTitleLabel { get; set; } = null;
        internal static UltraLabel Sage50ConnectionTerminalPathLabel { get; set; } = null;
        internal static UltraTextEditor Sage50ConnectionTerminalPathUltraTextEditor { get; set; } = null;
        internal static UltraLabel Sage50ConnectionUserNameLabel { get; set; } = null;
        internal static UltraTextEditor Sage50ConnectionUserNameUltraTextEditor { get; set; } = null;
        internal static UltraLabel Sage50ConnectionPasswordLabel { get; set; } = null;
        internal static UltraTextEditor Sage50ConnectionPasswordUltraTextEditor { get; set; } = null;
        internal static UltraLabel Sage50ConnectionCompanyNumberLabel { get; set; } = null;
        internal static UltraTextEditor Sage50ConnectionCompanyNumberUltraTextEditor { get; set; } = null;
        internal static UltraButton Sage50ConnectionConnectButton { get; set; } = null;
        internal static UltraButton Sage50ConnectionDisconnectButton { get; set; } = null;
        internal static UltraPictureBox Sage50ConnectionLoadingSpinnerPictureBox { get; set; } = null;
        internal static UltraComboEditor Sage50CompanyGroupDropDownMenu { get; set; } = null;


        internal static TableLayoutPanel Sage50ConnectionCenterRowCenterPanelTableLayoutPanel
        { get; set; } = null;












        internal static UltraTab GlobalActionsTab { get; set; } = null;
        internal static UltraTab ConfigurationTab { get; set; } = null;
        internal static UltraTab ClientsTab { get; set; } = null;
        internal static UltraTab ProvidersTab { get; set; } = null;
        internal static UltraTab TaxesTab { get; set; } = null;
        internal static UltraTab IssuedBillsTab { get; set; } = null;
        internal static UltraTab ReceivedBillsTab { get; set; } = null;









        internal static UltraPanel Sage50ConnectionTopRow { get; set; } = null;
        internal static UltraPanel Sage50ConnectionCenterRow { get; set; } = null;
        internal static UltraPanel Sage50ConnectionBottomRow { get; set; } = null;
        internal static UltraPanel Sage50ConnectionCenterRowLeftPanel { get; set; } = null;
        internal static UltraPanel Sage50ConnectionCenterRowCenterPanel { get; set; } = null;
        internal static TableLayoutPanel Sage50ConnectionCenterRowTableLayoutPanel { get; set; } = null;
        internal static UltraPanel Sage50ConnectionCenterRowRightPanel { get; set; } = null;


        internal static UltraLabel CenterRowCenterPanelStateLabel { get; set; } = null;
        internal static UltraLabel CenterRowCenterPanelStateStateMessageLabel { get; set; } = null;
        internal static UltraPictureBox CenterRowCenterPanelStateIcon1 { get; set; } = null;
        internal static UltraPictureBox CenterRowCenterPanelStateIcon2 { get; set; } = null;
        internal static UltraPictureBox CenterRowCenterPanelStateIcon3 { get; set; } = null;
        internal static UltraLabel CenterRowCenterPanelTitleLabel { get; set; } = null;
        internal static UltraLabel CenterRowCenterPanelLocalInstanceLabel { get; set; } = null;
        internal static UltraTextEditor CenterRowCenterPanelLocalInstanceTextBox { get; set; } = null;
        internal static UltraLabel CenterRowCenterPanelUsernameLabel { get; set; } = null;
        internal static UltraTextEditor CenterRowCenterPanelUsernameTextBox { get; set; } = null;
        internal static UltraLabel CenterRowCenterPanelPasswordLabel { get; set; } = null;
        internal static UltraTextEditor CenterRowCenterPanelPasswordTextBox { get; set; } = null;
        internal static UltraButton CenterRowCenterPanelValidateUserDataButton { get; set; } = null;
        internal static UltraLabel CenterRowCenterPanelSesionDataValidationLabel { get; set; } = null;


        internal static UltraPanel CenterRowCenterPanelRememberDataPanel { get; set; } = null;
        internal static UltraLabel CenterRowCenterPanelRememberDataLabel { get; set; } = null;
        internal static UltraCheckEditor CenterRowCenterPanelRememberDataCheckBox { get; set; } = null;
        


        internal static UltraButton CenterRowCenterPanelGetEnterpryseGroupButton { get; set; } = null;
        internal static UltraLabel CenterRowCenterPanelEnterpryseGroupLabel { get; set; } = null;
        internal static UltraComboEditor CenterRowCenterPanelEnterpryseGroupMenu { get; set; } = null;
        internal static UltraButton CenterRowCenterPanelConnectButton { get; set; } = null;
        internal static UltraButton CenterRowCenterPanelDisconnectButton { get; set; } = null;
        internal static UltraPictureBox CenterRowCenterPanelConnectingSpinner { get; set; } = null;

        internal static UltraButton CenterRowCenterPanelChangeEnterpryseGroupButton { get; set; } = null;
    }
}