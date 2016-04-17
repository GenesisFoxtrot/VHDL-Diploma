using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Options;
using Model.VHDLWords;
using Model.VHDLWords.Signals;

namespace Model.VHDLEement
{
    public class Decoder
    {
        public Decoder(Signal signalToReturnValue)
        {
            SignalToReturnValue = signalToReturnValue;
            CodedSignals = new List<InWatermarkSettings>();
        }

        public List<InWatermarkSettings> CodedSignals;

        public Signal SignalToReturnValue;


        public override string ToString()
        {
            var text = String.Empty;
            var concatedSignals = String.Empty;
            var comaSeparatedSignals = String.Empty;
            var code = String.Empty;
            var bits = 0;
            var outSignal = SignalToReturnValue == null? null :SignalToReturnValue.Name;
            if (CodedSignals.Any() && !String.IsNullOrEmpty(outSignal))
            {
                CodedSignals.ForEach(x =>
                {
                    concatedSignals += x.Port.Name + "  & ";
                    comaSeparatedSignals += x.Port.Name + ",";
                    bits += x.Port.Bits;
                    code += x.ActivaionCode;
                });
                concatedSignals = concatedSignals.Substring(0, concatedSignals.Length - 4);
                comaSeparatedSignals = comaSeparatedSignals.Substring(0, comaSeparatedSignals.Length - 1);

                const string newS = "\n  ";

                text =
                    newS +
                    "process(" + comaSeparatedSignals + ")" + newS +
                    "variable bcat : std_logic_vector(0 to " + (bits-1) + ");" + newS +
                    "begin" + newS +
                    "  bcat :=" + concatedSignals + ";" + newS +
                    "  case bcat is" + newS +
                    "      when \"" + code + "\" => "+ outSignal  + " <= '1';" + newS +
                    "      when others => " +          outSignal  + " <= '0';" + newS +
                    "  end case;"  + newS +
                    "end process;" + newS + "\n";
            }

            return text;

        }
    }
}
