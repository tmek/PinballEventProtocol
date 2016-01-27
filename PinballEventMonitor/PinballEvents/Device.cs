using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinballEvents
{
    class PinballDevice
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public bool State { get; set; }

        public PinballDevice(int number, string name, bool state)
        {
            this.Number = number;
            this.Name = name;
            this.State = state;
        }

        public void ToggleState()
        {
            State = !State;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Number, Name);
        }
    }
}
