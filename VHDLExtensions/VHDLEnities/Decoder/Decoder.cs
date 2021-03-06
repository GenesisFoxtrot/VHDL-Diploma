﻿using System;
using System.Collections.Generic;
using System.Linq;
using Diploma.VHDLWrapper.VHDLSetcions;
using Diploma.VHDLWrapper.VHDLSetcions.Signals;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.AssignmentSignals;

namespace Diploma.VHDLExtensions.VHDLEnities.Decoder
{
    public class Decoder : VHDLSection, ISignalsParentSection
    {
        public override VHDLDocument Document { get; }
        public override IVHDLSection ParentSection => Document;

        public Entity Entity => Document.Entity;
        public string EntityName => Document.Entity.Name;
        public Decoder(VHDLDocument document, Signal signalToReturnValue)
        {
            Document = document;
            SignalToReturnValue = signalToReturnValue;
            CodedSignals = new List<DecoderSignalCode>();
        }

        public List<DecoderSignalCode> CodedSignals;

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
                    concatedSignals += x.Signal.Name + "  & ";
                    comaSeparatedSignals += x.Signal.Name + ",";
                    bits += x.Signal.Bits;
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

        public void ChangeSignal(AssignmentSignal signal)
        {
            throw new NotImplementedException();
        }
    }
}
