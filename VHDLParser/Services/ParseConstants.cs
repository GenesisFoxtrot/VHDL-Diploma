namespace VHDLParser.Services
{
    public class ParseConstants
    {
        public const string MFS = "(( )+)?"; //Maybe few spases
        public const string EL = "\n";      //End of the line
        public const string SEL = MFS + EL;      //End of the line with spaces
        public const string VHDLName = "[a-zA-Z_0-9]+";
        public const string AEL = @"(" + MFS + ";" + ")?" + SEL;
        public const string Num = "[0-9]+";
        public const string VHDLChasrs = "[a-z,A-Z()=>0-9_, \"'():;\n]+"; 
        //----------------Port assignment-------------------------------------
        public const string Enumeration = "\\([a-zA-Z0-9 ]+\\)";
        public const string MEnumeration = "(" + MFS + Enumeration + ")?";
        public const string StrConst = "\"[a-zA-Z0-9]+\"";
        public const string SingleConst = "'[a-zA-Z0-9]'";
        public const string Default = MFS + ":=" + MFS + "(" + StrConst + "|" + SingleConst + "|" + Ns + ")";
        public const string MDefault = @"(" + MFS + Default + ")?";
        public const string Ns = "\\([a-zA-Z0-9,. =>']+\\)";
        public const string GenericDefault = MFS + VHDLName + MFS + ":=" + MFS + "(" + StrConst + "|" + SingleConst + "|" + Ns + ")";
        public const string LeftSide = VHDLName;
        public const string RightSide = MFS + "[a-zA-Z]+" + MFS + VHDLName + MEnumeration + MDefault + AEL;
        public const string PortAsigment = MFS + LeftSide + MFS + ":" + RightSide;
        public const string GenericPortAsigment = MFS + LeftSide + MFS + ":" + MFS + GenericDefault;
        public const string MPortAsigments = "((" + PortAsigment + ")+)?";

        //--------------------------------------------------------------------
        public const string ComponentPatternTop = "component" + MFS + VHDLName + SEL;
        public const string ComponentPatternBottom = MFS + "end" + MFS + "component" + MFS + ";" + AEL;
        public const string MGeneric =
            "("
            + MFS + "generic" + MFS + "\\("
            + "[a-zA-Z0-9 _():=>.,*;\"'\n]+" //+ AEL + GenericPortAsigment + MFS 
            + "\\);" + AEL
            + ")?";
        public const string Ports = MFS + "port" + MFS + "\\(" + AEL + MPortAsigments + MFS + "\\);" + AEL;
        public const string ComponentPattern = ComponentPatternTop + MGeneric + Ports + ComponentPatternBottom;
        //---------------------------------------------------------------------
        public const string EntityPatternTop = MFS + "entity" + MFS + VHDLName + MFS + "is" + SEL;
        public const string EntityPatternBottom = MFS + "end" + MFS + VHDLName + MFS + AEL;
        public const string EntityPattern = EntityPatternTop  + Ports + EntityPatternBottom;
        //---------------------------------------------------------------------
        public const string EntityName = "(?<=entity" + MFS + ")" + VHDLName + "(?=" + MFS + "is)";
        public const string ComponentName = "(?<=component" + MFS + ")" + VHDLName;
        public const string PortName = VHDLName + "(?=" + MFS + ":)";
        public const string PortType = "(?<=:" + MFS + ")" + VHDLName;
        public const string DefaultValue = "(?<=:=" + MFS + ")" + "(" + StrConst + "|" + SingleConst + ")";
        //-----------------------------------------------------------------------------------------------------
        public const string signalSectionPattern = "(?<=is" + SEL + ")" + VHDLChasrs + "(?=" + MFS + "begin)";
        public const string SignalPattern = MFS + "signal" + MFS + VHDLName + MFS + ":" + MFS + VHDLName + MEnumeration + MDefault + AEL;
        public const string Beh = "(?<=begin)" + VHDLChasrs + "(?=end" + MFS + VHDLName + MFS + ";)";
        public const string OneMap = "[a-z,A-Z_ 0-9]+ :" + MFS+VHDLName + SEL + MFS + "(generic map|port map)" + "[a-z,A-Z()=>0-9_, \"\'\n]+" + "(?=;)";
        public const string RegularTitle = "[a-z,A-Z_ 0-9]+ : [a-z,A-Z_0-9]+";
        public const string Assigments = "(?<=port map( )?\\()[a-zA-Z0-9=>, _()\"'\n]+(?=\\))";
        public const string OneAssimnet = MFS + VHDLName + MFS + "=>" + MFS + "[a-zA-Z0-9=>, _()\"']+";
        public const string MapName = PortName;
        public const string MapEntity = PortType;
        public const string AssigmentsLeftSide = VHDLName + "(?=" + MFS + "=>)";
        public const string AssigmentsRightSide = "(?<==>" + MFS + ")" + "[a-zA-Z0-9_()\"\']+";
        public const string GenericAsigments = "(?<=generic map\\()"+VHDLChasrs+"(?=\\)" + SEL+ MFS + "port map)" ;
        public const string OneGenericAssimnet = MFS + VHDLName + MFS + "=>" + MFS + "[a-zA-Z0-9=>, _()\"']+";
        public const string GenericAssigmentsRightSide = "(?<==>" + MFS + ")" + "[a-zA-Z0-9=>, _()\"']+";
        // -----------------------------------------------------------------------------------------------------
    }
}
