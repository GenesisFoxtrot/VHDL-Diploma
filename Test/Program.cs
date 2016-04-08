using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VHDLParser;
using Model;
using System.IO;
using System.Text.RegularExpressions;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            var fileService = new FileService();
            var parser = new Parser();
            var vhdlLib = fileService.GetVHDL(@"D:\111\simprim_Vcomponents2.sty");
            var vhdl = fileService.GetVHDL(@"D:\111\222.vhd");
            var document = new VHDLDocument(vhdl);

            document.Components = parser.ParseCompenets(vhdlLib);
            var vhdlConstants = document.Components.Where(x=> x.Ports.Count() == 1 && x.Ports.FirstOrDefault().PortType == PortTypes.Out &&  x.Ports.FirstOrDefault().DefaultValue != null).ToList();
            document.Maps = parser.ParseMaps(vhdl);
            

            var zeroMaps = document.Maps.Where(x => vhdlConstants.Select(y=>y.Name).Contains(x.Entity)).ToList();
            var constSignals = zeroMaps.SelectMany(x => x.Assigmnets)
                .Where(y => vhdlConstants.Select(c => c.Ports.FirstOrDefault().Name).Contains(y.LeftSide))
                .Select(s=>s.RightSide)
                .ToList();                 //Const signals


            var componentsWithFreePorts =
                document.Maps.Where(x => x.Assigmnets.Any(y => constSignals.Contains(y.RightSide))).ToList();
            //???
            var freeLuts = componentsWithFreePorts.Where(x => x.Entity.ToLower().Contains("lut")).ToList();

            //======================================================================
            //Watermarking==========================================================
            //======================================================================

            var entity = parser.ParEntities(vhdl);

            //IN->LUT->OUT
            const string prefix = "WATERMARKED_";
            
            constSignals.ForEach( x =>document.AddSameSignal(x, prefix + x));
            var outForReplace = entity.FirstOrDefault().Ports.FirstOrDefault(x => x.PortType == PortTypes.Out); //HARDCODE


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
            File.WriteAllText(@"D:\111.vhdl", document.Document);
            Console.ReadLine();
        }
    }
}


/**
            Console.WriteLine("OUT:");  
            entity.FirstOrDefault().Ports.Where(x=>x.PortType == PortTypes.Out).ToList().ForEach(y=>Console.WriteLine("  "+y.Name + "  " + y.ValueType));
            Console.WriteLine("IN:");
            entity.FirstOrDefault().Ports.Where(x => x.PortType == PortTypes.In).ToList().ForEach(y => Console.WriteLine("  " + y.Name + "  " + y.ValueType));
**/
