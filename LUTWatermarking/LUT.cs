using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Diploma.VHDLWrapper.VHDLSetcions;
using Diploma.VHDLWrapper.VHDLSetcions.Maps;

namespace Diploma.LUTWatermarking
{
    public class LUT
    {
        private const string LUTName = "LUT";
        private const string InputBitsPattern = "(?<=LUT)[0-9]+";
        private const string PrefixPattern = "[a-zA-Z0-9_]+(?=LUT)";
        private const string PostfixPattern = "(?<=LUT[0-9]+)[a-zA-Z0-9_]+";
        public string Prefix { get; private set; }
        public string Postfix { get; private set; }
        public int Bits { get; private set; }
        public Entity Entity { get; private set; }
        public string Name => Entity.Name;

        private LUT() { }

        public static LUT FromEntity(Entity entity)
        {
            var bitsStr = Regex.Match(entity.Name, InputBitsPattern).Value;
            var prefix = Regex.Match(entity.Name, PrefixPattern).Value;
            prefix = string.IsNullOrEmpty(prefix) ? null : prefix;
            var postfix = Regex.Match(entity.Name, PostfixPattern).Value;
            postfix = string.IsNullOrEmpty(postfix) ? null : postfix;
            int bits;
            if (int.TryParse(bitsStr, out bits))
            {
                return new LUT()
                {
                    Bits = bits,
                    Entity = entity,
                    Prefix = prefix,
                    Postfix = postfix
                };
            }
            return null;
        }

        public static bool IsLUT(Entity entity)
        {
            return entity.Name.Contains(LUTName);
        } 
    }
}
