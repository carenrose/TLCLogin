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
    public class AreaOfAssistance
    {
        public int    ID                  { get; set; }
        public string Name                { get; set; }
        public int    AutoLogoffLength    { get; set; }     // TODO validation      "The auto-logoff length must be greater than zero."
        public bool   SkipCourseSelection { get; set; }

        public override string ToString()
        {
            return $"{this.Name}: {this.AutoLogoffLength} minutes. " +
                   $"{(this.SkipCourseSelection ? "Skips" : "Does not skip")} course selection.";
        }
    }
}
