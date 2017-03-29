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
    public partial class frmStudentDemographics : Form
    {
        private frmMdiContainer parent;
        public Business.Student Student { get; set; }

        public frmStudentDemographics()
        {
            InitializeComponent();
        }

        private void frmStudentDemographics_Load(object sender, EventArgs e)
        {
            parent = (frmMdiContainer)this.MdiParent;

            // fill comboboxes
            try
            {
                // Programs of Study
                cboProgramStudy.DataSource = Data.TLCLoginDA.GetAllProgramsOfStudy()
                    .Select(kv => new { Key = kv.Key, Value = kv.Value }).ToArray();
                cboProgramStudy.ValueMember = "Key";
                cboProgramStudy.DisplayMember = "Value";

                // Countries
                List<KeyValuePair<int, string>> countries = Data.TLCLoginDA.GetAllCountries().OrderBy(i => i.Value).ToList();
                int index = countries.FindIndex(i => i.Value == "United States of America");
                if (index > -1)
                {
                    KeyValuePair<int, string> usa = countries[index];
                    countries.RemoveAt(index);
                    countries.Insert(0, usa);
                }
                cboCountryOrigin.DataSource = countries.Select
                    (kv => new { Key = kv.Key, Value = kv.Value }).ToArray();
                cboCountryOrigin.ValueMember = "Key";
                cboCountryOrigin.DisplayMember = "Value";
                cboCountryOrigin.SelectedIndex = 0;

                index = -1;

                // Languages
                List<KeyValuePair<int, string>> languages = Data.TLCLoginDA.GetAllLanguages().OrderBy(i => i.Value).ToList();
                index = languages.FindIndex(i => i.Value == "English");
                if (index > -1)
                {
                    KeyValuePair<int, string> english = languages[index];
                    languages.RemoveAt(index);
                    languages.Insert(0, english);
                }
                cboHomeLanguage.DataSource = languages.Select
                    (kv => new KeyValuePair<int, string>(kv.Key, kv.Value)).ToArray();
                cboHomeLanguage.ValueMember = "Key";
                cboHomeLanguage.DisplayMember = "Value";
                cboHomeLanguage.SelectedIndex = 0;

                // Special Programs
                chkLstSpecialPrograms.DataSource = Data.TLCLoginDA.GetAllEnabledSpecialPrograms();
                chkLstSpecialPrograms.DisplayMember = "Name";
                chkLstSpecialPrograms.ValueMember = "ID";


                // fill student data
                if (Student != null)
                {
                    txtStudentID.Text = Student.StudentID.ToString();
                    txtFirstName.Text = Student.FirstName;
                    txtLastName.Text = Student.LastName;

                    if (Student.ProgramOfStudy != null) cboProgramStudy.SelectedValue = Student.ProgramOfStudy;
                    if (Student.CountryOfOrigin > 0) cboCountryOrigin.SelectedValue = Student.CountryOfOrigin;
                    if (Student.NativeLanguage > 0) cboHomeLanguage.SelectedValue = Student.NativeLanguage;

                    // special programs
                    foreach (Business.SpecialProgram prog in Student.SpecialPrograms)
                    {
                        for (int i = 0; i < chkLstSpecialPrograms.Items.Count; i++)
                        {
                            if (((Business.SpecialProgram)chkLstSpecialPrograms.Items[i]).ID == prog.ID)
                            {
                                chkLstSpecialPrograms.SetItemChecked(i, true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                parent.ShowExceptionMessage(ex);
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            // gather form data
            Student.FirstName = txtFirstName.Text;
            Student.LastName = txtLastName.Text;
            Student.ProgramOfStudy = cboProgramStudy.SelectedValue.ToString();
            Student.CountryOfOrigin = Convert.ToInt16(cboCountryOrigin.SelectedValue);
            Student.NativeLanguage = Convert.ToInt16(cboHomeLanguage.SelectedValue);
            Student.SpecialPrograms.Clear();
            foreach (Business.SpecialProgram item in chkLstSpecialPrograms.CheckedItems)
            {
                Student.SpecialPrograms.Add(item);
            }

            // go
            try
            {
                parent.Controller.UpdateStudentInfo(Student);
                parent.GoToNextScreen();
            }
            catch (Exception ex)
            {
                parent.ShowExceptionMessage(ex);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            parent.StartOver();
        }
    }
}
