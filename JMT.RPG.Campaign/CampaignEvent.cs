using JMT.RPG.Core.Contracts.Combat;

namespace JMT.RPG.Campaign
{
    public record CampaignEvent
    {
        public bool IsCombatEvent { get; set; }
        public int EventSequence { get; set; }
        public int NumberOfLootRolls { get; set; }
        public int AwardedCurrency { get; set; }
        public CampaignEventLootTable? LootTable { get; set; }
        public ICollection<CampaignDialog> CampaignDialog { get; set; }        
    }
}
