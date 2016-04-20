using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.AssignmentSignals;

namespace Diploma.VHDLExtensions.VHDLEnities.Decoder
{
    public class DecoderSignalCode
    {
        public DecoderSignalCode(AssignmentSignal signal, string code)
        {
            Signal = signal;
            ActivaionCode = code;
        }
        public AssignmentSignal Signal { get; set; }
        public string ActivaionCode { get; set; }
    }
}
