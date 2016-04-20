using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Diploma.VHDLWrapper.Services;
using Diploma.VHDLWrapper.Services.Parsers;
using Diploma.VHDLWrapper.VHDLSetcions.Maps;
using Diploma.VHDLWrapper.VHDLSetcions.Signals;
using PC = Diploma.VHDLWrapper.Services.Parsers.ParsConstants;

namespace Diploma.VHDLWrapper.VHDLSetcions
{
    public class VHDLDocument : VHDLSection 
    {
        public override IVHDLSection ParentSection => null;
        public override VHDLDocument Document => this;
        public List<Entity> Components { get; set; }
        public List<Map> Maps { get; set; }
        public Entity Entity { get; set; }
        public Router Router { get; set; }
        public DocumnetSignals Signals { get; set; }
        

        public VHDLDocument(string vhdl)
        {
            Text = vhdl;
            Router = new Router(this);
        }

        public void Parse(string vhdlLib)
        {
            var parser = new Parser(this);
            Entity = parser.ParEntities(Text).FirstOrDefault();
            Components = parser.ParseCompenets(vhdlLib);
            Signals = new DocumnetSignals(Document, parser.ParseSignals(Text));
            Maps = parser.ParseMaps(Text);            
        }

        public void AddMap(Map map)
        {
           AddVHDLInBehaviorSection(Helper.MapToVHDL(map)); 
        }


        public void AddVHDLInBehaviorSection(string vhdl)
        {
            var oldMap = Regex.Match(Text, PC.OneMap + MFS + AEL).Value;
            Text = Text.Replace(oldMap, vhdl +  oldMap); ;
        }


        public void Redirect(Signal fromSignal, Signal toSignal)
        {
            var outMap = Maps.Where(x => x.Assigmnets.Any(y => y.Right.Signal.Name == fromSignal.Name)).ToList();
            outMap.ForEach(map =>
            {
                var assgn = map.Assigmnets.FirstOrDefault(y => y.Right.Signal.Name == fromSignal.Name);
                assgn.Right.Change(assgn.Right.Text.Replace(fromSignal.Name, toSignal.Name));
            });
        }
    }
}
