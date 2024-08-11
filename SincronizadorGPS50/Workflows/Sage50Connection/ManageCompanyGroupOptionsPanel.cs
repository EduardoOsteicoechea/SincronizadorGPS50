using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Sage50Connection
{
    internal class SelectCompanyGroupPanel : ISage50ConnectionUIStateTracker, ISage50ConnectionUIComponent
    {
        public bool IsConnected { get; set; } = false;
        public UltraPanel Panel { get; set; } = null;
        public TableLayoutPanel PanelTableLayoutPanel { get; set; } = null;
        public UltraLabel SelectEnterpryseGroupLabel { get; set; } = null;
        public UltraComboEditor SelectEnterpryseGroupMenu { get; set; } = null;
        public UltraButton ConnectButton { get; set; } = null;
        public System.Windows.Forms.ImageList ImageList { get; set; } = new ImageList();

        public bool IsDataCleared => throw new NotImplementedException();

        public event EventHandler ConnectionStateChanged;
        public event EventHandler DataCleared;

        public SelectCompanyGroupPanel
        (
            Sage50ConnectionUIManager sage50ConnectionUIManager,
            System.Windows.Forms.TableLayoutControlCollection parentControl,
            int parentControlColumn,
            int parentControlRow
        )
        {
            Panel = new UltraPanel();
            Panel.Dock = DockStyle.Fill;

            PanelTableLayoutPanel = new TableLayoutPanel();
            PanelTableLayoutPanel.ColumnCount = 1;
            PanelTableLayoutPanel.RowCount = 2;
            PanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            PanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            PanelTableLayoutPanel.Dock = DockStyle.Fill;

            ImageList.Images.Add(Resources.SemaforoRojo);
            ImageList.Images.Add(Resources.Semaforo_verde);

            // EnterpryseGroup
            // EnterpryseGroup
            // EnterpryseGroup
            // EnterpryseGroup
            // EnterpryseGroup

            SelectEnterpryseGroupLabel = new UltraLabel();
            SelectEnterpryseGroupLabel.Dock = DockStyle.Fill;
            SelectEnterpryseGroupLabel.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            SelectEnterpryseGroupLabel.Appearance.TextVAlign = Infragistics.Win.VAlign.Middle;
            SelectEnterpryseGroupLabel.Text = "Grupo de empresa";


            SelectEnterpryseGroupMenu = new UltraComboEditor();
            SelectEnterpryseGroupMenu.Dock = DockStyle.Fill;

            GestprojectDataManager.SynchronizerUserRememberableDataModel userRememberabledata = GestprojectDataManager.ManageUserData.GetSynchronizerUserRememberableDataForConnection(GestprojectDataHolder.GestprojectDatabaseConnection);

            if(
                Sage50ConnectionManager.ConnectionActions.Connect(
                    userRememberabledata.Sage50LocalTerminalPath,
                    userRememberabledata.Sage50Username,
                    userRememberabledata.Sage50Password
                )
            )
            {
                List<Sage50ConnectionManager.CompanyGroup> sage50CompanyGroupsList = Sage50ConnectionManager.Sage50CompanyGroupActions.GetCompanyGroups();

                if(SelectEnterpryseGroupMenu.Items.Count < 1)
                {
                    SelectEnterpryseGroupMenu.Items.Add("");
                };

                for(global::System.Int32 i = 0; i < sage50CompanyGroupsList.Count; i++)
                {
                    SelectEnterpryseGroupMenu.Items.Add(sage50CompanyGroupsList[i].CompanyName);
                    if(userRememberabledata.Sage50CompanyGroupName == sage50CompanyGroupsList[i].CompanyName)
                    {
                        SelectEnterpryseGroupMenu.SelectedIndex = i;
                    };
                };
            };



            PanelTableLayoutPanel.Controls.Add(SelectEnterpryseGroupLabel, 0, 0);
            PanelTableLayoutPanel.Controls.Add(SelectEnterpryseGroupMenu, 0, 1);

            Panel.ClientArea.Controls.Add(PanelTableLayoutPanel);

            parentControl.Add(Panel, parentControlColumn, parentControlRow);
        }

        public void EnableControls() => throw new NotImplementedException();
        public void ClearData() => throw new NotImplementedException();
        public void DisableControls() => throw new NotImplementedException();
        public void Forget() => throw new NotImplementedException();
        public void KeepData() => throw new NotImplementedException();
        public void Remember() {}
        public void SetUIToConnected() => throw new NotImplementedException();
        public void SetUIToDisconnected() => throw new NotImplementedException();
        public void Dispose() => throw new NotImplementedException();
    }
}