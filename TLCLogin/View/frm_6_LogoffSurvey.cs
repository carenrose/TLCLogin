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
        private int starsClicked;

        public frmLogoffSurvey()
        {
            InitializeComponent();
        }

        private void frmLogoffSurvey_Load(object sender, EventArgs e)
        {
            parent = (frmMdiContainer)this.MdiParent;
            
            // star buttons
            starBtns = new Button[5];
            starBtns[0] = btn1star;
            starBtns[1] = btn2stars;
            starBtns[2] = btn3stars;
            starBtns[3] = btn4stars;
            starBtns[4] = btn5stars;
            starsClicked = -1;

            // if first visit (this quarter)
            if (parent.Controller.CurrentLogin != null && parent.Controller.CurrentLogin.Student != null &&
                Data.TLCLoginDA.IsFirstTimeStudent(parent.Controller.CurrentLogin.Student.StudentID)) {

                pnlHowHeard.Visible = true;

                cboHeardAbout.DataSource = Data.TLCLoginDA.GetAllSurveyAdPlaces()
                    .Select(kv => new { Key = kv.Key, Value = kv.Value }).ToArray();
                cboHeardAbout.ValueMember = "Key";
                cboHeardAbout.DisplayMember = "Value";
            }

            // countdown timer
            countdown = 30;
            this.Timer = new Timer();
            Timer.Interval = 1000;
            Timer.Tick += new EventHandler(timer_Tick);
            Timer.Start();
            lblCountdown.Text = countdown.ToString();
        }
        
        protected void ButtonMouseHover(object sender, EventArgs e)
        {
            if (starsClicked == -1)
            {
                Button s = (Button)sender;
                for (int i = 0; i < Convert.ToInt16(s.Tag); i++)
                {
                    starBtns[i].BackgroundImage = Properties.Resources.star_yellow;
                }
            }
        }

        protected void ButtonMouseLeave(object sender, EventArgs e)
        {
            if (starsClicked == -1)
            {
                foreach (Button b in starBtns)
                {
                    b.BackgroundImage = Properties.Resources.star_grey;
                }
            }
        }

        protected void StarButtonClick(object sender, EventArgs e)
        {
            starsClicked = -1;
            Button s = (Button)sender;
            ButtonMouseHover(sender, e);
            starsClicked = Convert.ToInt16(s.Tag);
        }
        

        private void btnNope_Click(object sender, EventArgs e)
        {
            if (this.Timer != null) this.Timer.Stop();
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            int adplace = (cboHeardAbout.SelectedIndex > -1) ?
                Convert.ToInt16(cboHeardAbout.SelectedValue) :
                -1;
            
            // do 
            parent.Controller.Survey(starsClicked, adplace);
            
            // then close
            this.btnNope.PerformClick();
        }
    }
}
