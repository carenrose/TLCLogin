namespace TLCLogin.View
{
    partial class frmCourseSelection
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
            this.lblSubjectHeader = new System.Windows.Forms.Label();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnStartOver = new System.Windows.Forms.Button();
            this.cboCourse = new System.Windows.Forms.ComboBox();
            this.cboCourseCategory = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSubjectHeader
            // 
            this.lblSubjectHeader.AutoSize = true;
            this.lblSubjectHeader.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubjectHeader.Location = new System.Drawing.Point(192, 25);
            this.lblSubjectHeader.Name = "lblSubjectHeader";
            this.lblSubjectHeader.Size = new System.Drawing.Size(150, 30);
            this.lblSubjectHeader.TabIndex = 0;
            this.lblSubjectHeader.Text = "Subject Name";
            // 
            // btnContinue
            // 
            this.btnContinue.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinue.Location = new System.Drawing.Point(359, 345);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(155, 41);
            this.btnContinue.TabIndex = 7;
            this.btnContinue.Text = "Continue >";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnStartOver
            // 
            this.btnStartOver.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStartOver.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartOver.Location = new System.Drawing.Point(12, 345);
            this.btnStartOver.Name = "btnStartOver";
            this.btnStartOver.Size = new System.Drawing.Size(155, 41);
            this.btnStartOver.TabIndex = 6;
            this.btnStartOver.Text = "< Start Over";
            this.btnStartOver.UseVisualStyleBackColor = true;
            this.btnStartOver.Click += new System.EventHandler(this.btnStartOver_Click);
            // 
            // cboCourse
            // 
            this.cboCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCourse.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCourse.FormattingEnabled = true;
            this.cboCourse.Location = new System.Drawing.Point(41, 225);
            this.cboCourse.MaxDropDownItems = 12;
            this.cboCourse.Name = "cboCourse";
            this.cboCourse.Size = new System.Drawing.Size(453, 29);
            this.cboCourse.TabIndex = 10;
            // 
            // cboCourseCategory
            // 
            this.cboCourseCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCourseCategory.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCourseCategory.FormattingEnabled = true;
            this.cboCourseCategory.Location = new System.Drawing.Point(41, 119);
            this.cboCourseCategory.MaxDropDownItems = 12;
            this.cboCourseCategory.Name = "cboCourseCategory";
            this.cboCourseCategory.Size = new System.Drawing.Size(453, 29);
            this.cboCourseCategory.TabIndex = 11;
            this.cboCourseCategory.SelectedIndexChanged += new System.EventHandler(this.cboCourseCategory_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(37, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 21);
            this.label7.TabIndex = 8;
            this.label7.Text = "Course";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(37, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 21);
            this.label6.TabIndex = 9;
            this.label6.Text = "Category";
            // 
            // frmCourseSelection
            // 
            this.AcceptButton = this.btnContinue;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 398);
            this.Controls.Add(this.cboCourse);
            this.Controls.Add(this.cboCourseCategory);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.btnStartOver);
            this.Controls.Add(this.lblSubjectHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCourseSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCourseSelection";
            this.Load += new System.EventHandler(this.frmCourseSelection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSubjectHeader;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnStartOver;
        private System.Windows.Forms.ComboBox cboCourse;
        private System.Windows.Forms.ComboBox cboCourseCategory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}