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

namespace TLCLogin.Models
{
    public class Course
    {
        public string LetterCode  { get; set; }     // TODO "The course letter code must be 4 letters or less."
        public int    NumberCode  { get; set; }     // TODO "The course number code must be 4 numbers or less"
        public string CourseTitle { get; set; }

        public Course(string category, int code)
        {
            this.LetterCode = category;
            this.NumberCode = code;
        }

        public override string ToString()
        {
            return $"{this.LetterCode}-{this.NumberCode}: {this.CourseTitle}";
        }
    }
}
