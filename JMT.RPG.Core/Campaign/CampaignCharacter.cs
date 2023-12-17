using JMT.RPG.Core.Combat;

namespace JMT.RPG.Core.Campaign
{
    public record CampaignCharacter
    {
        public string CampaignCharacterID { get; init; }
        public string Name { get; init; }   
        public int Level { get; init; }
        public int TotalHealth { get; init; }
        public int RemainingHealth { get; init; }
        public int Strength { get; init; }
        public int Intellect { get; init; }
        public int Speed { get; init; }     
        public IEnumerable<CombatAbility> CombatAbilities { get; init; } // TODO: Consider making a library specific mapping for this, or using the core entity
    }
}
