using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model.VHDLWords
{
    public class SignalBit : Signal
    {
        public int BitNubmer { get; set; }
        public override int Bits => 1;

        public override string ToString()
        {
            return Name + "(" + BitNubmer+ ")";
        }
    }
}
