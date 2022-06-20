using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.Contracts.Combat
{
    public record CombatEffect
    {
        public string EffectedAttribute { get; set; }
        public string EffectType { get; set; }
        public int Magnitude { get; set; }
        public int MagnitudeFactor { get; set; } // -1 to do damage for example
        public CombatEffect ForwardEffect { get; set; }
    }
}
