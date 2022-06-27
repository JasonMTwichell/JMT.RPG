using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.Contracts.Combat
{
    public record CombatAbility
    {
        public string CombatAbilityID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int Cooldown { get; init; }
        public int RemainingCooldown { get; init; }                
        public IEnumerable<CombatEffect> Effects { get; init; }
    }
}
