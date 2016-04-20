using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;
using Model.VHDLWords;
using Model.VHDLWords.Maps;
using Model.VHDLWords.Signals;

namespace VHDLParser.Services
{
    public class SearchService
    {
        private const string Lut = "lut";
        private readonly VHDLDocument _document;

        public SearchService(VHDLDocument document)
        {
            _document = document;
        }

        public List<FullSignal> ConstValuesGenerators()
        {
            if (_document.Components == null || _document.Maps ==null || _document.Signals == null)
                return null;

            var vhdlConstants = _document.Components.Where(x => x.Ports.Count() == 1 && x.Ports.FirstOrDefault().PortType == PortTypes.Out && x.Ports.FirstOrDefault().DefaultValue != null).ToList();
            var zeroMaps = _document.Maps.Where(x => vhdlConstants.Select(y => y.Name).Contains(x.Entity)).ToList();

            var constSignals = zeroMaps.SelectMany(x => x.Assigmnets)
                .Where(y => vhdlConstants.Select(c => c.Ports.FirstOrDefault().Name).Contains(y.LeftSide))
                .Select(s => s.RightSide)
                .ToList();

            return _document.Signals.GetSignals(constSignals).Select(x=> x as FullSignal).ToList();

        }

        public List<Map> FreeLuts()
        {
            var componentsWithFreePorts =
                _document.Maps.Where(x => x.Assigmnets.Any(y => _document.ConstValueGenerators.Select(c=>c.Name).Contains(y.RightSide))).ToList();

            return componentsWithFreePorts.Where(x => x.Entity.ToLower().Contains(Lut)).ToList();
        } 
    }
}
