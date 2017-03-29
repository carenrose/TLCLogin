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
