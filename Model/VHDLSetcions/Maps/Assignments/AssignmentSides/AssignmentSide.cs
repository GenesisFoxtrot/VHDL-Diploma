using System.Collections;
using Model.Services.Parsers;
using Model.VHDLSetcions.Signals;
using Model.VHDLSetcions.Signals.AssignmentSignals;

namespace Model.VHDLSetcions.Maps.Assignments.AssignmentSides
{
    public class AssignmentSide : AssignmentSideBase
    {
        public AssignmentSignal Signal { get; set; }
        public ConstValue Value { get; set; }

        public string SignalName => Signal == null ? "Const" : Signal.Name;

        public int Bits => Signal == null ? Value.Bits : Signal.Bits;

        private AssignmentSide(Assignment assignment, string text) : base(assignment, text)
        {
            Text = text;
            Assignment = assignment;
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
    }
}
