using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.VHDLSetcions.Signals.Enumerations;

namespace Model.Services.Parsers
{
    public class EnumerationParser
    {
        public static EnumerationBase Parse(string text)
        {
            var enumeration = SimpleIndex.Parse(text);
            if (enumeration != null)
            {
                return enumeration;
            }
            return ComplexEnumeration.Parse(text);
        }
    }
}
