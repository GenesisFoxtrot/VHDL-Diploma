using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VHDLParser;
using Model;
using System.Text.RegularExpressions;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            FileTools file = new FileTools();
            var text = file.GetVHDL(@"D:\111\simprim_Vcomponents2.sty");
            Parser parser = new Parser();
            var compenets = parser.ParseCompenets(text);
            var zero = compenets.Where(x=> x.Ports.Count() == 1 && x.Ports.FirstOrDefault().PortType == PortTypes.Out &&  x.Ports.FirstOrDefault().DefaultValue != null).ToList();
            var text2 = file.GetVHDL(@"D:\111\222.vhd");
            var result = new List<string>();
            foreach (var v in zero)
            {
                var c = Regex.Matches(text2, v.Name ).Cast<Match>().Select(x => x.Value).ToList();
                result.AddRange(c);
            }

            //FOUND GND 
            string gnd = "GND";
            string vcc = "VCC";


            var a = parser.ParseSignal(text2, gnd);
            var newText = parser.AddSameSignal(text2, gnd, "WATERMARKED_GND");
            newText = parser.AddSameSignal(newText,   vcc, "WATERMARKED_VCC");
            var entity = parser.ParEntities(text2);
            //IN->LUT->OUT
            Console.WriteLine("OUT:");  
            entity.FirstOrDefault().Ports.Where(x=>x.PortType == PortTypes.Out).ToList().ForEach(y=>Console.WriteLine("  "+y.Name + "  " + y.ValueType));
            Console.WriteLine("IN:");
            entity.FirstOrDefault().Ports.Where(x => x.PortType == PortTypes.In).ToList().ForEach(y => Console.WriteLine("  " + y.Name + "  " + y.ValueType));
            Console.ReadLine();
        }
    }
}
