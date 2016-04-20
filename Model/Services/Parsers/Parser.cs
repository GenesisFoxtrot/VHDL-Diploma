using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Model.Entities;
using Model.Services.Parsers;
using Model.VHDLSetcions;
using Model.VHDLSetcions.Maps;
using Model.VHDLSetcions.Maps.Assignments;
using Model.VHDLSetcions.Signals;
using Model.VHDLSetcions.Signals.Enumerations;

namespace Model.Services
{
    public class Parser : ParsConstants
    {
        public override VHDLSection ParentSection => null; //TO DELETE 

        public VHDLDocument Document { get; private set; }

        public Parser(VHDLDocument document)
        {
            Document = document;
        }

        public List<Entity> ParseCompenets(string vhdl)
        {
            var entitiesStr = Regex.Matches(vhdl, ComponentPattern, RegexOptions.None, TimeSpan.FromSeconds(1000));
            return MatchesToStrings(entitiesStr).Select(text => Entity.Parse(Document, text, true)).ToList(); ;
        }

        public List<Port> ParsePorts(Entity entity, string vhdlPorts)
        {
            var portStings = MatchesToStrings(Regex.Matches(vhdlPorts, PortAsigment));
            return portStings.Select(x => Port.Parse(entity, x)).ToList(); 
        }

        public List<Entity> ParEntities(string vhdl)
        {
            var entitiesStr = Regex.Matches(vhdl, EntityPattern, RegexOptions.None, TimeSpan.FromSeconds(1000));
            var result = MatchesToStrings(entitiesStr).Select(x => Entity.Parse(Document, x)).ToList();
            return result;
        }

        public List<Map> ParseMaps(string vhdl)
        {
            var code = Regex.Match(vhdl, Beh).Value;  //Text from begin to end 
            var maps = Regex.Matches(code, OneMap);
            return MatchesToStrings(maps).Select(map => Map.Parse(Document, map)).ToList(); 
        }

        public List<SignalDefenition> ParseSignals(string vhdl)
        {

            var signalSection = Regex.Match(vhdl, signalSectionPattern).Value;
            List<SignalDefenition> signals = new List<SignalDefenition>();
            MatchesToStrings(Regex.Matches(signalSection, SignalPattern)).ForEach(x =>
            {
                Match pTypeMatch = Regex.Match(x, PortType);
                var remainngWithValueType = x.Substring(pTypeMatch.Index + pTypeMatch.Length);
                var defaultPart = Regex.Match(remainngWithValueType, Default).Value;
                var portEnumeration = Regex.Match(remainngWithValueType, Enumeration).Value;

                var newSignal = new SignalDefenition(Document.Entity)
                {
                    Name = Regex.Match(x, PortName).Value,
                    ValueType = pTypeMatch.Value,
                    DefaultValue = defaultPart != String.Empty ? Regex.Match(defaultPart, DefaultValue).Value : null,
                    Enumeration = !String.IsNullOrWhiteSpace(portEnumeration) ? EnumerationParser.Parse(portEnumeration) : null
                };

                signals.Add(newSignal);
            });

            return signals;
        }
    }
}
