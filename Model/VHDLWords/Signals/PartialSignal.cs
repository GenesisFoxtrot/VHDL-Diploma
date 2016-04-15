using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.VHDLWords.Enumerations;

namespace Model.VHDLWords.Signals
{
    public class PartialSignal : Signal
    {
        public PartialSignal(FullSignal parentSignal, EnumerationBase enumeration)
        {
            Name = parentSignal.Name;
            ParentSinal = parentSignal;
            Enumeration = enumeration;
        }

        public FullSignal ParentSinal { get; }
    }
}
