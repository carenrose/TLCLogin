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

namespace TLCLogin.View.Admin
{
    partial class frmAdminAddRemoveAdmins
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.lstExisting = new System.Windows.Forms.ListBox();
            this.btnAddRemove = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.chkEnableSimple = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSimpleUsername = new System.Windows.Forms.TextBox();
            this.txtSimplePass = new System.Windows.Forms.TextBox();
            this.btnSaveSimple = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hub/Windows Username:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(213, 14);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(108, 29);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(365, 21);
            this.label4.TabIndex = 12;
            this.label4.Text = "Note: changes apply to all locations/campuses";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(2, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(477, 2);
            this.label3.TabIndex = 11;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(71, 256);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(137, 33);
            this.btnAddNew.TabIndex = 10;
            this.btnAddNew.Text = "Add New Admin";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // lstExisting
            // 
            this.lstExisting.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstExisting.FormattingEnabled = true;
            this.lstExisting.ItemHeight = 21;
            this.lstExisting.Location = new System.Drawing.Point(16, 93);
            this.lstExisting.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstExisting.Name = "lstExisting";
            this.lstExisting.Size = new System.Drawing.Size(192, 151);
            this.lstExisting.TabIndex = 9;
            this.lstExisting.SelectedIndexChanged += new System.EventHandler(this.lstExisting_SelectedIndexChanged);
            // 
            // btnAddRemove
            // 
            this.btnAddRemove.Location = new System.Drawing.Point(328, 11);
            this.btnAddRemove.Name = "btnAddRemove";
            this.btnAddRemove.Size = new System.Drawing.Size(119, 33);
            this.btnAddRemove.TabIndex = 13;
            this.btnAddRemove.Text = "Add";
            this.btnAddRemove.UseVisualStyleBackColor = true;
            this.btnAddRemove.Click += new System.EventHandler(this.btnAddRemove_Click);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(221, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(2, 250);
            this.label2.TabIndex = 14;
            // 
            // chkEnableSimple
            // 
            this.chkEnableSimple.AutoSize = true;
            this.chkEnableSimple.Location = new System.Drawing.Point(232, 96);
            this.chkEnableSimple.Name = "chkEnableSimple";
            this.chkEnableSimple.Size = new System.Drawing.Size(220, 25);
            this.chkEnableSimple.TabIndex = 15;
            this.chkEnableSimple.Text = "Enable Simple Admin Login";
            this.chkEnableSimple.UseVisualStyleBackColor = true;
            this.chkEnableSimple.CheckedChanged += new System.EventHandler(this.chkEnableSimple_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(228, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 21);
            this.label5.TabIndex = 16;
            this.label5.Text = "Username";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(232, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 21);
            this.label6.TabIndex = 17;
            this.label6.Text = "Password";
            // 
            // txtSimpleUsername
            // 
            this.txtSimpleUsername.Location = new System.Drawing.Point(315, 127);
            this.txtSimpleUsername.Name = "txtSimpleUsername";
            this.txtSimpleUsername.Size = new System.Drawing.Size(130, 29);
            this.txtSimpleUsername.TabIndex = 18;
            // 
            // txtSimplePass
            // 
            this.txtSimplePass.Location = new System.Drawing.Point(315, 170);
            this.txtSimplePass.Name = "txtSimplePass";
            this.txtSimplePass.Size = new System.Drawing.Size(130, 29);
            this.txtSimplePass.TabIndex = 18;
            // 
            // btnSaveSimple
            // 
            this.btnSaveSimple.Location = new System.Drawing.Point(315, 211);
            this.btnSaveSimple.Name = "btnSaveSimple";
            this.btnSaveSimple.Size = new System.Drawing.Size(132, 33);
            this.btnSaveSimple.TabIndex = 19;
            this.btnSaveSimple.Text = "Save";
            this.btnSaveSimple.UseVisualStyleBackColor = true;
            this.btnSaveSimple.Click += new System.EventHandler(this.btnSaveSimple_Click);
            // 
            // frmAdminAddRemoveAdmins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 301);
            this.Controls.Add(this.btnSaveSimple);
            this.Controls.Add(this.txtSimplePass);
            this.Controls.Add(this.txtSimpleUsername);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkEnableSimple);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAddRemove);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.lstExisting);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdminAddRemoveAdmins";
            this.Text = "Add/Remove Admins";
            this.Load += new System.EventHandler(this.frmAdminAddRemoveAdmins_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.ListBox lstExisting;
        private System.Windows.Forms.Button btnAddRemove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEnableSimple;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSimpleUsername;
        private System.Windows.Forms.TextBox txtSimplePass;
        private System.Windows.Forms.Button btnSaveSimple;
    }
}