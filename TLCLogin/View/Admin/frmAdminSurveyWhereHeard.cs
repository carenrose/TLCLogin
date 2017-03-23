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
    public partial class frmAdminSurveyWhereHeard : Form
    {
        public frmMdiContainer PseudoParent { get; set; }

        public frmAdminSurveyWhereHeard()
        {
            InitializeComponent();
        }

        private void frmAdminSurveyWhereHeard_Load(object sender, EventArgs e)
        {
            ReloadListBox();
            ResetButtons();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text.Length > 0)
                {
                    // true: is add, false: not add
                    if (Convert.ToBoolean(btnUpdate.Tag))
                    {
                        // add new
                        Data.TLCLoginDA.AddSurveyWhereFound(txtName.Text);
                    }
                    else
                    {
                        // update
                        int seld = -1;
                        if (Int32.TryParse(btnAddNew.Tag.ToString(), out seld) && seld > -1)
                            Data.TLCLoginDA.UpdateSurveyWhereFound(seld, txtName.Text);
                    }

                    ReloadListBox();
                }
            }
            catch (Exception ex)
            {
                PseudoParent.ShowExceptionMessage(ex);
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            btnUpdate.Text = "Add";
            btnUpdate.Tag = true;
            lstExisting.SelectedIndex = -1;
            txtName.Clear();
        }

        private void lstExisting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstExisting.SelectedIndex > -1)
            {
                KeyValuePair<int, string> selected = (KeyValuePair <int, string>)lstExisting.SelectedItem;

                txtName.Text = selected.Value;
                btnAddNew.Tag = selected.Key;
            }
        }

        private void ReloadListBox()
        {
            lstExisting.DataSource = null;
            lstExisting.DataSource = Data.TLCLoginDA.GetAllSurveyAdPlaces()
                .Select(kv => new KeyValuePair<int, string>(kv.Key, kv.Value)).ToArray();
            lstExisting.ValueMember = "Key";
            lstExisting.DisplayMember = "Value";
        }

        private void ResetButtons()
        {
            btnUpdate.Text = "Update";
            btnUpdate.Tag = false;
        }
    }
}
