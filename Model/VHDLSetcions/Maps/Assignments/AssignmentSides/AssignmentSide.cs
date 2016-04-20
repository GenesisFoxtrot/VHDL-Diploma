using Diploma.VHDLWrapper.Services;
using Diploma.VHDLWrapper.Services.Parsers;
using Diploma.VHDLWrapper.VHDLSetcions.Signals;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.AssignmentSignals;

namespace Diploma.VHDLWrapper.VHDLSetcions.Maps.Assignments.AssignmentSides
{
    public class AssignmentSide : AssignmentSideBase, ISignalsParentSection
    {
        public AssignmentSignal Signal { get; set; }
        public ConstValue Value { get; set; }

        public string SignalName => Signal == null ? "Const" : Signal.Name;
        public Entity Entity => Assignment.Map.Entity;
        public string EntityName => Assignment.Map.EntityName;
        public int Bits => Signal?.Bits ?? Value.Bits;

        private AssignmentSide(Assignment assignment, string text) : base(assignment, text)
        {
            Text = text;
            Assignment = assignment;
        }

        public void ChangeSignal(AssignmentSignal signal)
        {
            Change(signal.ToString());
        }

        public static AssignmentSide Parse(Assignment assignment, string text)
        {
            var side = new AssignmentSide(assignment, text);
            side.Value = ConstValue.Parse(side, text);
            if (side.Value == null)
            {
                side.Signal = AssgnSinalParser.Parse(side, side.Text);
                side.Document.Router.AddSignal(side.Signal);
            }
            return side;
        }

        public override string ToString()
        {
            return Signal?.ToString() ?? Value.ToString();
        }
    }
}
