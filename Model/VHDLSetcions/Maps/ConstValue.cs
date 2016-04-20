using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Model.VHDLSetcions.Maps.Assignments.AssignmentSides;
using PC = Model.Services.ParsConstants;
namespace Model.VHDLSetcions.Maps
{
    public class ConstValue : VHDLSection
    {
        public AssignmentSide AssignmentSide { get; }
        public override VHDLSection ParentSection => AssignmentSide;
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
