namespace TLCLogin.View
{
    partial class frmMdiContainer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRemoveAdminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewLogonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewStudentsHoursThisWeekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSpecialProgramsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeLogoutSurveyFrequencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createBackupOfDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fbdBackupDirectoryChooser = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.sfdDatabaseBackup = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.adminToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeCenterToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // closeCenterToolStripMenuItem
            // 
            this.closeCenterToolStripMenuItem.Name = "closeCenterToolStripMenuItem";
            this.closeCenterToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.closeCenterToolStripMenuItem.Text = "Close Center For Day";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.quitToolStripMenuItem.Text = "Exit Program";
            // 
            // adminToolStripMenuItem
            // 
            this.adminToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRemoveAdminToolStripMenuItem,
            this.viewLogonsToolStripMenuItem,
            this.viewStudentsHoursThisWeekToolStripMenuItem,
            this.manageOptionsToolStripMenuItem,
            this.createBackupOfDatabaseToolStripMenuItem});
            this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            this.adminToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.adminToolStripMenuItem.Text = "Admin";
            // 
            // addRemoveAdminToolStripMenuItem
            // 
            this.addRemoveAdminToolStripMenuItem.Name = "addRemoveAdminToolStripMenuItem";
            this.addRemoveAdminToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.addRemoveAdminToolStripMenuItem.Text = "Add/Remove Admin";
            this.addRemoveAdminToolStripMenuItem.Click += new System.EventHandler(this.addRemoveAdminToolStripMenuItem_Click);
            // 
            // viewLogonsToolStripMenuItem
            // 
            this.viewLogonsToolStripMenuItem.Enabled = false;
            this.viewLogonsToolStripMenuItem.Name = "viewLogonsToolStripMenuItem";
            this.viewLogonsToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.viewLogonsToolStripMenuItem.Text = "View Current Logons";
            this.viewLogonsToolStripMenuItem.Click += new System.EventHandler(this.viewLogonsToolStripMenuItem_Click);
            // 
            // viewStudentsHoursThisWeekToolStripMenuItem
            // 
            this.viewStudentsHoursThisWeekToolStripMenuItem.Name = "viewStudentsHoursThisWeekToolStripMenuItem";
            this.viewStudentsHoursThisWeekToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.viewStudentsHoursThisWeekToolStripMenuItem.Text = "View Student\'s Hours This Week";
            this.viewStudentsHoursThisWeekToolStripMenuItem.Click += new System.EventHandler(this.viewStudentsHoursThisWeekToolStripMenuItem_Click);
            // 
            // manageOptionsToolStripMenuItem
            // 
            this.manageOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.editSpecialProgramsToolStripMenuItem,
            this.changeLogoutSurveyFrequencyToolStripMenuItem,
            this.changeToolStripMenuItem});
            this.manageOptionsToolStripMenuItem.Name = "manageOptionsToolStripMenuItem";
            this.manageOptionsToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.manageOptionsToolStripMenuItem.Text = "Manage Program Options";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.editToolStripMenuItem.Text = "Edit Categories";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editCategoriesToolStripMenuItem_Click);
            // 
            // editSpecialProgramsToolStripMenuItem
            // 
            this.editSpecialProgramsToolStripMenuItem.Name = "editSpecialProgramsToolStripMenuItem";
            this.editSpecialProgramsToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.editSpecialProgramsToolStripMenuItem.Text = "Edit Special Programs";
            this.editSpecialProgramsToolStripMenuItem.Click += new System.EventHandler(this.editSpecialProgramsToolStripMenuItem_Click);
            // 
            // changeLogoutSurveyFrequencyToolStripMenuItem
            // 
            this.changeLogoutSurveyFrequencyToolStripMenuItem.Name = "changeLogoutSurveyFrequencyToolStripMenuItem";
            this.changeLogoutSurveyFrequencyToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.changeLogoutSurveyFrequencyToolStripMenuItem.Text = "Change Logout Survey Frequency";
            this.changeLogoutSurveyFrequencyToolStripMenuItem.Click += new System.EventHandler(this.changeLogoutSurveyFrequencyToolStripMenuItem_Click);
            // 
            // changeToolStripMenuItem
            // 
            this.changeToolStripMenuItem.Name = "changeToolStripMenuItem";
            this.changeToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.changeToolStripMenuItem.Text = "Change Program Close Password";
            this.changeToolStripMenuItem.Click += new System.EventHandler(this.changeByePasswordToolStripMenuItem_Click);
            // 
            // createBackupOfDatabaseToolStripMenuItem
            // 
            this.createBackupOfDatabaseToolStripMenuItem.Name = "createBackupOfDatabaseToolStripMenuItem";
            this.createBackupOfDatabaseToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.createBackupOfDatabaseToolStripMenuItem.Text = "Create Backup Of Database";
            this.createBackupOfDatabaseToolStripMenuItem.Click += new System.EventHandler(this.createBackupOfDatabaseToolStripMenuItem_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // sfdDatabaseBackup
            // 
            this.sfdDatabaseBackup.Title = "Save Backup Of Database";
            // 
            // frmMdiContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMdiContainer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tutoring & Learning Center Login";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMdiContainer_FormClosing);
            this.Load += new System.EventHandler(this.frmLoginContainer_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem adminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewLogonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSpecialProgramsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewStudentsHoursThisWeekToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRemoveAdminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeLogoutSurveyFrequencyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog fbdBackupDirectoryChooser;
        private System.Windows.Forms.ToolStripMenuItem createBackupOfDatabaseToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SaveFileDialog sfdDatabaseBackup;
    }
}

