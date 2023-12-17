namespace JMT.RPG.Core.Campaign
{
    public record CampaignLoot
    {
        public int NumItemAwarded { get; init; }
        public CampaignPartyItem CampaignPartyItem { get; init; }
    }
}