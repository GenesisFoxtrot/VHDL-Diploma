using System.Collections.Generic;
using System.Linq;
using Diploma.VHDLWrapper.VHDLSetcions.Maps;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.Enumerations;

namespace Diploma.VHDLWrapper.VHDLSetcions.Signals.AssignmentSignals
{
    public class Bus
    {
        public string Name { get; private set; }
        public Router Router { get; private set; }
        private readonly BusDefenition _defenition;
        private readonly List<AssignmentSignal> _wires; 
        public Bus(Router router, string name)
        {
            Name = name;
            Router = router;
            _defenition = new BusDefenition(Router.Document.Entity);
            _wires = new List<AssignmentSignal>();
        }

        public AssignmentSignal GetWire(int i)
        {
            SimpleIndex simpleIndex = new SimpleIndex(i);
            return GetWire(simpleIndex);
        }

        public AssignmentSignal GetWire(EnumerationBase enumeration)
        {
            var result =  new AssignmentSignal(_defenition, null)
            {
                Name = Name,
                Enumeration = enumeration,
                IsComponentPort = false
            };
            _wires.Add(result);
            return result;
        }

        public SignalDefenition BuildSignal()
        {
            if (_wires.Any())
            {
                var min = _wires.Min(x => x.Enumeration.Bottom);
                var max = _wires.Max(x => x.Enumeration.Bottom);
                EnumerationBase enumeration = null;
                string valueType;
                if (min == max)
                {
                    valueType = "STD_LOGIC";
                }
                else
                {
                    valueType = "STD_LOGIC_VECTOR";
                    enumeration = new ComplexEnumeration(max,min, EnumerationDirections.Downto);
                }
                _defenition.ValueType = valueType;
                _defenition.Enumeration = enumeration;
                Router.InserSignalDefenition(_defenition.Name, valueType, enumeration);

                _wires.ForEach(x => Router.AddSignal(x));
                return _defenition;
            }
            return null;

        }
    }
}
