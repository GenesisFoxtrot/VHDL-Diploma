using Diploma.VHDLWrapper.VHDLSetcions.Signals;

namespace Diploma.LUTWatermarking.Options
{
    public class OutWatermarkSettings
    {
        public OutWatermarkSettings(SignalDefenition signal)
        {
            Signal = signal;
        }
        public SignalDefenition Signal { get; set; }
        public bool IsUsed { get; set; }
        public string SignatureCode { get; set; }
    }
}
