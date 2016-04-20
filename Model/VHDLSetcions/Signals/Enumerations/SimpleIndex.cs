using System.Text.RegularExpressions;
using PC = Model.Services.ParsConstants;

namespace Model.VHDLSetcions.Signals.Enumerations
{
    public class SimpleIndex : EnumerationBase
    {
        private static readonly string Pattern = "(?<=\\(" + PC.MFS + ")"  + "[0-9]+" + "(?=" + PC.MFS + "\\))";
        public int Index { get; private set; }
        public override int Bits => 1;
        public override int Bottom => Index;
        public override int Top => Index;

        public SimpleIndex(int index)
        {
            Index = index;
        }


        public static SimpleIndex Parse(string text)
        {
            var indexStr = Regex.Match(text, Pattern).Value;
            int index;
            if (int.TryParse(indexStr, out index))
            {
                return new SimpleIndex(index); 
            }
            return null;
        }

        public override bool IsSameBus(EnumerationBase enumeration)
        {
            if (enumeration != null && enumeration.Bits == 1)
            {
                if (enumeration is ComplexEnumeration)
                {
                    return (enumeration as ComplexEnumeration).To == Index;
                }
                return (enumeration as SimpleIndex)?.Index == Index;
            }
            return false;
        }

        public override string ToString()
        {
            return "(" + Index + ")";
        }

        public override object Clone()
        {
            return new SimpleIndex(Index);
        }
    }
}
