using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.VHDLWords;

namespace Model.Options
{
    public class OutWatermarkSettings
    {
        public OutWatermarkSettings(Port port)
        {
            Port = port;
        }
        public Port Port { get; }
        public bool IsUsed { get; set; }
        public string SignatureCode { get; set; }
    }
}
