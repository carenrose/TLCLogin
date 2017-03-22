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
