using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Model.VHDLWords.Enumerations
{
    public class SimpleIndex : EnumerationBase
    {
        public SimpleIndex(int index)
        {
            Index = index;
        }
        public int Index { get; }

        public override int Bits => 1;

        public override VHDLWordBase Parse()
        {
            throw new NotImplementedException();
        }

        public override string ToVHDL()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            return "(" + Index + ")";
        }
    }
}
