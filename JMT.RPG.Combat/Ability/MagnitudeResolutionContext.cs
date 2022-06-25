using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Combat.Ability
{
    public record MagnitudeResolutionContext
    {
        public string EffectType { get; set; }
        public int DefaultMagnitude { get; set; }
        public int MagnitudeFactor { get; set; }
        public int Strength { get; set; }
        public int Intellect { get; set; }
        public int Speed { get; set; }
    }
}
