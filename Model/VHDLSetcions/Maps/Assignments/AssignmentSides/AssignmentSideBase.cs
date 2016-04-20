namespace Diploma.VHDLWrapper.VHDLSetcions.Maps.Assignments.AssignmentSides
{
    public class AssignmentSideBase : VHDLSection
    { 
        public override IVHDLSection ParentSection => Assignment;
        public AssignmentBase Assignment {get; protected set; }

        public AssignmentSideBase(AssignmentBase assignment, string text)
        {
            Assignment = assignment;
            Text = text;
        }
    }
}
