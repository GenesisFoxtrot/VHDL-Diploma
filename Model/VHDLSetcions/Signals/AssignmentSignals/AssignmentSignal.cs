using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Model.Services;
using Model.VHDLSetcions.Maps.Assignments.AssignmentSides;

namespace Model.VHDLSetcions.Signals.AssignmentSignals
{
    public class AssignmentSignal : Signal
    {
        public SignalDefenition Defenition { get; private set; }

        public override int Bits => Enumeration?.Bits ?? Defenition.Bits;

        public bool? IsSource { get; set; }
        public bool IsComponentPort { get; set; } = false;

        public AssignmentSignal(SignalDefenition defenition, VHDLSection parent) : base(parent)
        {
            Defenition = defenition;
        }

        public static AssignmentSignal Parse(AssignmentSide parent, string text, SignalDefenition defaultDefenition = null)
        {

            var name = Regex.Match(text, ParsConstants.VHDLName).Value;
            var defenition = defaultDefenition ?? parent.Document.Signals.GetSignalDefenition(name);
            var isComponentPort = false;
            bool? isSource = null;
            if (defenition == null)
            {
                //Error unknown signal
                defenition = parent.Document.Entity.Ports.FirstOrDefault(x => x.Name == name);

                if (defenition == null)
                {
                    var component = parent.Document.Components
                        .FirstOrDefault(x => x.Name == parent.Assignment.Map.EntityName);
                    if (component != null)
                    {
                        defenition = component.Ports.FirstOrDefault(f => f.Name == name);
                    }
                    else
                    {
                        defenition = new Port(null) {Name = "Mock" + Helper.NewGuidName(), PortType = PortTypes.In}; //TODO Delete
                    }
                        
                }
                isComponentPort = true;
                isSource = (defenition as Port).PortType != PortTypes.In;
            }

            return new AssignmentSignal(defenition, parent)
            {
                Name = Regex.Match(text, ParsConstants.VHDLName).Value,
                IsComponentPort = isComponentPort,
                IsSource = isSource
            };
        }

        public override string ToString()
        {
            return Name + Enumeration;
        }
    }
}
