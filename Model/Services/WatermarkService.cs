using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using Model.Entities;
using Model.VHDLEement;
using Model.VHDLSetcions;
using Model.VHDLSetcions.Maps;
using Model.VHDLSetcions.Signals;
using Model.VHDLSetcions.Signals.AssignmentSignals;
using Model.VHDLSetcions.Signals.Enumerations;
using Decoder = Model.VHDLEement.Decoder;

namespace Model.Services
{
    public class WatermarkService
    {
        private const string Init = "init";
        private const string StdLogicVector = "STD_LOGIC_VECTOR";
        private readonly VHDLDocument _document;

        public WatermarkService(VHDLDocument document)
        {
            _document = document;
        }
        public VHDLDocument Watermark(WatermarkOptions options)
        {
            //-----------------DECODER----------------------------------------
            var isWatermark = _document.Router.InserSignalDefenition("IS_WATERMARK", "STD_LOGIC");
            var decoder = new Decoder(isWatermark);
            decoder.CodedSignals.AddRange(options.WatermarkSettings.Where(x => x.IsUsed).ToList());
            _document.AddVHDLInBehaviorSection(decoder.ToString());
            //-----------------DECODER----------------------------------------
            
            var signatureBus = _document.Router.CreateBus("WATERMARKED_OUT");
            int i = 0;

            options.WotermarikingDocument.FreeLuts.ForEach(lut =>
            {
                ChangeLutConstIputs(lut, isWatermark, options.WotermarikingDocument.ConstValueGenerators);
                ChangeLutInitVector(lut, true);
                var newLutOutput = signatureBus.GetWire(i);

                InjectLutOutput(lut, newLutOutput.Defenition);
                i++;
            });
            var watermarkedSignalDefenition = signatureBus.BuildSignal();
            
            i = 0;
            options.SignatureOutputSettings.Where(y=>y.IsUsed).ToList().ForEach(x =>
            {
                var fictionOutSignalDefenition =  _document.Router.InserSignalDefenition("FICTION_OUT" + i, x.Port.ValueType, x.Port.Enumeration);
                _document.Redirect(x.Port, fictionOutSignalDefenition);
                Mux mux = new Mux {To = x.Port, Condition = isWatermark + " = '1'", Else = fictionOutSignalDefenition};
                var enuemation = new ComplexEnumeration(x.Port.Bits, EnumerationDirections.Downto); //TODO
                mux.Then = _document.Router.AssignmentSignal(watermarkedSignalDefenition, enuemation);
                mux.Insert(_document);

                i++;
            });
            return _document;
        }

        public void ChangeLutConstIputs(Map lut, Signal signal, List<SignalDefenition> constSignals)
        {
            var inputAssigmnet = lut.Assigmnets.FirstOrDefault(assgn => constSignals.Select(c=>c.Name).Contains(assgn.Right.Signal.Name));
            inputAssigmnet.Right.Change(signal.Name);
        }

        public void ChangeLutInitVector(Map lut, bool isOne) //REWRITE
        {
            var genericAsigment = lut.GenericAssignments.FirstOrDefault(gen => gen.LeftSide.Text.ToLower().Contains(Init));
            genericAsigment.RightSide.Change(Helper.InitVector(genericAsigment.RightSide.Text, isOne));
        }

        public void InjectLutOutput ( Map lut, SignalDefenition signalToInject)
        {
            /**var outPort = _document.Components.FirstOrDefault(x => x.Name == lut.EntityName).Ports.
                    FirstOrDefault(x => x.PortType == PortTypes.Out);
            var outputAssigmnet = lut.Assigmnets.FirstOrDefault(assgn => assgn.Left.Signal.Name == outPort.Name);

            var to = _document.Signals.GetSignalDefenition(outputAssigmnet.Right.Text);


            _document.Router.NewRout(to, signalToInject);
            outputAssigmnet.RightSide.Change(signalToInject.ToString());**/
        }
    }
}
