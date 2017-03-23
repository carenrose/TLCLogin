using System;
using System.Drawing;
using System.Windows.Forms;

namespace TLCLogin.View
{
    public partial class frmPasswordPrompt : Form
    {
        public bool IsAdminPassword { get; set; }
        public frmMdiContainer PseudoParent { get; set; }

        public frmPasswordPrompt()
        {
            InitializeComponent();
        }
        
        private void frmPasswordPrompt_Load(object sender, EventArgs e)
        {
            if (IsAdminPassword)
            {
                txtUser.Visible = true;
                lblInstructions.Text = "Please enter username and password:";
            }
        }

        /// <summary>
        /// Draws a border around the form
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // bevel
            // https://social.msdn.microsoft.com/Forums/windows/en-US/21c8a0e9-1571-4f6a-9589-b5267021ccf3/how-do-i-make-the-panel-beveled?forum=winforms
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            ControlPaint.DrawBorder(e.Graphics, rect, Color.White, ButtonBorderStyle.Outset);
            base.OnPaint(e);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            string enteredUsername = txtUser.Text;
            string enteredPassword = txtPassword.Text;

            try
            {
                if (IsAdminPassword)
                {
                    // SIMPLE LOGIN
                    if (Data.TLCLoginDA.SimpleAdminLoginIsEnabled() &&
                        Data.TLCLoginDA.GetSimpleAdminLogin()[0] == enteredUsername &&
                        Data.TLCLoginDA.GetSimpleAdminLogin()[1] == enteredPassword)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    // NORMAL LOGIN
                    else if (Data.TLCLoginDA.IsValidAdminUsername(enteredUsername) &&
                             Data.WindowsAuthenticator.Authenticate(enteredUsername, enteredPassword))
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        LoginError();
                    }
                }
                else
                {
                    // THIS IS THE NON-ADMIN PASSWORD
                    if (enteredPassword == Data.TLCLoginDA.GetByePassword())
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        LoginError();
                    }
                }
            }
            catch (Exception ex)
            {
                PseudoParent.ShowExceptionMessage(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void LoginError()
        {
            txtPassword.Clear();
            lblError.Visible = true;
            if (Control.IsKeyLocked(Keys.CapsLock)) lblInstructions.Text = "Caps Lock is on.";
            txtUser.Focus();
        }
    }
}
