﻿/* Copyright (C) 2017 Brianna Williams
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
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace TLCLogin.View
{
    public partial class frmMdiContainer : Form
    {
        private int currentStep;
        private bool alreadyValidatedClose;
        public TLCLoginController Controller { get; set; }
        public List<Form> ChildForms { get; set; }
        private Random random;

        public frmMdiContainer()
        {
            this.Controller = new TLCLoginController();
            this.ChildForms = new List<Form>();
            this.random = new Random();
            currentStep = 0;
            InitializeComponent();

            //if (! this.menuStrip1.Items.Contains(exitToolStripMenuItem))
            //    this.menuStrip1.Items.Add(exitToolStripMenuItem);

            //if (! this.menuStrip1.Items.Contains(adminToolStripMenuItem))
            //    this.menuStrip1.Items.Add(adminToolStripMenuItem);

            //this.exitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            //this.closeCenterToolStripMenuItem,
            //this.quitToolStripMenuItem});

            //this.adminToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            //this.addRemoveAdminToolStripMenuItem,
            //this.viewLogonsToolStripMenuItem,
            //this.viewStudentsHoursThisWeekToolStripMenuItem,
            //this.manageOptionsToolStripMenuItem,
            //this.createBackupOfDatabaseToolStripMenuItem});
        }
        
        /// <summary>
        /// Form Load Event - loads first child form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLoginContainer_Load(object sender, EventArgs e)
        {
            GoToNextScreen();
        }
        
        /// <summary>
        /// Form close event - prompts for password if X button was clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMdiContainer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;

            if (! alreadyValidatedClose && ! PromptForPassword(false)) e.Cancel = true;
        }



        /// <summary>
        /// Sets campus in Controller and title bar
        /// </summary>
        /// <param name="id">Campus ID</param>
        /// <param name="text">Campus name</param>
        public void SetCampus(int id, string text)
        {
            this.Controller.CampusID = id;
            this.Controller.CampusName = text;
            viewLogonsToolStripMenuItem.Enabled = true;

            this.Text += " - " + text + " Campus";
        }


        #region Messages

        /// <summary>
        /// Shows a basic message box message
        /// </summary>
        /// <param name="message"></param>
        public void ShowMessage(string message)
        {
            frmMessage mb = new frmMessage();
            mb.Message = message;
            mb.ShowDialog();
        }

        /// <summary>
        /// Shows a message that disappears on its own after 10 seconds
        /// </summary>
        /// <param name="message"></param>
        public void ShowTimeoutMessage(string message)
        {
            frmMessage mb = new frmMessage();
            mb.IsTimeoutMessage = true;
            mb.Message = message;
            mb.ShowDialog();
        }

        /// <summary>
        /// Shows the message of an Exception (in red text)
        /// </summary>
        /// <param name="e"></param>
        public void ShowExceptionMessage(Exception e)
        {
            frmMessage mb = new frmMessage();
            mb.TextColor = System.Drawing.Color.DarkRed;
            mb.Message = e.Message;
            mb.ShowDialog();
        }

        #endregion

        #region Form Navigation

        /// <summary>
        /// Chooses which form to display based on currentStep
        /// </summary>
        public void GoToNextScreen()
        {
            Form frm = null;

            switch (currentStep)
            {
                // Select Campus
                case 0: frm = new frmSelectCampus(); break;

                // Show Student ID entry form
                case 1:
                    frm = new frmLoginEntry(); break;

                // Go to Demographics OR Log out
                case 2:
                    if (Controller.CurrentStudent != null)
                    {
                        if (Controller.CreateStudent(Controller.CurrentStudent.StudentID))
                        {
                            // if in db already, go to next screen
                            currentStep++;
                            GoToNextScreen();
                        }
                        else
                        {
                            // else go to demographics screen
                            frm = new frmStudentDemographics();
                            ((frmStudentDemographics)frm).Student = Controller.CurrentStudent;
                        }
                    }
                    else
                    {
                        // log out or show survey
                        int frequency = Data.TLCLoginDA.GetSurveyFrequencyPercent();
                        frequency = (frequency == -1) ? 50 : frequency;

                        if (random.Next(1, 100) < frequency)
                        {
                            ShowSurvey();
                        }
                        else
                        {
                            ShowTimeoutMessage("You have been logged off.");
                            StartOver();
                        }
                    }
                    break;

                // Area Of Assistance selection buttons
                case 3:
                    frm = new frmSubjectArea(); break;

                // Course selection form
                case 4:
                    frm = new frmCourseSelection();
                    if (Controller.CurrentLogin != null && Controller.CurrentLogin.AreaOfAssistance != null)
                        ((frmCourseSelection)frm).Title = Controller.CurrentLogin.AreaOfAssistance.Name;
                    break;

                // Show thank you and start over
                case 5:
                    string message = "Thank you for signing in. ";
                    if (Controller.CurrentLogin != null)
                    {
                        message += "You will automatically be logged off in ";
                        message += Controller.CurrentLogin.AreaOfAssistance.AutoLogoffLength + " minutes." + Environment.NewLine ;
                        message += "If you finish before that time, please log out when you leave.";
                    }
                    ShowTimeoutMessage(message);
                    StartOver();
                    break;

                case 6:
                    frm = new frmLogoffSurvey();
                    break;
            }

            if (frm != null)
            {
                if (this.ActiveMdiChild != null) this.ActiveMdiChild.Hide();

                // show new form
                frm.MdiParent = this;
                ChildForms.Add(frm);
                frm.Show();

                currentStep++;
            }
        }

    /*
        public void GoToPreviousScreen()
        {
            // TODO: Doesn't actually log in when using back button

            Form last = ChildForms[ChildForms.Count - 2];       // the last in the list is the current

            ActiveMdiChild.Close();
            ChildForms.RemoveAt(ChildForms.Count - 1);

            last.MdiParent = this;
            last.Show();

            currentStep--;
        }
    */

        /// <summary>
        /// Skips the course selection form (form 5) - used with certain Areas of Assistance
        /// </summary>
        public void SkipCourseScreen()
        {
            currentStep++;
            GoToNextScreen();
        }

        /// <summary>
        /// Starts at Form 2 (Student ID entry form), closes other child forms
        /// </summary>
        public void StartOver()
        {
            // reset all
            Controller.ClearCurrent();
            ChildForms.Clear();
            currentStep = 1;

            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }

            // start over
            GoToNextScreen();
        }

        /// <summary>
        /// Goes directly to form 6 (the logoff survey)
        /// </summary>
        public void ShowSurvey()
        {
            currentStep = 6;
            GoToNextScreen();
        }

        /// <summary>
        /// Prompts the user to enter a (username and) password
        /// </summary>
        /// <param name="adminPassword">True to require a valid admin's credentials, false for standard "bye"</param>
        /// <returns>If a valid password was entered</returns>
        private bool PromptForPassword(bool adminPassword)
        {
            frmPasswordPrompt pwd = new frmPasswordPrompt();
            pwd.IsAdminPassword = adminPassword;
            pwd.PseudoParent = this;
            return pwd.ShowDialog() == DialogResult.OK;
        }

        #endregion

        #region Menu Strip Events

        private void viewLogonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // prompt password - admin
            if (PromptForPassword(true))
            {
                Admin.frmAdminViewLogins frm = new Admin.frmAdminViewLogins();
                frm.PseudoParent = this;
                frm.ShowDialog();
            }
        }

        private void editCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // prompt password - admin
            if (PromptForPassword(true))
            {
                Admin.frmAdminAreasOfAsst frm = new Admin.frmAdminAreasOfAsst();
                frm.PseudoParent = this;
                frm.ShowDialog();
            }
        }

        private void editSpecialProgramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // prompt password - admin
            if (PromptForPassword(true))
            {
                Admin.frmAdminStudentServices frm = new Admin.frmAdminStudentServices();
                frm.PseudoParent = this;
                frm.ShowDialog();
            }
        }

        private void closeCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // prompt password - bye
            if (PromptForPassword(false))
            {
                try
                {
                    this.Controller.CloseCenter();
                    this.alreadyValidatedClose = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    ShowExceptionMessage(ex);
                }
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // prompt password - "bye"
            if (PromptForPassword(false))
            {
                this.alreadyValidatedClose = true;
                this.Close();
            }
        }

        private void viewStudentsHoursThisWeekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // prompt password - admin
            if (PromptForPassword(true))
            {
                Admin.frmAdminViewStudentHours frm = new Admin.frmAdminViewStudentHours();
                frm.PseudoParent = this;
                frm.ShowDialog();
            }
        }

        private void addRemoveAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // prompt password - admin
            if (PromptForPassword(true))
            {
                Admin.frmAdminAddRemoveAdmins frm = new Admin.frmAdminAddRemoveAdmins();
                frm.PseudoParent = this;
                frm.ShowDialog();
            }
        }

        private void changeLogoutSurveyFrequencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // prompt password - admin
            if (PromptForPassword(true))
            {
                Admin.frmAdminSurveyFrequency frm = new Admin.frmAdminSurveyFrequency();
                frm.PseudoParent = this;
                frm.ShowDialog();
            }
        }

        private void changeByePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // prompt password - admin
            if (PromptForPassword(true))
            {
                Admin.frmAdminChangeByePassword frm = new Admin.frmAdminChangeByePassword();
                frm.PseudoParent = this;
                frm.ShowDialog();
            }
        }

        private void createBackupOfDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + @"\";
            string origFilename = "TLCLoginDatabase.mdb";

            string quarter = "Quarter";
            switch (DateTime.Now.Month)
            {
                case 1:
                case 2:
                case 3:
                    quarter = "Winter";
                    break;
                case 4:
                case 5:
                case 6:
                    quarter = "Spring";
                    break;
                case 7:
                case 8:
                case 9:
                    quarter = "Summer";
                    break;
                case 10:
                case 11:
                case 12:
                    quarter = "Fall";
                    break;
            }

            string backupDir = dir + @"\Backup";
            string newFilename = DateTime.Now.Year + "_" + quarter + ".mdb";
            
            // show file dialog
            sfdDatabaseBackup.InitialDirectory = backupDir;
            sfdDatabaseBackup.FileName = newFilename;
            sfdDatabaseBackup.DefaultExt = "mdb";
            sfdDatabaseBackup.ShowDialog();

            string saved = sfdDatabaseBackup.FileName;
            FileSystem.CopyFile(dir + origFilename, saved);
        }

        private void editSurveyWhereDidYouHearAboutUsOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // prompt password - admin
            if (PromptForPassword(true))
            {
                Admin.frmAdminSurveyWhereHeard frm = new Admin.frmAdminSurveyWhereHeard();
                frm.PseudoParent = this;
                frm.ShowDialog();
            }
        }

        #endregion
    }
}
