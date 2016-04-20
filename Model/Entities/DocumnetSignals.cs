using System.Collections.Generic;
using System.Linq;
using Model.VHDLSetcions;
using Model.VHDLSetcions.Signals;

namespace Model.Entities
{
    public class DocumnetSignals
    {
        public List<SignalDefenition> Signals { get; set; }
        public VHDLDocument Document { get; }

        public DocumnetSignals(VHDLDocument document, List<SignalDefenition> signals)
        {
            Document = document;
            Signals = signals;
        }
        public SignalDefenition GetSignalDefenition(string name)
        {
            var defenition =  Signals.FirstOrDefault(x => x.Name == name);
            defenition = defenition ?? Document.Entity.Ports.FirstOrDefault(x => x.Name == name);
            return defenition;
        }

        public List<SignalDefenition> GetSignals(List<string> names)
        {
            return Signals.Where(x => names.Contains(x.Name)).ToList();
        } 
    }
}
