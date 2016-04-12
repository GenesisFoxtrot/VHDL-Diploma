using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.VHDLWords;

namespace Model
{
    public class Signal
    {
        public string Name { get; set; }
        public string ValueType { get; set; }
        public string DefaultValue { get; set; }
        public Enumeration Enumeration { get; set; }

        public int Bits => Enumeration == null ? 1 : Math.Abs(Enumeration.To - Enumeration.From) + 1;
    }
}
