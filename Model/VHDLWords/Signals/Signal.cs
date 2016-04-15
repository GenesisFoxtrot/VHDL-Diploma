﻿using Model.VHDLWords.Enumerations;

namespace Model.VHDLWords.Signals
{
    public class Signal
    {
        public string Name { get; set; }
        public EnumerationBase Enumeration { get; set; }
        public virtual int Bits => Enumeration?.Bits ?? 1 ;

        public override string ToString()
        {
            return Name;
        }
    }
}