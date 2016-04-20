using System;
using System.Text.RegularExpressions;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.Enumerations;
using PC = Diploma.VHDLWrapper.Services.Parsers.ParsConstants;

namespace Diploma.VHDLWrapper.VHDLSetcions.Signals
{
    public class Port : SignalDefenition
    {
        public PortTypes PortType { get; set; }

        public override string ToString()
        {
            return Name + "("+ Bits + " bits)";
        }

        public Port(Entity entity) : base(entity)
        {
        }

        public Port(Entity entity, Signal signal, PortTypes type) : base(entity, signal)
        {
            PortType = type;
        }

        public static Port Parse(Entity entity, string x)
        {
            Match pTypeMatch = Regex.Match(x, PC.PortType);
            var remainngWithValueType = x.Substring(pTypeMatch.Index + pTypeMatch.Length);
            var vTypeM = Regex.Match(remainngWithValueType, VHDLName);
            var remaining = remainngWithValueType.Substring(vTypeM.Index + vTypeM.Length);
            var defaultPart = Regex.Match(remaining, PC.Default).Value;
            var portEnumeration = Regex.Match(remaining, PC.Enumeration).Value;
            var port = new Port(entity)
            {
                Name = Regex.Match(x, PC.PortName).Value,
                PortType = ParsePortType(pTypeMatch.Value),
                ValueType = Regex.Match(remainngWithValueType, VHDLName).Value,
                DefaultValue = defaultPart != String.Empty ? Regex.Match(defaultPart, PC.DefaultValue).Value : null,
                Enumeration = !String.IsNullOrWhiteSpace(portEnumeration) ? ComplexEnumeration.Parse(portEnumeration) : null
            };
            return port;
        }

        public static PortTypes ParsePortType(string type)
        {
            PortTypes portType;
            return PortTypes.TryParse(type, true, out portType) ? portType : PortTypes.Error;
        }
    }
}
