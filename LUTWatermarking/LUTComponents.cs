using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diploma.VHDLWrapper.VHDLSetcions;

namespace Diploma.LUTWatermarking
{
    public class LUTComponents
    {
        public List<LUT> LUTs { get; set; }
        public VHDLDocument Document { get;  }

        public LUTComponents(VHDLDocument document)
        {
            Document = document;
            LUTs = document.Components.Where(LUT.IsLUT).Select(LUT.FromEntity)
                .OrderBy(lut => lut.Bits).ToList();
        }

        public LUT GetBigerLut(LUT lut)
        {
            return LUTs.FirstOrDefault(l => lut.Bits < l.Bits 
                && lut.Postfix == l.Postfix 
                && lut.Prefix  == l.Prefix);
        }


    }
}
