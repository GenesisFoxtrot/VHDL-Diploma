using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Model.Services;
using Model.VHDLSetcions.Maps;
using Model.VHDLSetcions.Signals.AssignmentSignals;
using Model.VHDLSetcions.Signals.Enumerations;
using PC = Model.Services.ParsConstants;

namespace Model.VHDLSetcions.Signals
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


        public AssignmentSignal AssignmentSignal(SignalDefenition defenition, EnumerationBase enumeration)
        {
            if (!_routes.ContainsKey(defenition.Name))
                return null;
            var result = new AssignmentSignal(defenition, null) { Name = defenition.Name, Enumeration = enumeration };
            _routes[defenition.Name].AddSignal(result);
            return result;

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
    }
}
