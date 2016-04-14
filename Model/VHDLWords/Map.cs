using System.Collections.Generic;

namespace Model.VHDLWords
{
    public class Map
    {
        public string Name { get; set; }
        public string Entity { get; set; }
        public List<Assigment> Assigmnets { get; set; }
        public List<Assigment> GenericAssigmnets { get; set; }
        public string Text { get; set; }
    }
}
