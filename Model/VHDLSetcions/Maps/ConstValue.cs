using System.Text.RegularExpressions;
using Diploma.VHDLWrapper.VHDLSetcions.Maps.Assignments.AssignmentSides;
using PC = Diploma.VHDLWrapper.Services.Parsers.ParsConstants;
namespace Diploma.VHDLWrapper.VHDLSetcions.Maps
{
    public class ConstValue : VHDLSection
    {
        public AssignmentSide AssignmentSide { get; }
        public override IVHDLSection ParentSection => AssignmentSide;
        public int Bits => 1;

        private ConstValue(AssignmentSide assignmentSide, string text)
        {
            AssignmentSide = assignmentSide;
            Text = text;
        }

        public static ConstValue Parse(AssignmentSide parent, string text)
        {
            var resultStr = Regex.Match(text, PC.DefaultValueA).Value;
            if (!string.IsNullOrEmpty(resultStr))
            {
                return new ConstValue(parent,text);
            }
            return null;
        }
    }
}
