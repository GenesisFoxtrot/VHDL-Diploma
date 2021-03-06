﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Diploma.VHDLWrapper.VHDLSetcions.Signals;
using PC = Diploma.VHDLWrapper.Services.Parsers.ParsConstants;

namespace Diploma.VHDLWrapper.VHDLSetcions
{
    public class Entity : VHDLSection
    {
        public override VHDLDocument Document { get; }
        public override IVHDLSection ParentSection => Document;

        public Entity(VHDLDocument document)
        {
            Document = document;
        }

        public string Name { get; set; }
        public List<Port> Ports { get; set; }

        public static Entity Parse(VHDLDocument document, string text, bool asComponent =false)
        {
            var result = new Entity(document);

            result.Name = Regex.Match(text, (asComponent ?  PC.ComponentName : PC.EntityName)).Value;
            result.Ports = ParsePorts(result, Regex.Match(text, PC.Ports).Value);
            return result;
        }

        public static List<Port> ParsePorts(Entity entity, string vhdlPorts)
        {
            var portStings = PC.MatchesToStrings(Regex.Matches(vhdlPorts, PC.PortAsigment));
            return portStings.Select(x => Port.Parse(entity, x)).ToList();
        }
    }
}

