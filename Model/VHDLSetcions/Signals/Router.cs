using System.Collections.Generic;
using System.Linq;
using Diploma.VHDLWrapper.Services;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.AssignmentSignals;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.Enumerations;

namespace Diploma.VHDLWrapper.VHDLSetcions.Signals
{
    public class Router
    {
        private readonly Dictionary<string, SignalEntity> _routes;
        private readonly List<Bus> _dusses;
        public VHDLDocument Document { get; }

        public Router(VHDLDocument document)
        {
            Document = document;
            _routes = new Dictionary<string, SignalEntity>();
            _dusses = new List<Bus>();
        }

        public void AddSignal(AssignmentSignal signal)
        {
            SignalEntity entity = null;
            if (!_routes.ContainsKey(signal.Name))
            {
                entity = AddObserv(signal.Defenition);
            }
            entity = entity ??  _routes[signal.Name];
            entity.AddSignal(signal);
        }


        public SignalDefenition InserSignalDefenition(string name, string valueType, EnumerationBase enumeration = null)
        {
            SignalDefenition signal = new SignalDefenition(Document.Entity)
            {
                Name = name,
                ValueType = valueType,
                Enumeration = enumeration
            };
            AddObserv(signal);
            return signal;
        }

        public SignalEntity AddObserv(SignalDefenition defenition)
        {
            var entity = new SignalEntity(defenition);
            _routes.Add(defenition.Name, entity);
            return entity;
        }

        public SignalDefenition InserSignalDefenition(SignalDefenition signal)
        {
            AddObserv(signal);
            return signal;
        }


        public AssignmentSignal AssignmentSignal(ISignalsParentSection parent, SignalDefenition defenition,  EnumerationBase enumeration = null)
        {
            if (!_routes.ContainsKey(defenition.Name))
                return null;
            enumeration = enumeration ?? defenition.Enumeration?.CloneEnumeration();
            var result = new AssignmentSignal(defenition, parent) { Name = defenition.Name, Enumeration = enumeration };
            _routes[defenition.Name].AddSignal(result);
            return result;

        }

        public AssignmentSignal AssignmentPort(ISignalsParentSection parent, Port port)
        {
            return new AssignmentSignal(port, parent) { Name = port.Name };
        }

        public Bus CreateBus(string name)
        {
            var bus = new Bus(this, name);
            _dusses.Add(bus);
            return bus;
        }

        public bool NewRout(AssignmentSignal to, AssignmentSignal from)
        {
            if (_routes.ContainsKey(to.Name) && _routes.ContainsKey(from.Name))
            {
                to.IsSource = true;
                AddSignal(to);
                from.IsSource = false;
                AddSignal(from);
                Document.AddVHDLInBehaviorSection(Helper.SimpleAssigment(to, from));
                return true;
            }
            return false;
        }

        public SignalEntity GetRoutes(SignalDefenition defenition)
        {
            if (_routes.ContainsKey(defenition.Name))
            {
                return _routes[defenition.Name];
            }
            return null;
        }

        public bool Replace(SignalDefenition replaced, SignalDefenition signal, EnumerationBase enumeration = null)
        {
            if (_routes.ContainsKey(replaced.Name) && _routes.ContainsKey(signal.Name))
            {
                enumeration = enumeration ?? signal.Enumeration;
                var enumerationBits = enumeration?.Bits ?? 1;
                if (replaced.Bits == enumerationBits)
                {
                    _routes[replaced.Name].Signals.ForEach(s => s.Replace(AssignmentSignal(s.SignalsParentSection, signal, enumeration)));
                    _routes[replaced.Name].Signals.Clear();
                    return true;
                }
            }
            return false;
        }

        public bool RedirectAllSources(SignalDefenition from, SignalDefenition to)
        {
            if (_routes.ContainsKey(to.Name) && _routes.ContainsKey(from.Name))
            {
                var sources = _routes[from.Name].Signals.Where(x => x.IsSource.Value).ToList();
                _routes[from.Name].Signals.RemoveAll(x => sources.Contains(x));
                sources.ForEach(asignal => asignal.Replace(AssignmentSignal(asignal.SignalsParentSection, to, asignal.Enumeration.CloneEnumeration())));
                return true;
            }
            return false;
        }
    }
}
