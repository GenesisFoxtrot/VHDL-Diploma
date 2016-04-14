using System;
using Model.Options;

namespace Model.VHDLWords
{
    public class Signal
    {
        public string Name { get; set; }
        public string ValueType { get; set; }
        public string DefaultValue { get; set; }
        public Enumeration Enumeration { get; set; }
        public virtual int Bits => Enumeration == null ? 1 : Math.Abs(Enumeration.To - Enumeration.From) + 1;

        public override string ToString()
        {
            return Name;
        }
    }
}
