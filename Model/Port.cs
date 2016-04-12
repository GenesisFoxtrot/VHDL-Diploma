using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Port : Signal
    {
        public PortTypes PortType { get; set; }

        public override string ToString()
        {
            return Name + "("+ Bits + " bits)";
        }
    }
}
