//  TutoringLogin
//  Author: Brianna Williams <williams.brianna.k.0607@gmail.com>
//  Copyright (c) 2020 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Timers;

namespace TLCLogin.Models
{
    public class UserLoginModel
    {
        public int      ID      { get; set; }
        public DateTime TimeIn  { get; set; }   // TODO start timer if valid date
        public DateTime TimeOut { get; set; }   // TODO stop timer if valid date (later than TimeIn)

        public Student          Student          { get; set; }  // validate not null
        public Course           Course           { get; set; }
        public AreaOfAssistance AreaOfAssistance { get; set; }

        private Timer Timer { get; set; } = new Timer(60 * 1000);        // every minute

        public delegate void AutoLogoffHandler(UserLoginModel login);
        public event AutoLogoffHandler AutoLogoff;

        // TODO pass in things
        public UserLoginModel()
        {
            this.Timer.Elapsed += Timer_Elapsed;
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var now = DateTime.Now;
            if (this.AreaOfAssistance != null)
            {
                // check time
                if (now >= this.TimeIn.AddMinutes(this.AreaOfAssistance.AutoLogoffLength))
                {
                    this.TimeOut = now;

                    // log off event
                    this.AutoLogoff?.Invoke(this);
                }
            }
        }

    }
}
