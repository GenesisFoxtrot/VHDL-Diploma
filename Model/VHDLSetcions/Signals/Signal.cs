using System.Text.RegularExpressions;
using Model.Services;
using Model.VHDLSetcions.Signals.Enumerations;

namespace Model.VHDLSetcions.Signals
{
    public abstract class Signal : VHDLSection
    {
        public readonly VHDLSection _parent;
        public override VHDLSection ParentSection => _parent;

        public string Name { get; set; }
        public EnumerationBase Enumeration { get; set; }

        public abstract int Bits { get; }

        protected Signal(VHDLSection parent)
        {
            _parent = parent;
        }

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
