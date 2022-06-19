using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.Roguelike.Core.Coordination
{
    public abstract class GameEvent : IGameEvent
    {
        public string EventID => Guid.NewGuid().ToString();

        public DateTime EventTimestamp => DateTime.Now;
    }
}
