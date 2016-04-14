using Model.Options;

namespace Model.VHDLWords
{
    public class Port : Signal
    {
        public PortTypes PortType { get; set; }

        public override string ToString()
        {
            return Name + "("+ Bits + " bits)";
        }
    }
}
