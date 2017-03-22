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
    public partial class frmSubjectArea : Form
    {
        private frmMdiContainer parent;

        public frmSubjectArea()
        {
            InitializeComponent();
        }

        private void frmSubjectArea_Load(object sender, EventArgs e)
        {
            parent = (frmMdiContainer)this.MdiParent;

            try
            {
                List<Business.AreaOfAssistance> areas = Data.TLCLoginDA.GetAllAreasOfAssistance();

                if (areas != null)
                {
                    int count = 0;
                    // 71, 124
                    int x = 71;
                    int y = 124;

                    foreach (Business.AreaOfAssistance a in areas)
                    {
                        Button b = new Button();
                        b.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        b.Size = new System.Drawing.Size(200, 39);
                        b.TabIndex = count;

                        b.Text = a.Name;
                        b.Tag = a;

                        if (count % 2 == 1) x += 200 + 13; else x = 71;
                        if (count % 2 == 0 && count > 0) y += 55;
                        b.Location = new System.Drawing.Point(x, y);

                        b.Click += AreaButton_Click;
                        this.Controls.Add(b);

                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                parent.ShowExceptionMessage(ex);
            }
        }

        private void AreaButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Business.AreaOfAssistance area = (Business.AreaOfAssistance)b.Tag;

            try
            {
                // do the thing
                parent.Controller.CreateLogin(area);

                if (area.SkipsCourseSelection)
                    parent.SkipCourseScreen();
                else
                    parent.GoToNextScreen();
            }
            catch (Exception ex)
            {
                parent.ShowExceptionMessage(ex);
            }
        }

        private void btnStartOver_Click(object sender, EventArgs e)
        {
            parent.StartOver();
        }
    }
}
