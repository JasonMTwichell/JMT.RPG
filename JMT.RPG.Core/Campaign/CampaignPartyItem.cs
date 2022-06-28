namespace JMT.RPG.Core.Campaign
{
    public record CampaignPartyItem
    {
        public string ItemID { get; init; }
        public string ItemName { get; init; }
        public string ItemDescription { get; init; }
        public IEnumerable<ItemEffect> Effects { get; init; }
    }
}