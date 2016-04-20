using System.Collections.Generic;
using Diploma.VHDLExtensions.Services;
using Diploma.VHDLWrapper.Services;
using Diploma.VHDLWrapper.VHDLSetcions;
using Diploma.VHDLWrapper.VHDLSetcions.Maps;
using Diploma.VHDLWrapper.VHDLSetcions.Signals;

namespace Diploma.LUTWatermarking.Options
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
