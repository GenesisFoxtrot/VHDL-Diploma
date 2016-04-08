using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace VHDLParser
{
    public class FileService
    {
        public string GetVHDL(string path)
        {
            if (!File.Exists(path))
            {
                return String.Empty;
            }
            StringBuilder result = new StringBuilder(String.Empty);
            var lines = File.ReadAllLines(path).ToList();
            lines.ForEach(x=> result.Append(x + "\n"));
            return result.ToString();
        }
    }
}
