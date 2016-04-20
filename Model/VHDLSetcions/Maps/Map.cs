using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Model.Entities;
using Model.Services;
using Model.VHDLSetcions.Maps.Assignments;
using PC = Model.Services.ParsConstants;

namespace Model.VHDLSetcions.Maps
{
    public class Map : VHDLSection
    {
        public string Name { get; set; }
        public string EntityName { get; set; }
        public Entity Entity { get; set; }
        public List<Assignment> Assigmnets { get; set; }
        public List<GenericAssignment> GenericAssignments { get; set; }
        public override VHDLDocument Document { get; }

        public override VHDLSection ParentSection => Document;
        public Map(VHDLDocument document, string text)
        {
            Document = document;
            Text = text;
        }

        public static Map Parse(VHDLDocument document, string text)
        {       
            var title = Regex.Match(text, PC.RegularTitle).Value;
            var assigments = Regex.Match(text, PC.Assigments).Value;
            var genericAssigments = Regex.Match(text, PC.GenericAsigments).Value;
            Map newMap = new Map(document, text)
            {
                Name = Regex.Match(title, PC.MapName).Value,
                EntityName = Regex.Match(title, PC.MapEntity).Value
            };
            newMap.Entity = document.Components.FirstOrDefault(x => x.Name == newMap.EntityName);
            newMap.GenericAssignments =
                PC.MatchesToStrings(Regex.Matches(genericAssigments, PC.OneGenericAssimnet)).Select(asgn => GenericAssignment.Parse(newMap, asgn)).ToList();

            newMap.Assigmnets =
                PC.MatchesToStrings(Regex.Matches(assigments, PC.OneAssimnet)).Select(asgn => Assignment.Parse(newMap, asgn)).ToList();
            return newMap;
        }

    }
}
