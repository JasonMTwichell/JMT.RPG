using JMT.RPG.Core.Coordination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.IO
{
    internal class InputResponseEvent : GameEvent
    {
        public InputResponseEvent(string playerID, string response)
        {
            PlayerID = playerID;
            Response = response;
        }

        public string PlayerID { get; private set; }
        public string Response { get; private set; }
    }
}
