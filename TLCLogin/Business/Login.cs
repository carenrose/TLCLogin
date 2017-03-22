using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLCLogin.Business
{
    public class Login
    {
        private int id;
        private Student student;
        private DateTime timeIn;
        private DateTime timeOut;
        private Course course;
        private AreaOfAssistance areaOfAssistance;
        private System.Timers.Timer timer;

        //public delegate void ThisIsAMethodSignature();
        //public event ThisIsAMethodSignature EventName;

        public delegate void AutoLogoffHandler(Login login);
        public event AutoLogoffHandler AutoLogoff;

        public int ID { get { return id; } set { id = value; } }

        public Student Student
        {
            get { return student; }
            set {
                if (value != null) student = value;
                else throw new ArgumentNullException("A valid Student object must be supplied.");
            }
        }

        public DateTime TimeIn
        {
            get { return timeIn; }
            set {
                if (value > DateTime.MinValue)
                {
                    timeIn = value;
                    if (timer != null) timer.Start();
                }
                else throw new ArgumentOutOfRangeException("The time in must be supplied.");
            }
        }

        public DateTime TimeOut
        {
            get { return timeOut; }
            set
            {
                if (value > DateTime.MinValue)
                {
                    if (value > this.TimeIn)
                    {
                        timeOut = value;
                        if (timer != null) timer.Stop();
                    }
                    else throw new ArgumentOutOfRangeException("The time out cannot be earlier than the time in.");
                }
                else
                {
                    throw new ArgumentOutOfRangeException("The time out must be a valid time.");
                }
            }
        }

        public Course Course
        {
            get { return course; }
            set { course = value; }
        }

        public AreaOfAssistance AreaOfAssistance
        {
            get { return areaOfAssistance; }
            set { areaOfAssistance = value; }
        }

        

        public Login()
        {
            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 60 * 1000;     // every minute
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (AreaOfAssistance != null)
            {
                // check time
                DateTime autoTime = TimeIn.AddMinutes(AreaOfAssistance.AutoLogoffLength);
                if (DateTime.Now >= autoTime)
                {
                    TimeOut = DateTime.Now;

                    // trigger event
                    if (AutoLogoff != null) AutoLogoff(this);
                }

            }
        }
    }
}
