namespace TLCLogin.View.Admin
{
    partial class frmAdminAreasOfAsst
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstExisting = new System.Windows.Forms.ListBox();
            this.nudLength = new System.Windows.Forms.NumericUpDown();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkSkipCourse = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Category Name:";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(158, 6);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(290, 29);
            this.txtName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Length In Minutes:";
            // 
            // lstExisting
            // 
            this.lstExisting.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstExisting.FormattingEnabled = true;
            this.lstExisting.ItemHeight = 21;
            this.lstExisting.Location = new System.Drawing.Point(17, 112);
            this.lstExisting.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstExisting.Name = "lstExisting";
            this.lstExisting.Size = new System.Drawing.Size(431, 151);
            this.lstExisting.TabIndex = 4;
            this.lstExisting.SelectedIndexChanged += new System.EventHandler(this.lstExisting_SelectedIndexChanged);
            // 
            // nudLength
            // 
            this.nudLength.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudLength.Location = new System.Drawing.Point(158, 43);
            this.nudLength.Maximum = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            this.nudLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLength.Name = "nudLength";
            this.nudLength.Size = new System.Drawing.Size(49, 29);
            this.nudLength.TabIndex = 2;
            this.nudLength.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(277, 271);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(171, 33);
            this.btnAddNew.TabIndex = 5;
            this.btnAddNew.Text = "Add New Category";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(350, 41);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(99, 29);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(-5, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(477, 2);
            this.label3.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(329, 21);
            this.label4.TabIndex = 8;
            this.label4.Text = "Note: changes apply to all locations/campuses";
            // 
            // chkSkipCourse
            // 
            this.chkSkipCourse.AutoSize = true;
            this.chkSkipCourse.Location = new System.Drawing.Point(220, 46);
            this.chkSkipCourse.Name = "chkSkipCourse";
            this.chkSkipCourse.Size = new System.Drawing.Size(119, 25);
            this.chkSkipCourse.TabIndex = 9;
            this.chkSkipCourse.Text = "Skips Course";
            this.chkSkipCourse.UseVisualStyleBackColor = true;
            // 
            // frmAdminAreasOfAsst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 316);
            this.Controls.Add(this.chkSkipCourse);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.nudLength);
            this.Controls.Add(this.lstExisting);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frmAdminAreasOfAsst";
            this.Text = "Areas Of Assistance";
            this.Load += new System.EventHandler(this.frmAdminCategories_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstExisting;
        private System.Windows.Forms.NumericUpDown nudLength;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkSkipCourse;
    }
}