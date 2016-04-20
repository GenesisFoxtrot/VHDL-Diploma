using System;

namespace Model.VHDLSetcions.Signals.Enumerations
{
    public abstract class EnumerationBase : ICloneable
    {
        public abstract int Bits {  get; }

        public abstract int Bottom { get; }
        public abstract int Top { get; }

        public abstract bool IsSameBus(EnumerationBase enumeration);
        public abstract object Clone();
        public EnumerationBase CloneEnumeration()
        {
            return Clone() as EnumerationBase;
        }
    }
}
 