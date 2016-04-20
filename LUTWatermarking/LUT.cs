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
        private static readonly string LUTName = "LUT";
        private static readonly string InputBitsPattern = "(?<=LUT)[0-9]+";
        public int Bits { get; private set; }
        public Entity Entity { get; private set; }
        public Map Map { get; private set; }
        public string Name => Entity.Name;

        private LUT() { }

        public static LUT FromEntity(Entity entity)
        {
            var bitsStr = Regex.Match(entity.Name, InputBitsPattern).Value;
            int bits;
            if (int.TryParse(bitsStr, out bits))
            {
                return new LUT()
                {
                    Bits = bits,
                    Entity = entity
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
