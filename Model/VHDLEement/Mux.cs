using Model.VHDLSetcions;
using Model.VHDLSetcions.Signals;

namespace Model.VHDLEement
{
    public class Mux : VHDLSection
    {
        private VHDLDocument _document;
        public override VHDLDocument Document => _document;
        public override VHDLSection ParentSection => Document;
        public Signal To { get;  set; }
        public string Condition { get;  set; }
        public Signal Then { get; set; }
        public Signal Else { get; set; }


        public Mux()
        {
            
        }


        public Mux(Signal to, string condition, Signal then, Signal @else)
        {
            To = to;
            Condition = condition;
            Then = then;
            Else = @else;
        }

        public bool Insert(VHDLDocument document)
        {   
            if (Document == null)
            {
                _document = document;
                document.AddVHDLInBehaviorSection(this.ToString());
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return "  " + To + " <= " + Then + " when (" + Condition + ")  else " + Else + ";\n";
        }
    }
}
