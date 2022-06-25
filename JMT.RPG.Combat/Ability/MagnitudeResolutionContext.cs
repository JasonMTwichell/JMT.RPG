using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Combat.Ability
{
    public record MagnitudeResolutionContext
    {
        public string EffectType { get; init; }
        public int DefaultMagnitude { get; init; }
        public int MagnitudeFactor { get; init; }
        public int Strength { get; init; }
        public int Intellect { get; init; }
        public int Speed { get; init; }
    }
}
