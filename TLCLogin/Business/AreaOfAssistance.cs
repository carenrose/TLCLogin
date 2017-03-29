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
    public class AreaOfAssistance
    {
        private int id;
        private string name;
        private int autoLogoffLength;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int AutoLogoffLength
        {
            get { return autoLogoffLength; }
            set { if (value > 0) autoLogoffLength = value;
                  else throw new ArgumentOutOfRangeException("The auto-logoff length must be greater than zero.");
            }
        }

        public bool SkipsCourseSelection { get; set; }
        public string Display { get { return ToString(); } }

        public AreaOfAssistance()
        {

        }

        public override string ToString()
        {
            string ret = Name + ": " + AutoLogoffLength + " minutes. ";
            ret += (SkipsCourseSelection) ? "Skips " : "Does not skip ";
            ret += "course selection.";
            return ret;
        }
    }
}
