using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.Combat
{
    public record CombatEffect
    {
        public string EffectedAttribute { get; init; }
        public string EffectType { get; init; }
        public int Magnitude { get; init; }
        public int MagnitudeFactor { get; init; } // all calcs are += so -1 to do damage for example
        public CombatEffect ForwardEffect { get; init; }
    }
}
