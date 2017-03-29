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
    public partial class frmAdminViewStudentHours : Form
    {
        public frmMdiContainer PseudoParent { get; set; }

        public frmAdminViewStudentHours()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                int studentID;
                if (Int32.TryParse(txtStudentID.Text, out studentID))
                {
                    double hours = Data.TLCLoginDA.GetTutoredHoursByStudent(studentID);
                    txtHours.Text = hours.ToString("N1");
                }
                else
                {
                    PseudoParent.ShowMessage("Enter only numbers in the Student ID field.");
                }
            }
            catch (Exception ex)
            {
                PseudoParent.ShowExceptionMessage(ex);
            }
        }

        private void txtStudentID_TextChanged(object sender, EventArgs e)
        {
            txtHours.Clear();
        }
    }
}
