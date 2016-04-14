using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.VHDLWords
{
    public class Enumeration
    {
        public EnumerationDirections Direction { get; set; }
        public int From { get; set; }
        public int To { get; set; }

        public Enumeration()
        {
            
        }

        public Enumeration(int n, EnumerationDirections direction)
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

        public override string ToString()
        {
            return "(" + From +" " + Direction.ToString().ToLower() + " " + To +")";
        }

         
    }
}
