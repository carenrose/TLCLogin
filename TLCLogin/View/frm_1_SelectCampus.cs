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
using System.Windows.Forms;

namespace TLCLogin.View
{
    public partial class frmSelectCampus : Form
    {
        private frmMdiContainer parent;

        public frmSelectCampus()
        {
            InitializeComponent();
        }


        private void frmSelectCampus_Load(object sender, EventArgs e)
        {
            parent = (frmMdiContainer)this.MdiParent;
            try
            {
                // Load buttons
                Dictionary<int, string> campuses = Data.TLCLoginDA.GetCampuses();

                // if campuses were returned create buttons for them
                if (campuses != null)
                {
                    int count = 0;
                    int x = 26;     // determined by manual placement
                    int y = 68;     // determined by manual placement

                    foreach (KeyValuePair<int, string> c in campuses)
                    {
                        Button b = new Button();
                        b.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        b.Size = new System.Drawing.Size(157, 39);
                        b.TabIndex = count;

                        b.Text = c.Value;
                        b.Tag = c.Key;

                        // simple button alignment - numbers from manual placement
                        if (count % 2 == 1) x += 157 + 13; else x = 26;
                        if (count % 2 == 0 && count > 0) y += 55;
                        b.Location = new System.Drawing.Point(x, y);

                        b.Click += CampusButton_Click;
                        this.Controls.Add(b);

                        // increment
                        count++;
                    }

                    // recenter label
                    //label1.AutoSize = false;
                    //label1.Width = this.ClientSize.Width;
                    label1.Left = (this.ClientSize.Width - label1.Width) / 2;
                }
                else
                {
                    parent.ShowMessage("Error: No campuses were found.");
                }
            }
            catch (Exception ex)
            {
                parent.ShowExceptionMessage(ex);
            }
        }

        
        private void CampusButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            parent.SetCampus(Convert.ToInt16(b.Tag), b.Text);
            parent.GoToNextScreen();
        }
    }
}
