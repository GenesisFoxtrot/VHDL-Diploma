using System.Data;
using Model.VHDLSetcions.Signals.AssignmentSignals;
using Model.VHDLSetcions.Signals.Enumerations;

namespace Model.VHDLSetcions.Signals
{
    public class SignalDefenition : Signal
    {
        public string ValueType { get; set; }
        public string DefaultValue { get; set; }
        public Entity Entity { get; set; }
        public override VHDLSection ParentSection => Entity;
        public override int Bits => Enumeration?.Bits ?? 1;

        //public PartialSignal CreatePartial(VHDLSection parent, EnumerationBase enumeration)
        //{
           // return new PartialSignal(parent, this, Enumeration);
        //}
        public SignalDefenition(Entity entity) : base(entity)
        {
            Entity = entity;
        }

        public SignalDefenition(Entity entity, Signal signal) : base(entity)
        {
            Entity = entity;
            Name = signal.Name;
            Enumeration = signal.Enumeration;
        }

        public override string ToString()
        {
            return "  signal " + Name + " : " + ValueType + (" " + Enumeration ?? "") + ";\n";
        }
    }
}
