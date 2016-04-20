namespace Diploma.VHDLWrapper.VHDLSetcions.Signals
{
    public class SignalDefenition : Signal
    {
        public string ValueType { get; set; }
        public string DefaultValue { get; set; }
        public Entity Entity { get; set; }
        public override IVHDLSection ParentSection => Entity;
        public override int Bits => Enumeration?.Bits ?? 1;

        //public PartialSignal CreatePartial(VHDLSection parent, EnumerationBase enumeration)
        //{
           // return new PartialSignal(parent, this, Enumeration);
        //}
        public SignalDefenition(Entity entity) 
        {
            Entity = entity;
        }

        public SignalDefenition(Entity entity, Signal signal) 
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
