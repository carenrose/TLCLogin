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
    public class Course
    {
        private string letterCode;
        private int numberCode;
        private string courseTitle;

        public string LetterCode
        {
            get { return letterCode; }
            set {
                if (value.Length > 0 && value.Length <= 4) letterCode = value;
                else throw new ArgumentException("The course letter code must be 4 letters or less.");
            }
        }

        public int NumberCode
        {
            get { return numberCode; }
            set {
                if (value >= 0 && value <= 9999) numberCode = value;
                else throw new ArgumentOutOfRangeException("The course number code must be 4 numbers or less");
            }
        }

        public string CourseTitle
        {
            get { return courseTitle; }
            set { courseTitle = value; }
        }

        public string Display { get { return ToString(); } }



        public Course()
        {

        }

        public Course(string category, int code)
        {
            this.LetterCode = category;
            this.NumberCode = code;
        }

        public override string ToString()
        {
            return LetterCode + "-" + NumberCode + ": " + CourseTitle;
        }
    }
}
