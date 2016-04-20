using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.VHDLSetcions.Signals;

namespace Model.Options
{
    public class InWatermarkSettings
    {
        public InWatermarkSettings(Port port)
        {
            Port = port;
        }
        public Port Port { get; private set; }
        public bool IsUsed { get; set; }
        public string ActivaionCode { get; set; }
    }
}
