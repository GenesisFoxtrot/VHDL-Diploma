﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using Model.VHDLSetcions.Maps;
using Model.VHDLSetcions.Maps.Assignments;
using Model.VHDLSetcions.Maps.Assignments.AssignmentSides;
using Model.VHDLSetcions.Signals.AssignmentSignals;

namespace Model.Services
{
    public static class Helper
    {
        public static string ExtractPortName(string vectorPortaAssigment)
        {
           return vectorPortaAssigment.Split('(').FirstOrDefault(); //TO REWRITE
        }

        public static string AssigmentToVHDL(AssignmentBase assignment)
        {
            return "      " + assignment.LeftSide.Text + " => " + assignment.RightSide.Text;
        }

        public static string MapToVHDL(Map map)
        {
            var newMap = "  " + map.Name + " : " + map.EntityName + "\n";
            if (map.GenericAssignments != null)
            {
                newMap += "    generic map(\n";
                map.GenericAssignments.ForEach(ass =>
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

        public static string SimpleAssigment(AssignmentSignal to, AssignmentSignal @from)
        {
             return "  " + to + " <= " +  @from + " ;\n";
        }


        public static string NewGuidName()
        {
            return "PM_" + Guid.NewGuid().ToString().Replace("-", "_");
        }

        public static string InitVector(string init, bool isOne)
        {
            string hexPattern = "(?<=X\")[A-Fa-f0-9]+(?=\")";
            string hex = Regex.Match(init, hexPattern).Value;
            int hexInt = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            var binary = Convert.ToString(hexInt, 2);
            var result = binary.Substring(0, binary.Length/2);
            var outputChar = isOne ? '1' : '0';
            result = result + (new String(outputChar, binary.Length/2));
            string strHex = Convert.ToInt32(result, 2).ToString("X");
            return Regex.Replace(init, "(?<=X\")[A-Fa-f0-9]+(?=\")", strHex);
        }
    }
}