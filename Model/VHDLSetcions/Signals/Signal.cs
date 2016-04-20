using Diploma.VHDLWrapper.VHDLSetcions.Signals.Enumerations;

namespace Diploma.VHDLWrapper.VHDLSetcions.Signals
{
    public abstract class Signal : VHDLSection
    {
        public override IVHDLSection ParentSection { get; }

        public string Name { get; set; }
        public EnumerationBase Enumeration { get; set; }

        public abstract int Bits { get; }

        public bool IsSameWire(Signal anotherSignal)
        {
            if (Name == anotherSignal.Name && Bits == anotherSignal.Bits)
            {
                return Enumeration == null || Enumeration.IsSameBus(anotherSignal.Enumeration);
            }
            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
