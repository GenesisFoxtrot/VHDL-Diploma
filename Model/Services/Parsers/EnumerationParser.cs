using Diploma.VHDLWrapper.VHDLSetcions.Signals.Enumerations;

namespace Diploma.VHDLWrapper.Services.Parsers
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
