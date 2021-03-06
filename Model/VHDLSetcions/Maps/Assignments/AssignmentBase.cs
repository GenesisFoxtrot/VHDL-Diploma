﻿using Diploma.VHDLWrapper.VHDLSetcions.Maps.Assignments.AssignmentSides;

namespace Diploma.VHDLWrapper.VHDLSetcions.Maps.Assignments
{
    public abstract class AssignmentBase : VHDLSection
    {
        public Map Map { get; protected set; }
        public override VHDLDocument Document => Map.Document;
        public override IVHDLSection ParentSection => Map;
        public abstract AssignmentSideBase LeftSide { get;  }
        public abstract AssignmentSideBase RightSide { get; }
        public abstract string NewText();
    }
}
