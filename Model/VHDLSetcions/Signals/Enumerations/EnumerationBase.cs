using System;

namespace Diploma.VHDLWrapper.VHDLSetcions.Signals.Enumerations
{
    public abstract class EnumerationBase : ICloneable
    {
        public abstract int Bits {  get; }

        public abstract int Bottom { get; }
        public abstract int Top { get; }

        public abstract bool IsSameBus(EnumerationBase enumeration);
        public abstract object Clone();
        public abstract EnumerationBase GetBit(int num);
        public EnumerationBase CloneEnumeration()
        {
            return Clone() as EnumerationBase;
        }
    }
}
 