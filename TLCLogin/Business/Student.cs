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
    public class Student
    {
        private int studentID;
        private string firstName;
        private string lastName;
        private int nativeLanguage;
        private int countryOfOrigin;
        private string programOfStudy;
        private List<SpecialProgram> specialPrograms;

        public int StudentID
        {
            get { return studentID; }
            set {
                if (value >= 0 && value <= 999999999) studentID = value;
                else throw new ArgumentException("The student ID must be 1 to 9 digits.");
            }
        }
        public string FirstName
        {
            get { return firstName; }
            set {
                if (value.Length <= 255) firstName = value;
                else firstName = value.Substring(0, 255);
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (value.Length <= 255) lastName = value;
                else lastName = value.Substring(0, 255);
            }
        }
        public int NativeLanguage
        {
            get { return nativeLanguage; }
            set { nativeLanguage = value; }
        }
        public int CountryOfOrigin
        {
            get { return countryOfOrigin; }
            set { countryOfOrigin = value; }
        }
        public string ProgramOfStudy
        {
            get { return programOfStudy; }
            set { programOfStudy = value; }
        }
        public List<SpecialProgram> SpecialPrograms
        {
            get { return specialPrograms; }
            set { specialPrograms = value; }
        }

        public string Display { get { return this.ToString(); } }



        public Student()
        {
            this.SpecialPrograms = new List<SpecialProgram>();
        }


        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
