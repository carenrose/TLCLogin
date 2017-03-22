using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLCLogin.Business
{
    public class SpecialProgram
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public string Display { get { return this.ToString(); } }

        public SpecialProgram() { }

        public override string ToString()
        {
            string ts = Name + ", ";
            ts += (IsEnabled) ? "is enabled" : "is disabled";
            return ts;
        }

    }
}
