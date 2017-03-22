using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TLCLogin.View.Admin
{
    public partial class frmAdminAddRemoveAdmins : Form
    {
        public frmMdiContainer PseudoParent { get; set; }
        public List<string> Admins { get; set; }


        public frmAdminAddRemoveAdmins()
        {
            InitializeComponent();
        }

        private void frmAdminAddRemoveAdmins_Load(object sender, EventArgs e)
        {
            try
            {
                chkEnableSimple.Checked = Data.TLCLoginDA.SimpleAdminLoginIsEnabled();
                chkEnableSimple_CheckedChanged(null, null);

                string[] unpw = Data.TLCLoginDA.GetSimpleAdminLogin();
                txtSimpleUsername.Text = unpw[0];
                txtSimplePass.Text = unpw[1];
            }
            catch (Exception ex)
            {
                PseudoParent.ShowExceptionMessage(ex);
            }

            ReloadListBox();
            btnAddNew.PerformClick();
        }

        private void btnAddRemove_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != "")
            {
                if (Convert.ToBoolean(btnAddRemove.Tag))
                {
                    // is add
                    try
                    {
                        Data.TLCLoginDA.AddAdmin(txtUsername.Text);
                    }
                    catch (Exception ex)
                    {
                        PseudoParent.ShowExceptionMessage(ex);
                    }
                }
                else
                {
                    // is delete
                    try
                    {
                        Data.TLCLoginDA.RemoveAdmin(txtUsername.Text);
                    }
                    catch (Exception ex)
                    {
                        PseudoParent.ShowExceptionMessage(ex);
                    }
                }

                ReloadListBox();
                btnAddNew.PerformClick();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            ResetButton();
            ClearForm();
            txtUsername.Focus();
        }

        private void lstExisting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstExisting.SelectedIndex > -1)
            {
                // true: add, false: remove
                btnAddRemove.Tag = false;
                btnAddRemove.Text = "Remove";
                txtUsername.Text = lstExisting.SelectedItem.ToString();
            }
        }

        private void ClearForm()
        {
            txtUsername.Clear();
            ResetButton();
        }

        private void ReloadListBox()
        {
            try
            {
                lstExisting.DataSource = null;
                this.Admins = Data.TLCLoginDA.GetAllAdminUsernames();
                lstExisting.DataSource = Admins;
            }
            catch (Exception e)
            {
                PseudoParent.ShowExceptionMessage(e);
            }
        }

        private void ResetButton()
        {
            btnAddRemove.Text = "Add";
            btnAddRemove.Tag = true;
        }



        private void chkEnableSimple_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = chkEnableSimple.Checked;

            txtSimpleUsername.Enabled = enabled;
            txtSimplePass.Enabled = enabled;
            btnSaveSimple.Enabled = enabled;

            try
            {
                Data.TLCLoginDA.UpdateSimpleAdminLogin(enabled);
            }
            catch(Exception ex)
            {
                PseudoParent.ShowExceptionMessage(ex);
            }
        }

        private void btnSaveSimple_Click(object sender, EventArgs e)
        {
            if (txtSimpleUsername.Text.Length > 0 && txtSimplePass.Text.Length > 0)
            {
                try
                {
                    Data.TLCLoginDA.UpdateSimpleAdminLogin(chkEnableSimple.Checked, 
                                            txtSimpleUsername.Text, txtSimplePass.Text);
                }
                catch (Exception ex)
                {
                    PseudoParent.ShowExceptionMessage(ex);
                }
            }
            else
            {
                PseudoParent.ShowMessage("Both a username and a password are required.");
                txtSimpleUsername.Select();
                txtSimpleUsername.Focus();
            }
        }
    }
}
