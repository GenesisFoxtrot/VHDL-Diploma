using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Services;
using Model.VHDLSetcions;
using Model.VHDLSetcions.Maps;
using Model.VHDLSetcions.Signals;

namespace Model.Entities
{
    public class WotermarikingDocument
    {
        public VHDLDocument Document { get; }
        public List<SignalDefenition> ConstValueGenerators { get; set; }
        public List<Map> FreeLuts { get; set; }

        public WotermarikingDocument(VHDLDocument document)
        {
            Document = document;
        }

        public void Parse()
        {
            SearchService searchService = new SearchService(Document);
            ConstValueGenerators = searchService.ConstValuesGenerators();
            FreeLuts = searchService.FreeLuts(ConstValueGenerators);
        }
    }
}
