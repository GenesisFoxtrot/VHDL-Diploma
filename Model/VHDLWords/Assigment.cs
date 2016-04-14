using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Model.VHDLWords
{
    public class Assigment
    {
        const string MFS = "(( )+)?"; //Maybe few spases
        const string VHDLName = "[a-zA-Z_0-9]+";
        const string AssigmentsLeftSide = VHDLName + "(?=" + MFS + "=>)";
        const string AssigmentsRightSide = "(?<==>" + MFS + ")" + "[a-zA-Z0-9_()\"\']+";

        public string LeftSide { get; set; }
        public string RightSide { get; set; }
        public string Text { get; set; }

        public string NewText()
        {
            var result = Regex.Replace(Text, AssigmentsLeftSide, LeftSide);
            return Regex.Replace(result, AssigmentsRightSide, RightSide);
        }
    }
}
