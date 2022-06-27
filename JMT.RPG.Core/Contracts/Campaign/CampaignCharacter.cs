using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Core.Contracts.Campaign
{
    public class CampaignCharacter
    {
        public string CampaignCharacterID { get; init; }
        public string Name { get; init; }   
        public int Level { get; init; }
        public int TotalHealth { get; set; }
        public int RemainingHealth { get; set; }
        public int Strength { get; set; }
        public int Intellect { get; set; }
        public int Speed { get; set; }     
        public IEnumerable<CombatAbility> CombatAbilities { get; set; } // TODO: Consider making a library specific mapping for this, or using the core entity
    }
}
