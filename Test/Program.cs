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

            var vhdlLib = fileService.GetVHDL(@"D:\111\simprim_Vcomponents2.sty");
            var vhdl = fileService.GetVHDL(@"D:\111\222.vhd");
            var parser = new Parser();
            var entity = parser.ParEntities(vhdl).FirstOrDefault();
            WatermarkService service = new WatermarkService();
            var document = service.Watermark(vhdl, vhdlLib, entity);
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
