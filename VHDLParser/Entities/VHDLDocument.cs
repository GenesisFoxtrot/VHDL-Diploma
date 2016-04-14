using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Model.VHDLWords;
using VHDLParser.Services;

namespace VHDLParser.Entities
{
    public class VHDLDocument : Parser
    {
        public string Document { get; set; }
        public List<Entity> Components { get; set; }
        public List<Map> Maps { get; set; }
        public List<Signal> Signals { get; set; }
        public List<Signal> ConstValueGenerators { get; set; } 
        public List<Map> FreeLuts { get; set; } 
        public Entity Entity { get; set; }

        public VHDLDocument(string vhdl)
        {
            Document = vhdl;
        }

        public void AddSameSignal(string signal, string signalToAdd) //TODO Add change in map
        {
            
            var oldSignal = Regex.Match(Document, MFS + "signal" + MFS + signal + MFS + ":" + MFS + VHDLName + MFS + ";" + AEL).Value;
            var newSignal = oldSignal.Replace(signal, signalToAdd);
            
            Document = Document.Replace(oldSignal, oldSignal + newSignal); ;
        }

        public void AddSignal(Signal signal)
        {
            var oldSignal = Regex.Match(Document, SignalPattern).Value;
            var newSignal = Helper.SignalToVHDL(signal);
            Document = Document.Replace(oldSignal, oldSignal + newSignal); ;

        }
        public void AddMap(Map map)
        {
           AddVHDLInBehaviorSection(Helper.MapToVHDL(map)); 
        }


        public void AddVHDLInBehaviorSection(string vhdl)
        {
            var oldMap = Regex.Match(Document, OneMap + MFS + AEL).Value;
            Document = Document.Replace(oldMap, vhdl +  oldMap); ;
        }

        public void AddSimpeAssigment(Signal to, Signal from)
        {
            AddVHDLInBehaviorSection(Helper.SimpleAssigment(to.ToString(), from.ToString()));
        }


        public void AddMuxAssigment(Signal to, string condition , Signal thenSignal , Signal elseSignal)
        {
            AddVHDLInBehaviorSection(Helper.MuxAssigment(to.Name, condition, thenSignal.Name, elseSignal.Name)); 
        }

        public void ChangeAsigmentInMap(Map map, Assigment oldAssigment, Assigment newAssigment)
        {
            newAssigment.Text = newAssigment.Text ?? Helper.AssigmentToVHDL(newAssigment);
            var newMap = map.Text.Replace(oldAssigment.Text, newAssigment.Text);
            Document = Document.Replace(map.Text, newMap);
            map.Text = newMap;
            map.Assigmnets.Remove(oldAssigment);
            map.Assigmnets.Add(newAssigment);
        }

        public void Redirect(Signal fromSignal, Signal toSignal)
        {

            var outMap = Maps.Where(x => x.Assigmnets.Any(y => Helper.ExtractPortName(y.RightSide) == fromSignal.Name)).ToList();

            //tMap.Select(x => x.Text.Replace(selectedOut.Name, ));
            //document.Document.Replace()

            Map map = new Map();
            map.Name = Helper.NewGuidName();
            map.Entity = outMap.FirstOrDefault().Entity;
            map.Assigmnets = outMap.FirstOrDefault().Assigmnets;
            AddMap(map);

            outMap.ForEach(x =>
            {
                var ass = x.Assigmnets.FirstOrDefault(y => Helper.ExtractPortName(y.RightSide) == fromSignal.Name);
                ChangeAsigmentInMap(x, ass,
                    new Assigment()
                    {
                        LeftSide = ass.LeftSide,
                        RightSide = ass.RightSide.Replace(fromSignal.Name, toSignal.Name)
                    });
            });
        }

        public void SignalInMeadle(Signal fromSignal, List<Signal> replacedSignals)
        {
            int bits = 0;
            replacedSignals.ForEach(x =>
            {
                bits += x.Bits;
            });
        }

        public void RefreshAssigment(Assigment assigment)
        {
            Document = Document.Replace(assigment.Text, assigment.NewText());
            assigment.Text = assigment.NewText();
        }
    }
}
