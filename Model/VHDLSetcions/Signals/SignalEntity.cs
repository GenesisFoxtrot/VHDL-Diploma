using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.VHDLSetcions.Signals.AssignmentSignals;

namespace Model.VHDLSetcions.Signals
{
    public class SignalEntity
    {
        public SignalDefenition Defenition { get; set; }

        private List<AssignmentSignal> Signals;

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
