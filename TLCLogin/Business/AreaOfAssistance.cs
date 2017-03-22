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
