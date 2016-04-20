using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;
using Model.VHDLSetcions;
using Model.VHDLSetcions.Maps.Assignments.AssignmentSides;
using Model.VHDLSetcions.Signals;
using Model.VHDLSetcions.Signals.AssignmentSignals;

namespace Model.Services.Parsers
{
    public class AssgnSinalParser
    {
        public static AssignmentSignal Parse(AssignmentSide parent, string text)
        {
            AssignmentSignal signal =  PartialSignal.Parse(parent, text);
            if (signal == null)
            {
                signal = AssignmentSignal.Parse(parent, text);
            }
            return signal;
        }
    }
}
