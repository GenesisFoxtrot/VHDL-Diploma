using System;

namespace Model.VHDLWords.Enumerations
{
    public class ComplexEnumeration : EnumerationBase
    {
        public EnumerationDirections Direction { get; set; }
        public int From { get; set; }
        public int To { get; set; }

        public override int Bits 
        {
            get
            {
                return Math.Abs(To - From) + 1;
            }
        }

        public ComplexEnumeration()
        {
            
        }

        public ComplexEnumeration(int n, EnumerationDirections direction)
        {
            Direction = direction;
            if (direction == EnumerationDirections.To)
            {
                From = 0;
                To = n - 1;
            }
            if (direction == EnumerationDirections.Downto)
            {
                To = 0;
                From = n - 1;
            }
        }

        public override VHDLWordBase Parse()
        {
            throw new NotImplementedException();
        }

        public override string ToVHDL()
        {
            return "(" + From + " " + Direction.ToString().ToLower() + " " + To + ")";
        }

        public override string ToString()
        {
            return "(" + From +" " + Direction.ToString().ToLower() + " " + To +")";
        }

         
    }
}
