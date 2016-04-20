using System.Collections.Generic;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.AssignmentSignals;

namespace Diploma.VHDLWrapper.VHDLSetcions.Signals
{
    public class SignalEntity
    {
        public SignalDefenition Defenition { get; set; }

        public List<AssignmentSignal> Signals { get; set; }

        public SignalEntity(SignalDefenition defenition)
        {
            Signals = new List<AssignmentSignal>();
            Defenition = defenition;
        }

        public void AddSignal(AssignmentSignal signal)
        {
            Signals.Add(signal);
        }


    }
}
