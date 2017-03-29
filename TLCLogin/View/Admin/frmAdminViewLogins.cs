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
    public partial class frmAdminViewLogins : Form
    {
        public int CampusID { get; set; }
        public frmMdiContainer PseudoParent { get; set; }

        public frmAdminViewLogins()
        {
            InitializeComponent();
        }

        private void frmAdminViewLogins_Load(object sender, EventArgs e)
        {
            if (PseudoParent != null) CampusID = PseudoParent.Controller.CampusID;
            ReloadData();
            dgvLogins.Focus();
        }

        private void ReloadData()
        {
            try
            {
                DataTable dt = Data.TLCLoginDA.GetAllLoginsTable(CampusID);
                dgvLogins.DataSource = dt;

                // set columns
                if (dt != null && (dgvLogins.Rows != null || dgvLogins.Rows.Count == 0))
                {
                    // the column names are from the DataTable returned by GetAllLoginsTable()
                    dgvLogins.Columns["HistoryID"].Visible = false;
                    dgvLogins.Columns["StudentID"].HeaderText = "Student ID";
                    dgvLogins.Columns["StudentName"].HeaderText = "Student";
                    dgvLogins.Columns["AreaName"].HeaderText = "Subject";
                    dgvLogins.Columns["DefaultLogoffMinutes"].Visible = false;
                    dgvLogins.Columns["MinutesRemain"].HeaderText = "Minutes Until AutoLogoff";
                    dgvLogins.Columns["LogInTime"].HeaderText = "Log In Time";
                    dgvLogins.Columns["LogInTime"].DefaultCellStyle.Format = "h:mm tt, M/d/yy";
                    dgvLogins.Columns["LogInTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    // don't duplicate the button column
                    if (dgvLogins.Columns.GetLastColumn
                        (DataGridViewElementStates.None, DataGridViewElementStates.None).Name != "LogOut") {

                        DataGridViewButtonColumn buttonCol = new DataGridViewButtonColumn();
                        buttonCol.UseColumnTextForButtonValue = true;
                        buttonCol.Text = "Log Out";
                        buttonCol.Name = "LogOut";
                        dgvLogins.Columns.Add(buttonCol);
                    }

                    dgvLogins.AutoResizeRows();
                }
                else
                {
                    // if there's no rows there's no need for columns
                    if (dgvLogins.Columns != null) dgvLogins.Columns.Clear();
                }

                dgvLogins.Refresh();
            }
            catch (Exception ex)
            {
                PseudoParent.ShowExceptionMessage(ex);
            }
        }


        private void dgvLogins_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLogins.Columns[e.ColumnIndex].Name == "LogOut")
            {
                btnLogOutRow_Click(sender, e);
            }
        }
        private void btnLogOutRow_Click(object sender, EventArgs e)
        {
            int row = dgvLogins.CurrentCell.RowIndex;

            try
            {
                // get login id from hidden column and log off
                int selectedHistoryID = Convert.ToInt32(dgvLogins.Rows[row].Cells["HistoryID"].Value);
                Data.TLCLoginDA.LogStudentOff(selectedHistoryID, DateTime.Now);

                ReloadData();
            }
            catch (Exception ex)
            {
                PseudoParent.ShowExceptionMessage(ex);
            }
        }

        private void btnLogOffAll_Click(object sender, EventArgs e)
        {
            try
            {
                Data.TLCLoginDA.LogAllStudentsOff(CampusID);
                this.Close();
            }
            catch (Exception ex)
            {
                PseudoParent.ShowExceptionMessage(ex);
            }
        }

        private void dgvLogins_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // format Minutes Until Autologoff red if it is past
            if (Convert.ToInt32(dgvLogins.Rows[e.RowIndex].Cells["MinutesRemain"].Value) < 0)
            {
                dgvLogins.Rows[e.RowIndex].Cells["MinutesRemain"].Style.BackColor = Color.Salmon;
            }
        }
    }
}
