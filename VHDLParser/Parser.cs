using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Model; 

namespace VHDLParser
{
    public class Parser
    {
        private const string MFS = "(( )+)?"; //Maybe few spases
        private const string EL = "\n";      //End of the line
        private const string SEL = MFS + EL;      //End of the line with spaces
        private const string VHDLName = "[a-zA-Z_0-9]+";
        private const string AEL = @"(" + MFS + ";" + ")?" + SEL;
        //----------------Port assignment-------------------------------------
        private const string MEnumeration = "(" + MFS + "\\([a-zA-Z0-9 ]+\\))?";
        private const string StrConst = "\"[a-zA-Z0-9]+\"";
        private const string SingleConst = "'[a-zA-Z0-9]'";
        private const string Default = MFS + ":=" + MFS + "(" + StrConst + "|" + SingleConst + "|" + Ns + ")";
        private const string MDefault = @"(" + MFS + Default + ")?";
        private const string Ns = "\\([a-zA-Z0-9,. =>']+\\)";
        private const string GenericDefault = MFS + VHDLName + MFS + ":=" + MFS + "(" + StrConst + "|" + SingleConst + "|" + Ns + ")";
        private const string LeftSide = VHDLName;
        private const string RightSide = MFS + "[a-zA-Z]+" + MFS + VHDLName  + MEnumeration + MDefault + AEL; 
        public const string PortAsigment = MFS + LeftSide  + MFS + ":" + RightSide;
        public const string GenericPortAsigment = MFS + LeftSide + MFS + ":" + MFS + GenericDefault;
        public const string MPortAsigments = "((" + PortAsigment +")+)?";
        //--------------------------------------------------------------------

        private const string ComponentPatternTop = "component"+ MFS + VHDLName +SEL;
        private const string ComponentPatternBottom =  MFS + "end" + MFS + "component" + MFS  + ";" + AEL;
     
        private const string MGeneric = 
            "(" 
            + MFS + "generic" + MFS + "\\("
            + "[a-zA-Z0-9 _():=>.,*;\"'\n]+" //+ AEL + GenericPortAsigment + MFS 
            + "\\);" + AEL
            + ")?";
        private const string Ports = MFS + "port" + MFS + "\\(" + AEL + MPortAsigments + MFS + "\\);" + AEL;
        private const string ComponentPattern = ComponentPatternTop +  MGeneric + Ports + ComponentPatternBottom;

        //---------------------------------------------------------------------
        private const string EntityPatternTop = MFS + "entity"+ MFS + VHDLName + MFS + "is" + SEL;
        private const string EntityPatternBottom = MFS + "end" + MFS + VHDLName + MFS + AEL;
        private const string EntityPattern = EntityPatternTop + MGeneric + Ports + EntityPatternBottom;

        //---------------------------------------------------------------------
        //
        private const string EntityName = "(?<=entity(( )+)?)" + VHDLName + "(?=(( )+)?)is";
        private const string ComponentName = "(?<=component(( )+)?)" + VHDLName;
        private const string PortName =  VHDLName + "(?=(( )+)?:)";
        private const string PortType = "(?<=:(( )+)?)" +  VHDLName;
        private const string DefaultValue = "(?<=:=(( )+)?)" + "(" + StrConst + "|" + SingleConst + ")";

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
                var port = new Port
                {
                    Name = Regex.Match(x, PortName).Value,
                    PortType = ParsePortType(pTypeMatch.Value),
                    ValueType = Regex.Match(remainngWithValueType, VHDLName).Value,
                    DefaultValue = defaultPart != String.Empty ? Regex.Match(defaultPart, DefaultValue).Value : null
                };
                results.Add(port);
            });
            return results;
        }

        public string ParseSignal(string text,  string signal)
        {
            return Regex.Match(text, "signal" + MFS + signal + MFS + ":" + MFS + VHDLName ).Value;
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

        public string AddSameSignal(string text, string signal, string signalToAdd)
        {
            var oldSignal =  Regex.Match(text, MFS + "signal" + MFS + signal + MFS + ":" + MFS + VHDLName + MFS + ";" + AEL ).Value;
            var newSignal = oldSignal.Replace(signal, signalToAdd);
            return text.Replace(oldSignal, oldSignal + newSignal); ;
        }


        public PortTypes ParsePortType(string type)
        {
            PortTypes portType;
            return PortTypes.TryParse(type, true, out portType) ? portType : PortTypes.Error;
        }
    }
}
