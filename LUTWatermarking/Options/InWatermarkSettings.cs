using Diploma.VHDLWrapper.VHDLSetcions.Signals;

namespace Diploma.LUTWatermarking.Options
{
    public class InWatermarkSettings
    {
        public InWatermarkSettings(SignalDefenition signal)
        {
            Signal = signal;
        }
        public SignalDefenition Signal { get; set; }
        public bool IsUsed { get; set; }
        public string ActivaionCode { get; set; }
    }
}
