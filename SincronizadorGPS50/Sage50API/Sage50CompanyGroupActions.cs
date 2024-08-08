
namespace SincronizadorGPS50.Sage50API
{
    internal class Sage50CompanyGroupActions
    {
        internal void ChangeCompanyGroup(object sender, System.EventArgs e) 
        {
            if(UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedText != "") 
            {
                if(
                    Sage50ConnectionManager.Sage50CompanyGroupActions.ChangeCompanyGroup(
                        UIHolder.CenterRowCenterPanelEnterpryseGroupMenu.SelectedText
                    )
                )
                {
                    UIHolder.Sage50ConnectionCenterRowCenterPanelTableLayoutPanel.Controls.Add(UIHolder.CenterRowCenterPanelConnectButton, 0, 16);

                    UIHolder.CenterRowCenterPanelConnectButton.Focus();
                }
                else
                { 
                
                };
            };
        }
    }
}
