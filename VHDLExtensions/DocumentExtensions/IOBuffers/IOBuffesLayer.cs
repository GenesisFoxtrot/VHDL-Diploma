using System.Collections.Generic;
using System.Linq;
using Diploma.VHDLWrapper.Services;
using Diploma.VHDLWrapper.VHDLSetcions;
using Diploma.VHDLWrapper.VHDLSetcions.Signals;
using Diploma.VHDLWrapper.VHDLSetcions.Signals.Enumerations;

namespace Diploma.VHDLExtensions.DocumentExtensions.IOBuffers
{
    public class IOBuffesLayer
    { 
        public VHDLDocument Document { get; }

        private readonly Dictionary<SignalDefenition, SignalDefenition> Equivalents;

        public IOBuffesLayer(VHDLDocument document)
        {
            Document = document;
            Equivalents = new Dictionary<SignalDefenition, SignalDefenition>();
        }

        public void Parse()
        {
            var buffers = new List<IOBuffer>();
            var ioBuffers =
                Document.Maps.Where(buf => buf.Name.Contains("IBUF"))
                    .Select(map => IOBuffer.Extract(map, false)).ToList();
            ioBuffers.AddRange(Document.Maps.Where(buf => buf.Name.Contains("OBUF"))
                .Select(map => IOBuffer.Extract(map, true)));
            buffers.AddRange(ioBuffers);

            buffers.GroupBy(x => x.OutsideSignal.Defenition, y => y).ToList().ForEach(o =>
            {
                if (o.Count() == 1)
                {
                    Equivalents.Add(o.Key, o.Single().InsideSignal.Defenition);
                }
                else
                {
                    var signalForInject = new SignalDefenition(Document.Entity)
                    {
                        Name = o.Key.Name + Helper.NewGuidName(),
                        Enumeration = o.Key.Enumeration.Clone() as EnumerationBase,
                        ValueType = o.Key.ValueType
                    };
                    Document.Router.InserSignalDefenition(signalForInject);
                    o.ToList().ForEach(b=>
                    {
                        Document.Router.Replace(b.InsideSignal.Defenition, signalForInject, b.OutsideSignal.Enumeration?.CloneEnumeration());
                        //Document.Router.NewRout(newPeaceOfWire, innside);
                    });
                    Equivalents.Add(o.Key, signalForInject);
                }
            }
            );
        }

        public bool IsBuffered(SignalDefenition signal)
        {
            return Equivalents.ContainsKey(signal);
        }

        public SignalDefenition GetEquivalent(SignalDefenition signal)
        {
            if (IsBuffered(signal))
            {
                return Equivalents[signal];
            }
            return null;
        }

        public SignalDefenition WhetherEquivalent(SignalDefenition signal)
        {
            return IsBuffered(signal) ? Equivalents[signal] : signal;
        }

    }
}
