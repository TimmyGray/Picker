using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picker.Classes
{
    internal class Secitem:Itimeitem
    {
        public int TimeValue { get;private set; }
        public Secitem(int timeValue)
        {
            TimeValue = timeValue;
        }   
    }
}
