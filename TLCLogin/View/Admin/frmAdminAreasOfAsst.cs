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
using TLCLogin.Business;

namespace TLCLogin.View.Admin
{
    public partial class frmAdminAreasOfAsst : Form
    {
        public List<AreaOfAssistance> AreasOfAssistance { get; set; }
        public int CurrentID { get; set; }
        public frmMdiContainer PseudoParent { get; set; }



        public frmAdminAreasOfAsst()
        {
            InitializeComponent();
        }

        private void frmAdminCategories_Load(object sender, EventArgs e)
        {
            ReloadListBox();
            lstExisting.ValueMember = "ID";
            lstExisting.DisplayMember = "Display";
        }



        private void lstExisting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstExisting.SelectedIndex > -1)
            {
                CurrentID = ((AreaOfAssistance)lstExisting.SelectedItem).ID;

                AreaOfAssistance chosen = AreasOfAssistance.Find(a => a.ID == CurrentID);

                if (chosen != null)
                {
                    txtName.Text = chosen.Name;
                    nudLength.Value = chosen.AutoLogoffLength;
                    chkSkipCourse.Checked = chosen.SkipsCourseSelection;
                    ResetButton();
                }
            }
        }



        private void btnAddNew_Click(object sender, EventArgs e)
        {
            ClearForm();

            txtName.Focus();
            btnUpdate.Text = "Add";
            btnUpdate.Tag = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(btnUpdate.Tag))
            {
                // is add
                if (txtName.Text != "" && nudLength.Value > 0)
                {
                    AreaOfAssistance area = new AreaOfAssistance();
                    area.Name = txtName.Text;
                    area.AutoLogoffLength = (int)nudLength.Value;
                    area.SkipsCourseSelection = chkSkipCourse.Checked;

                    try
                    {
                        Data.TLCLoginDA.AddAreaOfAssistance(area);

                        ResetButton();
                        ReloadListBox();
                    }
                    catch (Exception ex)
                    {
                        PseudoParent.ShowExceptionMessage(ex);
                    }
                }
            }
            else
            {
                // is update
                AreaOfAssistance area = new AreaOfAssistance();
                area.ID = CurrentID;
                area.Name = txtName.Text;
                area.AutoLogoffLength = (int)nudLength.Value;
                area.SkipsCourseSelection = chkSkipCourse.Checked;

                try
                {
                    Data.TLCLoginDA.UpdateAreaOfAssistance(area);
                    ReloadListBox();
                }
                catch (Exception ex)
                {
                    PseudoParent.ShowExceptionMessage(ex);
                }
            }
        }



        private void ClearForm()
        {
            txtName.Text = "";
            nudLength.Value = 90;
            chkSkipCourse.Checked = false;
        }

        private void ResetButton()
        {
            btnUpdate.Text = "Update";
            btnUpdate.Tag = false;
        }

        private void ReloadListBox()
        {
            try
            {
                lstExisting.DataSource = null;
                this.AreasOfAssistance = Data.TLCLoginDA.GetAllAreasOfAssistance();
                lstExisting.DataSource = AreasOfAssistance;
            }
            catch (Exception e)
            {
                PseudoParent.ShowExceptionMessage(e);
            }
        }
    }
}
