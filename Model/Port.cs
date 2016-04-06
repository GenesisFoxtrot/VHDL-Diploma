using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Port
    {
        public string Name { get; set; }
        public PortTypes PortType { get; set; }
        public string ValueType { get; set; }
        public string DefaultValue { get; set; }
        
    }
}
