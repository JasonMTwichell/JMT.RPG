using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.Game
{
    public class Ability
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cooldown { get; set; }
        public int SlotRequirement { get; set; }
        public string AbilityType { get; set; }
        public IEnumerable<Effect> Effect { get; set; }
    }
}
