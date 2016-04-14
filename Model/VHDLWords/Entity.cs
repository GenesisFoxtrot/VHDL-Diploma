using System.Collections.Generic;

namespace Model.VHDLWords
{
    public class Entity
    {
        public string Name { get; set; }
        public List<Port> Ports { get; set; } 
    }
}
