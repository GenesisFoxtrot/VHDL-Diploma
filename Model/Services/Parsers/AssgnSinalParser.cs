using Diploma.VHDLWrapper.VHDLSetcions.Maps.Assignments.AssignmentSides;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.AssignmentSignals;

namespace Diploma.VHDLWrapper.Services.Parsers
{
    public class AssgnSinalParser
    {
        public static AssignmentSignal Parse(AssignmentSide parent, string text)
        {
            AssignmentSignal signal = null;//PartialSignal.Parse(parent, text);
            if (signal == null)
            {
                signal = AssignmentSignal.Parse(parent, text);
            }
            return signal;
        }
    }
}
