namespace TLCLogin.View.Admin
{
    partial class frmAdminViewLogins
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
            this.btnLogOffAll = new System.Windows.Forms.Button();
            this.dgvLogins = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogins)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogOffAll
            // 
            this.btnLogOffAll.Location = new System.Drawing.Point(619, 293);
            this.btnLogOffAll.Name = "btnLogOffAll";
            this.btnLogOffAll.Size = new System.Drawing.Size(131, 36);
            this.btnLogOffAll.TabIndex = 1;
            this.btnLogOffAll.Text = "Log Off All";
            this.btnLogOffAll.UseVisualStyleBackColor = true;
            this.btnLogOffAll.Click += new System.EventHandler(this.btnLogOffAll_Click);
            // 
            // dgvLogins
            // 
            this.dgvLogins.AllowUserToAddRows = false;
            this.dgvLogins.AllowUserToDeleteRows = false;
            this.dgvLogins.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLogins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogins.Location = new System.Drawing.Point(12, 12);
            this.dgvLogins.Name = "dgvLogins";
            this.dgvLogins.ReadOnly = true;
            this.dgvLogins.RowHeadersVisible = false;
            this.dgvLogins.Size = new System.Drawing.Size(738, 275);
            this.dgvLogins.TabIndex = 2;
            this.dgvLogins.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLogins_CellContentClick);
            this.dgvLogins.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvLogins_CellFormatting);
            // 
            // frmAdminViewLogins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 339);
            this.Controls.Add(this.dgvLogins);
            this.Controls.Add(this.btnLogOffAll);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frmAdminViewLogins";
            this.Text = "Current Logins";
            this.Load += new System.EventHandler(this.frmAdminViewLogins_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogins)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnLogOffAll;
        private System.Windows.Forms.DataGridView dgvLogins;
    }
}