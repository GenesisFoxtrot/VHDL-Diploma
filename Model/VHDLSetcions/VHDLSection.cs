namespace Model.VHDLSetcions
{
    public abstract class VHDLSection
    {
        public string Text { get; protected set; }
        public abstract VHDLSection ParentSection { get; }
        public virtual VHDLDocument Document => ParentSection.Document;
        public virtual void Change(string text)
        {
            if (ParentSection != null)
            {
                var newText = ParentSection.Text.Replace(this.Text, text);
                ParentSection.Change(newText);
            }
            this.Text = text;
        }
        
        //---------------Some Comon Patterns-----------------------------------------
        public static string MFS = "(( )+)?";                      //Maybe few spases
        public static string EL = "\n";                            //End of the line
        public static string SEL = MFS + EL;                       //End of the line with spaces
        public static string VHDLName = "[a-zA-Z_0-9]+";
        public static string AEL = @"(" + MFS + ";" + ")?" + SEL;
        public static string Num = "[0-9]+";
        public static string VHDLChasrs = "[a-z,A-Z()=>0-9_, \"'():;\n]+";
    }
}
