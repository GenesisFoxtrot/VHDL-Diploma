using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Model.VHDLWords;
using Model.VHDLWords.Enumerations;
using Model.VHDLWords.Maps;
using Model.VHDLWords.Signals;

namespace VHDLParser.Services
{
    public class Parser : ParseConstants
    {
        private List<string> MatchesToStrings(MatchCollection collection)
        {
            return collection.Cast<Match>().Select(match => match.Value).ToList();
        }
        public List<Entity> ParseCompenets(string vhdl)
        {
            var entitiesStr = Regex.Matches(vhdl, ComponentPattern, RegexOptions.None, TimeSpan.FromSeconds(1000));

            var result = MatchesToStrings(entitiesStr).Select(x => new Entity()
            {
                Name = Regex.Match(x, ComponentName).Value,
                Ports = ParsePorts(Regex.Match(x, Ports).Value)
            }).ToList();

            return result;
        }

        public List<Port> ParsePorts(string vhdlPorts)
        {
            var portStings = MatchesToStrings(Regex.Matches(vhdlPorts, PortAsigment));
            var results = new List<Port>();
            portStings.ForEach(x =>
            {
                Match pTypeMatch = Regex.Match(x, PortType);
                var remainngWithValueType = x.Substring(pTypeMatch.Index + pTypeMatch.Length);
                var vTypeM = Regex.Match(remainngWithValueType, VHDLName);
                var remaining = remainngWithValueType.Substring(vTypeM.Index + vTypeM.Length);
                var defaultPart = Regex.Match(remaining, Default).Value;
                var portEnumeration = Regex.Match(remaining, Enumeration).Value;
                var port = new Port
                {
                    Name = Regex.Match(x, PortName).Value,
                    PortType = ParsePortType(pTypeMatch.Value),
                    ValueType = Regex.Match(remainngWithValueType, VHDLName).Value,
                    DefaultValue = defaultPart != String.Empty ? Regex.Match(defaultPart, DefaultValue).Value : null,
                    Enumeration = !String.IsNullOrWhiteSpace(portEnumeration) ? ParseEnumeration(portEnumeration) : null
                };
                results.Add(port);
            });
            return results;
        }

        public string ParseSignal(string text, string signal)
        {
            return Regex.Match(text, "signal" + MFS + signal + MFS + ":" + MFS + VHDLName).Value;
        }


        private ComplexEnumeration ParseEnumeration(string portEnumeration)
        {
            var newEnumeration = new ComplexEnumeration();
            var to = "(downto|to)";
            var directionStr = Regex.Match(portEnumeration, to).Value;
            EnumerationDirections direction;
            if (!EnumerationDirections.TryParse(directionStr, true, out direction))
                return null;
            newEnumeration.Direction = direction;

            var leftNumber = Num + "(?=" + MFS + to + ")";
            var rightNumber = "(?<=" + to + MFS + ")" + Num;

            int left, right;
            var leftStr = Regex.Match(portEnumeration, leftNumber).Value;
            var rightStr = Regex.Match(portEnumeration, rightNumber).Value;

            if (!int.TryParse(leftStr, out left)) throw new Exception("!!!");
            if (!int.TryParse(rightStr, out right)) throw new Exception("!!!");
            newEnumeration.From = left;
            newEnumeration.To = right;


            return newEnumeration;
        }

        public List<Entity> ParEntities(string vhdl)
        {
            var entitiesStr = Regex.Matches(vhdl, EntityPattern, RegexOptions.None, TimeSpan.FromSeconds(1000));

            var result = MatchesToStrings(entitiesStr).Select(x => new Entity()
            {
                Name = Regex.Match(x, EntityName).Value,
                Ports = ParsePorts(Regex.Match(x, Ports).Value)
            }).ToList();

            return result;
        }

        public List<Map> ParseMaps(string vhdl)
        {
            var code = Regex.Match(vhdl, Beh).Value;  //Text from begin to end 
            var maps = Regex.Matches(code, OneMap);
            List<Map> mapList = new List<Map>();
            maps.Cast<Match>().ToList().ForEach(map =>
            {
                Map newMap = new Map();
                newMap.Text = map.Value;
                var title = Regex.Match(map.ToString(), RegularTitle).Value;
                newMap.Name = Regex.Match(title, MapName).Value;
                newMap.Entity = Regex.Match(title, MapEntity).Value;  //OneAssimnet
                var genericAssigments = Regex.Match(map.ToString(), GenericAsigments).Value;
                newMap.GenericAssigmnets =
                    MatchesToStrings(Regex.Matches(genericAssigments, OneGenericAssimnet)).Select(asgn => new Assigment()
                    {
                        Text = asgn,

                    }).ToList();

                var assigments = Regex.Match(map.ToString(), Assigments).Value;
                newMap.Assigmnets =
                    MatchesToStrings(Regex.Matches(assigments, OneAssimnet)).Select(asgn => new Assigment()
                    {
                        Text = asgn,
                        LeftSide = new AssignmentSide(Regex.Match(asgn, AssigmentsLeftSide).Value),
                        RightSide = new AssignmentSide(Regex.Match(asgn, AssigmentsRightSide).Value)
                    }).ToList();
                mapList.Add(newMap);
            });
            return mapList;
        }

        //public Assigment ParseAssigment()
        //{
            
        //}

        public List<FullSignal> ParseSignals(string vhdl)
        {

            var signalSection = Regex.Match(vhdl, signalSectionPattern).Value;
            List<FullSignal> signals = new List<FullSignal>();
            MatchesToStrings(Regex.Matches(signalSection, SignalPattern)).ForEach(x =>
            {
                Match pTypeMatch = Regex.Match(x, PortType);
                var remainngWithValueType = x.Substring(pTypeMatch.Index + pTypeMatch.Length);
                var defaultPart = Regex.Match(remainngWithValueType, Default).Value;
                var portEnumeration = Regex.Match(remainngWithValueType, Enumeration).Value;

                var newSignal = new FullSignal()
                {
                    Name = Regex.Match(x, PortName).Value,
                    ValueType = pTypeMatch.Value,
                    DefaultValue = defaultPart != String.Empty ? Regex.Match(defaultPart, DefaultValue).Value : null,
                    Enumeration = !String.IsNullOrWhiteSpace(portEnumeration) ? ParseEnumeration(portEnumeration) : null
                };

                signals.Add(newSignal);
            });

            return signals;
        }

        public PortTypes ParsePortType(string type)
        {
            PortTypes portType;
            return PortTypes.TryParse(type, true, out portType) ? portType : PortTypes.Error;
        }
    }
}
