using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Model.VHDLWords.Enumerations;

namespace Model.VHDLWords.Signals
{
    public class FullSignal : Signal
    {
        public string ValueType { get; set; }
        public string DefaultValue { get; set; }

        public PartialSignal CreatePartial(EnumerationBase enumeration)
        {
            return new PartialSignal(this, Enumeration);
        }

        public string ToDefenitionVHDL()
        {
            return "  signal " + Name + " : " + ValueType + (" " + Enumeration ?? "") + ";\n";
        }
    }
}
