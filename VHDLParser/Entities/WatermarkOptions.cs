using System.Collections.Generic;
using Model.Options;
using Model.VHDLWords;

namespace VHDLParser.Entities
{
    public class WatermarkOptions
    {
        public WatermarkOptions()
        {
            WatermarkSettings = new List<InWatermarkSettings>();
            SignatureOutputSettings = new List<OutWatermarkSettings>();
        }

        public List<InWatermarkSettings> WatermarkSettings { get; }
        public List<OutWatermarkSettings> SignatureOutputSettings { get; } 
        public bool[] Signature { get; set; }
    }
}
