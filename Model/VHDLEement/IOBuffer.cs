using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.VHDLSetcions.Maps;
using Model.VHDLSetcions.Maps.Assignments;
using Model.VHDLSetcions.Signals;
using Model.VHDLSetcions.Signals.AssignmentSignals;

namespace Model.VHDLEement
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

            var result = new IOBuffer(map.Assigmnets.FirstOrDefault(c => c.Left.Text.Contains(inside)).Right.Signal,
                    map.Assigmnets.FirstOrDefault(c => c.Left.Text.Contains(outside)).Right.Signal);

            result.IsPartial = result.OutsideSignal is PartialSignal;
            return result;
        }
    }
}
