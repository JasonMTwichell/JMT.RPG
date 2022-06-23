using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Core.Contracts.Combat
{
    public class CombatAbility
    {
        public string CombatAbilityID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cooldown { get; set; }
        public int RemainingCooldown { get; set; }                
        public IEnumerable<CombatEffect> Effects { get; set; }
    }
}
