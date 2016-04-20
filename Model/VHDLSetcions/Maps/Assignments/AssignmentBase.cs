using Model.Entities;
using Model.VHDLSetcions.Maps.Assignments.AssignmentSides;

namespace Model.VHDLSetcions.Maps.Assignments
{
    public abstract class AssignmentBase : VHDLSection
    {
        public Map Map { get; protected set; }
        public override VHDLDocument Document => Map.Document;
        public override VHDLSection ParentSection => Document;
        public abstract AssignmentSideBase LeftSide { get;  }
        public abstract AssignmentSideBase RightSide { get; }
        public abstract string NewText();
    }
}
