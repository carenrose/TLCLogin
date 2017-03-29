/* Copyright (C) 2017 Brianna Williams
 *
 * This file is part of TLC Login.
 * 
 * TLC Login is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * TLC Login is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with TLC Login.  If not, see <http://www.gnu.org/licenses/>.
 */

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
