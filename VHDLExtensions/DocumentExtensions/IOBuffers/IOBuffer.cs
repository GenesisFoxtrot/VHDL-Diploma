using System.Linq;
using Diploma.VHDLWrapper.VHDLSetcions.Maps;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.AssignmentSignals;

namespace Diploma.VHDLExtensions.DocumentExtensions.IOBuffers
{
    public class IOBuffer
    {
        private IOBuffer(AssignmentSignal inside, AssignmentSignal outside)
        {
            InsideSignal = inside;
            OutsideSignal = outside;
        }
        private bool IsPartial { get; set; }
        public AssignmentSignal InsideSignal { get; set; }
        public AssignmentSignal OutsideSignal { get; set; }

        public static IOBuffer Extract(Map map, bool isInputPort)
        {
            var inside = isInputPort ? "I" : "O";
            var outside = isInputPort ? "O" : "I";

            var insideSignal = map.Assigmnets.FirstOrDefault(c => c.Left.Text.Contains(inside)).Right.Signal;
            var outsideSignal = map.Assigmnets.FirstOrDefault(c => c.Left.Text.Contains(outside)).Right.Signal;
            var result = new IOBuffer(insideSignal, outsideSignal);

            result.IsPartial = result.OutsideSignal is PartialSignal;
            return result;
        }
    }
}
