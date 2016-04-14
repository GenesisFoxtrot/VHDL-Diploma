using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Model.VHDLWords;
using VHDLParser.Entities;
using Decoder = Model.VHDLEement.Decoder;

namespace VHDLParser.Services
{
    public class WatermarkService
    {
        private const string Init = "init";
        private const string StdLogicVector = "STD_LOGIC_VECTOR";
        private VHDLDocument _document;

        public WatermarkService(VHDLDocument document)
        {
            _document = document;
        }
        public VHDLDocument Watermark(WatermarkOptions options)
        {
            //-----------------DECODER----------------------------------------
            Signal isWatermark = new Signal
            {
                Name = "IS_WATERMARK",
                ValueType = "STD_LOGIC",
                Enumeration = null
            };
            _document.AddSignal(isWatermark);

            Decoder decoder = new Decoder(isWatermark);
            decoder.CodedSignals.AddRange(options.WatermarkSettings.Where(x => x.IsUsed).ToList());
            //-----------------DECODER----------------------------------------

            _document.AddVHDLInBehaviorSection(decoder.ToString());

            List<SignalBit> stSignalBits = new List<SignalBit>();
            int i = 0;
            _document.FreeLuts.ForEach(lut =>
            {
                ChangeLutConstIputs(lut, isWatermark, _document.ConstValueGenerators);

                ChangeLutInitVector(lut, true);


                var newSignal = new SignalBit() { Name = "WATERMARKED_OUT", BitNubmer = i };
                stSignalBits.Add(newSignal);

                InjectLutOutput(lut, newSignal);

                i++;
            });

            Signal watermarkedSignal = new Signal
            {
                Name = "WATERMARKED_OUT",
                ValueType = StdLogicVector,
                Enumeration = new Enumeration(stSignalBits.Sum(x => x.Bits), EnumerationDirections.Downto)
            };

            _document.AddSignal(watermarkedSignal);

            i = 0;
            options.SignatureOutputSettings.Where(y=>y.IsUsed).ToList().ForEach(x =>
            {
                Signal fictionOutSignal = new Signal
                {
                    Name = "FICTION_OUT" + i,
                    ValueType = x.Port.ValueType,
                    Enumeration = x.Port.Enumeration
                };
                _document.AddSignal(fictionOutSignal);
                _document.Redirect(x.Port, fictionOutSignal);              //TODO
                _document.AddMuxAssigment(x.Port, isWatermark + " = '1'", watermarkedSignal, fictionOutSignal); //TODO
                i++;
            });
           
            return _document;
        }

        public void ChangeLutConstIputs(Map lut, Signal signal, List<Signal> constSignals)
        {
            var inputAssigmnet = lut.Assigmnets.FirstOrDefault(assgn => constSignals.Select(c=>c.Name).Contains(assgn.RightSide));
            inputAssigmnet.RightSide = signal.Name;

            _document.RefreshAssigment(inputAssigmnet);
        }

        public void ChangeLutInitVector(Map lut, bool isOne)
        {
            var genericAsigment = lut.GenericAssigmnets.FirstOrDefault(gen => gen.LeftSide.ToLower().Contains(Init));
            genericAsigment.RightSide = Helper.InitVector(genericAsigment.RightSide, isOne);

            _document.RefreshAssigment(genericAsigment);
        }

        public void InjectLutOutput ( Map lut, Signal signalToInject)
        {
            var outPort = _document.Components.FirstOrDefault(x => x.Name == lut.Entity).Ports.
                    FirstOrDefault(x => x.PortType == PortTypes.Out);
            var outputAssigmnet = lut.Assigmnets.FirstOrDefault(assgn => assgn.LeftSide.Contains(outPort.Name));

            var to = _document.Signals.FirstOrDefault(x => outputAssigmnet.RightSide.Contains(x.Name));

            _document.AddSimpeAssigment(to, signalToInject);
            outputAssigmnet.RightSide = signalToInject.ToString();

            _document.RefreshAssigment(outputAssigmnet);
        }

    }


}
