using System.Collections.Generic;
using System.Linq;
using Diploma.VHDLWrapper.VHDLSetcions;
using Diploma.VHDLWrapper.VHDLSetcions.Maps;
using Diploma.VHDLWrapper.VHDLSetcions.Signals;

namespace Diploma.VHDLExtensions.Services
{
    public class SearchService
    {
        private const string Lut = "lut";
        private readonly VHDLDocument _document;

        public SearchService(VHDLDocument document)
        {
            _document = document;
        }

        public List<SignalDefenition> ConstValuesGenerators()
        {
            if (_document.Components == null || _document.Maps ==null || _document.Signals == null)
                return null;

            var vhdlConstants = _document.Components.Where(x => x.Ports.Count() == 1 && x.Ports.FirstOrDefault().PortType == PortTypes.Out && x.Ports.FirstOrDefault().DefaultValue != null).ToList();
            var zeroMaps = _document.Maps.Where(x => vhdlConstants.Select(y => y.Name).Contains(x.EntityName)).ToList();

            var constSignals = zeroMaps.SelectMany(x => x.Assigmnets)
                .Where(y => vhdlConstants.Select(c => c.Ports.FirstOrDefault().Name).Contains(y.Left.Signal.Name))
                .Select(s => s.Right)
                .ToList();

            return _document.Signals.GetSignals(constSignals.Select(x=>x.Signal.Name).ToList()).ToList();

        }

        public List<Map> FreeLuts(List<SignalDefenition> constValueGenerators)
        {
            var componentsWithFreePorts =
                _document.Maps.Where(x => x.Assigmnets.Any(y => constValueGenerators.Select(c=>c.Name).Contains(y.Right.SignalName))).ToList();

            return componentsWithFreePorts.Where(x => x.EntityName.ToLower().Contains(Lut)).ToList();
        } 
    }
}
