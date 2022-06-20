using JMT.RPG.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMT.RPG.Campaign
{
    public class CampaignCharacter
    {
        public string Id { get; init; }
        public string Name { get; init; }        
        public int TotalHealth { get; set; }
        public int RemainingHealth { get; set; }
        public int Strength { get; set; }
        public int Intellect { get; set; }
        public int Speed { get; set; }     
        public IEnumerable<CombatAbility> CombatAbilities { get; set; } // TODO: Consider making a library specific mapping for this, or using the core entity
    }
}
