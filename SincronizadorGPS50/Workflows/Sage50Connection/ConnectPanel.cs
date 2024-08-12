using GestprojectDataManager;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using sage.ew.db;
using Sage50ConnectionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SincronizadorGPS50.Workflows.Sage50Connection
{
    internal class ConnectPanel : ISage50ConnectionUIStateTracker, ISage50ConnectionUIComponent
    {
        public bool IsConnected { get; set; } = false;
        public UltraPanel Panel { get; set; } = null;
        public TableLayoutPanel PanelTableLayoutPanel { get; set; } = null;
        public UltraButton ConnectButton { get; set; } = null;
        public System.Windows.Forms.ImageList ImageList { get; set; } = new ImageList();
        public Sage50ConnectionUIManager Sage50ConnectionUIManager { get; set; } = null;

        public bool IsDataCleared => throw new NotImplementedException();

        public event EventHandler ConnectionStateChanged;
        public event EventHandler DataCleared;

        public ConnectPanel
        (
            Sage50ConnectionUIManager sage50ConnectionUIManager,
            System.Windows.Forms.TableLayoutControlCollection parentControl,
            int parentControlColumn,
            int parentControlRow
        )
        {
            Sage50ConnectionUIManager = sage50ConnectionUIManager;

            Panel = new UltraPanel();
            Panel.Dock = DockStyle.Fill;

            PanelTableLayoutPanel = new TableLayoutPanel();
            PanelTableLayoutPanel.ColumnCount = 1;
            PanelTableLayoutPanel.RowCount = 1;
            PanelTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            PanelTableLayoutPanel.Dock = DockStyle.Fill;

            ImageList.Images.Add(Resources.SemaforoRojo);
            ImageList.Images.Add(Resources.Semaforo_verde);


            ConnectButton = new UltraButton();
            ConnectButton.Dock = DockStyle.Fill;
            ConnectButton.AutoSize = true;
            ConnectButton.ImageList = ImageList;
            ConnectButton.Appearance.Image = 0;
            ConnectButton.Text = "Conectar";

            PanelTableLayoutPanel.Controls.Add(ConnectButton, 0, 0);

            Panel.ClientArea.Controls.Add(PanelTableLayoutPanel);

            parentControl.Add(Panel, parentControlColumn, parentControlRow);

            // Handle Events
            // Handle Events
            // Handle Events
            // Handle Events
            // Handle Events

            ConnectButton.Click += ConnectButton_Click;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DB.SQLDatabase("COMUNES"));

            ConnectButton.Appearance.Image = 1;
            Sage50ConnectionUIManager.ShowConnectionStateUI.StateImage3.Image = Sage50ConnectionUIManager.ShowConnectionStateUI.ImageList.Images[1];
            Sage50ConnectionUIManager.SetDataAcceptedAndConnetedUI();

            if(Sage50ConnectionUIManager.RememberAllDataUI.CheckBox.Checked) 
            {
                List<Sage50ConnectionManager.CompanyGroup> sage50CompanyGroupsList = Sage50ConnectionManager.Sage50CompanyGroupActions.GetCompanyGroups();

                ManageUserData.Save(
                    GestprojectDataHolder.GestprojectDatabaseConnection,
                    Sage50ConnectionUIManager.GetLocalTerminalUserDataUI.LocalInstanceTextBox.Text,
                    Sage50ConnectionUIManager.GetLocalTerminalUserDataUI.UsernameTextBox.Text,
                    Sage50ConnectionUIManager.GetLocalTerminalUserDataUI.PasswordTextBox.Text,
                    Sage50ConnectionUIManager.SelectCompanyGroupUI.SelectEnterpryseGroupMenu.Text,
                    sage50CompanyGroupsList.Select(companyGroup => companyGroup.CompanyName).ToList(),
                    sage50CompanyGroupsList.Select(companyGroup => companyGroup.CompanyMainCode).ToList(),
                    sage50CompanyGroupsList.Select(companyGroup => companyGroup.CompanyCode).ToList(),
                    sage50CompanyGroupsList.Select(companyGroup => companyGroup.CompanyGuidId).ToList()
                );
                
                Sage50ConnectionUIManager.SelectCompanyGroupUI.SelectEnterpryseGroupMenu.Appearance.BackColor = StyleHolder.c_gray_200;
                Sage50ConnectionUIManager.SelectCompanyGroupUI.SelectEnterpryseGroupMenu.Appearance.ForeColor = StyleHolder.c_gray_100;
            }
            else
            {
                ManageUserData.DisableRememberInGestprojectUserDataTable(GestprojectDataHolder.GestprojectDatabaseConnection);
            };
        }

        public void EnableControls()
        {
            ConnectButton.Enabled = true;
        }
        public void DisableControls()
        {
            ConnectButton.Enabled = false;
        }
        public void SetUIToConnected()
        {
            DisableControls();
        }
        public void SetUIToDisconnected()
        {
            EnableControls();
        }

        public void Forget() => throw new NotImplementedException();
        public void Remember() {}

        public void Dispose()
        {
            Panel.Dispose();
            GC.SuppressFinalize(Panel);
        }
    }
}