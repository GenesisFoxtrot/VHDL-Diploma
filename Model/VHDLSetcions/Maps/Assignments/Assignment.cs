using System;
using System.Text.RegularExpressions;
using Diploma.VHDLWrapper.VHDLSetcions.Maps.Assignments.AssignmentSides;
using Diploma.VHDLWrapper.VHDLSetcions.Signals;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.AssignmentSignals;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.Enumerations;
using PC = Diploma.VHDLWrapper.Services.Parsers.ParsConstants;

namespace Diploma.VHDLWrapper.VHDLSetcions.Maps.Assignments
{
    public class Assignment : AssignmentBase
    {
        private AssignmentSide _left;
        private AssignmentSide _rigt;
        public override AssignmentSideBase LeftSide => _left;
        public override AssignmentSideBase RightSide => _rigt;
        public AssignmentSide Left => _left;
        public AssignmentSide Right => _rigt;


        private Assignment() { }

        public static Assignment Create(Map map, string text, string left, string right)
        {
            var result = new Assignment()
            {
                Map = map,
                Text = text
            };
            var leftAs = AssignmentSide.Parse(result, left);
            var rightAs = AssignmentSide.Parse(result, right);
            if (rightAs.Signal != null)
                rightAs.Signal.IsSource = !leftAs.Signal.IsSource;
            result._left = leftAs;
            result._rigt = rightAs;
            if (!result.Validation())
            {
                throw new Exception("Inncorect bits");
            }
            return result;
        }

        public static Assignment Create(Map map, Port left, SignalDefenition right, EnumerationBase enumeration = null)
        {
            var result = new Assignment()
            {
                Map = map,
                Text = left + " => " + right + ","
            };

            result._left = AssignmentSide.Create(result, left);
            result._rigt = AssignmentSide.Create(result, right, enumeration);

            if (!result.Validation())
            {
                throw new Exception("Inncorect bits");
            }
            return result;
        }


        public override string NewText()
        {
            var result = Regex.Replace(Text, PC.AssigmentsLeftSide, Left.Text);
            return Regex.Replace(result, PC.AssigmentsRightSide, Right.Text);

        }

        public static Assignment Parse(Map map, string text)
        {
            var leftSideStr = Regex.Match(text, PC.AssigmentsLeftSide).Value;
            var rightSideStr = Regex.Match(text, PC.AssigmentsRightSide).Value;

            var assignment = Create(map, text, leftSideStr, rightSideStr);
            return assignment;
        }

        public override string ToString()
        {
            return Left + " => " + Right;
        }


        public bool Validation()
        {
            return true;
            //return _left.Bits == _rigt.Bits;
        }
    }
}
