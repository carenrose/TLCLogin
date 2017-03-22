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
    public partial class frmMessage : Form
    {
        private string message;
        public string Message
        {
            get { return message; }
            set {
                message = value;
                this.lblMessage.Text = value;
            }
        }

        private bool isTimeout;
        public bool IsTimeoutMessage
        {
            get { return isTimeout; }
            set {
                isTimeout = value;
                if (isTimeout)
                {
                    this.Timer = new Timer();
                    Timer.Interval = 10000;
                    Timer.Tick += new EventHandler(timer_Tick);
                    Timer.Start();
                }
            }
        }

        public Color TextColor { get; set; }
        private Timer Timer { get; set; }



        public frmMessage()
        {
            InitializeComponent();
        }

        private void frmMessage_Load(object sender, EventArgs e)
        {
            if (this.TextColor == null) this.TextColor = SystemColors.ControlText;
            lblMessage.ForeColor = TextColor;
        }

        /// <summary>
        /// Draws a border around the form so it does not blend in to background
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
            this.Close();
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

