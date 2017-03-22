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
    public partial class frmAdminChangeByePassword : Form
    {
        public frmMdiContainer PseudoParent { get; set; }

        public frmAdminChangeByePassword()
        {
            InitializeComponent();
        }

        private void frmAdminChangeByePassword_Load(object sender, EventArgs e)
        {
            try
            {
                txtPass.Text = Data.TLCLoginDA.GetByePassword();
            }
            catch (Exception ex)
            {
                PseudoParent.ShowExceptionMessage(ex);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtPass.Text.Length > 0)
            {
                try
                {
                    Data.TLCLoginDA.SetByePassword(txtPass.Text);
                    this.Close();
                }
                catch (Exception ex)
                {
                    PseudoParent.ShowExceptionMessage(ex);
                }
            }
            else
            {
                PseudoParent.ShowMessage("A password is required.");
            }
        }
    }
}
