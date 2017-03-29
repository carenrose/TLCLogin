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

namespace TLCLogin.View
{
    public partial class frmCourseSelection : Form
    {
        private frmMdiContainer parent;
        public string Title { get; set; }
        

        public frmCourseSelection()
        {
            InitializeComponent();
        }

        private void frmCourseSelection_Load(object sender, EventArgs e)
        {
            parent = (frmMdiContainer)this.MdiParent;

            // set title
            lblSubjectHeader.Text = Title;

            // fill category cbo
            try
            {
                cboCourseCategory.DataSource = Data.TLCLoginDA.GetAllCourseCategories()
                    .Select(kv => new { Key = kv.Key, Value = kv.Key + ": " + kv.Value }).ToArray();
                cboCourseCategory.ValueMember = "Key";
                cboCourseCategory.DisplayMember = "Value";
                // select nothing
                cboCourseCategory.SelectedIndex = -1;
                cboCourse.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                parent.ShowExceptionMessage(ex);
            }
        }

        private void cboCourseCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCourse.DataSource = null;
            cboCourse.SelectedIndex = -1;

            // fill cbo
            if (cboCourseCategory.SelectedIndex != -1)
            {
                try
                {
                    cboCourse.DataSource = Data.TLCLoginDA.GetCoursesByCategory(cboCourseCategory.SelectedValue.ToString());
                    cboCourse.ValueMember = "NumberCode";
                    cboCourse.DisplayMember = "Display";
                }
                catch (Exception ex)
                {
                    parent.ShowExceptionMessage(ex);
                }
            }
        }


        private void btnStartOver_Click(object sender, EventArgs e)
        {
            parent.StartOver();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                // DO THE THING!
                parent.Controller.UpdateLoginCourse(
                    new Business.Course(cboCourseCategory.SelectedValue.ToString(), Convert.ToInt16(cboCourse.SelectedValue)));

                parent.GoToNextScreen();
            }
            catch (Exception ex)
            {
                parent.ShowExceptionMessage(ex);
            }
        }
    }
}
