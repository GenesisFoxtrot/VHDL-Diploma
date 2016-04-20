using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.AssignmentSignals;

namespace Diploma.VHDLWrapper.VHDLSetcions.Signals
{
    public interface ISignalsParentSection : IVHDLSection
    {
        void ChangeSignal(AssignmentSignal signals);

        Entity Entity { get;  }

        string EntityName { get; }

    }
}
