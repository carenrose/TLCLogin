namespace TLCLogin.View
{
    partial class frmLogoffSurvey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogoffSurvey));
            this.btn1star = new System.Windows.Forms.Button();
            this.btn2stars = new System.Windows.Forms.Button();
            this.btn3stars = new System.Windows.Forms.Button();
            this.btn4stars = new System.Windows.Forms.Button();
            this.btn5stars = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnNope = new System.Windows.Forms.Button();
            this.lblCountdown = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.pnlHowHeard = new System.Windows.Forms.Panel();
            this.cboHeardAbout = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlHowHeard.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn1star
            // 
            this.btn1star.BackColor = System.Drawing.Color.Transparent;
            this.btn1star.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn1star.BackgroundImage")));
            this.btn1star.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn1star.FlatAppearance.BorderSize = 0;
            this.btn1star.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn1star.Location = new System.Drawing.Point(42, 263);
            this.btn1star.Name = "btn1star";
            this.btn1star.Size = new System.Drawing.Size(80, 76);
            this.btn1star.TabIndex = 3;
            this.btn1star.Tag = "1";
            this.btn1star.UseVisualStyleBackColor = false;
            this.btn1star.Click += new System.EventHandler(this.StarButtonClick);
            this.btn1star.MouseEnter += new System.EventHandler(this.ButtonMouseHover);
            this.btn1star.MouseLeave += new System.EventHandler(this.ButtonMouseLeave);
            // 
            // btn2stars
            // 
            this.btn2stars.BackColor = System.Drawing.Color.Transparent;
            this.btn2stars.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn2stars.BackgroundImage")));
            this.btn2stars.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn2stars.FlatAppearance.BorderSize = 0;
            this.btn2stars.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn2stars.Location = new System.Drawing.Point(128, 263);
            this.btn2stars.Name = "btn2stars";
            this.btn2stars.Size = new System.Drawing.Size(80, 76);
            this.btn2stars.TabIndex = 4;
            this.btn2stars.Tag = "2";
            this.btn2stars.UseVisualStyleBackColor = false;
            this.btn2stars.Click += new System.EventHandler(this.StarButtonClick);
            this.btn2stars.MouseEnter += new System.EventHandler(this.ButtonMouseHover);
            this.btn2stars.MouseLeave += new System.EventHandler(this.ButtonMouseLeave);
            // 
            // btn3stars
            // 
            this.btn3stars.BackColor = System.Drawing.Color.Transparent;
            this.btn3stars.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn3stars.BackgroundImage")));
            this.btn3stars.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn3stars.FlatAppearance.BorderSize = 0;
            this.btn3stars.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn3stars.Location = new System.Drawing.Point(214, 263);
            this.btn3stars.Name = "btn3stars";
            this.btn3stars.Size = new System.Drawing.Size(80, 76);
            this.btn3stars.TabIndex = 5;
            this.btn3stars.Tag = "3";
            this.btn3stars.UseVisualStyleBackColor = false;
            this.btn3stars.Click += new System.EventHandler(this.StarButtonClick);
            this.btn3stars.MouseEnter += new System.EventHandler(this.ButtonMouseHover);
            this.btn3stars.MouseLeave += new System.EventHandler(this.ButtonMouseLeave);
            // 
            // btn4stars
            // 
            this.btn4stars.BackColor = System.Drawing.Color.Transparent;
            this.btn4stars.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn4stars.BackgroundImage")));
            this.btn4stars.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn4stars.FlatAppearance.BorderSize = 0;
            this.btn4stars.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn4stars.Location = new System.Drawing.Point(300, 263);
            this.btn4stars.Name = "btn4stars";
            this.btn4stars.Size = new System.Drawing.Size(80, 76);
            this.btn4stars.TabIndex = 6;
            this.btn4stars.Tag = "4";
            this.btn4stars.UseVisualStyleBackColor = false;
            this.btn4stars.Click += new System.EventHandler(this.StarButtonClick);
            this.btn4stars.MouseEnter += new System.EventHandler(this.ButtonMouseHover);
            this.btn4stars.MouseLeave += new System.EventHandler(this.ButtonMouseLeave);
            // 
            // btn5stars
            // 
            this.btn5stars.BackColor = System.Drawing.Color.Transparent;
            this.btn5stars.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn5stars.BackgroundImage")));
            this.btn5stars.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn5stars.FlatAppearance.BorderSize = 0;
            this.btn5stars.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn5stars.Location = new System.Drawing.Point(386, 263);
            this.btn5stars.Name = "btn5stars";
            this.btn5stars.Size = new System.Drawing.Size(80, 76);
            this.btn5stars.TabIndex = 7;
            this.btn5stars.Tag = "5";
            this.btn5stars.UseVisualStyleBackColor = false;
            this.btn5stars.Click += new System.EventHandler(this.StarButtonClick);
            this.btn5stars.MouseEnter += new System.EventHandler(this.ButtonMouseHover);
            this.btn5stars.MouseLeave += new System.EventHandler(this.ButtonMouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(62, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(384, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rate your experience with your tutor:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 342);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Poor";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 342);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "Average";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(393, 342);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "Excellent";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(429, 63);
            this.label5.TabIndex = 5;
            this.label5.Text = "You have been logged off.\r\nPlease take a moment to let us know how your session w" +
    "ent.\r\nThis survey is optional.";
            // 
            // btnNope
            // 
            this.btnNope.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNope.Location = new System.Drawing.Point(178, 75);
            this.btnNope.Name = "btnNope";
            this.btnNope.Size = new System.Drawing.Size(150, 36);
            this.btnNope.TabIndex = 1;
            this.btnNope.Text = "No Thanks";
            this.btnNope.UseVisualStyleBackColor = true;
            this.btnNope.Click += new System.EventHandler(this.btnNope_Click);
            // 
            // lblCountdown
            // 
            this.lblCountdown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCountdown.AutoSize = true;
            this.lblCountdown.Location = new System.Drawing.Point(469, 9);
            this.lblCountdown.Name = "lblCountdown";
            this.lblCountdown.Size = new System.Drawing.Size(28, 21);
            this.lblCountdown.TabIndex = 7;
            this.lblCountdown.Text = "30";
            this.lblCountdown.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Location = new System.Drawing.Point(178, 379);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(150, 36);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pnlHowHeard
            // 
            this.pnlHowHeard.Controls.Add(this.cboHeardAbout);
            this.pnlHowHeard.Controls.Add(this.label6);
            this.pnlHowHeard.Location = new System.Drawing.Point(16, 144);
            this.pnlHowHeard.Name = "pnlHowHeard";
            this.pnlHowHeard.Size = new System.Drawing.Size(481, 75);
            this.pnlHowHeard.TabIndex = 8;
            this.pnlHowHeard.Visible = false;
            // 
            // cboHeardAbout
            // 
            this.cboHeardAbout.FormattingEnabled = true;
            this.cboHeardAbout.Location = new System.Drawing.Point(26, 29);
            this.cboHeardAbout.Name = "cboHeardAbout";
            this.cboHeardAbout.Size = new System.Drawing.Size(424, 29);
            this.cboHeardAbout.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(138, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(204, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "How did you hear about us?";
            // 
            // frmLogoffSurvey
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnNope;
            this.ClientSize = new System.Drawing.Size(509, 427);
            this.Controls.Add(this.pnlHowHeard);
            this.Controls.Add(this.lblCountdown);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnNope);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn5stars);
            this.Controls.Add(this.btn3stars);
            this.Controls.Add(this.btn4stars);
            this.Controls.Add(this.btn2stars);
            this.Controls.Add(this.btn1star);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmLogoffSurvey";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLogoffSurvey";
            this.Load += new System.EventHandler(this.frmLogoffSurvey_Load);
            this.pnlHowHeard.ResumeLayout(false);
            this.pnlHowHeard.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn1star;
        private System.Windows.Forms.Button btn2stars;
        private System.Windows.Forms.Button btn3stars;
        private System.Windows.Forms.Button btn4stars;
        private System.Windows.Forms.Button btn5stars;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnNope;
        private System.Windows.Forms.Label lblCountdown;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel pnlHowHeard;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboHeardAbout;
    }
}