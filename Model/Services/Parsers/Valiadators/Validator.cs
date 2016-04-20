using System.Collections.Generic;
using System.Linq;
using Diploma.VHDLWrapper.VHDLSetcions;

namespace Diploma.VHDLWrapper.Services.Parsers.Valiadators
{
    public class Validator
    {
        public List<ValidationResilt> Validate(VHDLDocument document)
        {
            var a = document.Components.Where(x => x.Name.ToUpper().Contains("LUT")).ToList();
            var maps = document.Maps.Where(x => x.Entity == null).ToList();
            return new List<ValidationResilt>();
        }
    }
}
