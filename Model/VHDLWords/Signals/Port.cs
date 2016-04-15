namespace Model.VHDLWords.Signals
{
    public class Port : FullSignal
    {
        public PortTypes PortType { get; set; }

        public override string ToString()
        {
            return Name + "("+ Bits + " bits)";
        }
    }
}
