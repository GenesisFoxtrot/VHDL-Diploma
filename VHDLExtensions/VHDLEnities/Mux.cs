using Diploma.VHDLWrapper.VHDLSetcions;
using Diploma.VHDLWrapper.VHDLSetcions.Signals;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.AssignmentSignals;

namespace Diploma.VHDLExtensions.VHDLEnities
{
    public class Mux : VHDLSection , ISignalsParentSection
    {
        private VHDLDocument _document;
        public override VHDLDocument Document => _document;
        public Entity Entity => _document.Entity;
        public string EntityName => _document.Entity.Name;
        public override IVHDLSection ParentSection => Document;
        public Signal To { get;  set; }
        public string Condition { get;  set; }
        public Signal Then { get; set; }
        public Signal Else { get; set; }


        public Mux()
        {
            
        }


        public Mux(AssignmentSignal to, string condition, AssignmentSignal then, AssignmentSignal @else)
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

        public void ChangeSignal(AssignmentSignal signal)
        {
            To = signal;
            Change(ToString()); 
        }

        public override string ToString()
        {
            return "  " + To + " <= " + Then + " when (" + Condition + ")  else " + Else + ";\n";
        }
    }
}
