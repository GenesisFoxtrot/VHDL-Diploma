using System.Text.RegularExpressions;
using Diploma.VHDLWrapper.VHDLSetcions.Maps.Assignments.AssignmentSides;
using PC = Diploma.VHDLWrapper.Services.Parsers.ParsConstants;

namespace Diploma.VHDLWrapper.VHDLSetcions.Maps.Assignments
{
    public class GenericAssignment : AssignmentBase
    {
        public AssignmentSideBase _left;
        public AssignmentSideBase _right;
        private GenericAssignment(Map map, string text)
        {
            Map = map;
            Text = text;
        }
        public static GenericAssignment Parse(Map map, string text)
        {
            var result = new GenericAssignment(map, text);
            result._left = new AssignmentSideBase(result, Regex.Match(text, PC.AssigmentsLeftSide).Value);
            result._right = new AssignmentSideBase(result,Regex.Match(text, PC.GenericAssigmentsRightSide).Value);
            return result;
        }

        public override AssignmentSideBase LeftSide => _left;
        public override AssignmentSideBase RightSide => _right;
        public override string NewText()
        {
            var result = Regex.Replace(Text, PC.AssigmentsLeftSide, LeftSide.Text);
            return Regex.Replace(result, PC.GenericAssigmentsRightSide, RightSide.Text);
        }
    }
}
