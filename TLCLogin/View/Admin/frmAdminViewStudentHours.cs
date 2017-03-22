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
    public partial class frmAdminViewStudentHours : Form
    {
        public frmMdiContainer PseudoParent { get; set; }

        public frmAdminViewStudentHours()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                int studentID;
                if (Int32.TryParse(txtStudentID.Text, out studentID))
                {
                    double hours = Data.TLCLoginDA.GetTutoredHoursByStudent(studentID);
                    txtHours.Text = hours.ToString("N1");
                }
                else
                {
                    PseudoParent.ShowMessage("Enter only numbers in the Student ID field.");
                }
            }
            catch (Exception ex)
            {
                PseudoParent.ShowExceptionMessage(ex);
            }
        }

        private void txtStudentID_TextChanged(object sender, EventArgs e)
        {
            txtHours.Clear();
        }
    }
}
