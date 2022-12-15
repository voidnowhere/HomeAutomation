using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.Models
{
    internal class TV : Equipment
    {
        [DefaultValue(0)]
        public int Volume { get; set; }
    }
}
