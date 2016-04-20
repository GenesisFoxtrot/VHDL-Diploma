using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Diploma.VHDLWrapper.VHDLSetcions;

namespace Diploma.VHDLWrapper.Services.Parsers
{
    public abstract class ParsConstants : VHDLSection
    {

        //----------------Port assignment-------------------------------------
        public static string Enumeration = "\\([a-zA-Z0-9 ]+\\)";
        public static string MEnumeration = "(" + MFS + Enumeration + ")?";
        public static string Strstatic = "\"[a-zA-Z0-9]+\"";
        public static string Singlestatic = "'[a-zA-Z0-9]'";
        public static string Default = MFS + ":=" + MFS + "(" + Strstatic + "|" + Singlestatic + "|" + Ns + ")";
        public static string MDefault = @"(" + MFS + Default + ")?";
        public static string Ns = "\\([a-zA-Z0-9,. =>']+\\)";
        public static string GenericDefault = MFS + VHDLName + MFS + ":=" + MFS + "(" + Strstatic + "|" + Singlestatic + "|" + Ns + ")";
        public static string LeftSide = VHDLName;
        public static string RightSide = MFS + "[a-zA-Z]+" + MFS + VHDLName + MEnumeration + MDefault + AEL;
        public static string PortAsigment = MFS + LeftSide + MFS + ":" + RightSide;
        public static string GenericPortAsigment = MFS + LeftSide + MFS + ":" + MFS + GenericDefault;
        public static string MPortAsigments = "((" + PortAsigment + ")+)?";

        //--------------------------------------------------------------------
        public static string ComponentPatternTop = "component" + MFS + VHDLName + SEL;
        public static string ComponentPatternBottom = MFS + "end" + MFS + "component" + MFS + ";" + AEL;
        public static string MGeneric =
            "("
            + MFS + "generic" + MFS + "\\("
            + "[a-zA-Z0-9 _():=>.,*;\"'\n]+" //+ AEL + GenericPortAsigment + MFS 
            + "\\);" + AEL
            + ")?";
        public static string Ports = MFS + "port" + MFS + "\\(" + AEL + MPortAsigments + MFS + "\\);" + AEL;
        public static string ComponentPattern = ComponentPatternTop + MGeneric + Ports + ComponentPatternBottom;
        //---------------------------------------------------------------------
        public static string EntityPatternTop = MFS + "entity" + MFS + VHDLName + MFS + "is" + SEL;
        public static string EntityPatternBottom = MFS + "end" + MFS + VHDLName + MFS + AEL;
        public static string EntityPattern = EntityPatternTop  + Ports + EntityPatternBottom;
        //---------------------------------------------------------------------
        public static string EntityName = "(?<=entity" + MFS + ")" + VHDLName + "(?=" + MFS + "is)";
        public static string ComponentName = "(?<=component" + MFS + ")" + VHDLName;
        public static string PortName = VHDLName + "(?=" + MFS + ":)";
        public static string PortType = "(?<=:" + MFS + ")" + VHDLName;
        public static string DefaultValue = "(?<=:=" + MFS + ")" + "(" + Strstatic + "|" + Singlestatic + ")";
        public static string DefaultValueA = "(" + Strstatic + "|" + Singlestatic + ")";
        //-----------------------------------------------------------------------------------------------------
        public static string signalSectionPattern = "(?<=is" + SEL + ")" + VHDLChasrs + "(?=" + MFS + "begin)";
        public static string SignalPattern = MFS + "signal" + MFS + VHDLName + MFS + ":" + MFS + VHDLName + MEnumeration + MDefault + AEL;
        public static string Beh = "(?<=begin)" + VHDLChasrs + "(?=end" + MFS + VHDLName + MFS + ";)";
        public static string OneMap = "[a-z,A-Z_ 0-9]+ :" + MFS+VHDLName + SEL + MFS + "(generic map|port map)" + "[a-z,A-Z()=>0-9_, \"\'\n]+" + "(?=;)";
        public static string RegularTitle = "[a-z,A-Z_ 0-9]+ : [a-z,A-Z_0-9]+";
        public static string Assigments = "(?<=port map( )?\\()[a-zA-Z0-9=>, _()\"'\n]+(?=\\))";
        public static string OneAssimnet = MFS + VHDLName + MFS + "=>" + MFS + "[a-zA-Z0-9=>, _()\"']+";
        public static string MapName = PortName;
        public static string MapEntity = PortType;
        public static string AssigmentsLeftSide = VHDLName + "(?=" + MFS + "=>)";
        public static string AssigmentsRightSide = "(?<==>" + MFS + ")" + "[a-zA-Z0-9_()\"\']+";
        public static string GenericAsigments = "(?<=generic map\\()"+VHDLChasrs+"(?=\\)" + SEL+ MFS + "port map)" ;
        public static string OneGenericAssimnet = MFS + VHDLName + MFS + "=>" + MFS + "[a-zA-Z0-9=>, _()\"']+";
        public static string GenericAssigmentsRightSide = "(?<==>" + MFS + ")" + "[a-zA-Z0-9=>, _()\"']+";
        // -----------------------------------------------------------------------------------------------------

        public static List<string> MatchesToStrings(MatchCollection collection)
        {
            return collection.Cast<Match>().Select(match => match.Value).ToList();
        }
    }
}
