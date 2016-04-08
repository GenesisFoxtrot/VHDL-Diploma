using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Model;

namespace VHDLParser
{
    public class VHDLDocument : Parser
    {
        public string Document { get; set; }
        public List<Entity> Components { get; set; }
        public List<Map> Maps { get; set; }
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
            var oldMap = Regex.Match(Document, OneMap + MFS + AEL).Value;
            Document = Document.Replace(oldMap, oldMap + Helper.MapToVHDL(map)); ;
        }

        public void AddSimpeAssigment(Signal to, Signal from)
        {
            var oldMap = Regex.Match(Document, OneMap + MFS + AEL).Value;
            Document = Document.Replace(oldMap, oldMap + Helper.SimpleAssigment(to.Name,from.Name)); ;
        }


        public void AddMuxAssigment(Signal to, string condition , Signal thenSignal , Signal elseSignal)
        {
            var oldMap = Regex.Match(Document, OneMap + MFS + AEL).Value;
            Document = Document.Replace(oldMap, oldMap + Helper.MuxAssigment(to.Name, condition, thenSignal.Name, elseSignal.Name)); ;
        }

        public void ChangeAsigmentInMap(Map map, Assigmnet oldAssigmnet, Assigmnet newAssigment)
        {
            newAssigment.Text = newAssigment.Text ?? Helper.AssigmentToVHDL(newAssigment);
            var newMap = map.Text.Replace(oldAssigmnet.Text, newAssigment.Text);
            Document = Document.Replace(map.Text, newMap);
            map.Text = newMap;
            map.Assigmnets.Remove(oldAssigmnet);
            map.Assigmnets.Add(newAssigment);
        }



        public void Redirect(Signal fromSignal, Signal toSignal)
        {

            var outMap = Maps.Where(x => x.Assigmnets.Any(y => Helper.ExtractPortName(y.RightSide) == fromSignal.Name)).ToList();

            //tMap.Select(x => x.Text.Replace(selectedOut.Name, ));
            //document.Document.Replace()

            Map map = new Map();
            map.Name = Guid.NewGuid().ToString().Replace("-", "_");
            map.Entity = outMap.FirstOrDefault().Entity;
            map.Assigmnets = outMap.FirstOrDefault().Assigmnets;
            AddMap(map);


            outMap.ForEach(x =>
            {
                var ass = x.Assigmnets.FirstOrDefault(y => Helper.ExtractPortName(y.RightSide) == fromSignal.Name);
                ChangeAsigmentInMap(x, ass,
                    new Assigmnet()
                    {
                        LeftSide = ass.LeftSide,
                        RightSide = ass.RightSide.Replace(fromSignal.Name, toSignal.Name)
                    });
            });
        }

        


    }
}
