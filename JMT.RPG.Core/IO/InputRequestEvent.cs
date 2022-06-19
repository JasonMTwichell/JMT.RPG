using JMT.RPG.Core.Coordination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.IO
{
    internal class InputRequestEvent : GameEvent
    {
        public InputRequestEvent(string playerID)
        {
            PlayerID = playerID;
        }

        public string PlayerID { get; set; }
    }
}
