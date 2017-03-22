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
    public partial class frmLogoffSurvey : Form
    {
        Button[] starBtns;
        private Timer Timer { get; set; }
        private int countdown;
        private frmMdiContainer parent;


        public frmLogoffSurvey()
        {
            InitializeComponent();

            countdown = 30;
            this.Timer = new Timer();
            Timer.Interval = 1000;
            Timer.Tick += new EventHandler(timer_Tick);
            Timer.Start();
        }

        private void frmLogoffSurvey_Load(object sender, EventArgs e)
        {
            parent = (frmMdiContainer)this.MdiParent;

            lblCountdown.Text = countdown.ToString();

            starBtns = new Button[5];
            starBtns[0] = btn1star;
            starBtns[1] = btn2stars;
            starBtns[2] = btn3stars;
            starBtns[3] = btn4stars;
            starBtns[4] = btn5stars;
        }
        
        protected void ButtonMouseHover(object sender, EventArgs e)
        {
            Button s = (Button)sender;
            for (int i = 0; i < Convert.ToInt16(s.Tag); i++)
            {
                starBtns[i].BackgroundImage = Properties.Resources.star_yellow;
            }
        }

        protected void ButtonMouseLeave(object sender, EventArgs e)
        {
            foreach (Button b in starBtns)
            {
                b.BackgroundImage = Properties.Resources.star_grey;
            }
        }

        protected void ButtonClick(object sender, EventArgs e)
        {
            Button s = (Button)sender;
            int stars = Convert.ToInt16(s.Tag);

            // do 
            parent.Controller.Survey(stars);

            // then close
            this.btnNope.PerformClick();
        }
        

        private void btnNope_Click(object sender, EventArgs e)
        {
            parent.StartOver();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            countdown--;
            lblCountdown.Text = countdown.ToString();
            if (countdown <= 0)
            {
                this.btnNope.PerformClick();
            }
        }
    }
}
