using JMT.RPG.Core.Combat;

namespace JMT.RPG.Core.Campaign
{
    public record CampaignEvent
    {
        public bool IsCombatEvent { get; init; }
        public int EventSequence { get; init; }
        public int NumberOfLootRolls { get; init; }
        public int AwardedCurrency { get; init; }
        public IEnumerable<CampaignCharacter> EnemyParty { get; init; }
        public CampaignEventLootTable? LootTable { get; init; }
        public IEnumerable<CampaignEventDialog> CampaignDialog { get; init; }        
    }
}
