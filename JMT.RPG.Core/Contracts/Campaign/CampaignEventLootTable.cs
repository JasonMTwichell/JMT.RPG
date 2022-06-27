namespace JMT.RPG.Core.Contracts.Campaign
{
    public record CampaignEventLootTable
    {
        public IEnumerable<CampaignLoot> Loot { get; init; }
    }
}