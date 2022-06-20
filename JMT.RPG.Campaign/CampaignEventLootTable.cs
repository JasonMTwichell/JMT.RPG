namespace JMT.RPG.Campaign
{
    public record CampaignEventLootTable
    {
        public ICollection<CampaignLoot> Loot { get; set; }
    }
}