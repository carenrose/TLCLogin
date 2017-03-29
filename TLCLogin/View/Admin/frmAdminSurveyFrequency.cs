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

namespace TLCLogin.View.Admin
{
    public partial class frmAdminSurveyFrequency : Form
    {

        public frmMdiContainer PseudoParent { get; set; }

        public frmAdminSurveyFrequency()
        {
            InitializeComponent();
        }
        
        private void frmAdminSurveyFrequency_Load(object sender, EventArgs e)
        {
            int perc = Data.TLCLoginDA.GetSurveyFrequencyPercent();
            if (perc > -1)
            {
                txtPercent.Text = perc + "";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                int entered;

                if (Int32.TryParse(txtPercent.Text, out entered))
                {
                    if (entered >= 0 && entered <= 100)
                    {
                        Data.TLCLoginDA.SetSurveyFrequencyPercent(entered);
                        this.Close();
                    }
                    else
                    {
                        PseudoParent.ShowMessage("Number must be between 0 and 100 (inclusive).");
                        txtPercent.Focus();
                    }
                } 
                else
                {
                    PseudoParent.ShowMessage("Enter numbers only, no decimal places.");
                    txtPercent.Focus();
                }
            }
            catch (Exception ex)
            {
                PseudoParent.ShowExceptionMessage(ex);
            }
        }
    }
}
