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
    public partial class frmAdminStudentServices : Form
    {
        // public int CurrentID { get; set; }
        public Business.SpecialProgram CurrentItem { get; set; }
        public frmMdiContainer PseudoParent { get; set; }


        public frmAdminStudentServices()
        {
            InitializeComponent();
        }

        private void frmAdminStudentServices_Load(object sender, EventArgs e)
        {
            ReloadListBox();
        }



        private void lstExisting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstExisting.SelectedIndex > -1)
            {
                //CurrentID = ((KeyValuePair<int,string>)lstExisting.SelectedItem).Key;
                //txtName.Text = ((KeyValuePair<int, string>)lstExisting.SelectedItem).Value;
                Business.SpecialProgram selected = (Business.SpecialProgram)lstExisting.SelectedItem;
                CurrentItem = selected;
                txtName.Text = CurrentItem.Name;

                //  the button is here to do the opposite of what it is now
                btnDisable.Text = (CurrentItem.IsEnabled) ? "Disable" : "Enable";
                btnDisable.Tag = ! CurrentItem.IsEnabled;  // true: enable, false: disable

                ResetButton();
            }
        }



        private void btnAddNew_Click(object sender, EventArgs e)
        {
            ClearForm();

            txtName.Focus();
            btnDisable.Enabled = false;
            btnUpdate.Text = "Add";
            btnUpdate.Tag = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(btnUpdate.Tag))   //is add
                {
                    if (txtName.Text != "")
                    {
                        Data.TLCLoginDA.AddStudentService(txtName.Text);

                        ResetButton();
                        ReloadListBox();
                    }
                }
                else                                     //is update
                {
                    if (txtName.Text != "")
                    {
                        Data.TLCLoginDA.UpdateStudentService(CurrentItem.ID, txtName.Text);
                        ReloadListBox();
                    }
                }
            }
            catch (Exception ex)
            {
                PseudoParent.ShowExceptionMessage(ex);
            }
        }


        private void btnDisable_Click(object sender, EventArgs e)
        {
            try
            {
                Data.TLCLoginDA.EnableDisableStudentService(CurrentItem.ID, Convert.ToBoolean(btnDisable.Tag));
                ReloadListBox();
            }
            catch (Exception ex)
            {
                PseudoParent.ShowExceptionMessage(ex);
            }
        }



        private void ClearForm()
        {
            txtName.Text = "";
        }

        private void ResetButton()
        {
            btnDisable.Enabled = true;
            btnUpdate.Text = "Update";
            btnUpdate.Tag = false;
        }

        private void ReloadListBox()
        {
            try
            {
                lstExisting.DataSource = null;
                List<Business.SpecialProgram> progs = Data.TLCLoginDA.GetAllSpecialPrograms();
                lstExisting.DataSource = new BindingSource(progs, null);
                lstExisting.DisplayMember = "Display";
                lstExisting.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                PseudoParent.ShowExceptionMessage(ex);
            }
        }
    }
}
