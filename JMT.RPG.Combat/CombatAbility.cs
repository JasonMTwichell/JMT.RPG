using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.Roguelike.Combat
{
    public class CombatAbility
    {
        public string CombatAbilityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cooldown { get; set; }
        public int RemainingCooldown { get; set; }                
        public IEnumerable<CombatEffect> Effects { get; set; }
    }
}
