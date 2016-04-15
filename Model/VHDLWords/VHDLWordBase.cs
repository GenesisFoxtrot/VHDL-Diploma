using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.VHDLWords
{
    public abstract class VHDLWordBase
    {
        public abstract VHDLWordBase Parse();
        public abstract string ToVHDL();
    }
}
