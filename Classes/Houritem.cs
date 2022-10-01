using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picker.Classes
{
    internal class Houritem : Itimeitem
    {
        public int TimeValue { get; private set; }
        public Houritem(int timevalue)
        {
            TimeValue = timevalue;
        }

    }
}
