using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diploma.VHDLWrapper.VHDLSetcions.Maps;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.AssignmentSignals;

namespace Diploma.LUTWatermarking
{
    public class WatermarkBit
    {
        public bool IsOne { get; set; }
        public MapLUT LUT { get; set; }
        public AssignmentSignal RealSignal { get; set; }
        public AssignmentSignal Signal { get; set; }
    }
}
