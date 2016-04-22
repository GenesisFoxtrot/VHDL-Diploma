using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Diploma.LUTWatermarking
{
    public class InitVector
    {
        public static string InitVectorMultiply(string init, int times)
        {
            string hex = ExtractInitVectorValut(init);
            var strHex = new StringBuilder(hex);
            for (int i = 1; i < times; i++)
            {
                strHex.Append(hex);
            }
            return Regex.Replace(init, "(?<=X\")[A-Fa-f0-9]+(?=\")", strHex.ToString());
        }

        public static string InitVectorA(string init, bool isOne)
        {
            var hex = ExtractInitVectorValut(init);
            var binary = FromHexToBibValue(hex);
            var result = binary.Substring(0, binary.Length / 2);
            var outputChar = isOne ? '1' : '0';
            result = result + (new String(outputChar, binary.Length / 2));
            string strHex = FomBinToHexValue(result);
            return Regex.Replace(init, "(?<=X\")[A-Fa-f0-9]+(?=\")", strHex);
        }



        public static string ValueToVector(string value, char c = 'X')
        {
            return c + "\"" + value + "\"";
        }

        public static string FomBinToHexValue(string bin)
        {
            return Convert.ToInt32(bin, 2).ToString("X");
        }

        public static string FromHexToBibValue(string hex)
        {
            int hexInt = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            return Convert.ToString(hexInt, 2);
        }

        private const string hexPattern = "(?<=X\")[A-Fa-f0-9]+(?=\")";
        public static string ExtractInitVectorValut(string init)
        {
            return Regex.Match(init, hexPattern).Value;
        }
    }
}
