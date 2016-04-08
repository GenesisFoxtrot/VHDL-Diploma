using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace VHDLParser
{
    public static class Helper
    {
        public static string ExtractPortName(string vectorPortaAssigment)
        {
           return vectorPortaAssigment.Split('(').FirstOrDefault();
        }

        public static string AssigmentToVHDL(Assigmnet assigmnet)
        {
            return "    " + assigmnet.LeftSide + " => " + assigmnet.RightSide;
        }

        public static string SignalToVHDL(Signal signal)
        {
            return "  signal " + signal.Name + " : " + signal.ValueType + " " + signal.Enumeration + ";\n";
        }
        public static string MapToVHDL(Map map)
        {
            var newMap = "  " + map.Name + " : " + map.Entity + "\n";
            if (map.GenericAssigmnets != null)
            {
                newMap += "    generic map(\n";
                map.GenericAssigmnets.ForEach(ass =>
                {
                    newMap += Helper.AssigmentToVHDL(ass);
                });
                newMap += "    )\n";
            }

            if (map.Assigmnets != null)
            {
                newMap += "    port map(\n";
                map.Assigmnets.ForEach(ass =>
                {
                    newMap += Helper.AssigmentToVHDL(ass) + ",\n";
                });
                newMap = newMap.Substring(0, newMap.Length - 2) + "\n";
                newMap += "    );\n";
            }
            return newMap;
        }

        public static string SimpleAssigment(string to, string @from)
        {
             return "    " + to + " <= " +  @from + " ;\n";
        }
        public static string MuxAssigment(string to , string conditional, string then,  string els)
        {
            return "    " + to + " <= " + then + " when (" +conditional + ")  else " + els + ";\n";
        }
    }
}
