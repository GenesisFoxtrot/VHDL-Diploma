using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Diploma.VHDLWrapper.Services;
using Diploma.VHDLWrapper.VHDLSetcions.Maps.Assignments;
using PC = Diploma.VHDLWrapper.Services.Parsers.ParsConstants;

namespace Diploma.VHDLWrapper.VHDLSetcions.Maps
{
    public class Map : VHDLSection
    {
        public string Name { get; set; }
        public string EntityName { get; set; }
        public Entity Entity { get; set; }
        public List<Assignment> Assigmnets { get; set; }
        public List<GenericAssignment> GenericAssignments { get; set; }
        public override VHDLDocument Document { get; }

        public override IVHDLSection ParentSection => Document;
        public Map(VHDLDocument document, string text)
        {
            Document = document;
            Text = text;
        }

        protected Map(Map map)
        {
            Name = map.Name;
            EntityName = map.EntityName;
            Entity = map.Entity;
            Assigmnets = map.Assigmnets;
            GenericAssignments = map.GenericAssignments;
            Document = map.Document;
            Text = map.Text;
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


        public bool ChangeEnity(Entity newEnity)
        {
            if (Entity.Ports.All(x => newEnity.Ports.Any(x.IsEquivalent)))
            {
                var newText = Text.Replace(EntityName, newEnity.Name);
                Entity = newEnity;
                EntityName = newEnity.Name;      
                Change(newText);
                return true;
            }
            return false;
        }
        public virtual void AddAssigment(Assignment assignment)
        {
            Assigmnets.Add(assignment);
            var assignments = Regex.Match(Text, PC.Assigments).Value;
            var oneAssignment = Regex.Match(assignments, PC.OneAssimnet).Value;
          
            var newText = Text.Replace(oneAssignment, oneAssignment + "\n" + assignment + Helper.MaybeComma(oneAssignment));
            Change(newText);
        }

    }
}
