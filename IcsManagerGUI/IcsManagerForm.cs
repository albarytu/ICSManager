using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using NETCONLib;
using IcsManagerLibrary;
using System.Text;

namespace IcsManagerGUI
{
    public partial class IcsManagerForm : Form
    {

        public IcsManagerForm()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }

        private void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            bool top = TopMost;
            TopMost = true;
            TopMost = top;
            RefreshStatus();
        }

        private void FormSharingManager_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshStatus();
            }
            catch (UnauthorizedAccessException)
            {
                stopOnClose.Checked = false;
                MessageBox.Show("Please restart this program with administrative priviliges.");
                Close();
            }
            catch (NotImplementedException)
            {
                stopOnClose.Checked = false;
                MessageBox.Show("This program is not supported on your operating system.");
                Close();
            }
        }

        private void RefreshStatus()
        {
            bool SharingEnabled = false;
            cbSharedConnection.Items.Clear();
            cbHomeConnection.Items.Clear();
            foreach (NetworkInterface nic in IcsManager.GetAllIPv4Interfaces())
            {
                ConnectionItem connItem = new ConnectionItem(nic);
                cbSharedConnection.Items.Add(connItem);
                cbHomeConnection.Items.Add(connItem);
                INetConnection netShareConnection = connItem.Connection;
                if (netShareConnection != null)
                {
                    INetSharingConfiguration config = IcsManager.GetConfiguration(netShareConnection);
                    if (config.SharingEnabled)
                    {
                        SharingEnabled = true;
                        switch (config.SharingConnectionType)
                        {
                            case tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PUBLIC:
                                cbSharedConnection.SelectedIndex = cbSharedConnection.Items.Count - 1;
                                break;
                            case tagSHARINGCONNECTIONTYPE.ICSSHARINGTYPE_PRIVATE:
                                cbHomeConnection.SelectedIndex = cbSharedConnection.Items.Count - 1;
                                break;
                        }
                    }
                }
            }
            this.ButtonApply.Enabled = !SharingEnabled;
            this.buttonStopSharing.Enabled = SharingEnabled;
            this.cbSharedConnection.Enabled = !SharingEnabled;
            this.cbHomeConnection.Enabled = !SharingEnabled;
            cbHomeConnection_SelectedIndexChanged(null, null);
            cbSharedConnection_SelectedIndexChanged(null, null);
        }

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            ConnectionItem sharedConnectionItem = cbSharedConnection.SelectedItem as ConnectionItem;
            ConnectionItem homeConnectionItem = cbHomeConnection.SelectedItem as ConnectionItem;
            if ((sharedConnectionItem == null) || (homeConnectionItem == null))
            {
                MessageBox.Show(@"Please select both connections.");
                return;
            }
            if (sharedConnectionItem.Connection == homeConnectionItem.Connection)
            {
                MessageBox.Show(@"Please select different connections.");
                return;
            }
            IcsManager.ShareConnection(sharedConnectionItem.Connection, homeConnectionItem.Connection);
            RefreshStatus();
        }

        private string getInterfaceData(NetworkInterface nic)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("Description: {0}", nic.Description));
            sb.AppendLine(string.Format("Status: {0}", nic.OperationalStatus));

            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                IPInterfaceProperties ipconfig = nic.GetIPProperties();
                foreach (UnicastIPAddressInformation a in ipconfig.UnicastAddresses)
                {
                    if (a.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        sb.AppendLine(string.Format("Address: {0}/{1}", a.Address, a.IPv4Mask));
                    }
                }
                foreach (GatewayIPAddressInformation a in ipconfig.GatewayAddresses)
                {
                    if (a.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        sb.AppendLine(string.Format("Gateway: {0}", a.Address));
                    }
                }
            }
            return sb.ToString();
        }

        private void cbHomeConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConnectionItem homeConnectionItem = cbHomeConnection.SelectedItem as ConnectionItem;
            if (homeConnectionItem != null)
            {
                homeDetails.Text = getInterfaceData(homeConnectionItem.Nic);
            } else
            {
                homeDetails.Text = "";
            }
        }

        private void buttonStopSharing_Click(object sender, EventArgs e)
        {
            IcsManager.ShareConnection(null, null);
            RefreshStatus();
        }

        private void IcsManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (stopOnClose.Checked)
            {
                IcsManager.ShareConnection(null, null);
            }
        }

        private void cbSharedConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConnectionItem sharedConnectionItem = cbSharedConnection.SelectedItem as ConnectionItem;
            if (sharedConnectionItem != null)
            {
                sharedDetails.Text = getInterfaceData(sharedConnectionItem.Nic);
            } else
            {
                sharedDetails.Text = "";
            }
        }
    }
}
