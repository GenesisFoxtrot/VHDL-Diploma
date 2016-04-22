using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Diploma.LUTWatermarking.Options;
using Diploma.VHDLExtensions.DocumentExtensions.IOBuffers;
using Diploma.VHDLExtensions.VHDLEnities;
using Diploma.VHDLExtensions.VHDLEnities.Decoder;
using Diploma.VHDLWrapper.Services;
using Diploma.VHDLWrapper.VHDLSetcions;
using Diploma.VHDLWrapper.VHDLSetcions.Maps;
using Diploma.VHDLWrapper.VHDLSetcions.Maps.Assignments;
using Diploma.VHDLWrapper.VHDLSetcions.Signals;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.AssignmentSignals;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.Enumerations;
using Decoder = Diploma.VHDLExtensions.VHDLEnities.Decoder.Decoder;

namespace Diploma.LUTWatermarking.Services
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
            var doc = options.WotermarikingDocument.Document;

            var IOBuffesLayer = new IOBuffesLayer(_document);
            IOBuffesLayer.Parse();

            options.WatermarkSettings.ForEach(x => x.Signal = IOBuffesLayer.WhetherEquivalent(x.Signal));
            options.SignatureOutputSettings.ForEach(x => x.Signal = IOBuffesLayer.WhetherEquivalent(x.Signal));

            List<WatermarkBit> watermarkBits = new List<WatermarkBit>();

            options.SignatureOutputSettings.ForEach(ports =>
            {
                for (int j = 0; j < ports.SignatureCode.Length; j++)
                {
                    if (ports.SignatureCode[j] != '-')
                    {
                        var newWB = new WatermarkBit()
                        {
                            IsOne = ports.SignatureCode[j] == '1',
                            Signal =
                                doc.Router.AssignmentSignal(null, ports.Signal, ports.Signal.Enumeration?.GetBit(j))
                        };
                        newWB.Signal.IsSource = false;
                        watermarkBits.Add(newWB);
                    }
                }
            });

            //var wbit2 = watermarkBits.FirstOrDefault();
            //var routes2 = doc.Router.GetRoutes(wbit2.Signal.Defenition);

            //var d = routes2.Signals.Where(signal => signal.IsSource == null).ToList();
            //var e = routes2.Signals.Where(signal => signal.IsSource != null && !signal.IsSource.Value).ToList();
            //var c = routes2.Signals.Where(signal => signal.IsSource != null && signal.IsSource.Value).ToList();


            //-----------------DECODER----------------------------------------
            var isWatermark = _document.Router.InserSignalDefenition("IS_WATERMARK", "STD_LOGIC");
            var decoder = new Decoder(doc, isWatermark);

            var decoderSettings = options.WatermarkSettings.Where(x => x.IsUsed)
                .Select(a => new DecoderSignalCode(doc.Router.AssignmentSignal(decoder, a.Signal), a.ActivaionCode))
                .ToList();


            decoder.CodedSignals.AddRange(decoderSettings);
            _document.AddVHDLInBehaviorSection(decoder.ToString());

            //-----------------DECODER----------------------------------------


            //----------------------Lut work---------------------------------- 

            /**var signatureBus = _document.Router.CreateBus("WATERMARKED_OUT");
            int i = 0;
            options.WotermarikingDocument.FreeLuts.ForEach(lut =>
            {
                ChangeLutConstIputs(lut, isWatermark, options.WotermarikingDocument.ConstValueGenerators);
                ChangeLutInitVector(lut, true);
                var newLutOutput = signatureBus.GetWire(i);

                InjectLutOutput(lut, newLutOutput.Defenition);
                i++;
            });
            var watermarkedSignalDefenition = signatureBus.BuildSignal();**/

            LUTComponents components = new LUTComponents(doc);

            var lutForExtenion = _document.Maps
                .Where(map => LUT.IsLUT(map.Entity) && !options.WotermarikingDocument.FreeLuts.Contains(map))
                .Select(map => MapLUT.FromMap(map)).ToList();

            lutForExtenion.ForEach(lut => lut.ExtendLUT(components)); //Extends lut

            var freeInputs = lutForExtenion.Select(x => x.GetFreeInput()).ToList();

            List<MapLUT> lutsForInsertion = new List<MapLUT>(lutForExtenion); //Merge all luts 
            //lutsForInsertion.AddRange(); //AddFreeLut


            lutsForInsertion.ForEach(lut =>
            {
                var watermarkBit = watermarkBits.FirstOrDefault(wb => wb.LUT == null);
                if (watermarkBit != null)
                {
                    var port = lut.GetFreeInput(); //TODO
                    lut.AddLutAssigment(Assignment.Create(lut, port, isWatermark));
                    lut.ConstValuePort(port, watermarkBit.IsOne);
                    watermarkBit.LUT = lut;
                }
            });

            //-----------------------------------------------------------------------------------------
            watermarkBits.GroupBy(x => x.Signal.Defenition).ToList().ForEach(def =>
            {
                var ficitonSignal = _document.Router
                .InserSignalDefenition(
                    "FO" + Helper.NewGuidName(),
                    def.Key.ValueType,
                    def.Key.Enumeration?.CloneEnumeration());

                doc.Router.RedirectAllSources(def.Key, ficitonSignal);
                def.ToList().ForEach(wbit =>
                {
                    wbit.RealSignal = doc.Router.GetRoutes(ficitonSignal) 
                        .Signals.FirstOrDefault(x => (x.Enumeration == null && wbit.Signal.Enumeration == null) ||
                        (x.Enumeration == null && wbit.Signal.Enumeration == null && x.Enumeration.IsSameBus(wbit.Signal.Enumeration)));

                    Mux mux = new Mux();
                    mux.To = wbit.Signal;
                    mux.Condition =  doc.Router.AssignmentSignal(mux, isWatermark) + " = '1'";
                    mux.Else = wbit.RealSignal;
                    mux.Then = wbit.LUT.GetOutput();
                    mux.Insert(_document);
                });
            });

            return _document;
        }
    }
}
