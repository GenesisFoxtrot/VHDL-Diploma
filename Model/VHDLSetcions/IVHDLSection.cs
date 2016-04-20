using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diploma.VHDLWrapper.VHDLSetcions
{
    public interface IVHDLSection
    {
        string Text { get; }
        IVHDLSection ParentSection { get; }
        VHDLDocument Document { get; }
        bool IsBelongs(IVHDLSection section);
        void Change(string text);
    }
}
