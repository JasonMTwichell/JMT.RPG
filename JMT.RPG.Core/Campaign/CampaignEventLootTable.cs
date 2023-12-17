namespace JMT.RPG.Core.Campaign
{
    public record CampaignEventLootTable
    {
        public IEnumerable<CampaignLoot> Loot { get; init; }
    }
}