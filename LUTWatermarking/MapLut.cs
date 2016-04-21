using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Diploma.VHDLWrapper.Services;
using Diploma.VHDLWrapper.VHDLSetcions;
using Diploma.VHDLWrapper.VHDLSetcions.Maps;
using Diploma.VHDLWrapper.VHDLSetcions.Maps.Assignments;
using Diploma.VHDLWrapper.VHDLSetcions.Signals;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.AssignmentSignals;

namespace Diploma.LUTWatermarking
{
    public class MapLUT : Map
    {
        public LUT LUT { get; private set; }
        private readonly Map Map;
        private MapLUT(Map map) : base(map)
        {
            Map = map;
        }




        public static MapLUT FromMap(Map map)
        {
            if (LUT.IsLUT(map.Entity))
            {
                return new MapLUT(map)
                {
                    LUT = LUT.FromEntity(map.Entity)
                };
            }
            return null;
        }

        public void ExtendLUT(LUTComponents components)
        {
            var lut = LUT.FromEntity(Entity);
            var biggerLut = components.GetBigerLut(lut);
            if (ChangeEnity(biggerLut.Entity))
            {
                Map.ChangeEnity(biggerLut.Entity);
                var extraBits = biggerLut.Bits - lut.Bits;
                int times = (int)Math.Pow(2, extraBits);
                ChangeInitVector(Helper.InitVectorMultiply(GetInitVector(), times));
                Map.Change(Text);
            }
        }

        public Port GetFreeInput()
        {
            return Entity.Ports.FirstOrDefault(x => x.PortType == PortTypes.In && Assigmnets.All(a => a.Left.Signal.Name != x.Name));
        }

        public AssignmentSignal GetOutput()
        {
            return Assigmnets.FirstOrDefault(x=> x.Left.Signal.Name == Entity.Ports.FirstOrDefault(c => c.PortType == PortTypes.Out).Name).Right.Signal;
        }

        public string GetInitVector()
        {
            return GenericAssignments.FirstOrDefault(x => x.LeftSide.Text.ToUpper().Contains("INIT")).RightSide.Text;
        }

        public void ChangeInitVector(string text)
        {
            var initAssignent = GenericAssignments.FirstOrDefault(x => x.LeftSide.Text.ToUpper().Contains("INIT"));
            initAssignent.RightSide.Change(text);
        }


        private const string IndexPattern = "(?<=[a-zA-Z]+)[0-9]+";
        public void ConstValuePort(Port port, bool isOne)
        {
           //var a = Assignment.Parse(this, port.N );
            if (Assigmnets.FirstOrDefault(a => a.Left.Signal.Name == port.Name) != null)
            {
                var istr = Regex.Match(port.Name, IndexPattern).Value;
                int index;
                int.TryParse(istr, out index);
                var initVector = Helper.ExtractInitVectorValut(GetInitVector());
            }
        }
    }
}
