using System;
using System.Text.RegularExpressions;
using Diploma.VHDLWrapper.Services.Parsers;
using Diploma.VHDLWrapper.VHDLSetcions.Maps.Assignments.AssignmentSides;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.Enumerations;
using PC = Diploma.VHDLWrapper.Services.Parsers.ParsConstants;

namespace Diploma.VHDLWrapper.VHDLSetcions.Signals.AssignmentSignals
{
    public class PartialSignal : AssignmentSignal
    {
        public PartialSignal(AssignmentSide parent, SignalDefenition defenition, EnumerationBase enumeration) : base(defenition, parent)
        {
            Name = defenition.Name;
            Enumeration = enumeration;
        }

        public static PartialSignal Parse(AssignmentSide parent, string text)
        {

            var signalStr = Regex.Match(text, PC.VHDLName + PC.MFS + PC.Enumeration).Value;
            if (String.IsNullOrEmpty(signalStr))
            {
                return null;
            }

            var name = Regex.Match(signalStr, PC.VHDLName).Value;
            var enumerationStr = Regex.Match(signalStr, PC.Enumeration).Value;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(enumerationStr))
                return null;

            var signalDefenition = parent.Document.Signals.GetSignalDefenition(name);
            var enumeration = EnumerationParser.Parse(enumerationStr);


            if (enumeration == null || signalDefenition == null)
                return null;

            return new PartialSignal(parent, signalDefenition, enumeration);

        }
    }
}
