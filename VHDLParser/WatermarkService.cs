using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace VHDLParser
{
    public class WatermarkService
    {

        public VHDLDocument Watermark(string vhdl, string vhdlLib, Entity entity)
        {
            var parser = new Parser();
            var document = new VHDLDocument(vhdl);

            document.Components = parser.ParseCompenets(vhdlLib);
            var vhdlConstants = document.Components.Where(x => x.Ports.Count() == 1 && x.Ports.FirstOrDefault().PortType == PortTypes.Out && x.Ports.FirstOrDefault().DefaultValue != null).ToList();
            document.Maps = parser.ParseMaps(vhdl);


            var zeroMaps = document.Maps.Where(x => vhdlConstants.Select(y => y.Name).Contains(x.Entity)).ToList();
            var constSignals = zeroMaps.SelectMany(x => x.Assigmnets)
                .Where(y => vhdlConstants.Select(c => c.Ports.FirstOrDefault().Name).Contains(y.LeftSide))
                .Select(s => s.RightSide)
                .ToList();                 //Const signals


            var componentsWithFreePorts =
                document.Maps.Where(x => x.Assigmnets.Any(y => constSignals.Contains(y.RightSide))).ToList();
            //???
            var freeLuts = componentsWithFreePorts.Where(x => x.Entity.ToLower().Contains("lut")).ToList();

            //======================================================================
            //Watermarking==========================================================
            //======================================================================



            //IN->LUT->OUT
            const string prefix = "WATERMARKED_";

            constSignals.ForEach(x => document.AddSameSignal(x, prefix + x));
            var outForReplace = entity.Ports.FirstOrDefault(x => x.PortType == PortTypes.Out); //HARDCODE


            Signal fictionOutSignal = new Signal
            {
                Name = "FICTION_OUT",
                ValueType = outForReplace.ValueType,
                Enumeration = outForReplace.Enumeration
            };
            document.AddSignal(fictionOutSignal);

            Signal watermarkedSignal = new Signal
            {
                Name = "WATERMARKED_OUT",
                ValueType = outForReplace.ValueType,
                Enumeration = outForReplace.Enumeration
            };
            document.AddSignal(watermarkedSignal);


            document.Redirect(outForReplace, fictionOutSignal);
            document.AddMuxAssigment(outForReplace, "SEL = '1'", watermarkedSignal, fictionOutSignal); //TODO
            return document;
        }
    }
}
