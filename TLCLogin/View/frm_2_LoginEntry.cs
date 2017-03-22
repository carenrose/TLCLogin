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
    public partial class frmLoginEntry : Form
    {
        private frmMdiContainer parent;


        public frmLoginEntry()
        {
            InitializeComponent();
        }

        private void frmLoginEntry_Load(object sender, EventArgs e)
        {
            parent = (frmMdiContainer)this.MdiParent;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                if (parent.Controller.CheckLoginStatus(txtStudentID.Text))
                    parent.GoToNextScreen();
            }
            catch (Exception ex)
            {
                parent.ShowMessage("Error: " + ex.Message);
            }
        }

    }
}
