using System.Collections.Generic;
using Diploma.VHDLExtensions.DocumentExtensions.IOBuffers;
using Diploma.VHDLWrapper.VHDLSetcions;

namespace Diploma.LUTWatermarking.Options
{
    public class WatermarkOptions
    {
        public WatermarkOptions(VHDLDocument document )
        {
            WatermarkSettings = new List<InWatermarkSettings>();
            SignatureOutputSettings = new List<OutWatermarkSettings>();
            WotermarikingDocument = new WotermarikingDocument(document);
            WotermarikingDocument.Parse();
        }

        public List<InWatermarkSettings> WatermarkSettings { get; private set; }
        public List<OutWatermarkSettings> SignatureOutputSettings { get; private set; }
        public IOBuffesLayer IOBuffesLayer { get; set; }
        public WotermarikingDocument WotermarikingDocument { get; private set; }
        public bool[] Signature { get; set; }
    }
}
