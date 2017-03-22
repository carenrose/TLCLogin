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
