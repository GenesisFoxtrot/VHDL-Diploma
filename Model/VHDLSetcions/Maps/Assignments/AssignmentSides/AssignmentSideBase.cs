using Model.Entities;

namespace Model.VHDLSetcions.Maps.Assignments.AssignmentSides
{
    public class AssignmentSideBase : VHDLSection
    { 
        public override VHDLSection ParentSection => Assignment;
        public AssignmentBase Assignment {get; protected set; }

        public AssignmentSideBase(AssignmentBase assignment, string text)
        {
            Assignment = assignment;
            Text = text;
        }
    }
}
