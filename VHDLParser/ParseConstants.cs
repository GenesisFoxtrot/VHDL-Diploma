using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHDLParser
{
    public class ParseConstants
    {
        protected const string MFS = "(( )+)?"; //Maybe few spases
        protected const string EL = "\n";      //End of the line
        protected const string SEL = MFS + EL;      //End of the line with spaces
        protected const string VHDLName = "[a-zA-Z_0-9]+";
        protected const string AEL = @"(" + MFS + ";" + ")?" + SEL;
        protected const string Num = "[0-9]+";
        //----------------Port assignment-------------------------------------
        protected const string Enumeration = "\\([a-zA-Z0-9 ]+\\)";
        protected const string MEnumeration = "(" + MFS + Enumeration + ")?";
        protected const string StrConst = "\"[a-zA-Z0-9]+\"";
        protected const string SingleConst = "'[a-zA-Z0-9]'";
        protected const string Default = MFS + ":=" + MFS + "(" + StrConst + "|" + SingleConst + "|" + Ns + ")";
        protected const string MDefault = @"(" + MFS + Default + ")?";
        protected const string Ns = "\\([a-zA-Z0-9,. =>']+\\)";
        protected const string GenericDefault = MFS + VHDLName + MFS + ":=" + MFS + "(" + StrConst + "|" + SingleConst + "|" + Ns + ")";
        protected const string LeftSide = VHDLName;
        protected const string RightSide = MFS + "[a-zA-Z]+" + MFS + VHDLName + MEnumeration + MDefault + AEL;
        protected const string PortAsigment = MFS + LeftSide + MFS + ":" + RightSide;
        protected const string GenericPortAsigment = MFS + LeftSide + MFS + ":" + MFS + GenericDefault;
        protected const string MPortAsigments = "((" + PortAsigment + ")+)?";
        //--------------------------------------------------------------------
        protected const string ComponentPatternTop = "component" + MFS + VHDLName + SEL;
        protected const string ComponentPatternBottom = MFS + "end" + MFS + "component" + MFS + ";" + AEL;
        protected const string MGeneric =
            "("
            + MFS + "generic" + MFS + "\\("
            + "[a-zA-Z0-9 _():=>.,*;\"'\n]+" //+ AEL + GenericPortAsigment + MFS 
            + "\\);" + AEL
            + ")?";
        protected const string Ports = MFS + "port" + MFS + "\\(" + AEL + MPortAsigments + MFS + "\\);" + AEL;
        protected const string ComponentPattern = ComponentPatternTop + MGeneric + Ports + ComponentPatternBottom;
        //---------------------------------------------------------------------
        protected const string EntityPatternTop = MFS + "entity" + MFS + VHDLName + MFS + "is" + SEL;
        protected const string EntityPatternBottom = MFS + "end" + MFS + VHDLName + MFS + AEL;
        protected const string EntityPattern = EntityPatternTop + MGeneric + Ports + EntityPatternBottom;
        //---------------------------------------------------------------------
        protected const string EntityName = "(?<=entity" + MFS + ")" + VHDLName + "(?=" + MFS + "is)";
        protected const string ComponentName = "(?<=component" + MFS + ")" + VHDLName;
        protected const string PortName = VHDLName + "(?=" + MFS + ":)";
        protected const string PortType = "(?<=:" + MFS + ")" + VHDLName;
        protected const string DefaultValue = "(?<=:=" + MFS + ")" + "(" + StrConst + "|" + SingleConst + ")";
        //-----------------------------------------------------------------------------------------------------
        protected const string SignalPattern = MFS + "signal" + MFS + VHDLName + MFS + ":" + MFS + VHDLName + MFS + ";" + AEL;
        protected const string Beh = "(?<=begin)" + "[\n,a-z,A-Z, _ :;()=>0-9'\"]+" + "(?=end" + MFS + VHDLName + MFS + ";)";
        protected const string OneMap = "[a-z,A-Z_ 0-9]+ :" + MFS+VHDLName + SEL + MFS + "(generic map|port map)" + "+[a-z,A-Z()=>0-9_, \"\'\n]+(?=;)";
        protected const string RegularTitle = "[a-z,A-Z_ 0-9]+ : [a-z,A-Z_0-9]+";
        protected const string Assigments = "(?<=port map( )?\\()[a-zA-Z0-9=>, _()\"'\n]+(?=\\))";
        protected const string OneAssimnet = MFS + VHDLName + MFS + "=>" + MFS + "[a-zA-Z0-9=>, _()\"']+";
        protected const string MapName = PortName;
        protected const string MapEntity = PortType;
        protected const string AssigmentsLeftSide = VHDLName + "(?=" + MFS + "=>)";
        protected const string AssigmentsRightSide = "(?<==>" + MFS + ")" + "[a-zA-Z0-9_()\"\']+";
        // -----------------------------------------------------------------------------------------------------
    }
}
