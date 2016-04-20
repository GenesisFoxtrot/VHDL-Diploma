using System;
using System.Linq;
using System.Text.RegularExpressions;
using Diploma.VHDLWrapper.Services;
using Diploma.VHDLWrapper.Services.Parsers;
using Diploma.VHDLWrapper.VHDLSetcions.Maps.Assignments.AssignmentSides;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.Enumerations;
using PC = Diploma.VHDLWrapper.Services.Parsers.ParsConstants;

namespace Diploma.VHDLWrapper.VHDLSetcions.Signals.AssignmentSignals
{
    public class AssignmentSignal : Signal
    {
        public SignalDefenition Defenition { get; private set; }
        public override int Bits => Enumeration?.Bits ?? Defenition.Bits;

        public override IVHDLSection ParentSection => SignalsParentSection;
        public ISignalsParentSection SignalsParentSection { get; set; }
        public bool? IsSource { get; set; }
        public bool IsComponentPort { get; set; } = false;

        public AssignmentSignal(SignalDefenition defenition, ISignalsParentSection parent) 
        {
            Defenition = defenition;
            SignalsParentSection = parent;
        }

        public static AssignmentSignal Parse(ISignalsParentSection parent, string text, SignalDefenition defaultDefenition = null)
        {
            
            var signalStr = Regex.Match(text, PC.VHDLName + PC.MFS + PC.Enumeration).Value;
            var name = Regex.Match(signalStr, PC.VHDLName).Value;
          
            name = string.IsNullOrEmpty(name)  ? Regex.Match(text, ParsConstants.VHDLName).Value : name;
            EnumerationBase enumeration =null ;

            if (!String.IsNullOrEmpty(signalStr))
            {
                var enumerationStr = Regex.Match(signalStr, PC.Enumeration).Value;
                enumeration = EnumerationParser.Parse(enumerationStr);
                if (enumeration == null )
                    return null;
            }

            var isComponentPort = false;
            bool? isSource = null;
            var defenition = defaultDefenition ?? parent.Document.Signals.GetSignalDefenition(name);
            if (defenition == null)
            {
                //Error unknown signal
                defenition = parent.Document.Entity.Ports.FirstOrDefault(x => x.Name == name);

                if (defenition == null)
                {
                    var component = parent.Document.Components
                        .FirstOrDefault(x => x.Name == parent.EntityName);
                    if (component != null)
                    {
                        defenition = component.Ports.FirstOrDefault(f => f.Name == name);
                    }
                    else
                    {
                        defenition = new Port(null) {Name =  name, PortType = PortTypes.In}; //TODO Delete
                    }
                        
                }
                isComponentPort = true;
                isSource = (defenition as Port).PortType != PortTypes.In;
            }

            return new AssignmentSignal(defenition, parent)
            {
                Name = Regex.Match(text, ParsConstants.VHDLName).Value,
                IsComponentPort = isComponentPort,
                IsSource = isSource,
                Enumeration = enumeration
            };
        }

        public void Replace(AssignmentSignal signal)
        {
            if (signal.Bits != Bits || signal.IsComponentPort )
            {
                throw new Exception();
            }
            signal.IsSource = IsSource;
            signal.IsComponentPort = IsComponentPort;
            SignalsParentSection.ChangeSignal(signal);
        }

        public override string ToString()
        {
            return Name + Enumeration;
        }
    }
}
