using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using PC = Model.Services.ParsConstants;

namespace Model.VHDLSetcions.Signals.Enumerations
{
    public class ComplexEnumeration : EnumerationBase
    {
        public EnumerationDirections Direction { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public override int Bits => Math.Abs(To - From) + 1;

        public override int Bottom => Math.Min(To, From);
        public override int Top => Math.Max(To, From);

        public ComplexEnumeration()
        {
            
        }

        public ComplexEnumeration(int from, int to, EnumerationDirections directions)
        {
            From = from;
            To = to;
            Direction = directions;
        }

        public ComplexEnumeration(int n, EnumerationDirections direction)
        {
            Direction = direction;
            if (direction == EnumerationDirections.To)
            {
                From = 0;
                To = n - 1;
            }
            if (direction == EnumerationDirections.Downto)
            {
                To = 0;
                From = n - 1;
            }
        }

        public static ComplexEnumeration Parse(string text)
        {
            var newEnumeration = new ComplexEnumeration();
            const string to = "(downto|to)";
            var directionStr = Regex.Match(text, to).Value;
            EnumerationDirections direction;
            if (!EnumerationDirections.TryParse(directionStr, true, out direction))
                return null;

            newEnumeration.Direction = direction;

            var leftNumber = PC.Num + "(?=" + PC.MFS + to + ")";
            var rightNumber = "(?<=" + to +PC. MFS + ")" + PC.Num;

            int left, right;
            var leftStr = Regex.Match(text, leftNumber).Value;
            var rightStr = Regex.Match(text, rightNumber).Value;

            if (!int.TryParse(leftStr, out left)) return null;
            if (!int.TryParse(rightStr, out right)) return  null;
            newEnumeration.From = left;
            newEnumeration.To = right;

            return newEnumeration;
        }

        public override string ToString()
        {
            return "(" + From + " " + Direction.ToString().ToLower() + " " + To + ")";
        }

        public override bool IsSameBus(EnumerationBase enumeration)
        {
            if (enumeration == null && enumeration.Bits == Bits)
            {
                if (enumeration is ComplexEnumeration)
                {
                    var cenum = enumeration as ComplexEnumeration;
                    return From == cenum.From && To == cenum.To && Direction == cenum.Direction;
                }
                if (enumeration is SimpleIndex || Bits == 1)
                {
                    return (enumeration as SimpleIndex).Index == To;
                }
            }
            return false;
        }

        public override object Clone()
        {
            return new ComplexEnumeration(From, To, Direction);
        }
    }
}
